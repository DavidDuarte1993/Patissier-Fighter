using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenDeactivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject fadeScreen;
        fadeScreen = GameObject.Find("FadeScreen");
        if (fadeScreen != null)
        {
            fadeScreen.GetComponent<Animation>().Play("unfade");
        }
    }

}
