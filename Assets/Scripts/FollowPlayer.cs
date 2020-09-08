using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform focus;
    public float offset;
    private ObjectPoolNS pool;

    private void Start()
    {
        pool = GameObject.Find("Tilemap").GetComponent<ObjectPoolNS>();
    }
    void Update()
    {
        if(!GameManager.manager.dead && !GameManager.manager.movingReset)
        {
            
             transform.position = focus.position;
            
        }
        
    }
}
