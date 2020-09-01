using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    
    public void moveScene(string name)
    {
        SceneManager.LoadScene(name);
        GameManager.manager.dead = false;
        GameManager.manager.isUnder = false;
        GameManager.manager.bossBattle = false;
        GameManager.manager.distanceMoved = 0;
        GameManager.manager.bossAppearDist = 500;
        //GameManager.manager.play_hit_sfx = false;
        //GameManager.manager.play_death_sfx = false;

    }
}
