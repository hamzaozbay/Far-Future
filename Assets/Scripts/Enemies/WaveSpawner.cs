using System;
using UnityEngine;

[Serializable]
public class WaveSpawner {

    public string name;
    public ObjectPool pool;
    public int howManySpawned;
    [SerializeField] private int _howMany;


    public int HowMany { get { return _howMany; } }

}
