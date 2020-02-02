using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public string gameType = "Adventure";
    void Start()
    {
        PlayerPrefs.SetInt(StaticStrings.savedHealth, 5);
        PlayerPrefs.SetInt(gameType+StaticStrings.savedScore, 0);
    }

}
