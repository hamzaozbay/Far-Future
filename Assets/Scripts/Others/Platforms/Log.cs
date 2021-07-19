using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Fall")) {
            this.gameObject.SetActive(false);
        }
    }

}
