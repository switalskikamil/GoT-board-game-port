using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogTabBtnControl : MonoBehaviour {

	public Player player;

	public bool isAssigned = false; 
	public bool tabActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void assignPlayer(Player p) {
		this.player = p;
		isAssigned = true;

		if (player != null) gameObject.GetComponentInChildren<Text>().text = player.house.ToString();
	}

	public void tabClick() {
		tabActive = true;

		gameObject.GetComponentInParent<LogPanelControl>().deselectAllTabs();

		gameObject.GetComponent<Image>().color = gameObject.GetComponentInParent<LogPanelControl>().activeTabBgColor;
		gameObject.GetComponentInChildren<Text>().color = gameObject.GetComponentInParent<LogPanelControl>().activeTabLabelColor;

	}


}
