using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField, Tooltip("Barra de tiempo")]
    private Slider timeBar;
    private float startValue = 15f; //Valor inicial del tiempo

    void Start()
    {
        timeBar.value = startValue;
    }

    public void UpdateTimeBar(float value)
    {
        timeBar.value = value;
    }
}
