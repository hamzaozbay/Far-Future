using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    private int _previousScene;
    private int _nextScene;
    private int _playerHealth;
    private int _spaceshipHealth;


    public PlayerData(int previousScene, int nextScene, int playerHealth, int spaceshipHealth) {
        _previousScene = previousScene;
        _nextScene = nextScene;
        _playerHealth = playerHealth;
        _spaceshipHealth = spaceshipHealth;
    }


    public int PreviousScene { get { return _previousScene; } }
    public int NextScene { get { return _nextScene; } }
    public int PlayerHealth { get { return _playerHealth; } }
    public int SpaceshipHealth { get { return _spaceshipHealth; } }

}
