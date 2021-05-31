using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    [SerializeField, Tooltip("Prefabs de los bloques posibles que pueden salir en el árbol")]
    private GameObject[] prefabs;
    private float blockDistance = TreeLogic.GetDistanceBetweenBlocks();
    private int treeBlocksNumbers = 11; //Tamaño del árbol en bloques
    private int prevBlock = 0;

    // Start is called before the first frame update
    void Start()
    {
        //El primer bloque solo puede ser el tronco central
        TreeLogic.treeBlocks.Add(Instantiate(prefabs[0], Vector3.zero, prefabs[0].transform.rotation));
        TreeLogic.treeBlocksPositions.Add(IndexToVector(0));
        float blockPosition = blockDistance;
        for(int i=1; i<treeBlocksNumbers; i++)
        {
            Vector3 newPos = new Vector3(0, blockPosition, 0);
            SpawnNewBlock(newPos);
            blockPosition += blockDistance;
        }
    }

    //Generar un nuevo bloque
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

    /// <summary>
    /// Mapea el index correspondiente al array de prefabs (bloques) con un vector 3 que indica la posición que ocupa
    /// (100) -> izquierda
    /// (010) -> centro
    /// (001) -> derecha 
    /// </summary>
    /// <param name="i">Indice del bloque instanciado</param>
    /// <returns>Vector3 con la posición/dirección del bloque</returns>
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

    /// <summary>
    /// Genera un número entero aleatorio exceptuando el valor dado
    /// </summary>
    /// <param name="exception">Valor que se omitirá</param>
    /// <returns>Int generado aleatoriamente </returns>
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
