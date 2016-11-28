using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class GameControl : MonoBehaviour {



	public enum RoundPhases {
		rp_WesterosCards,
		rp_OrderPlacement,
		rp_Raids,
		rp_Marches,
		rp_Consolidate
	}

	[System.Serializable]
	public struct ArmyOrderTxt {
		public ArmyControler.ArmyOrderType orderType;
		public Sprite orderIcon;
		public string translateID;

	}

	/* Houses colors */
	public Color BaratheonColor;
	public Color LannisterColor;
	public Color MartellColor;
	public Color StarkColor;
	public Color TyrellColor;
	public Color GreyjoyColor;

	public Sprite BaratheonCrest;
	public Sprite LannisterCrest;
	public Sprite MartellCrest;
	public Sprite StarkCrest;
	public Sprite TyrellCrest;
	public Sprite GreyjoyCrest;

	public Sprite BaratheonPowerTokens;
	public Sprite LannisterPowerTokens;
	public Sprite MartellPowerTokens;
	public Sprite StarkPowerTokens;
	public Sprite TyrellPowerTokens;
	public Sprite GreyjoyPowerTokens;

	//players
	public Player mainPlayer; //local human player
	public Player[] players; //all players including mainPlayer
	private int currentPlayerID = 1; //value from 1 to 6, from a Iron Throne influence track

	public int wildlingsStrength;
	public int currentRound;
	public RoundPhases roundPhase;

	public GameObject gameMenu;

	//notifycations
	public NotifyPanelControl topNotify;
	
	public List<ArmyOrderTxt> armyOrderIconPack = new List<ArmyOrderTxt>();

	//list of orders blocked for this turn
	public List<Player.ArmyOrders> blockedTypeOfOrders = new List<Player.ArmyOrders>();


	// Use this for initialization
	void Start () {
		//do i want to display owner color indicator over every lands?
		PlayerPrefs.SetString("SHOW_OWNER_COLOR_INDICATOR","N");

		//tmp select language
		PlayerPrefs.SetString("SELECTED_LANGUAGE","POLISH");

		//recalculateSupplies() ;

		players = GetComponentsInChildren<Player>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!gameMenu.activeInHierarchy) gameMenu.SetActive(true); else gameMenu.SetActive(false);
		}

		//TODO: temporary game phase switcher, delete this
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			goToNextPhase();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			nextPlayerMove();
		}
	}

	//loads translations from dictionary, dependant of the player pref: SELECTED_LANGUAGE
	public string getTranslation(string ID) {
		string r = "???";
		gameObject.GetComponent<LanguageControl>().translation.TryGetValue(ID, out r);
		return r;
	}

	//TODO: output info of who lost, who gained
	public void recalculateSupplies() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject p in players) {
			p.GetComponent<Player>().supplyPos = p.GetComponent<Player>().barrelsOwned;
		}
	}

	//gets translation of current game phase
	public string getRoundPhase() {
		if (roundPhase == RoundPhases.rp_WesterosCards) return getTranslation("GAME_PHASE_01");
		else if (roundPhase == RoundPhases.rp_OrderPlacement) return getTranslation("GAME_PHASE_02");
		else if (roundPhase == RoundPhases.rp_Raids) return getTranslation("GAME_PHASE_03");
		else if (roundPhase == RoundPhases.rp_Marches) return getTranslation("GAME_PHASE_04");
		else return getTranslation("GAME_PHASE_05");
	}

	//switches game state to next phase (increments round if it was the last phase)
	private void goToNextPhase() {
		//new phase = player queue start from 1
		currentPlayerID = 1;

		if (roundPhase == RoundPhases.rp_WesterosCards) roundPhase = RoundPhases.rp_OrderPlacement;
		else if (roundPhase == RoundPhases.rp_OrderPlacement) roundPhase = RoundPhases.rp_Raids;
		else if (roundPhase == RoundPhases.rp_Raids) roundPhase = RoundPhases.rp_Marches;
		else if (roundPhase == RoundPhases.rp_Marches) roundPhase = RoundPhases.rp_Consolidate;
		else if (roundPhase == RoundPhases.rp_Consolidate) {
			currentRound++;
			roundPhase = RoundPhases.rp_WesterosCards;
		}
		bool showPlayer = false;

		if (roundPhase == RoundPhases.rp_Raids || 
		    roundPhase == RoundPhases.rp_Marches || 
		    roundPhase == RoundPhases.rp_Consolidate) showPlayer = true;

		//at start of new round recalculate each player
		if (roundPhase == RoundPhases.rp_WesterosCards) {
			foreach (Player p in players) {
				p.newRound();
			}
		}

		topNotify.setNotify(currentRound, getRoundPhase(), showPlayer);

	}

	public Player getCurrentPlayer() {
		foreach (Player p in players) {
			if (p.ironThronePos == currentPlayerID) {
				return p; 
			}
		}
		return null;
	}

	private void nextPlayerMove() {
		currentPlayerID++;


		//TODO: check if all orders of current type has been executed, if so, go to next phase
		if (currentPlayerID > 6) {
			currentPlayerID = 1;
		}

		if (roundPhase == RoundPhases.rp_Raids || 
		    roundPhase == RoundPhases.rp_Marches || 
		    roundPhase == RoundPhases.rp_Consolidate) topNotify.setNotify(currentRound, getRoundPhase(), true);


	}


}
