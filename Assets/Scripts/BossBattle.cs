using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBattle : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb2d;

    public float health;
    public GameObject BossWarning;
    //public GameObject Bullet;
    public bool when_to_shoot;
    public float boss_shoot_timer;
    public float shoot_timer;
    public bool moving;
    public bool when_to_charge;
    public float boss_charge_timer;
    public float charge_timer;
    
    public float battlePosx, idlePosx;

    [Header("Boss can do")]
    public bool can_shoot;
    public bool can_charge;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        InstantMove();
    }

    
    // Update is called once per frame
    private void Update()
    {

        if (GameManager.manager.bossBattle)
        {
            if (moving)
            {
                BossWarning.SetActive(true);
                when_to_shoot = false;
                when_to_charge = false;
            }
            else
            {
                BossWarning.SetActive(false);
                if (can_shoot)
                {
                    when_to_shoot = true;
                }
                if (can_charge)
                {
                    when_to_charge = true;
                }
            }

            if (when_to_shoot)
            {
                if (boss_shoot_timer >= 0)
                {
                    if(!GameManager.manager.dead && !GameManager.manager.movingReset)
                    {
                        boss_shoot_timer -= Time.deltaTime;
                    }
                }
                else
                {
                    boss_shoot_timer = shoot_timer;
                }
            }
            else
            {
                boss_shoot_timer = 0.1f;
            }

            if (when_to_charge)
            {
                if (boss_charge_timer >= 0)
                {
                    boss_charge_timer -= Time.deltaTime;
                    if (boss_charge_timer < .5f && boss_charge_timer > 0)
                    {
                        anim.SetTrigger("JumpAtk");
                    }
                    else
                    {
                        anim.ResetTrigger("JumpAtk");
                    }
                }
                else
                {

                    boss_charge_timer = charge_timer;
                }
            }
            else
            {
                boss_charge_timer = charge_timer;
            }


            if (transform.position.x != battlePosx)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(battlePosx, transform.position.y), 5f * Time.deltaTime);
                //BossWarning.SetActive(true);
                moving = true;
                //when_to_shoot = false;
            }
            else
            {
                moving = false;
                //BossWarning.SetActive(false);
                //when_to_shoot = true;
            }
        }
        else
        {
            when_to_shoot = false;
            when_to_charge = false;
            boss_shoot_timer = 0.1f;
            boss_charge_timer = charge_timer;
            BossWarning.SetActive(false);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(idlePosx, transform.position.y), 5f * Time.deltaTime);
            if(transform.position.x == idlePosx)
            {
                health = 3;
            }
        }
       

        
    }

    
    public void InstantMove()
    {
        transform.position = new Vector2(idlePosx, transform.position.y);
    }

    //public void Boss_Shoot()
    //{
    //    GameObject clone = Instantiate(Bullet, transform.position, transform.rotation);
    //    clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5, ForceMode2D.Impulse);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Projectile")
        {
            if (!GameManager.manager.dead && !GameManager.manager.movingReset)
            {
                if (GameManager.manager.bossBattle && !moving)
                {
                    Debug.Log("projectile hit boss");
                    health--;
                    if (health <= 0)
                    {
                        GameManager.manager.bossBattle = false;
                        GameManager.manager.bossAppearDist = GameManager.manager.distanceMoved + 500;
                        //gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile")
        {
            if (!GameManager.manager.dead && !GameManager.manager.movingReset)
            {
                if (GameManager.manager.bossBattle && !moving)
                {
                    Debug.Log("projectile hit boss");
                    health--;
                    if (health <= 0)
                    {
                        GameManager.manager.bossBattle = false;
                        GameManager.manager.bossAppearDist = GameManager.manager.distanceMoved + 500;
                        //gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
