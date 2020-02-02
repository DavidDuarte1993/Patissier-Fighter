using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    bool gameEnd = false;
    public bool GaneStart { get { return gameStart; } set { gameStart = value; } }
    Text clockText;
    [SerializeField]
    float battleTimer = 180;
    bool gameStart = false;
    private void Start()
    {
        clockText = GetComponentInChildren<Text>();
        clockText.text = "180";
    }

    void Update()
    {
        if (gameEnd||!gameStart) return;
        
         battleTimer -= Time.deltaTime;
        if (battleTimer <= 0)
        {
            battleTimer = 0;
            finalCountDown();
        }
         clockText.text = Mathf.FloorToInt(battleTimer).ToString();
         
    }

    //タイマー時間が終わったら終了
    void finalCountDown()
    {
            gameEnd = true;
            clockText.fontSize = 20;
            B_GameManager.instance.StartcountDown(false);
        
    }
}
