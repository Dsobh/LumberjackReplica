using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    
    public static List<Vector3> treeBlocksPositions = new List<Vector3>(); //Las posiciones de cada rama/bloque
    public static List<GameObject> treeBlocks = new List<GameObject>(); //Objetos de los bloques
    private static float distanceBetweenBlocks = 2.0f; //Cada cuanto se debe colocar un bloque

    /// <summary>
    /// Elimina el primer bloque, es decir, el primero que entró en la cola FIFO
    /// Baja un nivel todos los bloques
    /// </summary>
    public static void RemoveFirst(Vector3 playerPosition)
    {
        //Eliminamos toda la información relativa al primer bloque
        if(treeBlocksPositions[0] == new Vector3(0,1,0))
        {
            if(playerPosition == new Vector3(1,0,0))
            {
                treeBlocks[0].GetComponent<Animation>().Play("CentralToRight");
            }else
            {
                treeBlocks[0].GetComponent<Animation>().Play("CentralToLeft");
            }
        }else
        {
            treeBlocks[0].GetComponent<Animation>().Play();
        }
        
        
        treeBlocksPositions.RemoveAt(0);
        treeBlocks.RemoveAt(0);
        //Destroy(treeBlocks[0]);

        //Bajamos los bloques en función de la distancia que hay entre ellos
        foreach (GameObject block in treeBlocks)
        {
            block.transform.position = new Vector3(0, block.transform.position.y - distanceBetweenBlocks, 0); 
        }
    }

    /// <summary>
    /// Comprueba si la posición del player coincide con la de una rama
    /// </summary>
    /// <param name="playerPosition">Vector3 con la posición del player</param>
    /// <returns>True si la posición no es válida. False si es válida</returns>
    public static bool IsNotValidPosition(Vector3 playerPosition)
    {
        if(treeBlocksPositions[1] != Vector3.zero)
        {
            if(playerPosition == treeBlocksPositions[0] || playerPosition == treeBlocksPositions[1])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Método get para DistanceBetweenBlocks
    /// </summary>
    /// <returns>DistanceBetweenBlocks</returns>
    public static float GetDistanceBetweenBlocks()
    {
        return distanceBetweenBlocks;
    }

    public static void RestartLogic()
    {
        treeBlocksPositions = new List<Vector3>();
        treeBlocks = new List<GameObject>();
        
    }

}
