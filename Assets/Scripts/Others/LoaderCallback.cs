using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour {

    private bool _isFirstUpdate = true;


    private void Update() {
        if (_isFirstUpdate) {
            _isFirstUpdate = false;
            SceneLoader.LoaderCallback();
        }
    }


}
