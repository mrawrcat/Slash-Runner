using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTilemapOffByTransform : MonoBehaviour
{
    private ObjectPoolNS tilemap_pool;
    // Start is called before the first frame update
    void Start()
    {
        tilemap_pool = GetComponentInParent<ObjectPoolNS>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -30)
        {
            if (GameManager.manager.bossBattle)
            {
                tilemap_pool.SpawnBossTM();
            }
            else
            {
                tilemap_pool.SpawnTileMap();
            }
            gameObject.SetActive(false);
        }
    }
}
