using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotifyPanelControl : MonoBehaviour {

	public Image notifyPanel;

	public Color startColor;
	public Color endColor;
	
	public Image notifyGlow;

	public float minPos;
	public float maxPos;
	public float waitForGlow = 11;
	public float flashSpeed = 700;
	public float colorShiftSpeed = 5;
	
	private bool canGo = false;

	private float waitForGlowCount = 0;
	private int alphaOffset = 200;
	private Text[] texts;

	// Use this for initialization
	void Start () {
		texts = notifyPanel.GetComponentsInChildren<Text>();

		//yield return new WaitForSeconds(3);
		//setNotify(4, "Some crazy text");
	}
	
	// Update is called once per frame
	//TODO: optimize
	void Update () {

		//if notify panel has been activated via setNotify method
		//start chaning panel color from startColor to endColor,
		//along with texts alpha value 
		if (canGo) {
			notifyPanel.color = transitionColor(notifyPanel.color, endColor);
			foreach (Text t in texts) fadeText(t, notifyPanel.color.a);
		} 

		//at half alpha of panel start moving glow effect over it
		if (notifyPanel.color.a > 0.5f) {
			waitForGlowCount += Time.deltaTime;

			//if we waited enough since last glow slide
			if (waitForGlowCount >= waitForGlow) {
				waitForGlowCount = 0;
				notifyGlow.rectTransform.anchoredPosition = new Vector2(minPos,0);
				notifyGlow.enabled = true;
			}

			moveGlowEffect();
		}
	}


	//if glow effect is enabled start moving it, along with changing it alpha
	private void moveGlowEffect() {
		if (notifyGlow.enabled) {
			notifyGlow.rectTransform.anchoredPosition = Vector2.MoveTowards(notifyGlow.rectTransform.anchoredPosition,new Vector2(maxPos,0), Time.deltaTime * flashSpeed);
			
			if (notifyGlow.rectTransform.anchoredPosition.x == maxPos) {
				notifyGlow.enabled = false;
			}
			
			Color t = notifyGlow.color;
			notifyGlow.color = new Color(t.r, t.g, t.b, (alphaOffset - Mathf.Abs(notifyGlow.rectTransform.anchoredPosition.x)/2)/255);
		}
	}

	//change current color towards endColor
	private Color transitionColor(Color start, Color end) {
		return Color.Lerp(start, end, Time.deltaTime * colorShiftSpeed);
	}


	private void fadeText(Text txtToFade, float alpha) {
		Color c = txtToFade.color;
		txtToFade.color = new Color(c.r, c.g, c.b, alpha);
	}


	public void setNotify(int round, string infoText, bool showCurrentPlayer = false) {
		//hide current text
		foreach (Text t in texts) fadeText(t, 0);
		GameObject gc = GameObject.FindGameObjectWithTag("GameController");

		texts[0].text = gc.GetComponent<GameControl>().getTranslation("LABEL_GAMEROUND_NO") + " " + round.ToString() + " : ";
		texts[1].text = " " +infoText;

		if (showCurrentPlayer) texts[1].text += " (" + gc.GetComponent<GameControl>().getCurrentPlayer().house + " )";

		//set up notify panel
		canGo = true;
		waitForGlowCount = waitForGlow;
		notifyPanel.color = startColor;
	}


}
