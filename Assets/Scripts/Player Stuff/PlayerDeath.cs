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
            //play death hit sound
            SoundManager.sound_manager.gameplay_soundtrack.Pause();
            if(transform.position.y <= -10)
            {
                //play gameover sound
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

    }

    public void InstantMove()
    {
        SoundManager.sound_manager.gameplay_soundtrack.Stop();
        SoundManager.sound_manager.gameplay_soundtrack.Play();
        transform.position = startpos.position;
    }
}
