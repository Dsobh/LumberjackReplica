using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public delegate void GameOverDelegate();
    public static event GameOverDelegate OnGameOver;

    private Transform blocksSpawner;
    private TreeGenerator _treeGenerator;
    private Vector3 playerPos;

    [Header("Tiempo de juego")]
    [SerializeField, Tooltip("Tiempo base que tarda en acabarse el juego")]
    private float timeToGameOver = 15f;
    private float timeToGameOverCounter = 0;
    [SerializeField, Tooltip("Tiempo adicional ganado cada vez que talamos con exito")]
    private float timeExtension = 1f;
    [SerializeField, Tooltip("Multiplicador de velocidad de tiempo")]
    private int timeMultiplier = 2;


    void Start()
    {
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

        //TODO: Animación de talar después de cada movimiento
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftMovement();

        }else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            RigthMovement();
        }

    }

    //Método para cambiar la dirección en el eje x del player
    private void ChangePlayerPosition()
    {
        this.transform.position = new Vector3(this.transform.position.x * -1, 0 , 0);
    }

    private void CheckGameOver()
    {
        if(TreeLogic.IsNotValidPosition(playerPos))
        {
            if(OnGameOver != null)
            {
                OnGameOver();
            }
        }
    }

    public float getTimeCounter()
    {
        return timeToGameOverCounter;
    }

    public void LeftMovement()
    {
        if(this.transform.position.x > 0)
        {
            ChangePlayerPosition();
            playerPos = new Vector3(1,0,0);
        }
        PostMovement();
    }

    public void RigthMovement()
    {
        if(this.transform.position.x < 0)
        {
            ChangePlayerPosition();
            playerPos = new Vector3(0,0,1);
        }
        PostMovement();
    }

    private void PostMovement()
    {
        CheckGameOver();
        TreeLogic.RemoveFirst();
        _treeGenerator.SpawnNewBlock(blocksSpawner.position);
        CheckGameOver();
        timeToGameOverCounter += timeExtension;
    }
}
