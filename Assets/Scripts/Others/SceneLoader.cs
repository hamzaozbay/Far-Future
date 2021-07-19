using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    private static Action _onLoaderCallback;


    public static void Load(Scene scene) {
        _onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }

    public static void Load(string scene) {
        _onLoaderCallback = () => {
            SceneManager.LoadScene(scene);
        };

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }
    public static void Load(Scenes scene) {
        _onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }


    public static void LoaderCallback() {
        if (_onLoaderCallback != null) {
            _onLoaderCallback();
            _onLoaderCallback = null;
        }

    }


}



public enum Scenes {
    MainMenu = 0,
    Loading = 1,
    Space = 2,
    Planet1x1 = 3,
    Planet1x2 = 4,
    Planet2x1 = 5,
    Planet2x2 = 6,
    Planet3x1 = 7,
    Planet3x2 = 8,
    Planet4x1 = 9,
    Planet4x2 = 10,
    Credits = 11,
    NULL
}
