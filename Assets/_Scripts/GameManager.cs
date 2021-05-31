using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
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

    void HandleGameOverEvent()
    {
        _playerController.enabled = false; //Desactivamos el controlador
        gameOverPanel.SetActive(true);
        controllersUI.SetActive(false);
    }
}
