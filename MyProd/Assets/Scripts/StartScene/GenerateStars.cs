using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStars : MonoBehaviour
{
    public GameObject star;
    void Start()
    {
        StartCoroutine(spawn(2f));
    }

    private IEnumerator spawn(float delay)
    {
        while (true)
        {
            Instantiate(star, Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2)), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            yield return new WaitForSeconds(delay);
        }
    }
}
