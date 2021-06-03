﻿using System.Collections;
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


    void Start()
    {
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

        #if UNITY_STANDALONE_WIN
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
    /// Cambia la posición del jugador
    /// </summary>
    private void ChangePlayerPosition()
    {
        this.transform.position = new Vector3(this.transform.position.x * -1, 0 , 0);
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
    }

    private void PostMovement()
    {
        Debug.Log("Treeblock: " + TreeLogic.treeBlocksPositions[1] + " Player: " + playerPos);
        CheckGameOver();
        TreeLogic.RemoveFirst(playerPos);
        _treeGenerator.SpawnNewBlock(blocksSpawner.position);
        if(OnCut != null)
        {
            OnCut();
        }
        //CheckGameOver();
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
