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
    public GameObject diamond;
    public TextMeshProUGUI diamondText;
    public GameObject shopButtons;
    public GameObject shop;
    public Button shop_button;
    public bool shopOpen;
    public TextMeshProUGUI diamonds;
    public Button sound_button;
    public GameObject blocSoundImg;
    public GameObject mainCamera;
    public GameObject mainScene;
    public GameObject mainMusic;
    public GameObject buttonMusic;
    public GameObject dropMusic;


    void Start()
    {
        shopOpen = false;
        start_game_button.onClick.AddListener(StartGame);
        close_game_button.onClick.AddListener(CloseGame);
        shop_button.onClick.AddListener(OpenShop);
        sound_button.onClick.AddListener(ChangeSound);
    }

    void ChangeSound()
    {
        if (buttonMusic.GetComponent<AudioSource>().enabled)
        {
            buttonMusic.GetComponent<AudioSource>().Play();
        }
        if (blocSoundImg.activeSelf)
        {
            blocSoundImg.SetActive(false);
            mainMusic.GetComponent<AudioSource>().enabled = true;
            buttonMusic.GetComponent<AudioSource>().enabled = true;
            dropMusic.GetComponent<AudioSource>().enabled = true;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            mainMusic.GetComponent<AudioSource>().enabled = false;
            buttonMusic.GetComponent<AudioSource>().enabled = false;
            dropMusic.GetComponent<AudioSource>().enabled = false;
            blocSoundImg.SetActive(true);
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

    void OpenShop()
    {
        if (buttonMusic.GetComponent<AudioSource>().enabled)
        {
            buttonMusic.GetComponent<AudioSource>().Play();
        }

        diamonds.text = PlayerPrefs.GetInt("DiamondCounter", 0).ToString();
        shop.SetActive(true);
        shopOpen = true;
        shop_button.enabled = false;
    }

    void StartGame()
    {
        if (!shopOpen)
        {
            if (buttonMusic.GetComponent<AudioSource>().enabled)
            {
                buttonMusic.GetComponent<AudioSource>().Play();
            }
            mainMusic.GetComponent<AudioSource>().volume = 0.5f;
            buttons.GetComponent<ScrollObj>().speed = -1f;
            buttons.GetComponent<ScrollObj>().range = -50f;

            mainText.text = "0";
            mainText.GetComponent<RectTransform>().offsetMin += new Vector2(0, -25);

            cube.GetComponent<Animation>().Play("StartGameCube");
            recordText.enabled = true;
            shopButtons.GetComponent<Shop>().isMenu = false;
            diamond.SetActive(true);
            diamondText.enabled = true;
            diamondText.text = ": " + PlayerPrefs.GetInt("DiamondCounter", 0).ToString();
            recordText.text = "Record: " + PlayerPrefs.GetInt("Record");
            StartCoroutine(AddRBody());

            startScene.GetComponent<GenNewPlatform>().enabled = true;
        }
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
        if (!shopOpen)
        {
            if (buttonMusic.GetComponent<AudioSource>().enabled)
            {
                buttonMusic.GetComponent<AudioSource>().Play();
            }
            startScene.GetComponent<GenNewPlatform>().closeGame = true;
            Application.Quit();
        }
    }

}
