using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	[SerializeField]
  private float timeBetweenAttacks;
  [SerializeField]
  private float attackRadius;
  [SerializeField]
  private Projectile projectile;
  private Enemy targetEnemy = null;
  private float attackCounter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private Enemy GetNearestEnemyInRange() {
    Enemy nearestEnemy = null;
    float smallestDistance = float.PositiveInfinity;
    foreach (Enemy enemy in GameManager.Instance.EnemyList)
    {
      float distance = Vector2.Distance(transform.position, enemy.transform.position);
      if (distance <= attackRadius && distance < smallestDistance) {
        smallestDistance = distance;
        nearestEnemy = enemy;
      }
    }
    return nearestEnemy;
  }
}
