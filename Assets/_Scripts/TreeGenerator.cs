using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    [SerializeField, Tooltip("Prefabs de los bloques posibles que pueden salir en el árbol")]
    private GameObject[] prefabs;
    private float blockDistance = 1.5f;
    private int treeBlocksNumbers = 8; //Tamaño del árbol en bloques
    private int prevBlock = 0;

    // Start is called before the first frame update
    void Start()
    {
        //El primer bloque solo puede ser el tronco central
        TreeLogic.treeBlocks.Add(Instantiate(prefabs[0], Vector3.zero, prefabs[0].transform.rotation));
        TreeLogic.treeBlocksPositions.Add(IndexToVector(0));
        float blockPosition = 1.5f;
        for(int i=1; i<treeBlocksNumbers; i++)
        {
            Vector3 newPos = new Vector3(0, blockPosition, 0);
            SpawnNewBlock(newPos);
            blockPosition += blockDistance;
        }
    }

    //TODO: Generar un nuevo bloque
    public void SpawnNewBlock(Vector3 position)
    {
        int blockIndex = Random.Range(0, prefabs.Length);
        //TODO: Aqui saca los bloques intedambiados pero puede haber bloques repetidos 
        if((prevBlock == 1 && blockIndex == 2) || (prevBlock == 2 && blockIndex == 1)){
            blockIndex = RandomRangeWithException(blockIndex);  
        }
        prevBlock = blockIndex;
        GameObject newBlock = prefabs[blockIndex];
        TreeLogic.treeBlocks.Add(Instantiate(newBlock, position, newBlock.transform.rotation));
        TreeLogic.treeBlocksPositions.Add(IndexToVector(blockIndex));
    }

    //Mapea la posición del tronco en función de la rama que ha spawneado
    private Vector3 IndexToVector(int i)
    {
        switch (i)
        {
            case 0: 
                return new Vector3(0, 1, 0);
            case 1:
                return new Vector3(1, 0, 0);
            case 2:
                return new Vector3(0, 0, 1);
            default:
                Debug.LogWarning("IndexToVector in Script: TreeGenerator. Index not valid");
                return new Vector3(0,0,0);
        }
    }

    //Generamos un numero aleatorio exceptuando un valor
    private int RandomRangeWithException(int exception)
    {
        while(true)
        {
            int index = Random.Range(0, prefabs.Length);
            if(index != exception)
            {
                return index;
            }
        }
    }
}
