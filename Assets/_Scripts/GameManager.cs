using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private UIManager _uiManager;
    [SerializeField, Tooltip("Panel que contiene la pantalla del GameOver")]
    private GameObject gameOverPanel;
    [SerializeField, Tooltip("Panel que contiene los botones de control")]
    private GameObject controllersUI;


    private int score; //Puntuación de la partida
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _uiManager.UpdateTimeBar(_playerController.getTimeCounter());
    }

    void OnEnable()
    {
        PlayerController.OnGameOver += HandleGameOverEvent;
        PlayerController.OnCut += HandleCutEvent;
    }

    void OnDisable() {
        PlayerController.OnGameOver -= HandleGameOverEvent;
        PlayerController.OnCut -= HandleCutEvent;
    }

    /// <summary>
    /// Manejador del evento de GameOver
    /// </summary>
    void HandleGameOverEvent()
    {
        _playerController.enabled = false; //Desactivamos el controlador
        gameOverPanel.SetActive(true);
        controllersUI.SetActive(false);
        SaveNewScore();
    }

    void HandleCutEvent()
    {
        score += 5;
        _uiManager.UpdateScore(this.score);
    }

    /// <summary>
    /// Método para reiniciar al juego
    /// </summary>
    public void RestartGame()
    {
        TreeLogic.RestartLogic();
        SceneManager.LoadScene(1);
       /* _playerController.RestartTime();
        _playerController.enabled = true;
        gameOverPanel.SetActive(false);
        controllersUI.SetActive(true);*/
    }

    public void SaveNewScore()
    {
        if(PlayerPrefs.GetInt("Score") < score)
        {
            PlayerPrefs.SetInt("Score", score);
        }
        _uiManager.SetScoreRecord(PlayerPrefs.GetInt("Score"));
    }

    public void ReturnToMenu()
    {
        TreeLogic.RestartLogic();
        SceneManager.LoadScene(0);
    }
}
