using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShop : MonoBehaviour
{
    public GameObject shop;
    public GameObject buttons;
    public GameObject buttonMusic;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Exit);
        buttons.GetComponent<Buttons>().shopOpen = false;
    }

    private void Exit()
    {
        if (buttonMusic.GetComponent<AudioSource>().enabled)
        {
            buttonMusic.GetComponent<AudioSource>().Play();
        }
        shop.SetActive(false);
        buttons.GetComponent<Buttons>().shopOpen = false;

        buttons.GetComponent<Buttons>().shop_button.enabled = true;
    }
}
