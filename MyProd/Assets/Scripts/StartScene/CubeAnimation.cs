using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CubeAnimation : MonoBehaviour
{
    private Vector3 forceVector;
    private static float way;
    private float speed;
    private GameObject activePlatform;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI recordText;
    public TextMeshProUGUI diamondCounter;
    public Material material;
    public GameObject dropSound;
    private bool firstBlock = true;

    void Start()
    {
        forceVector = new Vector3(0, 1, 0);
        way = -1f;
        speed = 1f;
    }

    void FixedUpdate()
    {
        if ((GetComponent<Transform>().localScale.y > 0.5f && way < 0f) ||
            (way > 0f && GetComponent<Transform>().localScale.y < 1f))
        {
            GetComponent<Transform>().localPosition += forceVector * way * speed * Time.deltaTime;
            GetComponent<Transform>().localScale += forceVector * way * speed * Time.deltaTime * 3;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!firstBlock)
        {
            if (dropSound.GetComponent<AudioSource>().enabled)
            {
                dropSound.GetComponent<AudioSource>().Play();
            }
        }
        firstBlock = false;
        if (activePlatform)
        {
            if (other.gameObject == activePlatform && Mathf.Abs(activePlatform.transform.position.x - transform.position.x) < 1.683f && (transform.position.y > activePlatform.transform.position.y))
            {
                CubeJump.isOver = true;
            }
            else
            {
                activePlatform = other.gameObject;
                if (Mathf.Abs(activePlatform.transform.position.x - transform.position.x) < 1.683f && (transform.position.y > activePlatform.transform.position.y))
                {
                    activePlatform.GetComponent<Renderer>().material = material;
                    CubeJump.afterFlying = false;
                    int cnt = Convert.ToInt32(currentScore.text) + 1;
                    currentScore.text = cnt.ToString();
                    if (PlayerPrefs.GetInt("Record") < cnt)
                    {
                        PlayerPrefs.SetInt("Record", cnt);
                    }
                }
                List<GameObject> platforms = GenNewPlatform.platforms;
                for (int i = 0; i < platforms.Count; ++i)
                {
                    if (platforms[i] != activePlatform)
                    {
                        StartCoroutine(DelPlatform(platforms[i]));
                        platforms.Remove(platforms[i]);
                        --i;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            activePlatform = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int cnt = PlayerPrefs.GetInt("DiamondCounter") + 1;
        PlayerPrefs.SetInt("DiamondCounter", cnt);
        diamondCounter.text = ": " + cnt.ToString();
        if (other.gameObject)
        {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator DelPlatform(GameObject platform)
    {
        if (platform)
        {
            platform.GetComponent<Collider>().enabled = false;

            for (int i = 100; i >= 0; --i)
            {
                if (platform)
                {
                    Color color = platform.GetComponent<Renderer>().material.color;
                    if (color.a > 0f)
                    {
                        platform.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a - 0.01f);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            if (platform)
            {
                Destroy(platform);
            }
        }
    }

    public static void ChangeWay()
    {
        way = -way;
    }
}
