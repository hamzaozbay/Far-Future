using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet1Platform : MonoBehaviour {

    [SerializeField] private GameObject dissolvePrefab;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(spawnDissolve());
        }
    }

    private IEnumerator spawnDissolve() {
        yield return new WaitForSeconds(.25f);
        Instantiate(dissolvePrefab, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }


}
