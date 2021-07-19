using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraBossSpawner : EnemySpawner {

    [Header("---------------------------------------------")]
    [Space(10f)]
    [SerializeField] private ObjectPool _enemyRobotExplosionPool;
    [SerializeField] private ObjectPool _scorpionBulletPool;
    [SerializeField] private Vector2 _mudMonsterSpawnPosition;



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
        else if (e is MudMonster) {
            MudMonster mudMonster = (MudMonster)e;
            e.transform.position = new Vector2(Random.Range(_mudMonsterSpawnPosition.x, _mudMonsterSpawnPosition.y), e.StartPos.y);
            if (mudMonster.dead) mudMonster.Revive();
        }
        else if (e is Scorpion) {
            Scorpion scorpion = (Scorpion)e;
            scorpion.bulletPool = _scorpionBulletPool;
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
