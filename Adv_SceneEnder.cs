using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adv_SceneEnder : MonoBehaviour
{
    SceneLoader sc;
    void Start()
    {
        sc = FindObjectOfType<SceneLoader>();
        sc.loadGAME();
    }

}
