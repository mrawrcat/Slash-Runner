using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform start_pos;

    [Header("Interactable Check")]
    public bool can_dmg_player;
    public bool shootable;
    public bool isBox;
    public bool isBarrel;
    public bool isBoss;
    public bool isSpike;

    [Header("Particles")]
    //public ParticleSystem box_break;

    private ObjectPoolNS barrelPool;
    private CamShake shake;
    public float health;
    private BoxCollider2D box_collider;
    private void Start()
    {
        shake = FindObjectOfType<CamShake>();
        box_collider = GetComponent<BoxCollider2D>();
        transform.position = start_pos.position;
        barrelPool = GameObject.Find("Barrel Spawner").GetComponent<ObjectPoolNS>();
        health = 1;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            if (collision.collider.tag == "Player")
            {

                if(GameManager.manager.invincible > 0)
                {
                    if (!isSpike)
                    {
                        gameObject.SetActive(false);
                        transform.position = start_pos.position;
                    }
                }
                else
                {
                    shake.Shake();
                    GameManager.manager.dead = true;
                    
                }
            }
            else if (collision.collider.tag == "Projectile")
            {
                if (shootable)
                {
                    gameObject.SetActive(false);
                    transform.position = start_pos.position;
                }
                
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            if (collision.collider.tag == "Player")
            {

                if (GameManager.manager.invincible > 0)
                {
                    if (!isSpike)
                    {
                        gameObject.SetActive(false);
                        transform.position = start_pos.position;
                    }
                }
                else
                {
                    shake.Shake();
                    GameManager.manager.dead = true;

                }
            }
            else if (collision.collider.tag == "Projectile")
            {
                if (shootable)
                {
                    gameObject.SetActive(false);
                    transform.position = start_pos.position;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            if (collision.tag == "Player")
            {

                if (GameManager.manager.invincible > 0)
                {
                    if (!isSpike)
                    {
                        gameObject.SetActive(false);
                        transform.position = start_pos.position;
                    }
                }
                else
                {
                    shake.Shake();
                    GameManager.manager.dead = true;

                }
            }
            else if (collision.tag == "Projectile")
            {
                if (shootable)
                {
                    gameObject.SetActive(false);
                    transform.position = start_pos.position;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            if (collision.tag == "Player")
            {

                if (GameManager.manager.invincible > 0)
                {
                    if (!isSpike)
                    {
                        gameObject.SetActive(false);
                        transform.position = start_pos.position;
                    }
                }
                else
                {
                    shake.Shake();
                    GameManager.manager.dead = true;

                }
            }
            else if (collision.tag == "Projectile")
            {
                if (shootable)
                {
                    gameObject.SetActive(false);
                    transform.position = start_pos.position;
                }
            }
        }
    }

    public void TakeDmg()
    {
        health--;
        if(health <= 0)
        {
            //box_break.Play();
            //gameObject.SetActive(false);
        }
    }
}
