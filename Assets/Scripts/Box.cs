using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Transform start_pos;
    private ObjectPoolNS box_particles;
    
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        box_particles = GameObject.FindGameObjectWithTag("Box Particle Pool").GetComponent<ObjectPoolNS>();
        transform.position = start_pos.position;
        health = 1;
    }

    // Update is called once per frame
    public void Take_Dmg()
    {
        health--;
        if (health <= 0)
        {
            box_particles.SpawnParticle(transform);
            GameManager.manager.coins += 10;
            gameObject.SetActive(false);
        }
    }



}
