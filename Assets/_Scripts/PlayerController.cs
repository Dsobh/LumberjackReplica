using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Transform blocksSpawner;
    private TreeGenerator _treeGenerator;
    private Vector3 playerPos;

    void Start()
    {
        blocksSpawner = GameObject.Find("TreeGenerator").GetComponent<Transform>();
        _treeGenerator = GameObject.Find("TreeGenerator").GetComponent<TreeGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Animación de talar después de cada movimiento
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(this.transform.position.x > 0)
            {
                ChangePlayerPosition();
                playerPos = new Vector3(1,0,0);
            }
            CheckGameOver();
            TreeLogic.RemoveFirst();
            _treeGenerator.SpawnNewBlock(blocksSpawner.position);
            CheckGameOver();

        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(this.transform.position.x < 0)
            {
                ChangePlayerPosition();
                playerPos = new Vector3(0,0,1);
            }
            CheckGameOver();
             TreeLogic.RemoveFirst();
             _treeGenerator.SpawnNewBlock(blocksSpawner.position);
            CheckGameOver();
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
            Debug.Log("GAME OVER!");
        }
    }
}
