using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceshipSpawner : EnemySpawner {

    [Header("---------------------------------------------")]
    [Space(10f)]
    [SerializeField] private ObjectPool _enemySpaceship1LaserPool;
    [SerializeField] private ObjectPool _enemySpaceship2LaserPool;
    [SerializeField] private ObjectPool _enemySpaceship3LaserPool;
    [SerializeField] private ObjectPool _enemySpaceshipExplosionPool;


    protected override void SetEnemy(GameObject enemy) {
        base.SetEnemy(enemy);
        
        enemy.transform.position = new Vector3(player.transform.position.x + Random.Range(-2f, 2f), 10f);
        enemy.transform.position = new Vector3(Mathf.Clamp(enemy.transform.position.x, -7f, 7f), 10f, 0f);

        EnemySpaceship enemySpaceship = enemy.GetComponent<EnemySpaceship>();
        if (enemySpaceship.IsDead()) {
            enemySpaceship.Revive();
        }
        enemySpaceship.player = player;
        enemySpaceship.spaceshipExplosionPool = _enemySpaceshipExplosionPool;

        if (enemySpaceship is EnemySpaceship1) {
            enemySpaceship.enemyLaserPool = _enemySpaceship1LaserPool;
        }
        else if (enemySpaceship is EnemySpaceship2) {
            enemySpaceship.enemyLaserPool = _enemySpaceship2LaserPool;
        }
        else if (enemySpaceship is EnemySpaceship3) {
            enemySpaceship.enemyLaserPool = _enemySpaceship3LaserPool;
        }

        if (!enemy.activeSelf) enemy.SetActive(true);
    }


}
