using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceship1 : EnemySpaceship {

    protected override void Update() {
        base.Update();

        Fire();
    }


    private void Fire() {
        if(canFire && !dead) {
            GameObject laser1 = enemyLaserPool.GetObject();
            laser1.transform.position = laserPoints[0].transform.position;
            laser1.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            laser1.SetActive(true);

            GameObject laser2 = enemyLaserPool.GetObject();
            laser2.transform.position = laserPoints[1].transform.position;
            laser2.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            laser2.SetActive(true);

            GameObject laser3 = enemyLaserPool.GetObject();
            laser3.transform.position = laserPoints[2].transform.position;
            laser3.transform.rotation = Quaternion.Euler(0f, 0f, -120f);
            laser3.SetActive(true);

            GameObject laser4 = enemyLaserPool.GetObject();
            laser4.transform.position = laserPoints[3].transform.position;
            laser4.transform.rotation = Quaternion.Euler(0f, 0f, -60f);
            laser4.SetActive(true);

            AudioManager.instance.PlaySoundWithIndex("EnemyLaser", 2);

            canFire = false;
            StartCoroutine(WaitForFireRate());
        }
    }


}
