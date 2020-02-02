using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADV_Peace : MonoBehaviour
{
    [SerializeField]
    GameObject[] characters=null;
    [SerializeField]
    GameObject activationCube = null;
    GameObject fadeScreen;
    [SerializeField]
    GameObject smile = null;
   void Start()
    {
        if (Soundmanager.instance != null)
        {
            Soundmanager.instance.StopBgm();
        }
        fadeScreen = GameObject.Find("FadeScreen");
        StartCoroutine(peaceCo());
    }

     IEnumerator peaceCo()
    {
        yield return new WaitForSeconds(7);
        foreach(var c in characters)
        {
            if (c != null)
            {
                c.GetComponent<Animator>().SetTrigger("PEACE");
            }
        }
        if (smile != null)
            smile.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        fadeScreen.GetComponent<Animation>().Play("fade");
        yield return new WaitForSeconds(2f);
        activationCube.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fadeScreen.GetComponent<Animation>().Play("unfade");
        
    }
}
