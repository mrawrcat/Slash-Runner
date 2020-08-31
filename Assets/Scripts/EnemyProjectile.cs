using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private CamShake shake;
    public string[] tags;
    private void Start()
    {
        shake = FindObjectOfType<CamShake>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < tags.Length; i++)
        {
            if(collision.collider.tag == tags[i])
            {
                gameObject.SetActive(false);
            }
        }

        if(!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            if(collision.collider.tag == "Player")
            {
                if (GameManager.manager.invincible <= 0)
                {
                    shake.Shake();
                    GameManager.manager.dead = true;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.collider.tag == tags[i])
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Atk_Projectile()
    {
        gameObject.SetActive(false);
    }
}
