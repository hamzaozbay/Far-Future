using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitEnemy : Enemy {

    [SerializeField] private Transform _bulletPlace;
    [SerializeField] private ObjectPool _spitEnemyBulletPool;



    public void WakeUp() {
        anim.Play("WakeUp", 0, 0f);
    }
    private IEnumerator Idle() {
        anim.Play("Idle", 0, 0f);
        yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
        RotateToPlayer();
        anim.Play("Attack", 0, 0f);
    }
    private IEnumerator Sleep() {
        anim.Play("Idle", 0, 0f);
        yield return new WaitForSeconds(Random.Range(0.75f, 1.75f));
        anim.Play("Sleep", 0, 0f);
    }
    private IEnumerator SleeptoWakeUp() {
        yield return new WaitForSeconds(Random.Range(2f, 3.5f));
        WakeUp();
    }


    public void Fire() {
        GameObject bullet = _spitEnemyBulletPool.GetObject();
        bullet.transform.position = _bulletPlace.transform.position;
        bullet.transform.rotation = _bulletPlace.transform.rotation;

        bullet.gameObject.SetActive(true);
    }


    private void EnemyGhostLayer() {
        gameObject.layer = 12; //EnemyGhost layer
    }
    private void EnemyLayer() {
        gameObject.layer = 11; //Enemy layer
    }
}
