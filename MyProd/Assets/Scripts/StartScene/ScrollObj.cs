using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObj : MonoBehaviour
{
    private RectTransform obj;
    public float speed = 1f, range = 0f;
    void Start()
    {
        obj = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (speed > 0 && obj.offsetMin.y < range)
        {
            obj.offsetMin += new Vector2(0, speed);
            obj.offsetMax += new Vector2(0, speed);
        }
        else if (speed < 0 && obj.offsetMin.y > range)
        {
            obj.offsetMin += new Vector2(0, speed);
            obj.offsetMax += new Vector2(0, speed);
        }
    }
}
