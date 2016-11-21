using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrderButton : MonoBehaviour {

	public ArmyControler.ArmyOrderType armyOrderType;

	private Color usedColor;
	private Color blockedColor;
	private ColorBlock cb;


	// Use this for initialization
	void Awake () {
		Color.TryParseHexString("A1A1A1B0",out usedColor);
		Color.TryParseHexString("D50404BB", out blockedColor);

		cb = gameObject.GetComponent<Button>().colors;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Disables the button.
	/// </summary>
	/// <param name="isBlocked">If set to <c>true</c> is blocked.</param>
	public void disableButton(bool isBlocked = false) {

		//Debug.Log("Block: " + this.armyOrderType.ToString());

		//set a color of a disabled button according to reason why its blocked
		if (isBlocked) {
			cb.disabledColor = blockedColor;
		} else {
			cb.disabledColor = usedColor;
		}
		gameObject.GetComponent<Button>().colors = cb;

		//disable button
		gameObject.GetComponent<Button>().interactable = false;
	
	}

	/// <summary>
	/// Enables the button. 
	/// </summary>
	public void enableButton() {
		gameObject.GetComponent<Button>().interactable = true;
	}
}
