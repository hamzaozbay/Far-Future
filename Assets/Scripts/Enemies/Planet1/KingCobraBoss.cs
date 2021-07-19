using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCobraBoss : Boss {

    [SerializeField] private LaunchArcRenderer _arcCalculater;
    [SerializeField] private ObjectPool _posionPool;
    [SerializeField] private float _angle = 7f;
    [SerializeField] private float _maxRange = 14f;
    [SerializeField] private ObjectPool _tornadoPool;
    [SerializeField] private Transform _tornadoPosition;

    private float _time = 0f;
    private float _distance, _height;


    protected override void Start() {
        base.Start();

        _time = attackSpeed - 3f;
    }



    private void Update() {
        if (canAttack) {
            _time += Time.deltaTime;

            if (_time >= attackSpeed && health > 0f) {
                int r = RandomAttack();
                if (r == 0) TornadoAttackAnimation();
                else Attack();

                _time = 0f;
            }
        }

    }

    private void Attack() {
        if (CalculateDistance()) {
            anim.Play("Attack", 0, 0f);
        }
    }
    private bool CalculateDistance() {
        _distance = Mathf.Abs(GameManager.instance.player.transform.position.x - transform.position.x) - 2f; //deneyerek bulundu.
        _height = transform.position.y + (2.75f); //deneyerek bulundu.

        if (_distance < 2f || _distance > _maxRange) return false;

        return true;
    }
    private void PosionAttack() {
        GameObject poisonGameObject = _posionPool.GetObject();
        BossPoison poison = poisonGameObject.GetComponent<BossPoison>();
        poison.wayPoints = _arcCalculater.TakePoints(_distance, _height, _angle);
        poisonGameObject.transform.position = poison.wayPoints[0];
        poisonGameObject.SetActive(true);
    }


    private void TornadoAttack() {
        GameObject tornado = _tornadoPool.GetObject();
        tornado.transform.position = _tornadoPosition.position;
        Enemy e = tornado.GetComponent<Enemy>();
        tornado.SetActive(true);
        e.canMove = true;
    }
    private void TornadoAttackAnimation() {
        anim.Play("AttackTornado", 0, 0f);
    }


    private void PlayIdle() {
        anim.Play("Idle", 0, 0f);
    }



    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3((this.transform.position.x - _maxRange) - 2f, this.transform.position.y - 2f), .5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3((this.transform.position.x - 2f) - 2f, this.transform.position.y - 2f), .5f);
    }

}
