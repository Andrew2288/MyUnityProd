using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public Button start_game_button;
    public Button close_game_button;
    public GameObject buttons;
    public TextMeshProUGUI mainText;
    public GameObject cube;
    public GameObject detectClicksCube;
    public GameObject startScene;
    public TextMeshProUGUI recordText;

    void Start()
    {
        start_game_button.onClick.AddListener(StartGame);
        close_game_button.onClick.AddListener(CloseGame);
    }

    void StartGame()
    {
        buttons.GetComponent<ScrollObj>().speed = 1f;
        buttons.GetComponent<ScrollObj>().range = -50f;

        mainText.text = "0";
        mainText.GetComponent<RectTransform>().offsetMin += new Vector2(250, 0);

        cube.GetComponent<Animation>().Play("StartGameCube");
        recordText.enabled = true;
        recordText.text = "Record: " + PlayerPrefs.GetInt("Record");
        StartCoroutine(AddRBody());

        startScene.GetComponent<GenNewPlatform>().enabled = true;
    }

    private IEnumerator AddRBody()
    {
        yield return new WaitForSeconds(1f);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        cube.GetComponent<Rigidbody>().mass = 10;
        yield return new WaitForSeconds(0.1f);
        detectClicksCube.GetComponent<CubeJump>().enabled = true;
    }

    void CloseGame()
    {
        startScene.GetComponent<GenNewPlatform>().closeGame = true;
    }

}
