﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArmyOrderTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	//public Transform cameraPosition;
	public float rotationSpeed;
	public GameObject orderChangePanel;
	public bool zoomLocked = false;

	private Camera mainCam;
	private Quaternion defaultRotation;
	private Vector3 defaultScale;
	private Quaternion currentRotation;
	private Quaternion targetRotation;
	private Vector3 currentScale;
	private Vector3 targetScale;

	private ArmyControler armyControler;
	private GameControl GC;

	// Use this for initialization
	void Start () {
		//store information about current state of ui panel
		defaultRotation = gameObject.GetComponent<RectTransform>().rotation;
		defaultScale = gameObject.GetComponent<RectTransform>().localScale;
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		currentScale = defaultScale;
		targetScale = defaultScale;

		//get reference to a army controler and game controler
		armyControler = gameObject.GetComponentInParent<ArmyControler>();
		GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
		//if current state is not a target state -> animate it!
		if (currentRotation != targetRotation) gameObject.GetComponent<RectTransform>().rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
		if (currentScale != targetScale) gameObject.GetComponent<RectTransform>().localScale = Vector3.Slerp(currentScale, targetScale, Time.deltaTime * rotationSpeed);
		currentRotation = gameObject.GetComponent<RectTransform>().rotation;
		currentScale = gameObject.GetComponent<RectTransform>().localScale;
	}

	/// <summary>
	/// Raises the pointer enter event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		targetRotation = Quaternion.LookRotation(mainCam.transform.forward);
		targetScale = defaultScale * (2 * mainCam.GetComponent<CameraControl>().getCameraZoomLvl());
	}

	/// <summary>
	/// Raises the pointer exit event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit(PointerEventData eventData)
	{
		if (!zoomLocked) {
			targetRotation = defaultRotation;
			targetScale = defaultScale;
		}
	}

	/// <summary>
	/// Displays menu for assaigning order to army
	/// </summary>
	public void assignOrderEvent() {
		//hides other opened order assign panels
		hideAllOtherOrderAssignPanels();

		//if panel is already on, then disable it and dont change order
		if (orderChangePanel.activeInHierarchy) {
			//hide panel for order switching
			orderChangePanel.SetActive(false);

			//turn of zoomlock, so order panel can return to original position after pointer exit event
			zoomLocked = false;
		} else 
		//if current player is owner of this army
		if (GC.mainPlayer == armyControler.owner) {
			//if orders still can be changed 
			if (GC.roundPhase == GameControl.RoundPhases.rp_OrderPlacement) {
				//lock zoom, so player can changed order without raising pointer exit event
				zoomLocked = true;

				//display panel to change order
				orderChangePanel.SetActive(true);
			}
			// if raven token has been used
			else if (GC.roundPhase == GameControl.RoundPhases.rp_Raids && armyControler.owner.ravenTokenState == Player.RavenTokenState.rts_Active ) {

				//after order has been changed, we need to turn of raven token
				armyControler.owner.useRavenToken(true);
			}

		}

		//check orders, mark used and blocked
		checkOrderAvailability();
	}

	/// <summary>
	/// Assigns the order. Takes string verions of enum order. Passes enum value to armyControler.
	/// </summary>
	/// <param name="order">String value of enum order type</param>
	private void assignOrder(string order) {
		//convert string verions to enum order
		ArmyControler.ArmyOrderType enumOrder = (ArmyControler.ArmyOrderType) System.Enum.Parse(typeof(ArmyControler.ArmyOrderType), order);

		//reassign order to this army
		armyControler.changeOrder(enumOrder);

		//refresh availability list, and button display, also hide select order menu
		assignOrderEvent();
	}

	/// <summary>
	/// Checks the order availability. Turns on not-availible mark.
	/// </summary>
	public void checkOrderAvailability() {

		//at first enable all buttons
		enableAllButtons();

	
		int availibleSpecialOrders = armyControler.owner.getNumberOfSpecialOrders();

		//if player can't use special orders, just block them
		//also block if player already used all special orders availible for him
		//but only block unused special orders
		if (availibleSpecialOrders == 0 || availibleSpecialOrders == armyControler.owner.UsedSpecialOrders) {
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoConsolidate_special, false, true));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoMarch_special, false, true));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoSupport_special, false, true));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoDefense_special, false, true));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoRaid_special, false, true));
		} else {
			//if special orders were blocked, now ublock them
			//but check if they were used before unblocking
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoConsolidate_special, armyControler.owner.checkOrderUse(ArmyControler.ArmyOrderType.aoConsolidate_special), false));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoMarch_special, armyControler.owner.checkOrderUse(ArmyControler.ArmyOrderType.aoMarch_special), false));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoSupport_special, armyControler.owner.checkOrderUse(ArmyControler.ArmyOrderType.aoSupport_special), false));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoDefense_special, armyControler.owner.checkOrderUse(ArmyControler.ArmyOrderType.aoDefense_special), false));
			armyControler.owner.lockOrder(new Player.ArmyOrders(ArmyControler.ArmyOrderType.aoRaid_special, armyControler.owner.checkOrderUse(ArmyControler.ArmyOrderType.aoRaid_special), false));
		}


		//loop thru a list in gamecontroler
		foreach (Player.ArmyOrders a in GC.blockedTypeOfOrders) {
			armyControler.owner.lockOrder(a);
		}



		//check players list for already locked orders
		foreach (Player.ArmyOrders a in armyControler.owner.getLockedOrders()) {
			findAndDisableBtn(a.orderType, a.blocked);
		}
		
	}


	/// <summary>
	/// Finds the and disable button.
	/// </summary>
	/// <param name="a">Army Order Type</param>
	/// <param name="isBlocked">If set to <c>true</c> is blocked.</param>
	private void findAndDisableBtn(ArmyControler.ArmyOrderType a, bool isBlocked = false) {

		//get list of all buttons in children list of this panel
		OrderButton[] orderButtons = gameObject.GetComponentsInChildren<OrderButton>();

		foreach (OrderButton ob in orderButtons) {
			//disable button by a given type
			if (ob.armyOrderType == a) {
				//Debug.Log("disable : " + a.ToString());
				ob.disableButton(isBlocked);
			}
		}
	}

	/// <summary>
	/// Enables all buttons.
	/// </summary>
	public void enableAllButtons() { 
		OrderButton[] orderButtons = gameObject.GetComponentsInChildren<OrderButton>();
		
		foreach (OrderButton ob in orderButtons) {
			ob.enableButton();
		}
	}

	/// <summary>
	/// Hides all other order assign panels.
	/// </summary>
	private void hideAllOtherOrderAssignPanels() {
		//get reference to all army order tooltips
		ArmyOrderTooltip[] armies = GameObject.FindObjectsOfType<ArmyOrderTooltip>();

		//for each opened except this one call onlick event and pointer exit (so it hides and zoomouts)
		foreach (ArmyOrderTooltip a in armies) {
			if (a != this && a.zoomLocked) {
				a.assignOrderEvent();
				a.OnPointerExit(null);
			}
		}
	
	}



}
