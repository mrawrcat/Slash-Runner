using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string[] tags;

    private Rigidbody2D rb2d;
    private ObjectPoolNS particles;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particles = GameObject.Find("Particles").GetComponent<ObjectPoolNS>();
        //rb2d.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }
    //when it hits anything so be vigilant about adding stuff
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < tags.Length; i++)
        {
            if(collision.collider.tag == tags[i])
            {
                particles.SpawnParticle(transform);
                gameObject.SetActive(false);
                Debug.Log("player projectile hit :" + collision.collider.tag);
            }
        }
        if(collision.collider.tag == "TurnObjectsOn")
        {
            gameObject.SetActive(false);
            Debug.Log("player projectile hit :" + collision.collider.tag);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.collider.tag == tags[i])
            {
                particles.SpawnParticle(transform);
                gameObject.SetActive(false);
            }
        }
        if (collision.collider.tag == "TurnObjectsOn")
        {
            gameObject.SetActive(false);
            Debug.Log("player projectile hit :" + collision.collider.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                particles.SpawnParticle(transform);
                gameObject.SetActive(false);
                Debug.Log("hit :" + collision.tag);
            }
        }
        if (collision.tag == "TurnObjectsOn")
        {
            gameObject.SetActive(false);
            Debug.Log("player projectile hit :" + collision.tag);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.tag == tags[i])
            {
                particles.SpawnParticle(transform);
                gameObject.SetActive(false);
            }
        }
        if (collision.tag == "TurnObjectsOn")
        {
            gameObject.SetActive(false);
            Debug.Log("player projectile hit :" + collision.tag);
        }
    }
}
