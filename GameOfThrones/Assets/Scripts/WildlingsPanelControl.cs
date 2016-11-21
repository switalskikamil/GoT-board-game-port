using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WildlingsPanelControl : MonoBehaviour {

	public bool isShown;
	
	public Vector3 shownPos;
	public Vector3 hidPos;
	public float slideSpeed;
	
	public Text barLabel;
	public Text strengthLabel;
	public Text strengthValue;
	
	private Vector3 destPos;
	private GameObject gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController");
		barLabel.text = gc.GetComponent<GameControl>().getTranslation("LABEL_WILDLINGS");
		strengthLabel.text = gc.GetComponent<GameControl>().getTranslation("LABEL_WILDLINGS_STR");
		destPos = gameObject.GetComponent<RectTransform>().anchoredPosition; 
	}
	
	// Update is called once per frame
	void Update () {
		movePanel();

		strengthValue.text = gc.GetComponent<GameControl>().wildlingsStrength.ToString(); 
	}

	public void changeState() {
		if (isShown) {
			isShown = false; 
			destPos = hidPos;
		} else {
			isShown = true;
			destPos = shownPos;
		}
	}
	
	public void movePanel() {
		Vector3 currentPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
		if (currentPos != destPos) {
			gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(currentPos, destPos, slideSpeed * Time.deltaTime);
		}
	}
}
