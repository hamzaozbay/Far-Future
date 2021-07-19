using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : Enemy {

    [SerializeField] private float _attackedSpeed = 6f;
    [SerializeField] private float _attackDistance = 2.5f;
    private bool _attacked = false;
    private Vector3 _target;



    private void Update() {
        if (canMove && !dead) {
            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < _attackDistance && !_attacked) {
                Attack();
            }
            if (_attacked) {
                transform.position += _target.normalized * _attackedSpeed * Time.deltaTime;
            }
            else {
                transform.position += -transform.right * speed * Time.deltaTime;
            }
        }

    }

    private void Attack() {
        if (player.transform.position.y < this.transform.position.y) {
            _target = (player.transform.position - this.transform.position) + new Vector3(Random.Range(0f, 2.5f), 0f);
            _attacked = true;
            anim.Play("Attack", 0, 0f);
        }
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            TakeDamage(health);
            this.gameObject.SetActive(false);
        }

    }

}
