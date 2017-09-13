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

	private Transform enemy;
	private float navigationTime = 0;
	private int target = 0;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
    GameManager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(wayPoints != null) {
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
    if (other.tag.ToLower() == "checkpoint") {
      target++;
    } else if (other.tag.ToLower() == "finish") {
      GameManager.Instance.UnregisterEnemy(this);
    }
  }
}
