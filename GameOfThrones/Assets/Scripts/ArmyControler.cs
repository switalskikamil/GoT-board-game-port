using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArmyControler : MonoBehaviour {

	public enum ArmyOrderType {
		aoNone,
		aoMarch_lower,
		aoMarch_normal,
		aoMarch_special,
		aoDefense_lower,
		aoDefense_normal,
		aoDefense_special,
		aoSupport_lower,
		aoSupport_normal,
		aoSupport_special,
		aoRaid_lower,
		aoRaid_normal,
		aoRaid_special,
		aoConsolidate_lower,
		aoConsolidate_normal,
		aoConsolidate_special
	}

	public ArmyOrderType orderAssigned;
	public Player owner;
	public Button orderAssignedBtn;

	private GameControl GC;

	// Use this for initialization
	void Start () {
		GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Changes the order.
	/// </summary>
	/// <param name="aot">new army order type</param>
	public void changeOrder(ArmyOrderType aot) {
		//unlock previous order
		owner.lockOrder(new Player.ArmyOrders(orderAssigned, false, false));

		//lock new order
		owner.lockOrder(new Player.ArmyOrders(aot, true, false));

		if (aot == ArmyControler.ArmyOrderType.aoConsolidate_special ||
		    aot == ArmyControler.ArmyOrderType.aoDefense_special ||
		    aot == ArmyControler.ArmyOrderType.aoMarch_special ||
		    aot == ArmyControler.ArmyOrderType.aoRaid_special ||
		    aot == ArmyControler.ArmyOrderType.aoSupport_special) {

			//if we just used a special order
			owner.UsedSpecialOrders++; 
		}

		if (orderAssigned == ArmyControler.ArmyOrderType.aoConsolidate_special ||
		    orderAssigned == ArmyControler.ArmyOrderType.aoDefense_special ||
		    orderAssigned == ArmyControler.ArmyOrderType.aoMarch_special ||
		    orderAssigned == ArmyControler.ArmyOrderType.aoRaid_special ||
		    orderAssigned == ArmyControler.ArmyOrderType.aoSupport_special) {
			
			//if we just freed a special order
			owner.UsedSpecialOrders--; 
		}

		orderAssigned = aot;

		//assaign icon and text of new order
		foreach (GameControl.ArmyOrderTxt o in GC.armyOrderIconPack) {
			if (o.orderType == aot) {
				orderAssignedBtn.image.sprite = o.orderIcon;
				orderAssignedBtn.GetComponentInChildren<Text>().text = GC.getTranslation(o.translateID);
				break;
			}
		}

	}
}
