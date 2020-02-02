using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertName : MonoBehaviour
{
    string NM;
    InputField field;
    [SerializeField]
    Text downText = null;
    public string gameType = "Adventure";
    private void Start()
    {
        field = GetComponentInChildren<InputField>();
    }
    public void SetName()
    {
        NM = field.GetComponentInChildren<Text>().text;
        saveGame();
    }
   public void saveGame()
    {
        PlayerPrefs.SetString(StaticStrings.playerName, NM);
        downText.text = NM + " ようこそ";
        SceneLoader loader = FindObjectOfType<SceneLoader>();
        loader.loadGAME();
        
    }
}
