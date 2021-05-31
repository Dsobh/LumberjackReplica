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
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    void OnDisable() {
        PlayerController.OnGameOver -= HandleGameOverEvent;
    }

    /// <summary>
    /// Manejador del evento de GameOver
    /// </summary>
    void HandleGameOverEvent()
    {
        _playerController.enabled = false; //Desactivamos el controlador
        gameOverPanel.SetActive(true);
        controllersUI.SetActive(false);
    }

    /// <summary>
    /// Método para reiniciar al juego
    /// </summary>
    public void RestartGame()
    {
        Scene _scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_scene.name);
       /* _playerController.RestartTime();
        _playerController.enabled = true; //Desactivamos el controlador
        gameOverPanel.SetActive(false);
        controllersUI.SetActive(true);*/
    }
}
