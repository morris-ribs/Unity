using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
  
  public List<Enemy> EnemyList = new List<Enemy>();

  private int enemiesOnScreen = 0;
  const float spawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
    StartCoroutine(spawn());
	}

  IEnumerator spawn() {
    if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies) {
      for(int i = 0; i < enemiesPerSpawn; i++) {
        if (EnemyList.Count < maxEnemiesOnScreen) {
          Random rnd = new Random();
          GameObject newEnemy = Instantiate(enemies[Random.Range(0, 3)]) as GameObject;
          newEnemy.transform.position = spawnPoint.transform.position;
        }
      }
      yield return new WaitForSeconds(spawnDelay);
      StartCoroutine(spawn());
    }
  }

  public void RegisterEnemy(Enemy enemy) {
    EnemyList.Add(enemy);
  }

  public void UnregisterEnemy(Enemy enemy) {
    EnemyList.Remove(enemy);
    Destroy(enemy.gameObject);
  }

  public void DestroyAllEnemies() {
    foreach(Enemy enemy in EnemyList) {
      Destroy(enemy.gameObject);
    }

    EnemyList.Clear();
  }

}
