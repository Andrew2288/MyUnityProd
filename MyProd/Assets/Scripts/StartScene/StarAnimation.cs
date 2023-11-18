using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{

    private SpriteRenderer obj;
    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 2.01f);
    }

    void Update()
    {
        obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.PingPong(Time.time, 1f));
    }
}
