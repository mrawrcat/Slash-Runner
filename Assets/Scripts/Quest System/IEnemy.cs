using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int ID { get; set; }
    //void Take_Dmg(float dmg_amt);
    void Die();
}
