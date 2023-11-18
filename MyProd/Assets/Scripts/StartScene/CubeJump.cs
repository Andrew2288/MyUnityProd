using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour
{
    private bool gameStarted;
    public GameObject mainCube;
    private float startJumpTime;
    private List<GameObject> platforms;
    private Vector3 forceVector;
    private float speed = 250f;
    private float prevPosY;
    public static bool afterFlying;
    public static bool isOver;
    public GameObject loseButtons;
    private bool canJump = false;
    private void Awake()
    {
        isOver = false;
        gameStarted = false;
    }
    private void Start()
    {
        gameStarted = true;
        prevPosY = 1;
        afterFlying = false;
    }
    private void OnMouseDown()
    {
        if (GenNewPlatform.letsStart)
        {
            if (gameStarted)
            {
                if (!mainCube.GetComponent<CubeAnimation>().enabled)
                {
                    mainCube.GetComponent<CubeAnimation>().enabled = true;
                }
                CubeAnimation.ChangeWay();
                startJumpTime = Time.time;
                canJump = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if (GenNewPlatform.letsStart)
        {
            if (gameStarted && canJump)
            {
                CubeAnimation.ChangeWay();

                float mousePosX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0)).x;
                forceVector = new Vector3((mousePosX - mainCube.GetComponent<Transform>().position.x), 15, 0);

                if (!afterFlying)
                {
                    afterFlying = true;
                    prevPosY = mainCube.GetComponent<Transform>().position.y;
                    float time = Time.time - startJumpTime;
                    if (time < 1f)
                    {
                        mainCube.GetComponent<Rigidbody>().AddForce(forceVector * speed);
                    }
                    else if (time < 3f)
                    {
                        mainCube.GetComponent<Rigidbody>().AddForce(forceVector * speed * time);
                    }
                    else
                    {
                        mainCube.GetComponent<Rigidbody>().AddForce(forceVector * speed * 3f);
                    }
                }
            }
        }
        else
        {
            GenNewPlatform.letsStart = true;
        }
        GenNewPlatform.UpPlatform = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (mainCube.GetComponent<Transform>().position.y < -10)
            {
                GameOver();
                return;
            }
            else if (isOver)
            {
                GameOver();
                return;
            }

            if (mainCube.GetComponent<Rigidbody>().IsSleeping())
            {
                GenNewPlatform.UpPlatform = false;
                afterFlying = false;
            }
        }
    }

    public void GameOver()
    {
        if (gameStarted)
        {
            gameStarted = false;
            if (mainCube.GetComponent<Rigidbody>())
            {
                Destroy(mainCube.GetComponent<Rigidbody>());
            }
            GenNewPlatform.loseGame = true;
            loseButtons.GetComponent<LoseGameScript>().enabled = true;
        }
    }
}
