using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public bool dead = false;
    public bool canMove = false;

    [SerializeField] protected float speed;
    [SerializeField] protected bool canTakeDamage = true;
    [SerializeField] protected bool DestroyOnPlayerTouch = false;
    [SerializeField] protected int health;
    [SerializeField] protected Color puffColor;
    [SerializeField] protected Vector2 startPos;

    [Space(30f)]
    [SerializeField] protected UnityEvent moveAction;
    [SerializeField] protected UnityEvent deathAction;

    protected Animator anim;
    protected ObjectPool puffPool;
    protected int baseHealth;
    protected ObjectPool enemyPool;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;
    protected List<Collider2D> colliders = new List<Collider2D>();

    private bool _initialized = false;


    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        enemyPool = this.transform.parent.GetComponent<ObjectPool>();

        Collider2D[] colls = GetComponents<Collider2D>();
        if (colls != null) colliders.AddRange(colls);
    }


    protected virtual void Start() {
        _initialized = true;

        baseHealth = health;
        startPos = this.transform.position;

        puffPool = GameManager.instance.PuffPool;
    }


    public void TakeDamage(int damage) {
        health -= damage;

        if (health > 0 && !dead) {
            StartCoroutine(ColorChange());
        }
        else if (health <= 0 && !dead) {
            Death();
        }
    }


    protected void Death() {
        canMove = false;
        dead = true;
        rb.isKinematic = true;
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }

        if (puffPool != null) {
            GameObject puff = puffPool.GetObject();
            puff.transform.position = this.transform.position;
            puff.GetComponent<PuffExplosion>().TintColor = this.puffColor;
            puff.gameObject.SetActive(true);
        }
        deathAction.Invoke();

        this.gameObject.SetActive(false);
    }

    public virtual void Revive() {
        foreach (Collider2D collider in colliders) {
            collider.enabled = true;
        }
        health = baseHealth;
        sprite.color = Color.white;
        rb.isKinematic = false;
        dead = false;
        this.gameObject.SetActive(true);
        moveAction.Invoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (DestroyOnPlayerTouch) {
                TakeDamage(health);
            }
        }
        else if (other.gameObject.CompareTag("Laser") && canTakeDamage) {
            if (!dead) {
                TakeDamage(10);
            }
        }
        else if (other.gameObject.CompareTag("Fall") || other.gameObject.CompareTag("DestroyEnemy")) {
            if (this.gameObject.activeSelf) {
                this.gameObject.SetActive(false);
            }
        }
    }


    protected virtual void OnDisable() {
        if (!_initialized || enemyPool == null) return;

        enemyPool.ReturnObject(this.gameObject);
    }



    protected void RotateToPlayer() {
        if (player.transform.position.x < transform.position.x) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (player.transform.position.x > transform.position.x) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    public void CanMoveTrue() {
        StartCoroutine(canMoveTrueRandomTime());
    }

    private IEnumerator canMoveTrueRandomTime() {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        canMove = true;
    }

    protected IEnumerator ColorChange() {
        sprite.color = new Color(1f, 0.3f, 0.3f, 1f);
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;
    }


    public void DestroyItsef() {
        TakeDamage(health);
    }


    public Vector2 StartPos { get { return startPos; } }
    public int BaseHealth { get { return this.baseHealth; } }
    public Animator Anim { get { return anim; } }
    public int Health { get { return health; } }
    public UnityEvent MoveAction { get { return moveAction; } }

}
