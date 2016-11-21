using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPanelControl : MonoBehaviour {

	public Player owner;

	public Image houseCrest;
	public Image deckReverse;
	public Image powerTokenIcon;
	public Text houseName;
	public Text playerName;
	public Text castleCounter;
	public Text powerCounter;
	public Text barrelsCounter;
	public Text crownsCounter;
	public Text cardsInHandCounter;
	
	public bool moveable;
	public GameObject deckViewer;
	public Camera mainCamera;

	private GameObject GC;


	// Use this for initialization
	void Start () {
		GC =  GameObject.FindWithTag("GameController");

		deckReverse.sprite = owner.GetComponentInChildren<Deck>().deckReverse;
		powerTokenIcon.sprite = owner.getPowerTokensIcon();

	}
	
	// Update is called once per frame
	void Update () {
		//TODO: optimize
		refreshPanel();
	
	}

	public void refreshPanel() {
		houseCrest.sprite = owner.getHouseCrest();
		houseName.text = GC.GetComponent<GameControl>().getTranslation("LABEL_HOUSE") + " " + owner.house.ToString();
		houseName.color = owner.getPlayerColor();
		playerName.text = owner.playerName;
		castleCounter.text = "" + owner.castlesOwned.ToString(); 
		powerCounter.text = owner.powerTokens.ToString();
		crownsCounter.text = owner.crownsOwned.ToString();
		cardsInHandCounter.text = "(" + owner.getHouseCardsInHand().ToString() + ")";
		barrelsCounter.text = owner.barrelsOwned.ToString();
	}

	

	public void loadPlayerDeck() {
		deckViewer.GetComponent<DeckViewerControl>().loadDeck(owner.GetComponentInChildren<Deck>());
	}

	public void centerCameraAtHouseHome() {
		//mainCamera.
	}
}
