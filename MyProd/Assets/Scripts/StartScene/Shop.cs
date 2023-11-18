using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public bool isMenu;
    private float speed = 50f;
    void Start()
    {
        isMenu = true;
    }

    void FixedUpdate()
    {
        if (isMenu)
        {
            if (GetComponent<RectTransform>().offsetMin.y > 30f)
            {
                GetComponent<RectTransform>().offsetMin -= new Vector2(0, speed * Time.deltaTime);
                GetComponent<RectTransform>().offsetMax -= new Vector2(0, speed * Time.deltaTime);
            }
        }
        else
        {
            if (GetComponent<RectTransform>().offsetMin.y < 70f)
            {
                GetComponent<RectTransform>().offsetMin += new Vector2(0, speed * Time.deltaTime);
                GetComponent<RectTransform>().offsetMax += new Vector2(0, speed * Time.deltaTime);
            }
        }
    }
}