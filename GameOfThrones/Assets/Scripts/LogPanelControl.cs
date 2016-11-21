using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogPanelControl : MonoBehaviour {


	public Color activeTabLabelColor;
	public Color activeTabBgColor;
	public Color inactiveTabLabelColor;
	public Color inactiveTabBgColor;

	public Button eventsTabBtn;
	public Button oppon1TabBtn;
	public Button oppon2TabBtn;
	public Button oppon3TabBtn;
	public Button oppon4TabBtn;
	public Button oppon5TabBtn;

	public GameObject contentPanel;
	public int contentMinHeight;
	public int contentMaxHeight;

	// Use this for initialization
	void Start () {
		//get all opponents and set their hous names to the tab labels
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject p in players) {
			if (p.GetComponent<Player>().playerType != Player.PlayerType.LOCAL_PLAYER) {
				assignButton(p.GetComponent<Player>());
			}
		}
		deselectAllTabs();
		eventsTabBtn.GetComponent<LogTabBtnControl>().tabClick();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void assignButton(Player p) {
		if (!oppon1TabBtn.GetComponent<LogTabBtnControl>().isAssigned) oppon1TabBtn.GetComponent<LogTabBtnControl>().assignPlayer(p);
		else if (!oppon2TabBtn.GetComponent<LogTabBtnControl>().isAssigned) oppon2TabBtn.GetComponent<LogTabBtnControl>().assignPlayer(p);
		else if (!oppon3TabBtn.GetComponent<LogTabBtnControl>().isAssigned) oppon3TabBtn.GetComponent<LogTabBtnControl>().assignPlayer(p);
		else if (!oppon4TabBtn.GetComponent<LogTabBtnControl>().isAssigned) oppon4TabBtn.GetComponent<LogTabBtnControl>().assignPlayer(p);
		else if (!oppon5TabBtn.GetComponent<LogTabBtnControl>().isAssigned) oppon5TabBtn.GetComponent<LogTabBtnControl>().assignPlayer(p);
	}

	private void deselectTab(Button b) {
		b.GetComponent<Image>().color = inactiveTabBgColor;
		b.GetComponentInChildren<Text>().color = inactiveTabLabelColor;
		b.GetComponent<LogTabBtnControl>().isAssigned = false;
	}

	public void deselectAllTabs() {
		//TODO: optimize
		deselectTab(eventsTabBtn);
		deselectTab(oppon1TabBtn);
		deselectTab(oppon2TabBtn);
		deselectTab(oppon3TabBtn);
		deselectTab(oppon4TabBtn);
		deselectTab(oppon5TabBtn);
	}
}
