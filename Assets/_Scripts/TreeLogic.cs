using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    
    public static List<Vector3> treeBlocksPositions = new List<Vector3>();
    public static List<GameObject> treeBlocks = new List<GameObject>();
    private static float distanceBetweenBlocks = 1.5f;

    public static void RemoveFirst()
    {
        treeBlocksPositions.RemoveAt(0);
        Destroy(treeBlocks[0]);
        treeBlocks.RemoveAt(0);

        foreach (GameObject block in treeBlocks)
        {
            Debug.Log(block.name);
            block.transform.position = new Vector3(0, block.transform.position.y - distanceBetweenBlocks, 0); 
        }
    }

    public static bool IsNotValidPosition(Vector3 playerPosition)
    {
        Debug.Log("Treeblock: " + treeBlocksPositions[0] + " Player: " + playerPosition);
        if(treeBlocksPositions[0] != Vector3.zero)
        {
            return playerPosition == treeBlocksPositions[0];
        }
        return false;
    }

}
