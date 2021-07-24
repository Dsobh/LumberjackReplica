using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Eventos ==========================================
    public delegate void GameOverDelegate();
    public static event GameOverDelegate OnGameOver;
    public delegate void CutActionDelegate();
    public static event CutActionDelegate OnCut;
    //==================================================

    private Transform blocksSpawner;
    private TreeGenerator _treeGenerator;
    private Vector3 playerPos; //Contiene la información de la posición del jugador en el sistema del juego

    [Header("Tiempo de juego")]
    [SerializeField, Tooltip("Tiempo base que tarda en acabarse el juego")]
    private float timeToGameOver = 15f;
    private float timeToGameOverCounter = 0;
    [SerializeField, Tooltip("Tiempo adicional ganado cada vez que talamos con exito")]
    private float timeExtension = 1f;
    [SerializeField, Tooltip("Multiplicador de velocidad de tiempo")]
    private int timeMultiplier = 2;

    [SerializeField]
    private int targetScoreToChangeTime = 500;
    private int auxScore;

    private AudioManager _audioManager;
    private GameManager _gameManager;


    void Start()
    {
        auxScore = targetScoreToChangeTime;
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerPos = new Vector3(0,0,1);
        timeToGameOverCounter = timeToGameOver;
        blocksSpawner = GameObject.Find("TreeGenerator").GetComponent<Transform>();
        _treeGenerator = GameObject.Find("TreeGenerator").GetComponent<TreeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(timeToGameOverCounter > 0)
        {
            timeToGameOverCounter -= Time.deltaTime*timeMultiplier;
        }else
        {
            OnGameOver();
        }

        #if UNITY_STANDALONE_WIN || UNITY_WEBGL
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LeftMovement();

            }else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                RigthMovement();
            }
        #endif

    }

    /// <summary>
    /// Ejecuta la animación de golpear del jugador
    /// </summary>
    private void PlayerHit()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Action");
    }

    /// <summary>
    /// Cambia la posición del jugador
    /// </summary>
    private void ChangePlayerPosition()
    {
        this.transform.position = new Vector3(this.transform.position.x * -1, 0 , 0);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX;
    }

    /// <summary>
    /// Comprueba si el juego ha llegado a GameOver
    /// </summary>
    private void CheckGameOver()
    {
        if(TreeLogic.IsNotValidPosition(playerPos))
        {
            if(OnGameOver != null)
            {
                _audioManager.PlayGameOver();
                OnGameOver(); //Lanzamos el evento de GameOver
            }
        }
    }

    //TODO: Animación de talar hacia la derecha
    public void LeftMovement()
    {
        if(this.transform.position.x > 0)
        {
            ChangePlayerPosition();
            playerPos = new Vector3(1,0,0);
        }
        PostMovement();
        PlayerHit();
    }

    //TODO: Animación de talar hacia la izquierda
    public void RigthMovement()
    {
        if(this.transform.position.x < 0)
        {
            ChangePlayerPosition();
            playerPos = new Vector3(0,0,1);
        }
        PostMovement();
        PlayerHit();
    }

    private void PostMovement()
    {
        Debug.Log("Treeblock: " + TreeLogic.treeBlocksPositions[1] + " Player: " + playerPos);
        CheckGameOver();
        _audioManager.PlayCut();
        TreeLogic.RemoveFirst(playerPos);
        _treeGenerator.SpawnNewBlock(blocksSpawner.position);
        if(OnCut != null)
        {
            OnCut();
        }
        if(_gameManager.GetScore() >= auxScore)
        {
            auxScore += targetScoreToChangeTime;
            timeMultiplier += 1;
        }
        timeToGameOverCounter += timeExtension;
    }

    /// <summary>
    /// Reinicia el contador de tiempo
    /// </summary>
    public void RestartTime()
    {
        timeToGameOverCounter = timeToGameOver;
    }

    /// <summary>
    /// Método get de TimeToGameOverTime
    /// </summary>
    /// <returns>TimeToGameOverTime</returns>
    public float getTimeCounter()
    {
        return timeToGameOverCounter;
    }
}
