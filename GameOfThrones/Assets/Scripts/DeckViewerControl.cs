using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckViewerControl : MonoBehaviour {

	public Deck deck;


	public Color cardDiscardedColor;
	public Color cardDisabledColor;
	public Sprite swordIcon;
	public Sprite towerIcon;


	public Card currentCard;
	public Card selectedCard;

	public Text cardLabel;
	public Text cardDescription;
	public Image portrait;
	public Text combatValue;
	public Text cardCounter;

	public GameObject cardDisabler;
	public Image cardDisablerSign;
	public GameObject cardDescriptor;
	public GameObject cardAddStats;

	public Image specialStatIcon_1;
	public Image specialStatIcon_2;
	public Image specialStatIcon_3;

	private GameObject gc;
	private Card[] cards = new Card[10];
	private int cardNo;
	private int cardMax;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController");


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadCard() {
		if (gc == null) gc = GameObject.FindGameObjectWithTag("GameController");

		currentCard = cards[cardNo];
		cardLabel.text = gc.GetComponent<GameControl>().getTranslation(currentCard.cardID.ToString() + "_LABEL"); 
		cardDescription.text = gc.GetComponent<GameControl>().getTranslation(currentCard.cardID.ToString() + "_DESCR"); 
		portrait.sprite = currentCard.cardSprite;
		combatValue.text = currentCard.combatStr.ToString();

		if (currentCard.cardState == Card.CardState.cs_cardInDiscard) {
			cardDisabler.SetActive(true);
			cardDisablerSign.color = cardDiscardedColor;
		} else if (currentCard.cardState == Card.CardState.cs_cardDisabled) {
			cardDisabler.SetActive(true);
			cardDisablerSign.color = cardDisabledColor;
		} else {
			cardDisabler.SetActive(false);
		}
		cardCounter.text = "( " + (cardNo+1).ToString() + " / " + cardMax.ToString() + " )"; 

		setSpecialStats();
	}

	public void loadDeck(Deck newDeck) {
		deck = newDeck;

		cards = deck.GetComponentsInChildren<Card>();

		cardMax = 0;
		foreach (Card c in cards) {
			if (c != null) cardMax ++;
		}

		cardNo = 0;
		currentCard = cards[cardNo];


		gameObject.SetActive(true);
		loadCard();

	}

	public void nextCard() {
		if (cardNo < cardMax-1) cardNo++; else cardNo = 0;
		loadCard();
	}

	public void prevCard() {
		if (cardNo > 0) cardNo--; else cardNo = cardMax-1;
		loadCard();
	}

	public void select() {
		selectedCard = currentCard;
		gameObject.SetActive(false);
	}

	private void setSpecialStats() {
		specialStatIcon_1.enabled = false;
		specialStatIcon_2.enabled = false;
		specialStatIcon_3.enabled = false;


		int takenSlots = 0;

		if (currentCard.swordsIcons + currentCard.towersIcons > 0) {

			cardAddStats.SetActive(true);

			if (currentCard.swordsIcons == 3) {
				specialStatIcon_1.sprite = swordIcon; specialStatIcon_1.enabled = true;
				specialStatIcon_2.sprite = swordIcon; specialStatIcon_2.enabled = true;
				specialStatIcon_3.sprite = swordIcon; specialStatIcon_3.enabled = true;
				takenSlots = 3;
			} else if  (currentCard.swordsIcons == 2) {
				specialStatIcon_1.sprite = swordIcon; specialStatIcon_1.enabled = true;
				specialStatIcon_2.sprite = swordIcon; specialStatIcon_2.enabled = true;
				takenSlots = 2;
			} else if (currentCard.swordsIcons == 1) 
			{
				specialStatIcon_1.sprite = swordIcon; specialStatIcon_1.enabled = true;
				takenSlots = 1;
			}

			if (currentCard.towersIcons == 3) {
				specialStatIcon_1.sprite = towerIcon; specialStatIcon_1.enabled = true;
				specialStatIcon_2.sprite = towerIcon; specialStatIcon_2.enabled = true;
				specialStatIcon_3.sprite = towerIcon; specialStatIcon_3.enabled = true;
				takenSlots = 3;
			} else if (currentCard.towersIcons == 2) {
				if (takenSlots == 0) {
					specialStatIcon_1.sprite = towerIcon; specialStatIcon_1.enabled = true;
					specialStatIcon_2.sprite = towerIcon; specialStatIcon_2.enabled = true;
					takenSlots = 2;
				} else if (takenSlots == 1) {
					specialStatIcon_2.sprite = towerIcon; specialStatIcon_2.enabled = true;
					specialStatIcon_3.sprite = towerIcon; specialStatIcon_3.enabled = true;
					takenSlots = 3;
				}
			} else if (currentCard.towersIcons == 1) {
				if (takenSlots == 0) {
					specialStatIcon_1.sprite = towerIcon; specialStatIcon_1.enabled = true;
					takenSlots = 1;
				} else if (takenSlots == 1) {
					specialStatIcon_2.sprite = towerIcon; specialStatIcon_2.enabled = true;
					takenSlots = 2;
				} else if (takenSlots == 2) {
					specialStatIcon_3.sprite = towerIcon; specialStatIcon_3.enabled = true;
					takenSlots = 3;
				}
			}
		} else {
			cardAddStats.SetActive(false);
		}

		repositionSpecialStatIcons(takenSlots);
	}

	private void repositionSpecialStatIcons(int takenSlots) {
		float offsetY = -35.0f;

		if (takenSlots != 0) {
			if (takenSlots == 1) {
				specialStatIcon_1.rectTransform.anchoredPosition = new Vector3(0, offsetY, 0);
			} else if (takenSlots == 2) {
				specialStatIcon_1.rectTransform.anchoredPosition = new Vector3(-30, offsetY, 0);;
				specialStatIcon_2.rectTransform.anchoredPosition = new Vector3(30, offsetY, 0);;
			} else if (takenSlots == 3) {
				specialStatIcon_1.rectTransform.anchoredPosition = new Vector3(-50, offsetY, 0);;
				specialStatIcon_2.rectTransform.anchoredPosition = new Vector3(0, offsetY, 0);;
				specialStatIcon_3.rectTransform.anchoredPosition = new Vector3(50, offsetY, 0);;
			}
		}
	}
}
