using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject BossWarning;
    public float battlePosx;
    public float idlePosx;
    public bool moving;

    public float bossShoot_Timer;
    public float charging;

    private Animator anim;
    private PlaceholderBoss boss;
    private PlayerDeath player_death;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<PlaceholderBoss>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.bossBattle)
        {
            anim.SetFloat("Charging", charging);
            if (moving)
            {
                BossWarning.SetActive(true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(battlePosx, transform.position.y), 5f * Time.deltaTime);

            }
            else
            {
                BossWarning.SetActive(false);
            }



            if (transform.position.x != battlePosx)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }

            if(transform.position.x == battlePosx)
            {
                if(bossShoot_Timer >= 0)
                {
                    bossShoot_Timer -= Time.deltaTime;
                }
                else
                {
                    charging += Time.deltaTime;
                }
            }
        }
        else
        {
            BossWarning.SetActive(false);
            charging = 0;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(idlePosx, transform.position.y), 10f * Time.deltaTime);
            if (transform.position.x == idlePosx)
            {
                boss.health = 3;
                //health = 3;
            }
        }
    }


    public void InstantMove()
    {
        transform.position = new Vector2(idlePosx, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //hit player
            player_death.Die();
        }
        else if(collision.tag == "PlayerProjectile")
        {
            //get ienemy take dmg?
            boss.Take_Dmg();
        }
    }
}
