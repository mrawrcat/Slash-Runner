using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Transform start_pos;
    public ParticleSystem box_break;
    private ObjectPoolNS barrels;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start_pos.position;
        barrels = GameObject.Find("Barrel Spawner").GetComponent<ObjectPoolNS>();
        health = 1;
    }

    // Update is called once per frame
    public void Take_Dmg()
    {
        health--;
        if(health <= 0)
        {
            barrels.transform.position = gameObject.transform.position;
            barrels.SpawnProjectile(transform);
            gameObject.SetActive(false);
        }
    }
}
