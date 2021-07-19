using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBoss : Boss {

    [SerializeField] private Transform _crabBombPosition;
    [SerializeField] private ObjectPool _crabBombPool;
    [SerializeField] private Transform _bombPuffPosition;
    [SerializeField] private ObjectPool _bombPuffPool;
    [SerializeField] private ObjectPool _bombExplosionPool;
    [SerializeField] private ObjectPool _clawPool;
    [SerializeField] private Transform _clawPosition;

    private float _time = 0f;



    protected override void Start() {
        base.Start();

        _time = attackSpeed - 3f;
    }



    private void Update() {
        if (canAttack) {
            _time += Time.deltaTime;

            if (_time >= attackSpeed && health > 0f) {
                int r = RandomAttack();
                if (r == 0) anim.Play("DropClaw", 0, 0f);
                else anim.Play("BombAttack", 0, 0f);
                _time = 0f;
            }
        }

    }


    private void BombAttack() {
        GameObject bomb = _crabBombPool.GetObject();

        CrabBossBomb b = bomb.GetComponent<CrabBossBomb>();
        if (b.explosionPool == null) b.explosionPool = _bombExplosionPool;

        bomb.transform.position = _crabBombPosition.position;
        bomb.transform.up = _crabBombPosition.up;

        GameObject puff = _bombPuffPool.GetObject();
        puff.transform.position = _bombPuffPosition.position;

        puff.SetActive(true);
        bomb.SetActive(true);
    }


    private void ClawAttack() {
        CrabClaw claw = _clawPool.GetObject().GetComponent<CrabClaw>();
        claw.transform.position = _clawPosition.position;
        claw.gameObject.SetActive(true);
    }


    public void RegenarateClawAnimationSpeed() {
        if (health == 50 || health == 30) {
            anim.SetFloat("regenarateSpeed", anim.GetFloat("regenarateSpeed") * 1.35f);
        }
    }

}
