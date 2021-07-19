using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossSpawner : EnemySpawner {

    [Header("---------------------------------------------")]
    [Space(10f)]
    [SerializeField] private ObjectPool _enemyRobotExplosionPool;



    protected override void SetEnemy(GameObject enemy) {
        base.SetEnemy(enemy);
        Enemy e = enemy.GetComponent<Enemy>();
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
        if (e is EnemyRobot) {
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
            if (e != null) e.TakeDamage(e.Health);
        }
    }


}
