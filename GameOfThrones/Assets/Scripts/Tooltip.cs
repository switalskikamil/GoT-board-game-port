using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

	public enum TooltipType {
		TOOLTIP_LAND,
		TOOLTIP_ARMY,
		TOOLTIP_CASTLE
	}

	/* PUBLIC VAR */
	public TooltipType type;
	public string objectDescription;
	public GameObject tooltipPanel;
	public Text tt_Title;

	/* PRIVATE VAR */
	//private string objectName;


	// Use this for initialization
	void Start () {
		tooltipPanel.GetComponent<TooltipControl>().init();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTooltip() {

		if (type == TooltipType.TOOLTIP_LAND) { 
			tooltipPanel.GetComponent<TooltipControl>().setData(type);
		}
	}

	public void showHideTooltip(bool v) {
		tooltipPanel.SetActive(v);
		if (v) setTooltip();

	}
}
