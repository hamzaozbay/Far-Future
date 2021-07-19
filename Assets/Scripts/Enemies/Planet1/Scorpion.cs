using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Enemy {

    public ObjectPool bulletPool;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    private float _time;
    private bool _canWalk = true;



    protected override void Start() {
        base.Start();

        _time = _fireRate - 0.5f;
        anim.Play("Idle", 0, 0f);
    }


    private void Update() {
        if (canMove && !dead) {
            _time += Time.deltaTime;

            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) > 8f && _canWalk) {
                Walk();
            }
            else if (_time > _fireRate) {
                Attack();
                _time = 0f;
            }
            else {
                Idle();
            }
        }

    }

    private void Walk() {
        transform.position += transform.right * speed * Time.deltaTime;
        if (anim.GetBool("Walk")) return;

        anim.SetBool("Idle", false);
        anim.SetBool("Walk", true);
    }

    private void Attack() {
        RotateToPlayer();
        anim.SetBool("Walk", false);
        anim.SetTrigger("Attack");
        _canWalk = false;
    }

    private void Idle() {
        if (anim.GetBool("Idle")) return;

        anim.SetBool("Walk", false);
        anim.SetBool("Idle", true);
    }

    

    private void CanWalkTrue() {
        _canWalk = true;
    }

    private void OnEnable() {
        if (anim != null) {
            CanWalkTrue();
        }
    }

    public void Fire() {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = _firePoint.transform.position;
        bullet.transform.rotation = _firePoint.transform.rotation;
        bullet.gameObject.SetActive(true);
    }

}
