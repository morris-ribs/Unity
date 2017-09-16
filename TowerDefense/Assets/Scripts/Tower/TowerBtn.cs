using UnityEngine;

public class TowerBtn : MonoBehaviour {
	[SerializeField]
  private Tower towerObject;
	public Tower TowerObject { 
    get {
    return towerObject;
    } 
    private set { } 
  }

  [SerializeField]
  private Sprite dragSprite;

  public Sprite DragSprite { get { return dragSprite; } private set { }  }
  
  [SerializeField]
  private int towerPrice;
  public int TowerPrice
  {
      get { return towerPrice;}
  }
  
}
