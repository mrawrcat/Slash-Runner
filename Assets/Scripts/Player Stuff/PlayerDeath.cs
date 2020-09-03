using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Transform startpos;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            if(transform.position.y <= -10)
            {
                if (!SoundManager.sound_manager.play_death_sfx)//play gameover sound
                {
                    SoundManager.sound_manager.death_sfx.Play();
                    SoundManager.sound_manager.play_death_sfx = true;
                }
                GameManager.manager.isUnder = true;
                transform.position = new Vector2(-5, -10);
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
            transform.position = Vector2.MoveTowards(transform.position, startpos.position, 5f * Time.deltaTime);

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

    public void InstantMove()
    {
        SoundManager.sound_manager.gameplay_soundtrack.Stop();
        SoundManager.sound_manager.gameplay_soundtrack.Play();
        transform.position = startpos.position;
    }
}
