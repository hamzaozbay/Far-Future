using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy {

    public float attackDistance = 2.5f;
    [SerializeField] private float _attackMovementSpeed = 6.5f;

    private bool _attacked = false;
    private float _baseSpeed;


    protected override void Start() {
        base.Start();

        anim.Play("Fly", 0, 0f);
        _baseSpeed = speed;
    }


    private void Update() {
        if (canMove && !dead) {
            transform.position += transform.right * speed * Time.deltaTime;

            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < attackDistance && !_attacked) {
                Attack();
            }
        }
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            TakeDamage(health);
        }

    }


    private void Attack() {
        anim.Play("Attack", 0, 0f);

        transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, -67.5f);

        speed = _attackMovementSpeed;
        _attacked = true;
    }


    public override void Revive() {
        base.Revive();

        _attacked = false;
        canMove = true;
        transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, 0f);
        speed = _baseSpeed;
        anim.Play("Fly", 0, 0f);
    }

}
