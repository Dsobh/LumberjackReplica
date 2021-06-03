using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAnim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Se ha borrado");
        }   
    }
}
