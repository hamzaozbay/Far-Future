using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Enemy {

    public ObjectPool bombPool, explosionPool;
    public Transform wayPoint1, wayPoint2;

    [SerializeField] private LaunchArcRenderer _arcRenderer;
    [SerializeField] private float _maxRange = 10f;
    [SerializeField] private float _throwAngle = 53f;

    private float _distance, _height;
    private Vector2 _activeTarget;



    protected override void Start() {
        base.Start();

        ChooseTarget();
        Idle();

        if (transform.parent.parent != null)
            enemyPool = transform.parent.parent.GetComponent<ObjectPool>();
    }


    private void Update() {
        if (!dead && canMove) {
            Move();
        }

    }

    private void Move() {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, _activeTarget, speed * Time.deltaTime);

        if (transform.localPosition.x == _activeTarget.x) {
            if (CalculateDistance()) {
                canMove = false;
                anim.Play("idle", 0, 0f);
                ChooseTarget();
                RotateToPlayer();
                anim.Play("throw", 0, 0f);
            }
            else {
                canMove = false;
                Idle();
                ChooseTarget();
            }

        }
    }


    private bool CalculateDistance() {
        _distance = Mathf.Abs(player.transform.position.x - transform.position.x) + 1f;
        _height = transform.position.y - (-1.75f);

        if (_distance < 1.5f || _distance > _maxRange)
            return false;

        return true;
    }


    private void Throw() {
        GameObject bomb = bombPool.GetObject();
        Bomb b = bomb.GetComponent<Bomb>();
        b.bombPool = bombPool;
        b.explosionPool = explosionPool;
        b.CreateWaypoints(_arcRenderer.TakePoints(_distance, _height, _throwAngle));
        bomb.SetActive(true);
    }


    public void ChooseTarget() {
        _activeTarget = new Vector2(Random.Range(wayPoint1.localPosition.x, wayPoint2.localPosition.x), this.transform.localPosition.y);
    }



    private void Idle() {
        anim.Play("idle", 0, 0f);
        StartCoroutine(canWalk());
    }

    private IEnumerator canWalk() {
        yield return new WaitForSeconds(.5f);
        Walk();
    }

    private void Walk() {
        if (_activeTarget.x > transform.localPosition.x) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (_activeTarget.x < transform.localPosition.x) {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        canMove = true;
        anim.Play("walk", 0, 0f);
    }

}
