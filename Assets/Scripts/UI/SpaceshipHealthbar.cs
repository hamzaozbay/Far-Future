using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipHealthbar : MonoBehaviour {

    [SerializeField] private Sprite _healthSprite, _healthEmptySprite;

    private int _health;



    private void Start() {
        UpdateHealthbar();
    }


    public void UpdateHealthbar() {
        _health = GameManager.instance.spaceshipHealth;

        for (int i = 0; i < 5; i++) {
            if (i > _health - 1) {
                this.gameObject.transform.GetChild(i).GetComponent<Image>().sprite = _healthEmptySprite;
                continue;
            }

            this.gameObject.transform.GetChild(i).GetComponent<Image>().sprite = _healthSprite;
        }

    }



}
