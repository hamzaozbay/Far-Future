using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool canSpawn = false;
    public float spawnRateTime = 5f;

    [SerializeField] private List<WaveSpawner> _waves = new List<WaveSpawner>();

    protected int enemyCount;
    protected Coroutine coroutine;
    protected GameObject player;
    protected List<GameObject> _allEnemies = new List<GameObject>();





    protected void Start() {
        player = GameManager.instance.player;
    }


    private void Update() {
        if (canSpawn) {
            SpawnRandomEnemy();
        }
    }


    protected void SpawnRandomEnemy() {
        canSpawn = false;
        enemyCount = 0;
        foreach (WaveSpawner s in _waves) {
            s.howManySpawned = 0;
            enemyCount += s.HowMany;
        }
        List<WaveSpawner> tempList = new List<WaveSpawner>(_waves);
        coroutine = StartCoroutine(spawnEnemy(tempList));
    }


    private IEnumerator spawnEnemy(List<WaveSpawner> waves) {
        WaitForSeconds waitFor;

        while (enemyCount > 0) {
            WaveSpawner wave = waves[Random.Range(0, waves.Count)];
            if (wave.howManySpawned >= wave.HowMany) {
                waves.Remove(wave);
                continue;
            }

            GameObject enemy = wave.pool.GetObject();
            SetEnemy(enemy);

            enemyCount--;
            wave.howManySpawned++;

            waitFor = new WaitForSeconds(Random.Range(spawnRateTime, spawnRateTime * 2f));
            yield return waitFor;
        }

        canSpawn = true;
    }


    protected virtual void SetEnemy(GameObject enemy) { }


    public virtual void KillAllEnemies() {
        _allEnemies.Clear();
        foreach (WaveSpawner waveSpawner in _waves) {
            GameObject poolGameObject = waveSpawner.pool.gameObject;

            for (int i = 0; i < poolGameObject.transform.childCount; i++) {
                _allEnemies.Add(poolGameObject.transform.GetChild(i).gameObject);
            }
        }

    }


    public void StopSpawning() {
        if (coroutine != null) StopCoroutine(coroutine);
        canSpawn = false;
    }

}
