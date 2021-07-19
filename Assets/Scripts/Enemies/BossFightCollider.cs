using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossFightCollider : MonoBehaviour {

    [SerializeField] private UnityEvent _OnBossFightStart;
    [SerializeField] private CameraController _cam;
    [SerializeField] private Transform _bossFightCameraTarget;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Boss _boss;

    private BoxCollider2D _coll;
    private PlayerMovement _player;



    private void Start() {
        _coll = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (other.gameObject.transform.position.x > transform.position.x) {
                _OnBossFightStart.Invoke();
                GameManager.instance.lastCheckPoint = new Vector3(transform.position.x - 1f, transform.position.y, 0f);
            }
        }
    }



    public void BossFightStart() {
        _player = GameManager.instance.player.GetComponent<PlayerMovement>();
        _coll.isTrigger = false;
        _cam.smooth = 0.5f;
        _cam.target = _bossFightCameraTarget;
        StartCoroutine(stopPlayer());

        _enemySpawner.gameObject.SetActive(true);
        _enemySpawner.canSpawn = true;
        _boss.canAttack = true;
    }

    private IEnumerator stopPlayer() {
        _player.PlayerFunctionsOff();
        yield return new WaitForSeconds(2.5f);
        _player.PlayerFunctionsOn();
        _healthBar.SetActive(true);
    }

}
