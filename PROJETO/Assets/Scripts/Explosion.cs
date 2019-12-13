using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int damage = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {

        BOSS boss = other.GetComponent<BOSS>();

        Enemy otherEnemy = other.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            otherEnemy.TookDamege(damage);
        }

        if (boss != null)
        {
            boss.TookDamege(damage);
        }

    }


}
