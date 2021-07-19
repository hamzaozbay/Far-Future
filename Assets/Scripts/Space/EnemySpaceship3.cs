using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceship3 : EnemySpaceship {


    protected override void Update() {
        base.Update();

        Fire();
    }


    private void Fire() {
        if (canFire && !dead && player.transform.position.y - this.transform.position.y < -2.25f) {
            GameObject laser = enemyLaserPool.GetObject();
            laser.transform.position = laserPoints[0].transform.position;
            laser.GetComponent<EnemyLaserFollow>().SetTarget(player.transform);

            laser.SetActive(true);

            AudioManager.instance.PlaySoundWithIndex("EnemyLaser", 1);

            canFire = false;
            StartCoroutine(WaitForFireRate());
        }
    }

}
