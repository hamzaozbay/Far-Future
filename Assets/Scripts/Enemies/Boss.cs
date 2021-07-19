using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Boss : MonoBehaviour {
    
    public bool canAttack = false;

    [SerializeField] protected float health;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected float attackSpeed = 2f;
    [SerializeField] protected UnityEvent onHitByLaser;
    [SerializeField] protected UnityEvent onDead;
    [SerializeField] protected EnemySpawner enemySpawner;

    protected Collider2D bossCollider;
    protected Animator anim;
    protected float firstAttackChance = 1f, secondAttackChance = 2f;


    private void Awake() {
        anim = this.GetComponent<Animator>();
        bossCollider = GetComponent<Collider2D>();
    }


    protected virtual void Start() {
    }



    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser")) {
            onHitByLaser.Invoke();
            if (health <= 0f) {
                bossCollider.enabled = false;
                onDead.Invoke();
            }
        }
    }

    protected int RandomAttack() {
        float random = Random.Range(0f, 2f);

        if (random >= 0f && random <= firstAttackChance) {
            firstAttackChance -= .25f;
            return 0;
        }
        else if (random >= firstAttackChance && random <= secondAttackChance) {
            firstAttackChance += .25f;
            return 1;
        }

        return -1;
    }


    public void AttackSpeedChange() {
        if (health == 70 || health == 50 || health == 30) {
            enemySpawner.spawnRateTime *= .9f;
            attackSpeed *= 0.75f;
        }
    }


    public void TakeDamage(float dmg) {
        health -= dmg;
    }

    public void UpdateHealthBar() {
        healthBar.fillAmount = health / 100f;
    }

    public float GetHealth() {
        return health;
    }

    public void Dead() {
        anim.Play("Dead", 0, 0f);
        healthBar.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void DisableObject() {
        this.gameObject.SetActive(false);
    }

}
