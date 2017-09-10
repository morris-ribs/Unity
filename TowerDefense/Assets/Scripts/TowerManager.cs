using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

  private TowerBtn towerBtnPressed; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Vector2.zero gets exactly the point from where we are hitting the button on
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			placeTower(hit);
		}
	}

	public void placeTower(RaycastHit2D hit) {
		if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null) {
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
		}
	}

  public void selectTower(TowerBtn towerSelected) {
    towerBtnPressed = towerSelected;
		// Debug.Log("Button pressed: " + towerBtnPressed.TowerObject);
  }
}
