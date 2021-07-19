using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterWakeUp : MonoBehaviour {

    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    private bool _isTriggered = false;



    private void Start() {
        CheckLastCheckpoint();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!_isTriggered) {
                _isTriggered = true;
                foreach (Enemy e in _enemies) {
                    if (e == null) continue;

                    e.player = GameManager.instance.player;
                    e.gameObject.SetActive(true);
                    e.MoveAction.Invoke();
                }
            }

        }

    }

    public void ResetTrigger() {
        _isTriggered = false;
    }


    private void CheckLastCheckpoint() {
        if (GameManager.instance.lastCheckPoint.x > transform.position.x) {
            foreach (Enemy e in _enemies) {
                if (!e.gameObject.activeSelf) continue;

                e.gameObject.SetActive(false);
            }

            this.gameObject.SetActive(false);
        }
    }



    public List<Enemy> Enemies { get { return _enemies; } }

}
