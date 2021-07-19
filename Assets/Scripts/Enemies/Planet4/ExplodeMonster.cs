using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMonster : Enemy {

    public ObjectPool explosionPool;
    [SerializeField] private float explodeDistance;


    private void Update() {
        if (canMove && !dead) {
            this.transform.position += transform.right * speed * Time.deltaTime;

            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < explodeDistance) {
                Explode();
            }
        }

    }


    private void Explode() {
        canMove = false;
        anim.Play("Explode", 0);
    }


    public void WakeUp() {
        RotateToPlayer();
        anim.Play("WakeUp", 0);
    }

    private void PlayWalk() {
        anim.Play("Walk", 0);
        canMove = true;
    }


    private void ExplodeAction() {
        GameObject explosion = explosionPool.GetObject();
        explosion.transform.position = this.transform.position;
        explosion.SetActive(true);
        TakeDamage(health);
    }


    public override void Revive() {
        base.Revive();

        rb.isKinematic = true;
    }

}
