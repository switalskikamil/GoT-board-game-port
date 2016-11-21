using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipControl : MonoBehaviour {

	public GameObject ttObject;

	public Text tt_Title;
	public Text tt_houseName;
	public Image tt_houseCrest;
	public Image tt_res_01;
	public Image tt_res_02;
	public Image tt_res_03;
	public Image tt_army_01;
	public Image tt_army_02;
	public Image tt_army_03;
	public Image tt_army_04;

	public Text tt_labelResources;
	public Text tt_labelArmy;
	public Text tt_LabelFortifications;


	public Sprite iconBarrel;
	public Sprite iconCrown;
	public Sprite iconFootman;
	public Sprite iconShip;
	public Sprite iconSiege;
	public Sprite iconKnight;

	public Sprite castleIcon;
	public Sprite fortressIcon;
	public Text garrisonValue;
	public GameObject garrisonShieldIcon;
	public Image fortificationIcon;


	//private Vector3 offsetFromMouse;

	private GameObject gco;

	// Use this for initialization
	public void init() {
		gco =  GameObject.FindWithTag("GameController");
		gameObject.SetActive(false);
	}

	void Start () {
		init();
		//offsetFromMouse = new Vector3(gameObject.GetComponent<RectTransform>().rect.width + 20,-gameObject.GetComponent<RectTransform>().rect.height - 20,0);

	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy) {
			//gameObject.transform.position = Input.mousePosition + offsetFromMouse;
		}
	}

	public void setData(Tooltip.TooltipType type) {
		if (type == Tooltip.TooltipType.TOOLTIP_LAND && gameObject.activeInHierarchy) {
			//labels
			tt_labelResources.text = gco.GetComponent<GameControl>().getTranslation("LABEL_RESOURCES");
			tt_labelArmy.text = gco.GetComponent<GameControl>().getTranslation("LABEL_ARMY");
			tt_LabelFortifications.text = gco.GetComponent<GameControl>().getTranslation("LABEL_FORTIFICATIONS");

			//title
			tt_Title.text = ttObject.GetComponent<Land>().getLandName();
			tt_Title.color = ttObject.GetComponent<Land>().owner.getPlayerColor();

			//owner
			tt_houseCrest.sprite = ttObject.GetComponent<Land>().owner.getHouseCrest();
			tt_houseName.text = gco.GetComponent<GameControl>().getTranslation("LABEL_HOUSE") + " " + ttObject.GetComponent<Land>().owner.house.ToString(); 

			//resources
			prepareResources(ttObject.GetComponent<Land>().resourceBarrel, ttObject.GetComponent<Land>().resourceCrown);

			//army
			prepareArmy(ttObject.GetComponent<Land>().armyFootman, ttObject.GetComponent<Land>().armyShip, ttObject.GetComponent<Land>().armySiege, ttObject.GetComponent<Land>().armyKnight);

			//garrison value
			garrisonValue.text = ttObject.GetComponent<Land>().garrisonValue.ToString();

			//fortifications icons
			prepareFortifications();
		}
	}

	//prepares resources icons to display
	private void prepareResources(int barrels, int crowns) {
		tt_res_01.enabled = false;
		tt_res_02.enabled = false;
		tt_res_03.enabled = false;


		//set num of barrels max = 2
		if (barrels > 0) {
			setIcon(tt_res_01, tt_res_02, tt_res_03, null, iconBarrel, barrels);
		}

		//set num of crowns max = 3
		if (crowns > 0) {
			setIcon(tt_res_01, tt_res_02, tt_res_03, null, iconCrown, crowns);
		}

		//center icons
		Vector3 tmp = tt_res_01.rectTransform.anchoredPosition;
		if (barrels + crowns == 1) {
			tt_res_01.rectTransform.anchoredPosition = new Vector3(0,tmp.y, tmp.z);
		}
		else if (barrels + crowns == 2) {
			tt_res_01.rectTransform.anchoredPosition = new Vector3(-22,tmp.y, tmp.z);
			tt_res_02.rectTransform.anchoredPosition = new Vector3(23,tmp.y, tmp.z);
		}
		else if (barrels + crowns == 3) {
			tt_res_01.rectTransform.anchoredPosition = new Vector3(-45,tmp.y, tmp.z);
			tt_res_02.rectTransform.anchoredPosition = new Vector3(0,tmp.y, tmp.z);
			tt_res_03.rectTransform.anchoredPosition = new Vector3(45,tmp.y, tmp.z);
		}
	}

	//prepares army icons to display
	private void prepareArmy(int footman, int ship, int siege, int knight) {
		tt_army_01.enabled = false;
		tt_army_02.enabled = false;
		tt_army_03.enabled = false;
		tt_army_04.enabled = false;
		tt_army_01.color = ttObject.GetComponent<Land>().owner.getPlayerColor();
		tt_army_02.color = ttObject.GetComponent<Land>().owner.getPlayerColor();
		tt_army_03.color = ttObject.GetComponent<Land>().owner.getPlayerColor();
		tt_army_04.color = ttObject.GetComponent<Land>().owner.getPlayerColor();
		
		
		//set num of each soldier type max = 4
		if (footman > 0) {
			setIcon(tt_army_01, tt_army_02, tt_army_03, tt_army_04, iconFootman, footman);
		} 

		if (ship > 0) {
			setIcon(tt_army_01, tt_army_02, tt_army_03, tt_army_04, iconShip, ship);
		} 

		if (siege > 0) {
			setIcon(tt_army_01, tt_army_02, tt_army_03, tt_army_04, iconSiege, siege);
		} 
		if (knight > 0) {
			setIcon(tt_army_01, tt_army_02, tt_army_03, tt_army_04, iconKnight, knight);
		} 
		
		//center icons
		Vector3 tmp = tt_army_01.rectTransform.anchoredPosition;
		if (footman + ship + siege + knight == 1) {
			tt_army_01.rectTransform.anchoredPosition = new Vector3(0,tmp.y, tmp.z);
		}
		else if (footman + ship + siege + knight == 2) {
			tt_army_01.rectTransform.anchoredPosition = new Vector3(-22,tmp.y, tmp.z);
			tt_army_02.rectTransform.anchoredPosition = new Vector3(23,tmp.y, tmp.z);
		}
		else if (footman + ship + siege + knight == 3) {
			tt_army_01.rectTransform.anchoredPosition = new Vector3(-45,tmp.y, tmp.z);
			tt_army_02.rectTransform.anchoredPosition = new Vector3(0,tmp.y, tmp.z);
			tt_army_03.rectTransform.anchoredPosition = new Vector3(45,tmp.y, tmp.z);
		}
		else if (footman + ship + siege + knight == 4) {
			tt_army_01.rectTransform.anchoredPosition = new Vector3(-65,tmp.y, tmp.z);
			tt_army_02.rectTransform.anchoredPosition = new Vector3(-20,tmp.y, tmp.z);
			tt_army_03.rectTransform.anchoredPosition = new Vector3(25,tmp.y, tmp.z);
			tt_army_04.rectTransform.anchoredPosition = new Vector3(65,tmp.y, tmp.z);
		}
	}


	//sets image icon to first free element on the list
	private void setIcon(Image t1,Image t2,Image t3,Image t4, Sprite i, int count = 1 ) {
		if (t1 != null && t1.enabled == false) {
			t1.sprite = i;
			t1.enabled = true;
		} else if (t2 != null && t2.enabled == false) {
			t2.sprite = i;
			t2.enabled = true;
		} else if (t3 != null && t3.enabled == false) {
			t3.sprite = i;
			t3.enabled = true;
		} else if (t4 != null && t4.enabled == false) {
			t4.sprite = i;
			t4.enabled = true;
		}

		count--;

		if (count > 0) setIcon(t1, t2, t3, t4, i, count);

	}

	private void prepareFortifications() {
		//castle icon
		if (ttObject.GetComponent<Land>().castleType == Land.Castle.ctFortress) {
			fortificationIcon.sprite = fortressIcon;
			fortificationIcon.enabled = true;
			garrisonShieldIcon.SetActive(true);
		} else if (ttObject.GetComponent<Land>().castleType == Land.Castle.ctCastle) {
			fortificationIcon.sprite = castleIcon;
			fortificationIcon.enabled = true;
			garrisonShieldIcon.SetActive(true);
		} else if (ttObject.GetComponent<Land>().castleType == Land.Castle.ctNone) {
			fortificationIcon.enabled = false;
			garrisonShieldIcon.SetActive(false);
		}
	}
}
