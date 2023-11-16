using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseGameScript : MonoBehaviour
{
    public GameObject loseButtons;
    public Button menuButton;
    public Button exitButton;
    public GameObject shadow;
    public TextMeshProUGUI scoreText;
    public GameObject loseSceneText;
    public TextMeshProUGUI loseSceneScorText;

    private float range, speed;
    public bool lose = false;
    void Start()
    {
        shadow.SetActive(true);
        loseSceneText.SetActive(true);
        loseSceneScorText.text = scoreText.text;
        menuButton.onClick.AddListener(BackToMenu);
        exitButton.onClick.AddListener(Exit);
        loseButtons.GetComponent<ScrollObj>().enabled = true;
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Exit()
    {

    }
}
