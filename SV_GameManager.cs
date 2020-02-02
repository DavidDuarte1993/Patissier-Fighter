using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using td;

public class SV_GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject scorePanel = null;
    SV_EnemyManager spawner;
    [SerializeField]
    Text clocktext=null;
    [SerializeField]
    float gameTime = 180;
    bool gameEnd = false;
    bool loseBattle = false;
    Text congratulationText;
    Text pointText;
   void Start()
    {
        spawner = FindObjectOfType<SV_EnemyManager>();
        spawner.INIT();
        if (Soundmanager.instance != null)
            Soundmanager.instance.PlayBgmByName("duel");
        congratulationText = scorePanel.transform.GetChild(0).GetComponent<Text>();
        pointText = scorePanel.transform.GetChild(1).GetComponent<Text>();
    }
    private void Update()
    {
        if (gameEnd) return;
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            gameEnd = true;
            gameTime = 0;
            GoToEnd();
        }
        clocktext.text = ((int)gameTime).ToString();
    }

    public void GoToEnd()
    {
        if (!gameEnd)
            gameEnd = true;
        if (gameTime > 0)
        {
            loseBattle = true;
        }
        spawner.endGame();
       StartCoroutine(showEndCo());
    }

    IEnumerator showEndCo()
    {
        yield return new WaitForSeconds(1);
        scorePanel.SetActive(true);
        if (loseBattle)
        {
            congratulationText.text = "ケーキが食べた、残念";
        }
        else
        {
            congratulationText.text = "おめでとう！";
        }
        float value = 0;
        while (value < spawner.getScore())
        {
            value += Time.deltaTime * 500;
            if (value >= spawner.getScore())
            {
                value = spawner.getScore();
            }
            pointText.text = value.ToString();
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneLoader loader = FindObjectOfType<SceneLoader>();
        loader.loadGAME();
    }
}
