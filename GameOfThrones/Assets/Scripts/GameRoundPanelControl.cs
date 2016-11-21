using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRoundPanelControl : MonoBehaviour {



	public Text roundNoLabel;
	public Text roundNoValue;
	public Text phaseLabel;
	public Text phaseValue;

	//wildlings
	public Text wildStrengthLabel;
	public Text wildStrengthValue;
	
	private GameObject gc;

	// Use this for initialization
	void Start () {

		gc = GameObject.FindGameObjectWithTag("GameController");

		roundNoLabel.text = gc.GetComponent<GameControl>().getTranslation("LABEL_GAMEROUND_NO"); 
		phaseLabel.text = gc.GetComponent<GameControl>().getTranslation("LABEL_GAMEROUND_PHASE");

		wildStrengthLabel.text = gc.GetComponent<GameControl>().getTranslation("LABEL_WILDLINGS_STR");

		updateTexts();
	}
	
	// Update is called once per frame
	void Update () {


		wildStrengthValue.text = gc.GetComponent<GameControl>().wildlingsStrength.ToString(); 
		 
		//TODO: optimize
		updateTexts();
	}

	public void updateTexts() {
		roundNoValue.text = gc.GetComponent<GameControl>().currentRound.ToString();

		phaseValue.text = gc.GetComponent<GameControl>().getRoundPhase();
	}
	
	
}
