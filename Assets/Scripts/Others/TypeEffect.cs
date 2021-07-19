using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class TypeEffect : MonoBehaviour {

    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool doFade = false;
    [SerializeField] private UnityEvent textCompleted;

    private List<string> _tempTexts = new List<string>();
    private WaitForSeconds _waitForSeconds;
    private CanvasGroup _canvasGroup;
    


    private void Awake() {
        _waitForSeconds = new WaitForSeconds(typeSpeed);
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public void TypeText() {
        _tempTexts.Clear();
        foreach (TextMeshProUGUI t in texts) {
            _tempTexts.Add(t.text);
            t.text = "";
        }

        _canvasGroup.DOFade(1f, fadeDuration).OnComplete(() => {
            StartCoroutine(typeEffect());
        });
    }

    private IEnumerator typeEffect() {
        for (int i = 0; i < _tempTexts.Count; i++) {
            foreach (char c in _tempTexts[i]) {
                texts[i].text += c;
                AudioManager.instance.PlaySound("TypeSound");
                yield return _waitForSeconds;
            }
            yield return new WaitForSeconds(2f);
        }

        if (doFade) {
            _canvasGroup.DOFade(0f, fadeDuration).OnComplete(() => {
                textCompleted.Invoke();
            });
        }

    }


    public void DisableObject() {
        this.gameObject.SetActive(false);
    }

}
