using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField, Tooltip("Barra de tiempo")]
    private Slider timeBar;
    private GameObject fillBar;
    private float startValue = 15f; //Valor inicial del tiempo
    [SerializeField, Tooltip("Texto de puntuación")]
    private TMP_Text scoreText;
    [SerializeField, Tooltip("Texto de record de puntuación")]
    private TMP_Text scoreRecord;
    private Color color;

    void Start()
    {
        fillBar = GameObject.Find("Fill");
        color = fillBar.GetComponent<Image>().color;
        scoreText.SetText("0");
        timeBar.value = startValue;
    }

    public void UpdateTimeBar(float value)
    {
        timeBar.value = value;
        if(value <= 10)
        {
            fillBar.GetComponent<Image>().color = Color.red;
        }else{
            fillBar.GetComponent<Image>().color = color;
        }
    }

    public void UpdateScore(int value)
    {
        if(value < 999999)
        {
            scoreText.SetText(value.ToString());
        }
    }
    
    public void SetScoreRecord(int value)
    {
        if(value < 999999)
        {
            scoreRecord.SetText(value.ToString());
        }
    }

}
