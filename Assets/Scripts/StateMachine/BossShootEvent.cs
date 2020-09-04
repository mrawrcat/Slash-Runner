using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootEvent : MonoBehaviour
{
    public ObjectPoolNS bullet_pool;
    public Transform shootPos;
    private BossMove boss;

    private void Start()
    {
        boss = GetComponent<BossMove>();
    }

    public void Boss_Shoot()
    {
        bullet_pool.SpawnProjectile(shootPos);
        boss.bossShoot_Timer = 5f;
        boss.charging = 0;

    }
}
