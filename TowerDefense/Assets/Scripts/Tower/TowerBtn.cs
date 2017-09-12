using UnityEngine;

public class TowerBtn : MonoBehaviour {
	[SerializeField]
  private GameObject towerObject;
	public GameObject TowerObject { 
    get {
    return towerObject;
    } 
    private set { } 
  }

  [SerializeField]
  private Sprite dragSprite;

  public Sprite DragSprite { get { return dragSprite; } private set { }  }
}
