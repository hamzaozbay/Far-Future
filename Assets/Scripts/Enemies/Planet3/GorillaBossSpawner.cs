using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaBossSpawner : EnemySpawner {

    [Header("---------------------------------------------")]
    [Space(10f)]
    [SerializeField] private ObjectPool _monkeyBombPool;
    [SerializeField] private ObjectPool _monkeyBombExplosionPool;
    [SerializeField] private Vector2 _monkeySpawnPosition;

    [Space(10f)]
    [SerializeField] private ObjectPool _shootingPlantBulletPool;
    [SerializeField] private Vector2 _shootingPlantSpawnPosition;

    [Space(10f)]
    [SerializeField] private ObjectPool _enemyRobotExplosionPool;



    protected override void SetEnemy(GameObject enemy) {
        base.SetEnemy(enemy);
        Enemy e = enemy.GetComponent<Enemy>();
        if (e == null) e = enemy.transform.GetChild(0).GetComponent<Enemy>();

        enemy.transform.position = new Vector2(this.transform.position.x, e.StartPos.y);

        SetEnemyWithType(e);

        e.player = GameManager.instance.player;
        if (e.dead) {
            e.Revive();
            return;
        }

        if (!enemy.activeSelf) enemy.SetActive(true);
        e.MoveAction.Invoke();
    }


    private void SetEnemyWithType(Enemy e) {
        if (e is ShootingPlant) {
            ShootingPlant sp = e.GetComponent<ShootingPlant>();
            sp.bulletPool = _shootingPlantBulletPool;
            sp.transform.position = new Vector3(Random.Range((int)_shootingPlantSpawnPosition.x, (int)_shootingPlantSpawnPosition.y), e.StartPos.y);
            if (sp.dead) sp.Revive();
        }
        else if (e is Monkey) {
            Monkey m = e.GetComponent<Monkey>();
            m.bombPool = _monkeyBombPool;
            m.explosionPool = _monkeyBombExplosionPool;
            m.transform.position = new Vector3(this.transform.position.x, -1f);
            m.wayPoint1.position = new Vector3(_monkeySpawnPosition.x - Random.Range(2, 5), 0f);
            m.wayPoint2.position = new Vector3(m.wayPoint1.position.x + Random.Range(3, 6), 0f);
            m.ChooseTarget();
            if (m.dead) m.Revive();
        }
        else if (e is EnemyRobot) {
            EnemyRobot robot = (EnemyRobot)e;
            robot.explosionPool = _enemyRobotExplosionPool;
            if (robot.dead) robot.Revive();
        }
    }

    public override void KillAllEnemies() {
        base.KillAllEnemies();

        foreach (GameObject go in _allEnemies) {
            if (!go.activeSelf) continue;

            Enemy e = go.GetComponent<Enemy>();
            if (e == null) e = go.transform.GetChild(0).GetComponent<Enemy>();

            e.TakeDamage(e.Health);
        }
    }

}
