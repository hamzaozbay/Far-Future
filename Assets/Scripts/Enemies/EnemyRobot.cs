using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : Enemy {

    public ObjectPool explosionPool;

    private Vector2 _contactPoint;



    private void Update() {
        if (canMove && !dead) {
            transform.position -= new Vector3(speed * Time.deltaTime, 0f);

            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < 1f) {
                canMove = false;
                rb.gravityScale = 3f;
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")) {
            ContactPoint2D contact = other.GetContact(0);
            _contactPoint = contact.point;

            TakeDamage(health);
        }
    }


    public void Explosion() {
        if (_contactPoint == Vector2.zero) _contactPoint = this.transform.position;

        GameObject explosion = explosionPool.GetObject();
        explosion.transform.position = _contactPoint;
        explosion.SetActive(true);
    }


    public override void Revive() {
        base.Revive();
        rb.gravityScale = 0f;
    }

}
