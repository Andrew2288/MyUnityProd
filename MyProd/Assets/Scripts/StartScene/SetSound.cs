using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    public GameObject blockSoundImg;
    public GameObject mainMusic;
    public GameObject buttonMusic;
    public GameObject dropMusic;
    void Start()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            mainMusic.GetComponent<AudioSource>().enabled = true;
            buttonMusic.GetComponent<AudioSource>().enabled = true;
            dropMusic.GetComponent<AudioSource>().enabled = true;
            blockSoundImg.SetActive(false);
        }
        else
        {
            mainMusic.GetComponent<AudioSource>().enabled = false;
            buttonMusic.GetComponent<AudioSource>().enabled = false;
            dropMusic.GetComponent<AudioSource>().enabled = false;
            blockSoundImg.SetActive(true);
        }
    }
}
