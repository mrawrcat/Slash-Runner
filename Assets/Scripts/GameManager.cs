using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public float finalSpeed;
    private float savefinalSpeed;
    public float coins;
    public float total_coins;
    public float distanceMoved;
    public float invincible;

    [Header("Game/Player Status")]
    public bool dead;
    public bool movingReset;
    public bool isUnder;

    [Header("Boss Stuff")]
    public bool bossBattle;
    public float bossAppearDist;
    public int whichBoss;

    [Header("Game Mode Stuff")]
    public bool enable_Boss;
    private float distMultiply;

    /*
    [Header("Sound Stuff")]
    public AudioSource jump_sfx;
    public AudioSource hit_sfx;
    public AudioSource death_sfx;
    public AudioSource smash_sfx;
    public AudioSource title_soundtrack;
    public AudioSource gameplay_soundtrack;
    public bool play_hit_sfx;
    public bool play_death_sfx;
    public bool play_soundtrack;
    public bool mute_music;
    public bool mute_sfx;
    */
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        savefinalSpeed = finalSpeed;
        bossAppearDist = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead || movingReset)
        {
            finalSpeed = 0;
            
        }
        else
        {
            //finalSpeed = savefinalSpeed + distMultiply;
        }

        if(distanceMoved >= 10000)
        {
            distMultiply = 10;
        }
        else
        {
            distMultiply = distanceMoved / 1000;
        }

        if(invincible > 0)
        {
            invincible -= Time.deltaTime;
        }

        if(distanceMoved > bossAppearDist && distanceMoved < bossAppearDist + 1)
        {
            if (enable_Boss)
            {
                bossBattle = true;
                randomBoss();
            }
        }
        /*
        if (mute_music)
        {
            title_soundtrack.mute = true;
            gameplay_soundtrack.mute = true;

        }
        else
        {
            title_soundtrack.mute = false;
            gameplay_soundtrack.mute = false;
        }

        if (mute_sfx)
        {
            jump_sfx.mute = true;
            hit_sfx.mute = true;
            death_sfx.mute = true;
            smash_sfx.mute = true;
        }
        else
        {
            jump_sfx.mute = false;
            hit_sfx.mute = false;
            death_sfx.mute = false;
            smash_sfx.mute = false;
        }
        */
    }

    public void randomBoss()
    {
        whichBoss = Random.Range(0, 4);
    }
    public void SaveData()
    {
        SaveDataSystem.SaveGameData(this);
    }
    public void LoadData()
    {
        StoreData data = SaveDataSystem.loadData();

        total_coins = data.total_coins;
    }
}
