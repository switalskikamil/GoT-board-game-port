using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class ArmyOrders {
	public ArmyControler.ArmyOrderType orderType;
	public bool used;
	public bool blocked;
}

public class Player : MonoBehaviour {

	public enum HouseName {
		Baratheon,
		Lannister,
		Martell,
		Stark,
		Tyrell,
		Greyjoy,
		None
	}

	public enum RavenTokenState {
		rts_notPossesed,
		rts_alreadyUsed,
		rts_notUsed,
		rts_cantUse,
		rts_Active
	}

	public enum PlayerType {
		LOCAL_PLAYER,
		REMOTE_PLAYER,
		AI_PLAYER
	}

	/* PUBLIC VAR */
	public string playerName;
	public HouseName house;
	public int powerTokens;
	public int castlesOwned;
	public int barrelsOwned;
	public int crownsOwned;
	public RavenTokenState ravenTokenState;

	public PlayerType playerType;

	//positions on tracks
	public int ironThronePos; //pos = 1 means he own a dominance token
	public int kingsCourtPos;
	public int fiefdomPos;
	public int supplyPos;

	//army orders
	public List<ArmyOrders> armyOrderPack = new List<ArmyOrders>();

	/* PRIVATE VAR */
	private Color playerColor;
	private Sprite houseCrest;
	private Sprite powerTokensIcon;


	// Use this for initialization
	void Start () {
		//get player color
		GameObject gc = GameObject.FindGameObjectWithTag("GameController");
		
		if (gc != null) {
			if (house == HouseName.Baratheon) {
				playerColor = gc.GetComponent<GameControl>().BaratheonColor;
				houseCrest = gc.GetComponent<GameControl>().BaratheonCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().BaratheonPowerTokens;
			}
			else if (house == HouseName.Lannister) {
				playerColor = gc.GetComponent<GameControl>().LannisterColor;
				houseCrest = gc.GetComponent<GameControl>().LannisterCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().LannisterPowerTokens;
			}
			else if (house == HouseName.Martell) {
				playerColor = gc.GetComponent<GameControl>().MartellColor;
				houseCrest = gc.GetComponent<GameControl>().MartellCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().MartellPowerTokens;
			}
			else if (house == HouseName.Stark) {
				playerColor = gc.GetComponent<GameControl>().StarkColor;
				houseCrest = gc.GetComponent<GameControl>().StarkCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().StarkPowerTokens;
			}
			else if (house == HouseName.Tyrell) {
				playerColor = gc.GetComponent<GameControl>().TyrellColor;
				houseCrest = gc.GetComponent<GameControl>().TyrellCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().TyrellPowerTokens;
			}
			else if (house == HouseName.Greyjoy) {
				playerColor = gc.GetComponent<GameControl>().GreyjoyColor;
				houseCrest = gc.GetComponent<GameControl>().GreyjoyCrest;
				powerTokensIcon = gc.GetComponent<GameControl>().GreyjoyPowerTokens;
			}
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public Color getPlayerColor() {
		return playerColor;
	}

	public Sprite getHouseCrest() {
		return houseCrest;
	}

	public int getHouseCardsInHand() {
		Card[] cards = gameObject.GetComponentsInChildren<Card>();

		int i = 0;
		foreach (Card c in cards) {
			if (c.cardState == Card.CardState.cs_cardInHand) i++;
		}
		return i;
	}

	public Sprite getPowerTokensIcon() {
		return powerTokensIcon;
	}

	/// <summary>
	/// Refreshs the state of the raven token.
	/// </summary>
	public void refreshRavenTokenState() {
		//if player has no 1 pos in fiefdom track
		if (fiefdomPos == 1) {
			ravenTokenState = RavenTokenState.rts_notUsed;
		} else {
			ravenTokenState = RavenTokenState.rts_notPossesed;
		}

	}

	/// <summary>
	/// Uses the raven token.
	/// </summary>
	/// <param name="activatedAndUsed">If set to <c>true</c> activated and used.</param>
	public void useRavenToken(bool activatedAndUsed = false) {
		if (activatedAndUsed) {
			ravenTokenState = RavenTokenState.rts_alreadyUsed;
		} else {
			ravenTokenState = RavenTokenState.rts_Active;
		}
	}

	/// <summary>
	/// Locks or Unlocks the order.
	/// </summary>
	/// <param name="orderType">Order type.</param>
	/// <param name="orderIsUsed">If set to <c>true</c> order is used.</param>
	/// <param name="orderIsBlocked">If set to <c>true</c> order is blocked.</param>
	public void lockOrder(ArmyControler.ArmyOrderType orderType, bool orderIsUsed, bool orderIsBlocked) {
		foreach (ArmyOrders a in  armyOrderPack) {
			if (a.orderType == orderType) {
				a.blocked = orderIsBlocked;
				a.used = orderIsUsed;
				break;
			}
		}
	}

	/// <summary>
	/// Gets the locked orders.
	/// </summary>
	/// <returns>The locked orders.</returns>
	public List<ArmyControler.ArmyOrderType> getLockedOrders() {

		List<ArmyControler.ArmyOrderType> lockedOrders = new List<ArmyControler.ArmyOrderType>();

		foreach (ArmyOrders a in  armyOrderPack) {
			if (a.blocked || a.used) {
				lockedOrders.Add(a.orderType);
			}
		}

		//debug
		/*Debug.Log("Locked Orders: ");
		foreach (ArmyControler.ArmyOrderType a in lockedOrders) {
			Debug.Log("  - " + a.ToString());
		}
		Debug.Log("---");*/

		return lockedOrders;
	}

	public int getNumberOfSpecialOrders() {
		int r = 0;

		switch (this.fiefdomPos) {
		case 1: 
			r = 3;
			break;
		case 2: 
			r = 3;
			break;
		case 3:
			r = 2;
			break;
		case 4:
			r = 1;
			break;
		case 5: 
			r = 0;
			break;
		case 6: 
			r = 0;
			break;
		default:
			break;
		}
	}

}
