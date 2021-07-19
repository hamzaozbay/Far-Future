using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour {

    public float speed;
    
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _gravitySpeed;
    [SerializeField] private ObjectPool _laserPool;
    [SerializeField] private Transform _laserPoint1, _laserPoint2;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private int _health = 5;
    [SerializeField] private SpriteRenderer _rocketFireSpriteRenderer;
    [SerializeField] private Transform _rocketFireTransform;
    [SerializeField] private SpaceshipHealthbar _healthbar;
    [SerializeField] private ObjectPool _explosionPool;
    [SerializeField] private UnityEvent _onPlayerDied;
    [SerializeField] private bool _godMode = false;

    private SpriteRenderer _spriteRenderer;
    private bool _canFire = true, _fireButtonDown = false, _canMoveForward = true;
    private float _fireTime;
    private bool _moveForward = false;
    private bool _gravity = true;
    private DirectionButton _leftButton, _rightButton;
    private WaitForSeconds _waitForFireRate;



    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _leftButton = GameManager.instance.leftButton;
        _rightButton = GameManager.instance.rightButton;
        GameManager.instance.player = this.gameObject;
        _waitForFireRate = new WaitForSeconds(_fireRate);
        _health = GameManager.instance.spaceshipHealth;
    }


    private void Update() {
        if (_health > 0) {
            Move();
            Fire();
            MoveForward();
        }

    }

    public void Move() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8f, 8f), transform.position.y, transform.position.z);

        transform.position += new Vector3((_leftButton.Direction + _rightButton.Direction +
        Input.GetAxisRaw("Horizontal")) * speed * Time.deltaTime, 0f);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy") && !_godMode) {
            TakeDamage();
        }
    }


    private void TakeDamage() {
        _health--;
        GameManager.instance.spaceshipHealth = _health;
        AudioManager.instance.PlayRandomSound("HitHurt");
        _healthbar.UpdateHealthbar();
        StartCoroutine(Damage());

        if (_health <= 0) {
            Dead();
        }
    }

    private void Dead() {
        GameObject explosion = _explosionPool.GetObject();
        explosion.transform.position = this.transform.position;
        explosion.gameObject.SetActive(true);

        _spriteRenderer.enabled = false;
        _rocketFireSpriteRenderer.enabled = false;
        StartCoroutine(pauseGame());
    }

    private IEnumerator pauseGame() {
        yield return new WaitForSecondsRealtime(1f);
        _onPlayerDied.Invoke();
        GameManager.instance.PauseGame();
    }

    private IEnumerator Damage() {
        WaitForSeconds waitFor = new WaitForSeconds(.15f);
        int a = 0;
        this.gameObject.layer = 16;

        while (a < 10) {
            a++;
            yield return waitFor;

            if (a % 2 == 1) {
                _spriteRenderer.color = new Color(1f, 0f, 0f, .2f);
                _rocketFireSpriteRenderer.color = new Color(1f, 0f, 0f, .2f);
            }
            else {
                _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                _rocketFireSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        this.gameObject.layer = 9;
    }


    private void Fire() {
        if (_fireButtonDown && _canFire) {
            Shoot();
        }
    }

    private void Shoot() {
        GameObject laser = _laserPool.GetObject();
        laser.transform.position = _laserPoint1.transform.position;
        laser.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        laser.SetActive(true);

        GameObject laser2 = _laserPool.GetObject();
        laser2.transform.position = _laserPoint2.transform.position;
        laser2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        laser2.SetActive(true);

        AudioManager.instance.PlaySound("LaserShoot");

        _canFire = false;
        StartCoroutine(fireCoroutine());
    }

    private IEnumerator fireCoroutine() {
        yield return _waitForFireRate;
        _canFire = true;
    }


    public void FireButton() {
        if (!GameManager.instance.isGamePaused) {
            _fireButtonDown = !_fireButtonDown;
        }
    }


    public void ForwardBool() {
        _moveForward = !_moveForward;
        _gravity = !_gravity;
    }

    private void MoveForward() {
        if (_gravity) {
            if (transform.position.y > GameManager.instance.ScreenBottom.y + 4f) {
                transform.position -= new Vector3(0f, _gravitySpeed * Time.deltaTime);
            }
        }
        if (_moveForward && transform.position.y < GameManager.instance.ScreenTop.y - 2f && _canMoveForward) {
            transform.position += new Vector3(0f, _forwardSpeed * Time.deltaTime);
        }
    }


    public void RocketFireBig() {
        if (_canMoveForward) {
            _rocketFireTransform.transform.localScale = new Vector3(1f, 1f, 1f);
            _rocketFireTransform.transform.localPosition = new Vector3(0f, -.85f, 0f);
        }

    }

    public void RocketFireSmall() {
        if (_canMoveForward) {
            _rocketFireTransform.transform.localScale = new Vector3(1f, .6f, 1f);
            _rocketFireTransform.transform.localPosition = new Vector3(0f, -.71f, 0f);
        }
    }

    public void CanFireFalse() {
        StopAllCoroutines();
        _canFire = false;
    }

    public void CanMoveForwardFalse() {
        _canMoveForward = false;
    }


}
