using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Slider timeBar;
    private float startValue = 15f;

    void Start()
    {
        timeBar.value = startValue;
    }

    public void UpdateTimeBar(float value)
    {
        timeBar.value = value;
    }
}
