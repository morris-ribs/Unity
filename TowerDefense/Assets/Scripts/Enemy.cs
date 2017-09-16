using UnityEngine;

public class Enemy : MonoBehaviour {
	 // this attribute makes the field innaccessible for other classes, 
  //    but accessible from the Inspector
  [SerializeField] 
  private Transform exitPoint;
	[SerializeField] 
  private Transform[] wayPoints;
	[SerializeField] 
  private float navigationUpdate;
  [SerializeField]
  private int healthPoints;

  [SerializeField]
  private int rewardAmt;

	private Transform enemy;
  private Collider2D enemyCollider;
  private Animator anim;
	private float navigationTime = 0;
	private int target = 0;
  private bool isDead = false;
  public bool IsDead { get { return isDead; } }

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
    enemyCollider = GetComponent<Collider2D>();
    anim = GetComponent<Animator>();
    GameManager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(wayPoints != null && !isDead) {
      navigationTime += Time.deltaTime;
      if (navigationTime > navigationUpdate) {
        if (target < wayPoints.Length) {
          enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
        } else {
          enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
        }
        navigationTime = 0;
      }
    }
	}

  /// <summary>
  /// Sent when another object enters a trigger collider attached to this
  /// object (2D physics only).
  /// </summary>
  /// <param name="other">The other Collider2D involved in this collision.</param>
  void OnTriggerEnter2D(Collider2D other)
  {
    switch (other.tag.ToLower()) {
      case "checkpoint": target++; break;
      case "finish": {
        GameManager.Instance.RoundEscaped++;
        GameManager.Instance.TotalEscaped++;
        GameManager.Instance.UnregisterEnemy(this);
        GameManager.Instance.isWaveOver();
      } 
      break;
      case "projectile": {
          Projectile newP = other.gameObject.GetComponent<Projectile>();
          if(newP != null && other != null) {
            enemyHit(newP.AttackStrength);
            Destroy(other.gameObject); 
          }
        } 
        break;
      default: return;
    }
  }

  public void enemyHit(int hitPoints) {
    GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
    int points = healthPoints - hitPoints;
    if (points > 0) {
      // enemy is hurt
      healthPoints -= hitPoints;
      // hurt animation here
      anim.Play("Hurt");
    }
    else { 
      GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
      // die animation here
      anim.SetTrigger("didDie");
      // enemy should die
      die();
    }
  }

  public void die() {
    isDead = true;
    enemyCollider.enabled = false;
    GameManager.Instance.TotalKilled++;
    GameManager.Instance.addMoney(rewardAmt);
    GameManager.Instance.isWaveOver();
  }
}
