using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstreoidSpawner : EnemySpawner {

    [Header("---------------------------------------------")]
    [Space(10f)]
    [SerializeField] private ObjectPool[] _explosionPools;


    protected override void SetEnemy(GameObject enemy) {
        base.SetEnemy(enemy);

        Vector2 targetPos = new Vector2(player.transform.position.x + Random.Range(-2f, 2f), GameManager.instance.ScreenTop.y + 2f);
        targetPos = new Vector2(Mathf.Clamp(targetPos.x, -7f, 7f), targetPos.y);
        enemy.transform.position = targetPos;

        Astreoid astreoid = enemy.GetComponent<Astreoid>();
        astreoid.SetAstreoid();
        astreoid.explosionPools = _explosionPools;

        if (!enemy.activeSelf) enemy.SetActive(true);
    }


}
