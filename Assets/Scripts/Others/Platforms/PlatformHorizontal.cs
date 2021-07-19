using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformHorizontal : MonoBehaviour {

    [SerializeField] protected float speed = 2f;
    [Range(0f, 2.5f)] [SerializeField] protected float playerSpeedFactor = 1.5f;
    [SerializeField] protected Transform pointA, pointB;

    private PlayerMovement _playerMovement = null;
    private bool _canChangeDirection = true;
    private WaitForSeconds waitForSeconds;


    private void Start() {
        waitForSeconds = new WaitForSeconds(0.5f);
    }



    private void Update() {
        transform.position += new Vector3(speed * Time.deltaTime, 0f);

        ChangeDirection();
    }


    private void ChangeDirection() {
        if (_canChangeDirection && (transform.localPosition.x < pointA.localPosition.x || transform.localPosition.x > pointB.localPosition.x)) {
            speed *= -1;
            _canChangeDirection = false;
            StartCoroutine(CanChangeDirectionWait());
        }
    }
    private IEnumerator CanChangeDirectionWait() {
        yield return waitForSeconds;
        _canChangeDirection = true;
    }


    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (_playerMovement == null) _playerMovement = GameManager.instance.player.GetComponent<PlayerMovement>();

            other.gameObject.transform.parent = this.gameObject.transform;
            _playerMovement.speed *= playerSpeedFactor;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.transform.parent = null;
            _playerMovement.speed = _playerMovement.BaseSpeed;
        }
    }

}
