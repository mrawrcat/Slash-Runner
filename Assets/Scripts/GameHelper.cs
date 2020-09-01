using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("UI stuff")]
    public float dead_countdown;
    private float savedDead;
    public GameObject deadPanel;
    public Image continuePanel;
    public GameObject pausePanel;
    public Text musictxt;
    public Text sfxtxt;
    private bool options_panel_onoff;

    public RectTransform reviveButtonRect;
    public RectTransform deadStats;

    [Header("Text stuff")]
    public Text coins;
    public Text distance;
    public Text dead5txt;
    public Text deadcoins;
    public Text deaddist;

    public ObjectPoolNS pool;
    public GameObject[] start_tilemaps;

    public GameObject[] Boss;

    private float distLerpHolder;
    private float coinsLerpHolder;
    private float lerpSpeed;

    private Quest_Giver quest_giver;
    public bool session_end_talked;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        reviveButtonRect.anchoredPosition = new Vector2(0, 400);
        deadStats.anchoredPosition = new Vector2(0, 500);
        savedDead = dead_countdown;
        quest_giver = FindObjectOfType<Quest_Giver>();
        session_end_talked = false;
        /*
        if (!GameManager.manager.mute_music)
        {
            GameManager.manager.title_soundtrack.Stop();
            if (!GameManager.manager.gameplay_soundtrack.isPlaying)
            {
                GameManager.manager.gameplay_soundtrack.Play();
            }
        }
        else
        {
            GameManager.manager.gameplay_soundtrack.Stop();
            GameManager.manager.title_soundtrack.Stop();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        show_text();
        if (!GameManager.manager.dead)
        {
            if (!GameManager.manager.movingReset)
            {
                GameManager.manager.distanceMoved += GameManager.manager.finalSpeed * Time.deltaTime;
            }
        }

        for (int i = 0; i < Boss.Length; i++)
        {
            if(i == GameManager.manager.whichBoss)
            {
                Boss[i].SetActive(true);
            }
            else
            {
                Boss[i].SetActive(false);
            }
        }

        if (GameManager.manager.isUnder)
        {
            Move_Continue();
            continuePanel.enabled = true;
            dead_countdown -= Time.deltaTime;
            if(dead_countdown <= 0)
            {
                reviveButtonRect.anchoredPosition = new Vector2(0, 400);
                deadPanel.SetActive(true);
                MoveDeadStats();
                lerpSpeed += Time.deltaTime / 2;
                if (!session_end_talked)
                {
                    quest_giver.Talk_to_Quest_Giver();
                    session_end_talked = true;
                }

            }
        }
        else
        {
            continuePanel.enabled = false;
            dead_countdown = savedDead;
            reviveButtonRect.anchoredPosition = new Vector2(0, 400);
            deadStats.anchoredPosition = new Vector2(0, 500);
            deadPanel.SetActive(false);
            lerpSpeed = 0;
        }


        Sound_Stuff();

    }

    private void Sound_Stuff()
    {
        if (options_panel_onoff)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
        /*
        if (GameManager.manager.mute_music)
        {
            musictxt.text = "Music Off";
        }
        else
        {
            musictxt.text = "Music On";
        }

        if (GameManager.manager.mute_sfx)
        {
            sfxtxt.text = "Sfx Off";
        }
        else
        {
            sfxtxt.text = "Sfx On";
        }
        */
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        options_panel_onoff = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        options_panel_onoff = false;

    }

    private void show_text()
    {
        dead5txt.text = dead_countdown.ToString("F0");
        coins.text = GameManager.manager.coins.ToString("F0");
        distance.text = GameManager.manager.distanceMoved.ToString("F0");
        deaddist.text = "Distance moved " + distLerpHolder.ToString("F0");
        deadcoins.text = "Total Coins Gained: " + coinsLerpHolder.ToString("F0");


    }

    public void Move_Continue()
    {
        reviveButtonRect.anchoredPosition = Vector2.Lerp(reviveButtonRect.anchoredPosition, new Vector2(0, 0), 5f * Time.deltaTime);
    }

    public void MoveDeadStats()
    {
        deadStats.anchoredPosition = Vector2.Lerp(deadStats.anchoredPosition, new Vector2(0, 65), 5f * Time.deltaTime);
        distLerpHolder = Mathf.Lerp(0, GameManager.manager.distanceMoved, lerpSpeed);
        coinsLerpHolder = Mathf.Lerp(0, GameManager.manager.coins, lerpSpeed);
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
        for(int i = 0; i< Boss.Length; i++)
        {
            Boss[i].GetComponent<BossBattle>().InstantMove();
        }
        GameManager.manager.bossAppearDist = 500;
        player.InstantMove();
    }

    public void Continue()
    {
        GameManager.manager.dead = false;
        GameManager.manager.movingReset = true;
        
    }

    /*
    public void Music_OnOff()
    {
        GameManager.manager.mute_music = !GameManager.manager.mute_music;
        if (!GameManager.manager.mute_music)
        {
            if (!GameManager.manager.gameplay_soundtrack.isPlaying)
            {
                GameManager.manager.gameplay_soundtrack.Play();
            }
            if (GameManager.manager.title_soundtrack.isPlaying)
            {
                GameManager.manager.title_soundtrack.Stop();
            }
        }
    }
    */
    public void Sfx_OnOff()
    {
        //GameManager.manager.mute_sfx = !GameManager.manager.mute_sfx;
    }
}
