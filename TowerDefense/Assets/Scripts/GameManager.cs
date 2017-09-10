using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
  // this attribute makes the field innaccessible for other classes, 
  //    but accessible from the Inspector
  [SerializeField] 
  private GameObject spawnPoint;
  [SerializeField]
  private GameObject[] enemies;
  [SerializeField]
  private int maxEnemiesOnScreen;
  [SerializeField]
  private int totalEnemies;
  [SerializeField]
  private int enemiesPerSpawn;
  private int enemiesOnScreen = 0;
  const float spawnDelay = 0.5f;

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
