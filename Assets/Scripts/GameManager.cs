using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    private void Awake() {
        if (instance != this) {
            if (instance != null) Destroy(instance.gameObject); //Destroy old Game Manager.

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public GameObject player;
    public Vector3 lastCheckPoint = Vector3.zero;
    public EndLevel endLevel;
    public Scenes previousScene;
    public Scenes nextScene;
    public bool isGamePaused = false;
    public int spaceshipHealth = 5;
    public int playerHealth = 3;
    public int watchedAdCount = 4;
    public DirectionButton leftButton, rightButton;

    [SerializeField] private GameObject _puffPoolPrefab;
    [SerializeField] private RewardedAdDemo _rewardedAd;

    private ObjectPool _puffPool;
    private Camera _cam;
    private Vector2 _screenRight, _screenTop, _screenLeft, _screenBottom;
    private int _basePlayerHealth, _baseSpaceshipHealth;


    private void Start() {
        GameObject go = Instantiate(_puffPoolPrefab);
        _puffPool = go.GetComponent<ObjectPool>();
        DontDestroyOnLoad(go);

        _cam = Camera.main;
        _screenRight = new Vector2(10f, 0f);
        _screenLeft = new Vector2(-10f, 0f);
        _screenBottom = new Vector2(0f, -_cam.orthographicSize - 1.5f);
        _screenTop = new Vector2(0f, _cam.orthographicSize + 1.5f);

        _basePlayerHealth = playerHealth;
        _baseSpaceshipHealth = spaceshipHealth;
    }



    public void ReloadPlanetScene() {
        UnPauseGame();
        Reset();
        SceneLoader.Load((Scenes)SceneManager.GetActiveScene().buildIndex);
    }

    public void StartAtBeginingLevel() {
        UnPauseGame();
        Reset();
        SceneLoader.Load((Scenes)SceneManager.GetActiveScene().buildIndex);
    }

    public void FallReloadScene() {
        SceneLoader.Load((Scenes)SceneManager.GetActiveScene().buildIndex);
    }

    private void Reset() {
        playerHealth = _basePlayerHealth;
        spaceshipHealth = _baseSpaceshipHealth;
    }



    public void Save() {
        SaveSystem.Save((int)previousScene, (int)nextScene, playerHealth, spaceshipHealth);
    }

    public void Load() {
        PlayerData data = SaveSystem.Load();

        playerHealth = data.PlayerHealth;
        spaceshipHealth = data.SpaceshipHealth;
        nextScene = (Scenes)data.NextScene;
        previousScene = (Scenes)data.PreviousScene;
    }



    public void PauseGame() {
        isGamePaused = true;

        PauseBackgroundMusic();

        Time.timeScale = 0f;
    }

    public void UnPauseGame() {
        isGamePaused = false;

        UnPauseBackgroundMusic();

        Time.timeScale = 1f;
    }

    public void PauseBackgroundMusic() {
        GameObject[] backgroundMusics = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        foreach (GameObject g in backgroundMusics) {
            if (!g.GetComponent<AudioSource>().isPlaying) continue;

            g.GetComponent<AudioSource>().Pause();
        }
    }

    public void UnPauseBackgroundMusic() {
        GameObject[] backgroundMusics = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        foreach (GameObject g in backgroundMusics) {
            g.GetComponent<AudioSource>().UnPause();
        }
    }


    public void ShowAd() {
        _rewardedAd.UserChoseToWatchAd();
    }

    public void SetCameraPosition(Vector3 pos) {
        _cam = Camera.main;
        _cam.transform.position = pos;
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_screenTop, new Vector3(1f, 1f));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_screenBottom, new Vector3(1f, 1f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_screenLeft, new Vector3(1f, 1f));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_screenRight, new Vector3(1f, 1f));
    }



    public Camera Cam { get { return _cam; } }
    public ObjectPool PuffPool { get { return _puffPool; } }
    public Vector2 ScreenTop { get { return _screenTop; } }
    public Vector2 ScreenRight { get { return _screenRight; } }
    public Vector2 ScreenBottom { get { return _screenBottom; } }
    public Vector2 ScreenLeft { get { return _screenLeft; } }


}
