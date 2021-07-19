using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy {

    [SerializeField] private float _jumpHeight = 8f;
    private bool _grounded = false;



    private void Update() {
        if (canMove && !dead && _grounded) {
            anim.Play("Move", 0);
            _grounded = false;
        }
    }

    private void Jump() {
        RotateToPlayer();
        rb.AddForce((transform.right * speed) + transform.up * _jumpHeight, ForceMode2D.Impulse);
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Ground")) {
            _grounded = true;
        }
    }
}
