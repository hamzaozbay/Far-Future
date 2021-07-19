using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GorillaBoss : Boss {

    [SerializeField] private ObjectPool _laserPool;
    [SerializeField] private Transform _laserPosition;
    [SerializeField] private ObjectPool _rockPool;
    [SerializeField] private Transform _rockPosition;

    private float _time;



    protected override void Start() {
        base.Start();

        _time = attackSpeed - 3f;
    }



    private void Update() {
        if (canAttack) {
            _time += Time.deltaTime;

            if (_time >= attackSpeed && health > 0f) {
                int r = RandomAttack();
                if (r == 0) anim.Play("LaserAttack", 0, 0f);
                else anim.Play("RockAttack", 0, 0f);
                _time = 0f;
            }
        }

    }


    private void PlayIdle() {
        anim.Play("Idle", 0, 0f);
    }


    private void LaserAttack() {
        GorillaBossLaser laser = _laserPool.GetObject().GetComponent<GorillaBossLaser>();
        laser.gameObject.transform.position = _laserPosition.position;
        laser.LookAtPlayer();
        laser.gameObject.SetActive(true);
    }

    private void RockAttack() {
        GameObject rock = _rockPool.GetObject();
        rock.transform.position = _rockPosition.position;
        Enemy e = rock.GetComponent<Enemy>();
        e.Revive();
        rock.SetActive(true);
    }



}
