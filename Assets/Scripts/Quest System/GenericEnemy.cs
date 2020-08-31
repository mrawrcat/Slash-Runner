using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour, IEnemy
{
    public int ID { get; set; }

    private float health;
    // Start is called before the first frame update
    void Start()
    {
        ID = 0;
    }

    
    public void Take_Dmg(float dmg_amt)
    {
        health -= dmg_amt;
        if(health <= 0)
        {
            CombatEvents.EnemyDied(this);
            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        CombatEvents.EnemyDied(this);
        Debug.Log("killed generic enemy");
        gameObject.SetActive(false);

    }

    
}
