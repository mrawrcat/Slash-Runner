﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Transform startpos;
    public GameObject target_tracker;
    private Rigidbody2D rb2d;
    private CamShake shake;
    private ObjectPoolNS pool;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shake = FindObjectOfType<CamShake>();
        pool = GameObject.Find("Tilemap").GetComponent<ObjectPoolNS>();
    }

    // Update is called once per frame
    void Update()
    {
        If_Player_Died();
    }

    private void If_Player_Died()
    {
        if (GameManager.manager.dead)
        {
            SoundManager.sound_manager.gameplay_soundtrack.Pause();
            if (!SoundManager.sound_manager.play_hit_sfx)//play death hit sound
            {
                SoundManager.sound_manager.hit_sfx.Play();
                SoundManager.sound_manager.play_hit_sfx = true;

            }
            if(transform.position.y <= GameManager.manager.deadheight -30)
            {
                if (!SoundManager.sound_manager.play_death_sfx)//play gameover sound
                {
                    SoundManager.sound_manager.death_sfx.Play();
                    SoundManager.sound_manager.play_death_sfx = true;
                }
                GameManager.manager.isUnder = true;
                transform.position = new Vector2(-5, GameManager.manager.deadheight - 30);
                rb2d.velocity = Vector2.zero;
                rb2d.gravityScale = 0;
            }
        }
        else if (GameManager.manager.movingReset)
        {
            SoundManager.sound_manager.gameplay_soundtrack.UnPause();
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity = Vector2.zero;
            }
            GameManager.manager.isUnder = false;
            startpos.position = new Vector2(-6, GameManager.manager.deadheight+5);
            transform.position = Vector2.MoveTowards(transform.position, startpos.position, 30f * Time.deltaTime);

            if (transform.position == startpos.position)
            {
                GameManager.manager.movingReset = false;
                GameManager.manager.invincible = 1f;
                rb2d.gravityScale = 3;
            }
        }

        if (!GameManager.manager.dead)
        {
            if (SoundManager.sound_manager.death_sfx.isPlaying)
            {
                SoundManager.sound_manager.death_sfx.Stop();
            }
            SoundManager.sound_manager.play_hit_sfx = false;
            SoundManager.sound_manager.play_death_sfx = false;
        }

    }

    public void Die()
    {
        GameManager.manager.deadheight = transform.position.y;
        GameManager.manager.dead = true;
        shake.Shake();

    }

    public void InstantMove()
    {
        SoundManager.sound_manager.gameplay_soundtrack.Stop();
        SoundManager.sound_manager.gameplay_soundtrack.Play();
        transform.position = new Vector2(-5,10);
        target_tracker.transform.position = new Vector2(15, 10);
    }
}
