using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceship : MonoBehaviour {

    public GameObject player;
    public ObjectPool enemyLaserPool;
    public ObjectPool spaceshipExplosionPool;

    [SerializeField] protected float speed;
    [SerializeField] protected float time;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected List<Transform> laserPoints;
    [SerializeField] protected bool canFire = false;
    [SerializeField] protected int health;
    [SerializeField] protected bool dead = false;

    protected ObjectPool enemySpaceshipPool;
    protected int baseHealth;
    protected WaitForSeconds waitForFireRate;
    protected WaitForSeconds waitForCanTakeDamage;
    protected bool canTakeDamage = false;

    private bool _initialized = false;


    private void Awake() {
        enemySpaceshipPool = this.transform.parent.GetComponent<ObjectPool>();
    }


    protected virtual void Start() {
        _initialized = true;

        waitForFireRate = new WaitForSeconds(fireRate);
        waitForCanTakeDamage = new WaitForSeconds(1f);

        baseHealth = health;
    }



    protected virtual void Update() {
        if (!dead) {
            transform.position -= new Vector3(0f, speed * Time.deltaTime);
        }

    }




    protected void OnTriggerEnter2D(Collider2D other) {
        if (canTakeDamage && (other.gameObject.CompareTag("Laser") || other.gameObject.CompareTag("Player"))) {
            TakeDamage(10);
        }
        else if (other.gameObject.CompareTag("DestroyEnemy")) {
            canTakeDamage = false;
            this.gameObject.SetActive(false);
        }
    }

    protected void TakeDamage(int dmg) {
        health -= dmg;

        if (health <= 0 && !dead) {
            Death();
        }
    }

    protected void Death() {
        GameObject spaceshipExplosion = spaceshipExplosionPool.GetObject();
        spaceshipExplosion.transform.position = this.transform.position;
        spaceshipExplosion.SetActive(true);
        dead = true;
        StopAllCoroutines();
        canFire = false;
        canTakeDamage = false;

        this.gameObject.SetActive(false);
    }

    public void Revive() {
        dead = false;
        health = baseHealth;
    }

    protected IEnumerator WaitForFireRate() {
        yield return waitForFireRate;
        canFire = true;
    }


    protected void OnBecameVisible() {
        StartCoroutine(EnableThings());
    }

    private IEnumerator EnableThings() {
        yield return waitForCanTakeDamage;
        canFire = true;
        canTakeDamage = true;
    }

    protected void OnDisable() {
        if (!_initialized) return;

        enemySpaceshipPool.ReturnObject(this.gameObject);
    }


    public bool IsDead() {
        return dead;
    }

}
