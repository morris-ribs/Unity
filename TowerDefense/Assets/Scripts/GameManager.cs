using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager instance = null;
  public GameObject spawnPoint;
  public GameObject[] enemies;
  public int maxEnemiesOnScreen;
  public int totalEnemies;
  public int enemiesPerSpawn;
  private int enemiesOnScreen = 0;
  const float spawnDelay = 0.5f;

  void Awake() {
    if(instance == null) {
      instance = this;
    }
    else if (instance != this) {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

	// Use this for initialization
	void Start () {
    StartCoroutine(spawn());
	}

  IEnumerator spawn() {
    if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies) {
      for(int i = 0; i < enemiesPerSpawn; i++) {
        if (enemiesOnScreen < maxEnemiesOnScreen) {
          Random rnd = new Random();
          GameObject newEnemy = Instantiate(enemies[Random.Range(0, 3)]) as GameObject;
          newEnemy.transform.position = spawnPoint.transform.position;
          enemiesOnScreen++;
        }
      }
      yield return new WaitForSeconds(spawnDelay);
      StartCoroutine(spawn());
    }
  }

  public void removeEnemyFromScreen() {
    if (enemiesOnScreen > 0)
      enemiesOnScreen--;
  }
}
