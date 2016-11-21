using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Land : MonoBehaviour {

	public enum Castle {
		ctNone,
		ctCastle,
		ctFortress
	}

	public Player owner;
	public string landID;
	public Sprite fullColor;
	public Sprite borderColor;

	public int resourceBarrel;
	public int resourceCrown;

	public int armyFootman;
	public int armyKnight;
	public int armySiege;
	public int armyShip;

	public Castle castleType; 
	public int garrisonValue = 0;

	public Player.HouseName houseHomeLand = Player.HouseName.None;

	private string landName;

	// Use this for initialization
	void Start () {
		//TODO: get land name by a given id
		GameObject go =  GameObject.FindWithTag("GameController");
		landName = go.GetComponent<GameControl>().getTranslation(landID);
	
		//LAND_THESTONYSHORE
	}
	
	void OnMouseEnter() {

		//Color n = new Color(redish.r, redish.g, redish.b,0.7f);
		//changeSpriteColor(n);
		gameObject.GetComponent<Tooltip>().showHideTooltip(true);
		changeSprite(borderColor);
		changeSpriteColor(owner.getPlayerColor(), 0.7f);
	}
	
	void OnMouseExit() {
		
		gameObject.GetComponent<Tooltip>().showHideTooltip(false);
		changeSpriteColor(owner.getPlayerColor(), 0);

	}
	


	
	// Update is called once per frame
	void Update () {

		//if we want to display owner colors over all lands
		if (PlayerPrefs.GetString("SHOW_OWNER_COLOR_INDICATOR","N") == "Y") {
			changeSprite(fullColor);
			changeSpriteColor(owner.getPlayerColor(), 0.4f);
		}

	
	}


	private void changeSpriteColor(Color n, float alpha = 1) {
		if (alpha == 1) {
			gameObject.GetComponent<SpriteRenderer>().color = n;
		} else {
			Color n2 = new Color(n.r, n.g, n.b, alpha);
			gameObject.GetComponent<SpriteRenderer>().color = n2;
		}
	}

	private void changeSprite(Sprite s) {
		gameObject.GetComponent<SpriteRenderer>().sprite = s;
	}

	public string getLandName() {
		return landName;
	}
}
