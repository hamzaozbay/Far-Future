using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMonster : Enemy {

    private void Update() {
        if (canMove && !dead) {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    public void WakeUp() {
        RotateToPlayer();
        anim.Play("Wakeup", 0, 0f);
    }

    public override void Revive() {
        health = baseHealth;
        sprite.color = Color.white;
        dead = false;
        this.gameObject.SetActive(true);
        moveAction.Invoke();
    }

    private void EnableCollider() {
        foreach (Collider2D collider in colliders) {
            collider.enabled = true;
        }
        canMove = true;
    }


}
