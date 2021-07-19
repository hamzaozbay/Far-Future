using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour {

    [SerializeField] private EndLevel _endLevel;
    [SerializeField] private SpriteRenderer _planet;
    [SerializeField] private EnemySpaceshipSpawner _spaceshipSpawner;
    [SerializeField] private AstreoidSpawner _astreoidSpawner;
    [SerializeField] private TypeEffect _yearText;
    [SerializeField] private PlanetNameUI _planetName;
    [SerializeField] private GameObject _enemyDestroyer;
    [SerializeField] private SpaceLevel[] _levels = new SpaceLevel[4];

    private float _actionTime = 45f;



    private void Start() {
        _spaceshipSpawner.transform.position = GameManager.instance.ScreenTop;
        _astreoidSpawner.transform.position = GameManager.instance.ScreenTop;
        _enemyDestroyer.transform.position = GameManager.instance.ScreenBottom;

        if (GameManager.instance.previousScene == Scenes.MainMenu) {
            SetLevel(_levels[0]);
            TypeYearText();
            return;
        }
        else if (GameManager.instance.previousScene == Scenes.Planet1x2) {
            SetNextScene();
            SetLevel(_levels[1]);
        }
        else if (GameManager.instance.previousScene == Scenes.Planet2x2) {
            SetNextScene();
            SetLevel(_levels[2]);
        }
        else if (GameManager.instance.previousScene == Scenes.Planet3x2) {
            SetNextScene();
            SetLevel(_levels[3]);
        }

        StartSpawning();
    }


    public void TypeYearText() {
        StartCoroutine(startYearText());
    }

    private IEnumerator startYearText() {
        yield return new WaitForSeconds(3f);
        _yearText.TypeText();
    }


    public void StartSpawning() {
        _spaceshipSpawner.canSpawn = true;
        _astreoidSpawner.canSpawn = true;
        StartCoroutine(spaceLevelEnd());
    }

    private IEnumerator spaceLevelEnd() {
        yield return new WaitForSeconds(_actionTime);
        _spaceshipSpawner.StopSpawning();
        _astreoidSpawner.StopSpawning();
        yield return new WaitForSeconds(1f);
        _planet.transform.position = new Vector2(0f, GameManager.instance.ScreenTop.y + 1f);
        _planet.gameObject.SetActive(true);
    }



    private void SetLevel(SpaceLevel level) {
        _actionTime = level.ActionTime;
        _spaceshipSpawner.spawnRateTime = level.SpaceshipSpawnerSpawnRate;
        _astreoidSpawner.spawnRateTime = level.AstreoidSpawnerSpawnRate;

        _planet.sprite = level.PlanetSprite;
        _planet.transform.localScale = level.PlanetScale;
        _planet.GetComponent<Planet>().stopTime = level.PlanetStopTime;
        _planetName.SetName(level.PlanetName);
        _planetName.Target.localPosition = level.PlanetNameUIPosition;
    }


    private void SetNextScene() {
        int nextScene = (int)GameManager.instance.previousScene + 1;
        _endLevel.nextScene = (Scenes)(nextScene);
    }

}
