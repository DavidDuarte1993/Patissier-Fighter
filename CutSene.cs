using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutSene : MonoBehaviour
{
  
    GameObject endtext = null;
    [SerializeField]
    GameObject sceneloader=null;
    string m = "";
    private void Start()
    {
        if (Soundmanager.instance != null)
        {
            Soundmanager.instance.StopBgm();
            Soundmanager.instance.PlayBgmByName("HappyRock");
        }
        if (PlayerPrefs.HasKey(StaticStrings.rugbyResult))
        {
            m = PlayerPrefs.GetString(StaticStrings.rugbyResult);
        }
        else
        {
            m = "test is null";
        }
        endtext = GameObject.Find("EndMessage");
        endtext.GetComponent<Text>().text = m;
        endtext.GetComponent<Animator>().SetTrigger("go");
        Invoke("Activetxt",6);
    }
    
  
    void Activetxt()
    {
        
        sceneloader.SetActive(true);

    }
   
}
