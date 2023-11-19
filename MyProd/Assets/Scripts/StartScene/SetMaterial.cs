    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material[] materials;
    private void Awake()
    {
        Color color = materials[PlayerPrefs.GetInt("CubeMaterial", 0)].color;
        materials[PlayerPrefs.GetInt("CubeMaterial", 0)].color = new Color(color.r, color.g, color.b, 1f);
        GetComponent<Renderer>().material = materials[PlayerPrefs.GetInt("CubeMaterial", 0)];
    }
}