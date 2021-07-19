using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planet : MonoBehaviour {

    public float stopTime = 10f;

    [SerializeField] private float _speed = 0.125f;
    [SerializeField] private EndLevel _endLevel;
    [SerializeField] private GameObject _player;

    private Camera _cam;
    private bool _canMove = false;
    private PlanetNameUI _planetName;



    private void Start() {
        _cam = Camera.main;
        _canMove = true;
        _planetName = GetComponent<PlanetNameUI>();
    }


    private void Update() {
        if (_canMove) {
            transform.position += -transform.up * _speed * Time.deltaTime;
        }
    }


    private void OnBecameVisible() {
        _player.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 5f).SetEase(Ease.InSine);

        Spaceship playerSpaceship = _player.GetComponent<Spaceship>();
        playerSpaceship.CanFireFalse();
        playerSpaceship.CanMoveForwardFalse();
        DOTween.To(() => playerSpaceship.speed, x => playerSpaceship.speed = x, playerSpaceship.speed / 4f, 5f);

        foreach (Spacebackground bg in _cam.GetComponentsInChildren<Spacebackground>()) {
            DOTween.To(() => bg.speed, x => bg.speed = x, bg.speed / 3f, 5f);
        }

        StartCoroutine(Stop());
    }

    private IEnumerator Stop() {
        yield return new WaitForSeconds(7f);
        _planetName.TypeName();
        yield return new WaitForSeconds(stopTime);
        _endLevel.LoadNextScene();
    }

}
