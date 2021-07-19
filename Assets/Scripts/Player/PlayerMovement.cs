using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    [SerializeField] private float _baseSpeed;
    [SerializeField] private int _health = 3;
    [SerializeField] private PlayerHealthbar _healthbar;

    [Space(30f)]
    [SerializeField] private UnityEvent _onPlayerDied;
    [SerializeField] private bool _godMode = false;


    private bool _jump = false;
    private bool _canJump = false;
    private bool _firstTimeDoubleJump = true;
    private bool _takeDamage = false;
    private CharacterController2D _controller;
    private float _horizontalMove = 0f;
    private bool _canMove = true;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private DirectionButton _rightButton, _leftButton;
    private Vector3 _startPos;



    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        GameManager.instance.player = this.gameObject;
    }



    private void Start() {
        _startPos = transform.position;
        _rightButton = GameManager.instance.rightButton;
        _leftButton = GameManager.instance.leftButton;
        speed = _baseSpeed;
        _health = GameManager.instance.playerHealth;

        if (GameManager.instance.lastCheckPoint != Vector3.zero) {
            this.transform.position = GameManager.instance.lastCheckPoint;
        }

        GameManager.instance.SetCameraPosition(this.transform.position);
    }


    private void Update() {
        if (!GameManager.instance.isGamePaused && _canMove) {
            if (!_takeDamage) {
                _horizontalMove = (_rightButton.Direction + _leftButton.Direction
                + Input.GetAxisRaw("Horizontal")) * speed;
            }
            else {
                _horizontalMove = 0f;
            }

            _anim.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        }

    }

    private void FixedUpdate() {
        if (!_takeDamage) {
            _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        }
    }


    public void OnLand() {
        _anim.SetBool("isJumping", false);
        _canJump = true;
        _jump = false;
        _firstTimeDoubleJump = true;
    }

    public void Jump() {
        if (!GameManager.instance.isGamePaused && _canMove) {
            if (_canJump) {
                _jump = true;
                _canJump = false;
                _anim.SetBool("isJumping", true);

                AudioManager.instance.PlaySound("Jump");
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Fall")) {
            TakeDamage();

            if (_health > 0) {
                GameManager.instance.FallReloadScene();
            }
        }
        else if (other.gameObject.CompareTag("JumpAgain")) {
            if (_firstTimeDoubleJump) {
                _controller.m_Rigidbody2D.velocity = new Vector2(_controller.m_Rigidbody2D.velocity.x, 0f);
                _controller.m_Rigidbody2D.AddForce(new Vector2(0f, _controller.m_JumpForce));
                _firstTimeDoubleJump = false;
            }

        }
        else if (other.gameObject.CompareTag("CheckPoint")) {
            Animator checkPointAnim = other.gameObject.GetComponent<Animator>();

            if (checkPointAnim.GetBool("Activated") == false) {
                GameManager.instance.lastCheckPoint = checkPointAnim.gameObject.transform.position + Vector3.up * 2f;
                checkPointAnim.SetBool("Activated", true);
            }
        }
        else if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Laser")) && !_takeDamage && !_godMode) {
            _takeDamage = true;

            if (_anim.GetBool("isJumping")) _anim.SetBool("isJumping", false);

            _controller.m_Rigidbody2D.velocity = Vector2.zero;
            _controller.m_Rigidbody2D.AddForce(transform.up * 9f, ForceMode2D.Impulse);
            StartCoroutine(DamageColorChange());

            TakeDamage();
        }

    }

    private IEnumerator DamageColorChange() {
        WaitForSeconds waitFor = new WaitForSeconds(.15f);
        int a = 0;
        this.gameObject.layer = 9; //PlayerGhost layer
        _takeDamage = false;

        while (a < 16) {
            a++;
            yield return waitFor;

            if (a % 2 == 1) {
                _spriteRenderer.color = new Color(1f, 0f, 0f, .2f);
            }
            else {
                _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        this.gameObject.layer = 8; //Player layer
    }

    private void TakeDamage() {
        _health--;
        GameManager.instance.playerHealth = _health;
        AudioManager.instance.PlayRandomSound("HitHurt");
        _healthbar.UpdateHealthbar();

        if (_health <= 0) {
            Dead();
        }
    }

    private void Dead() {
        GameObject puff = GameManager.instance.PuffPool.GetObject();
        puff.transform.position = this.transform.position;
        puff.GetComponent<PuffExplosion>().TintColor = Color.white;
        puff.gameObject.SetActive(true);

        StartCoroutine(pauseGame());
        PlayerFunctionsOff();
        _spriteRenderer.enabled = false;
    }

    private IEnumerator pauseGame() {
        yield return new WaitForSecondsRealtime(1f);
        _onPlayerDied.Invoke();
        GameManager.instance.PauseGame();
    }




    public void PlayerFunctionsOff() {
        _canMove = false;
        Weapon weapon = GetComponent<Weapon>();
        weapon.CanFireFalse();
        _horizontalMove = 0f;
        _anim.SetFloat("Speed", 0f);
        this.gameObject.layer = 9; //PlayerGhost layer
    }

    public void PlayerFunctionsOn() {
        _canMove = true;
        Weapon weapon = GetComponent<Weapon>();
        weapon.CanFireTrue();
        this.gameObject.layer = 8; //Player layer
    }

    public void StopPlayer() {
        _canMove = false;
        _horizontalMove = 0f;
        _anim.SetFloat("Speed", 0f);
    }


    public float BaseSpeed { get { return _baseSpeed; } }


}
