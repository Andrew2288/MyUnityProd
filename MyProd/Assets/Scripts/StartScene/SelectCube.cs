using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SelectCube : MonoBehaviour
{
    public GameObject[] cubeList;
    public GameObject mainCube;
    public TextMeshProUGUI diamonds;
    private void Start()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;

        if (gameObject == cubeList[PlayerPrefs.GetInt("CubeMaterial", 0)])
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 1f);
            PlayerPrefs.SetInt(gameObject.name, 2);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 150f / 255f);
            if (gameObject.transform.GetChild(1).gameObject.activeSelf)
            {
                PlayerPrefs.SetInt(gameObject.name, 1);
            }
        }

        if (PlayerPrefs.GetInt(gameObject.name, 0) == 2)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Selected";
        }
        else if (PlayerPrefs.GetInt(gameObject.name, 0) == 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Can select!";
        }
    }
    private void OnMouseDown()
    {
        int cnt = 0;
        bool moneyCheck = true;
        int price = Convert.ToInt32(gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        int bank = PlayerPrefs.GetInt("DiamondCounter", 0);

        if (price <= bank)
        {
            foreach (var cube in cubeList)
            {
                Color color = cube.GetComponent<Renderer>().material.color;

                if (cube == gameObject)
                {
                    if (!(cube.transform.GetChild(0)))
                    {
                        cube.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
                        PlayerPrefs.SetInt("CubeMaterial", cnt);
                    }
                    else
                    {
                        int res = bank - price;
                        PlayerPrefs.SetInt("DiamondCounter", bank - price);
                        diamonds.text = res.ToString();
                        cube.transform.GetChild(0).gameObject.SetActive(false);
                        cube.transform.GetChild(1).gameObject.SetActive(true);
                        cube.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Selected";
                        cube.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 1f);
                        mainCube.GetComponent<MeshRenderer>().material = cube.GetComponent<MeshRenderer>().material;
                        PlayerPrefs.SetInt("CubeMaterial", cnt);
                        PlayerPrefs.SetInt(gameObject.name, 2);
                    }
                }
                else
                {
                    if (moneyCheck)
                    {
                        cube.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 150f / 255f);
                        if (cube.transform.GetChild(1).gameObject.activeSelf)
                        {
                            cube.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Can select!";
                            PlayerPrefs.SetInt(cube.name, 1);
                        }
                    }
                }
                ++cnt;
            }
        }
    }
}