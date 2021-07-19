using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceLevel {

    [SerializeField] private string name;
    [SerializeField] private Sprite _planetSprite;
    [SerializeField] private Vector3 _planetScale;
    [SerializeField] private string _planetName;
    [SerializeField] private float _planetStopTime;
    [SerializeField] private Vector3 _planetNameUIPosition;
    [SerializeField] private float _actionTime;
    [SerializeField] private float _spaceshipSpawnerSpawnRate;
    [SerializeField] private float _astreoidSpawnerSpawnRate;



    public Sprite PlanetSprite { get { return _planetSprite; } }
    public Vector3 PlanetScale { get { return _planetScale; } }
    public string PlanetName { get { return _planetName; } }
    public float PlanetStopTime { get { return _planetStopTime; } }
    public Vector3 PlanetNameUIPosition { get { return _planetNameUIPosition; } }
    public float ActionTime { get { return _actionTime; } }
    public float SpaceshipSpawnerSpawnRate { get { return _spaceshipSpawnerSpawnRate; } }
    public float AstreoidSpawnerSpawnRate { get { return _astreoidSpawnerSpawnRate; } }

}
