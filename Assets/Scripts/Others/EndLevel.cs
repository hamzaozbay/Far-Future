using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public Scenes nextScene;

    [SerializeField] private Animator _sceneTransition;
    [SerializeField] private TypeEffect _planetClearedText;
    [SerializeField] private UnityEvent _endLevelEvent;



    private void Start() {
        GameManager.instance.endLevel = this;

        _sceneTransition.gameObject.SetActive(true);
        _sceneTransition.SetTrigger("Open");
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            StopPlayer();
            _endLevelEvent.Invoke();
        }
    }



    public void LoadNextScene() {
        GameManager.instance.previousScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.nextScene = nextScene;
        GameManager.instance.Save();
        StartCoroutine(WaitForSceneTransition());
    }
    public IEnumerator WaitForSceneTransition() {
        _sceneTransition.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationLength.ClipLength(_sceneTransition, "levelClosing"));
        GameManager.instance.lastCheckPoint = Vector3.zero;
        SceneLoader.Load(nextScene);
    }

    public void LoadNextSceneWithoutTransition() {
        GameManager.instance.previousScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
        GameManager.instance.nextScene = nextScene;
        GameManager.instance.Save();
        SceneLoader.Load(nextScene);
    }


    public void AfterBossDead() {
        StartCoroutine(TypeText());
    }
    private IEnumerator TypeText() {
        yield return new WaitForSeconds(1.5f);
        _sceneTransition.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        _planetClearedText.TypeText();
    }


    private void StopPlayer() {
        GameManager.instance.player.GetComponent<PlayerMovement>().StopPlayer();
    }


    public Animator SceneTransition { get { return _sceneTransition; } }

}
