using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum gameStatus {
  next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {
  // SerializeField makes the field innaccessible for other classes, 
  //    but accessible from the Inspector
  [SerializeField] 
  private int totalWaves = 10;
  [SerializeField] 
  private Text totalMoneyLbl;
  [SerializeField] 
  private Text currentWaveLbl;
  [SerializeField] 
  private Text totalEscapedLbl;
  [SerializeField] 
  private GameObject spawnPoint;
  [SerializeField]
  private Enemy[] enemies;
  [SerializeField]
  private int totalEnemies = 3;
  [SerializeField]
  private int enemiesPerSpawn;
  [SerializeField]
  private Text playBtnLbl;
  [SerializeField]
  private Button playBtn;
  [SerializeField]
  private Text gameBannerLbl;
  [SerializeField]
  private Image gameBanner;

  private int waveNumber = 0;
  private int totalMoney = 10;
  public int TotalMoney {
    get { return totalMoney;}
    set { 
      totalMoney = value;
      totalMoneyLbl.text = totalMoney.ToString();
    }
  }
  
  private int totalEscaped = 0;
  public int TotalEscaped { 
    get { return totalEscaped; } 
    set { totalEscaped = value; } 
  }
  private int roundEscaped = 0;
  public int RoundEscaped { 
    get { return roundEscaped; } 
    set { roundEscaped = value; } 
  }
  private int totalKilled = 0;
  public int TotalKilled { 
    get { return totalKilled; } 
    set { totalKilled = value; } 
  }
  private int whichEnemiesToSpawn = 0;
  private int enemiesToSpawn = 0;
  private gameStatus currentState = gameStatus.play;
  private AudioSource audioSource;
  public AudioSource AudioSource {
    get { 
      return audioSource;
    }
  }
  
  public List<Enemy> EnemyList = new List<Enemy>();

  private int enemiesOnScreen = 0;
  const float spawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
    playBtn.gameObject.SetActive(false);
		gameBanner.gameObject.SetActive(false);
    audioSource = GetComponent<AudioSource>();
    showMenu();
	}

  void Update () {
    handleEscape();
  }

  IEnumerator spawn() {
    if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies) {
      for(int i = 0; i < enemiesPerSpawn; i++) {
        int enemyIndex = UnityEngine.Random.Range(0, enemiesToSpawn);
        Enemy newEnemy = Instantiate(enemies[enemyIndex]);
        newEnemy.transform.position = spawnPoint.transform.position;
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

  public void addMoney(int amount) {
    TotalMoney += amount;
  }
  
  public void subtractMoney(int amount) {
    TotalMoney -= amount;
  }
  
  public void isWaveOver() {
    totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
    if (RoundEscaped + TotalKilled == totalEnemies) {
      if (waveNumber <= enemies.Length) {
        enemiesToSpawn = waveNumber;
      }
      setCurrentGameState();
      showMenu();
    }
  }

  public void setCurrentGameState() {
    if (TotalEscaped >= 10) {
      currentState = gameStatus.gameover;
    } else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0) {
      currentState = gameStatus.play;
    } else if (waveNumber >= totalWaves) {
      currentState = gameStatus.win;
    } else {
      currentState = gameStatus.next;
    }
  }

  private void showMenu()
  {
    switch (currentState)
    {
      case gameStatus.gameover: 
        playBtnLbl.text = "Play Again!"; 
        gameBannerLbl.text = "Game Over";
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
      break;
      case gameStatus.next:
        playBtnLbl.text = "Next Wave";
        gameBannerLbl.text = "Next wave: " + (waveNumber + 2);
      break;
      case gameStatus.play:
        playBtnLbl.text = "Play";
        gameBannerLbl.text = "Start";
      break;
      case gameStatus.win:
        playBtnLbl.text = "Play";
        gameBannerLbl.text = "You Win!";
      break;
      default: break;
    }
    playBtn.gameObject.SetActive(true);
		gameBanner.gameObject.SetActive(true);
  }

  public void playBtnPressed() {
    switch (currentState)
    {
      case gameStatus.next: {
          waveNumber++; 
          totalEnemies += waveNumber;
        }
      break;
      default: {
        totalEnemies = 3;
        waveNumber = 0;
        TotalEscaped = 0;
        TotalMoney = 10;
        enemiesToSpawn = 0;
        TowerManager.Instance.DestroyAllTowers();
        TowerManager.Instance.RenameTagsBuildSites();
        totalMoneyLbl.text = TotalMoney.ToString();
        totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
        audioSource.PlayOneShot(SoundManager.Instance.NewGame);
      } 
      break;
    }
    gameBannerLbl.text = "";
    DestroyAllEnemies();
    TotalKilled = 0;
    RoundEscaped = 0;
    currentWaveLbl.text = "Wave " + (waveNumber + 1);
    StartCoroutine(spawn());
		gameBanner.gameObject.SetActive(false);
    playBtn.gameObject.SetActive(false);
  }
  
  private void handleEscape() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      TowerManager.Instance.disableDragSprite();
      TowerManager.Instance.towerBtnPressed = null;
    }
  }
}
