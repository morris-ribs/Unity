using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
	[SerializeField]
	private AudioClip arrow;
  public AudioClip Arrow
  {
      get { return arrow;}
  }
  
	[SerializeField]
	private AudioClip death;
  public AudioClip Death
  {
      get { return death;}
  }
	[SerializeField]
	private AudioClip fireball;
  public AudioClip Fireball
  {
      get { return fireball;}
  }
	[SerializeField]
	private AudioClip gameOver;
  public AudioClip GameOver
  {
      get { return gameOver;}
  }
	[SerializeField]
	private AudioClip hit;
  public AudioClip Hit
  {
      get { return hit;}
  }
	[SerializeField]
	private AudioClip level;
  public AudioClip Level
  {
      get { return level;}
  }
	[SerializeField]
	private AudioClip newGame;
  public AudioClip NewGame
  {
      get { return newGame;}
  }
	[SerializeField]
	private AudioClip rock;
  public AudioClip Rock
  {
      get { return rock;}
  }
	[SerializeField]
	private AudioClip towerBuilt;
  public AudioClip TowerBuilt
  {
      get { return towerBuilt;}
  }
}
