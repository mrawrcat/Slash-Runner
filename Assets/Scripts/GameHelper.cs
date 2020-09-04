using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour
{
    public GameObject[] start_tilemaps;
    public ObjectPoolNS pool;
    public GameObject[] boss_obj;
    

    [Header("Dead stuff")]
    public GameObject continue_panel;
    public GameObject dead_panel;

    [Header("Text stuff")]
    public Text coins;
    public Text distance;
    public Text dead_countdownTxt;
    public Text deadcoins;
    public Text deaddist;

    [Header("RectTransforms")]
    public RectTransform reviveButtonRect;

    private float distLerpHolder;
    private float coinsLerpHolder;
    private float lerpSpeed;
    private float dead_countdown;

    private PlayerDeath player_death;
    private Quest_Giver quest_giver;
    // Start is called before the first frame update
    void Start()
    {
        player_death = FindObjectOfType<PlayerDeath>();
        quest_giver = FindObjectOfType<Quest_Giver>();
        //pool = GameObject.Find("Tilemap").GetComponent<ObjectPoolNS>();
    }

    // Update is called once per frame
    void Update()
    {
        show_text();
        If_Dead();
        Show_Boss();
    }

    private void Show_Boss()
    {
        for (int i = 0; i < boss_obj.Length; i++)
        {
            if (i == GameManager.manager.whichBoss)
            {
                boss_obj[i].SetActive(true);
            }
            else
            {
                boss_obj[i].SetActive(false);
            }
        }
    }

    private void show_text()
    {
        dead_countdownTxt.text = dead_countdown.ToString("F0");
        coins.text = GameManager.manager.coins.ToString("F0");
        distance.text = GameManager.manager.distanceMoved.ToString("F0");
        deaddist.text = "Distance moved " + distLerpHolder.ToString("F0");
        deadcoins.text = "Total Coins Gained: " + coinsLerpHolder.ToString("F0");
    }

    private void If_Dead()
    {
        if (GameManager.manager.isUnder && dead_countdown >= 0)
        {
            Move_Continue();
            dead_countdown -= Time.deltaTime;
            
        }
        else if (GameManager.manager.isUnder && dead_countdown < 0)
        {
            reviveButtonRect.anchoredPosition = new Vector2(0, 400);
            MoveDeadStats();
            lerpSpeed += Time.deltaTime;

        }
        else
        {
            continue_panel.SetActive(false);
            dead_countdown = 10f;
            reviveButtonRect.anchoredPosition = new Vector2(0, 400);
            //deadStats.anchoredPosition = new Vector2(0, 500);
            dead_panel.SetActive(false);
            lerpSpeed = 0;

        }
    }


    public void Move_Continue()
    {
        continue_panel.SetActive(true);
        reviveButtonRect.anchoredPosition = Vector2.Lerp(reviveButtonRect.anchoredPosition, new Vector2(0, 0), 5f * Time.deltaTime);
    }

    public void MoveDeadStats()//skipped or no more continues -> maybe talk to questgiver again here?
    {
        quest_giver.Talk_to_Quest_Giver();
        dead_panel.SetActive(true);
        //deadStats.anchoredPosition = Vector2.Lerp(deadStats.anchoredPosition, new Vector2(0, 65), 5f * Time.deltaTime);
        distLerpHolder = Mathf.Lerp(0, GameManager.manager.distanceMoved, lerpSpeed);
        coinsLerpHolder = Mathf.Lerp(0, GameManager.manager.coins, lerpSpeed);
    }


    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        pool.HideAllGridChild();
        GameManager.manager.distanceMoved = 0;
        GameManager.manager.coins = 0;
        for (int i = 0; i < start_tilemaps.Length; i++)
        {
            start_tilemaps[i].SetActive(true);
            start_tilemaps[i].transform.position = new Vector2(0 + (23 * i), 0);
        }
        GameManager.manager.dead = false;
        GameManager.manager.movingReset = true;
        GameManager.manager.bossBattle = false;
        
        for (int i = 0; i < boss_obj.Length; i++)
        {
            boss_obj[i].GetComponent<BossMove>().InstantMove();
        }
        GameManager.manager.bossAppearDist = 500;
        
        player_death.InstantMove();
        quest_giver.Forfeit_Quest();
        quest_giver.questNum = Random.Range(0, quest_giver.how_many_quests);
        quest_giver.Talk_to_Quest_Giver();
        quest_giver.showedQuest = false;
        
    }

    public void Continue()
    {
        GameManager.manager.dead = false;
        GameManager.manager.movingReset = true;
    }

    public void Refuse_Continue()
    {
        dead_countdown = 0;
    }
}
