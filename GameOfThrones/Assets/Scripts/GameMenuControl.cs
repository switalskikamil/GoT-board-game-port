using UnityEngine;
using System.Collections;

public class GameMenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void resumeGame() {
		gameObject.SetActive(false);
	}

	public void closeGame() {
		Application.Quit();
	}
}
