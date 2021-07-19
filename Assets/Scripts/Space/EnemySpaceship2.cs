using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceship2 : EnemySpaceship {

    protected override void Update() {
        base.Update();

        Fire();
    }


    private void Fire() {
        if (canFire && !dead) {
            GameObject laser1 = enemyLaserPool.GetObject();
            laser1.transform.position = laserPoints[0].transform.position;
            laser1.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            laser1.SetActive(true);

            GameObject laser2 = enemyLaserPool.GetObject();
            laser2.transform.position = laserPoints[1].transform.position;
            laser2.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            laser2.SetActive(true);


            AudioManager.instance.PlaySoundWithIndex("EnemyLaser", 0);

            canFire = false;
            StartCoroutine(WaitForFireRate());
        }
    }
}
