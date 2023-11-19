using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GenNewPlatform : MonoBehaviour
{
    public GameObject platformPrefab;
    public bool closeGame;
    private float delay;
    private float leftBoardX;
    private float rightBoardX;
    private float lowBoardY;
    private GameObject mainPlatform;
    private GameObject oldPlatform;
    private GameObject Platforms;
    public GameObject Empty;
    public static bool loseGame;
    public GameObject mainCube;
    public static List<GameObject> platforms;
    public static bool UpPlatform;
    private float TimeForLife;
    public static bool generingPlatforms;
    public static bool letsStart;
    public TextMeshProUGUI text;
    private bool killingNotActivate;
    public GameObject Shadow;
    private static int platformsCount;
    private bool secondPlatform;
    public GameObject diamondPrefab;
    private GameObject diamond;
    public Material[] materials;

    void Start()
    {
        secondPlatform = true;
        platformsCount = 1;
        UpPlatform = true;
        killingNotActivate = true;
        letsStart = false;
        generingPlatforms = true;
        TimeForLife = 8f;
        closeGame = false;
        loseGame = false;
        delay = 3f;


        lowBoardY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + 0.2f;
        leftBoardX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + 1.1f;
        rightBoardX = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x - 1.1f;
        platformPrefab.GetComponent<Renderer>().material = materials[UnityEngine.Random.Range(0, 4)];
        Platforms = Instantiate(Empty, new Vector3(0, 0, 0), Quaternion.identity);
        mainPlatform = CreatObj(new Vector3(-7, 1.9f, -3), platformPrefab);
        mainPlatform.transform.parent = Platforms.transform;
        Platforms.GetComponent<Rigidbody>().isKinematic = true;
        platforms = new List<GameObject>();
        platforms.Add(mainPlatform);

        if (PlayerPrefs.GetInt("GuideText") == 0)
        {
            PlayerPrefs.SetInt("GuideText", 1);
            text.enabled = true;
            Shadow.SetActive(true);
        }
        else
        {
            letsStart = true;
        }

        StartCoroutine(FlashText());
    }

    private IEnumerator FlashText()
    {
        while (!letsStart)
        {
            for (int i = 1; i <= 20; ++i)
            {
                Color color = text.color;
                text.color = new Color(color.r, color.g, color.b, i * 0.05f);
                yield return new WaitForSeconds(0.05f);
            }

            for (int i = 19; i >= 0; --i)
            {
                Color color = text.color;
                text.color = new Color(color.r, color.g, color.b, i * 0.05f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private IEnumerator PlatformsGenering()
    {
        while (true)
        {
            if (generingPlatforms)
            {
                if (secondPlatform)
                {
                    secondPlatform = false;
                    yield return new WaitForSeconds(1f);
                }

                float[,] ranges = { { leftBoardX, mainPlatform.GetComponent<Transform>().localPosition.x - 2 }, { mainPlatform.GetComponent<Transform>().localPosition.x + 2, rightBoardX } };

                if ((rightBoardX - (mainPlatform.GetComponent<Transform>().localPosition.x + 2)) < mainPlatform.GetComponent<Transform>().localScale.x)
                {
                    ranges[1, 0] = ranges[0, 0];
                    ranges[1, 1] = ranges[1, 1];
                }
                else if ((mainPlatform.GetComponent<Transform>().localPosition.x - 2 - leftBoardX) < mainPlatform.GetComponent<Transform>().localScale.x)
                {
                    ranges[0, 0] = ranges[1, 0];
                    ranges[0, 1] = ranges[1, 1];
                }

                int id = UnityEngine.Random.Range(0, 1);
                Vector3 randomPos = new Vector3(UnityEngine.Random.Range(ranges[id, 0], ranges[id, 1]), UnityEngine.Random.Range(lowBoardY, mainPlatform.GetComponent<Transform>().localPosition.y), -3);

                ++platformsCount;
                oldPlatform = mainPlatform;
                mainPlatform = CreatObj(randomPos, platformPrefab);
                if ((int)UnityEngine.Random.Range(0, 5) == 0)
                {
                    diamond = CreatObj(randomPos + new Vector3(0, 1, 0), diamondPrefab);
                    platforms.Add(diamond);
                    StartCoroutine(WaitForKill(diamond));
                }
                platforms.Add(mainPlatform);
                StartCoroutine(WaitForKill(mainPlatform));

                if (closeGame || loseGame)
                {
                    break;
                }

                if (platformsCount % 15 == 0)
                {
                    if (delay >= 1.5f)
                    {
                        delay -= 0.5f;
                    }
                    if (TimeForLife > 3f)
                    {
                        TimeForLife -= 1;
                    }
                }
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private GameObject CreatObj(Vector3 pos, GameObject prefab)
    {
        GameObject newPlatform = Instantiate(prefab, pos, Quaternion.identity);
        StartCoroutine(ActivPlatform(newPlatform));
        newPlatform.transform.parent = Platforms.transform;
        return newPlatform;
    }

    private IEnumerator DelPlatform(GameObject platform)
    {
        if (platform)
        {
            platform.GetComponent<Collider>().isTrigger = true;
            for (int i = 99; i >= 0; --i)
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

    private IEnumerator ActivPlatform(GameObject platform)
    {

        for (int i = 1; i <= 100; ++i)
        {
            if (platform)
            {
                Color color = platform.GetComponent<Renderer>().material.color;
                platform.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, i * 0.01f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator WaitForKill(GameObject platform)
    {
        yield return new WaitForSeconds(TimeForLife);
        StartCoroutine(DelPlatform(platform));
    }

    private void Update()
    {
        if (!loseGame)
        {
            if (letsStart && killingNotActivate)
            {
                killingNotActivate = false;
                StartCoroutine(WaitForKill(mainPlatform));
                StartCoroutine(PlatformsGenering());
                Shadow.SetActive(false);
                text.enabled = false;
            }
            if (mainCube.GetComponent<Rigidbody>())
            {

                if (mainCube.transform.localPosition.y < 2 && UpPlatform)
                {
                    Platforms.GetComponent<Rigidbody>().isKinematic = false;
                    Platforms.GetComponent<Rigidbody>().velocity = new Vector3(0, 10f, 0);
                }
                else
                {
                    Platforms.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        else
        {
            Platforms.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
