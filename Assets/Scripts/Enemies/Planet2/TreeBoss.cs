using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : Boss {

    [Header("Attack Objects")]
    [SerializeField] private Transform _fireballPosition;
    [SerializeField] private ObjectPool _fireballPool;

    [Space(20)]
    [SerializeField] private ObjectPool _batPool;
    [SerializeField] private Transform _batPosition;

    private float _time = 0f;



    protected override void Start() {
        base.Start();

        _time = attackSpeed - 3f;
    }


    private void Update() {
        if (canAttack) {
            _time += Time.deltaTime;

            if (_time >= attackSpeed && health > 0f) {
                int r = RandomAttack();
                if (r == 0) BatAttack();
                else anim.Play("FireballAttack", 0, 0f);

                _time = 0f;
            }
        }

    }


    private void FireballAttack() {
        GameObject fireball = _fireballPool.GetObject();
        fireball.transform.position = _fireballPosition.position;
        fireball.SetActive(true);
    }


    private void BatAttack() {
        StartCoroutine(batSpawn());
    }


    private IEnumerator batSpawn() {
        int index = 0, howMany = 5;
        WaitForSeconds waitFor = new WaitForSeconds(0.5f);
        do {
            GameObject bat = _batPool.GetObject();
            bat.transform.position = _batPosition.position;
            bat.transform.position += new Vector3(0f, Random.Range(-.35f, .35f));
            BatEnemy b = bat.GetComponent<BatEnemy>();
            b.player = GameManager.instance.player;
            b.attackDistance = Random.Range(1.5f, 2.5f);
            b.canMove = true;
            if (b.dead) b.Revive();
            bat.SetActive(true);
            index++;
            yield return waitFor;
        }
        while (index < howMany);

    }


    public void KillSpawnedEnemies() {
        Transform pool = _batPool.transform;
        for (int i = 0; i < pool.childCount; i++) {
            if (!pool.GetChild(i).gameObject.activeSelf) continue;

            pool.GetChild(i).GetComponent<Enemy>().TakeDamage(5000);
        }
    }


    private void PlayIdle() {
        anim.Play("Idle", 0, 0f);
    }

}
