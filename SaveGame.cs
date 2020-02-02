using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public string gametype = "Adventure";
   
    public static void SaveINTdata(string st, int v)
    {
        PlayerPrefs.SetInt(st, v);
    }
    public void saveGame()
    {
        int h = (int)FindObjectOfType<ADV_Player>().Get_Player_Health().getHealth();
        SaveINTdata(StaticStrings.savedHealth, h);
        SaveINTdata(gametype+StaticStrings.savedScore, ADV_UIManager.getScore());

    }
}
  
