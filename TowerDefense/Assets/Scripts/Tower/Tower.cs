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
  private bool isAttacking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		attackCounter -= Time.deltaTime;
    if (targetEnemy == null || targetEnemy.IsDead) {
      Enemy nearestEnemy = GetNearestEnemyInRange();
      if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius) {
        targetEnemy = nearestEnemy;
      } 
    } else if (attackCounter <= 0) {
      isAttacking = true;
      // reset attack counter
      attackCounter = timeBetweenAttacks;
    } else {
      isAttacking = false;
    }

    if (targetEnemy != null && Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius) {
      targetEnemy = null;
    }
	}

  void FixedUpdate() {
    if (isAttacking) {
      Attack();
    }
  }

  public void Attack() {
    isAttacking = false;
    Projectile newProjectile = Instantiate(projectile) as Projectile;
    newProjectile.transform.localPosition = transform.localPosition;
    
    switch (newProjectile.ProjectileType)
    {
      case proType.arrow: 
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Arrow);
      break;
      case proType.fireBall: 
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Fireball);
      break;
      default:
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Rock);
      break;
    }
    
    if (targetEnemy == null) {
      Destroy(newProjectile);
    } else {
      // move projectile to enemy
      StartCoroutine(MoveProjectile(newProjectile));
    }
  }

  IEnumerator MoveProjectile(Projectile projectile) {
    while (targetEnemy != null && getTargetDistance(targetEnemy) > 0.20f && projectile != null) {
      var dir = targetEnemy.transform.localPosition - transform.localPosition;
      var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
      projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
      yield return null;
    }
    if (projectile != null || targetEnemy == null) {
      Destroy(projectile);
    }
  }

  private float getTargetDistance(Enemy thisEnemy) {
    if (thisEnemy == null) {
      thisEnemy = GetNearestEnemyInRange();
      if (thisEnemy == null) {
        return 0f;
      }
    }
    return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
  }

  private Enemy GetNearestEnemyInRange() {
    Enemy nearestEnemy = null;
    float smallestDistance = float.PositiveInfinity;
    foreach (Enemy enemy in GameManager.Instance.EnemyList)
    {
      float distance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
      if (distance <= attackRadius && distance < smallestDistance) {
        smallestDistance = distance;
        nearestEnemy = enemy;
      }
    }
    return nearestEnemy;
  }
}
