using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelSlider : MonoBehaviour {

	public float slideSpeed;
	public Text barLabel;
	public bool startShown;
	public string panelNameToken;
	public Vector2 shownPos;
	public Vector2 hidPos;
	
	private Vector2 destPos;
	private GameObject gc;


	// Use this for initialization
	void Start () {


		if (startShown) {
			gameObject.GetComponent<RectTransform>().anchoredPosition = shownPos;
		} else {
			gameObject.GetComponent<RectTransform>().anchoredPosition = hidPos;
		}

		gc = GameObject.FindGameObjectWithTag("GameController");	

		barLabel.text = gc.GetComponent<GameControl>().getTranslation(panelNameToken);
		destPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
		movePanel();
	}

	public void movePanel() {
		Vector2 currentPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
		if (currentPos != destPos) {
			gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(currentPos, destPos, slideSpeed * Time.deltaTime);
		}
	}



	public void slide() {
		if (gameObject.GetComponent<RectTransform>().anchoredPosition == shownPos) destPos = hidPos; else destPos = shownPos;
	}
}
