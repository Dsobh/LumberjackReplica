using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField, Tooltip("MaxScore text")]
    private TMP_Text maxScoreText;

    void Start() {
        maxScoreText.SetText("Max Score: " + PlayerPrefs.GetInt("Score"));
    }

    public void ChargeScene(int value)
    {
        SceneManager.LoadScene(value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
