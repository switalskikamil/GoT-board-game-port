using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

	public enum DeckType {
		dtHouseDeck,
		dtWildlings,
		dtWesteros_1,
		dtWesteros_2,
		dtWesteros_3
	}

	public DeckType deckType;
	public Sprite deckReverse;
	//public List<Card> cards = new List<Card>();



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
