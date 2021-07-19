using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : Enemy {

    [SerializeField] private float _maxAttackTime;
    [SerializeField] private GameObject _jumpHeight;

    private bool _didBite = true;



    private void Update() {
        if(rb.velocity.y <= 0f && !_didBite) {
            Bite();
            _didBite = true;
        }
    }


    private void Attack() {
        _didBite = false;
        float gravity = Physics.gravity.magnitude * rb.gravityScale;
        float height = _jumpHeight.transform.position.y - this.transform.position.y;
        float initialVelocity = CalculateJumpSpeed(height, gravity);
 
        Vector3 direction = new Vector3(0f, 1f);
 
        anim.Play("attack", 0, 0f);
        rb.AddForce(initialVelocity * direction, ForceMode2D.Impulse);
    }

    private void Bite() {
        anim.Play("attackClose", 0, 0f);
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.gameObject.CompareTag("PlantHolder")) {
            Invoke("Attack", ChooseTime());
        }
    }

    private float CalculateJumpSpeed(float jumpHeight, float gravity) {
        return Mathf.Sqrt(2f * jumpHeight * gravity);
    }

    private float ChooseTime() {
        return Random.Range(1f, _maxAttackTime);
    }

}
