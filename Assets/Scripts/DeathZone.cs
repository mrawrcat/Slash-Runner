using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private CamShake shake;
    // Start is called before the first frame update
    void Start()
    {
        shake = FindObjectOfType<CamShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.tag == "Player")
    //    {
    //        //GameManager.manager.health -= GameManager.manager.health;
    //        //shake.Shake();
    //        GameManager.manager.dead = true;

    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!GameManager.manager.dead)
            {
                if (!GameManager.manager.movingReset)
                {
                    shake.Shake();

                }
            }

            if (!GameManager.manager.movingReset)
            {
                GameManager.manager.dead = true;
            }
        }
    }
}
