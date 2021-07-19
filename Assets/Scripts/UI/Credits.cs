using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour {

    [SerializeField] private Animator _sceneTransition;
    [SerializeField] private RectTransform _allCredits;
    [SerializeField] private float _endYValue = 0f;


    private void Start() {
        _sceneTransition.SetTrigger("Open");
        _allCredits.DOLocalMoveY(_endYValue, 10f);
    }



    public void LoadMainMenu() {
        StartCoroutine(load());
    }
    private IEnumerator load() {
        _sceneTransition.SetTrigger("Close");
        yield return new WaitForSeconds(1.2f);
        SceneLoader.Load(Scenes.MainMenu.ToString());
    }



}
