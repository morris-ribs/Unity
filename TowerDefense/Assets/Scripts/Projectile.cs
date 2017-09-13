using UnityEngine;

public enum proType {
  rock, arrow, fireBall
};

public class Projectile : MonoBehaviour {
	[SerializeField]
  private int attackStrength;
  public int AttackStrength { get { return attackStrength; } }

  [SerializeField]
  private proType projectileType;

  public proType ProjectileType { get { return projectileType; } }
}
