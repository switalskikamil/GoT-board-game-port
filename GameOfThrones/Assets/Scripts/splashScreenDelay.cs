using UnityEngine;
using System.Collections;

public class splashScreenDelay : MonoBehaviour {

	public float delayTime = 10;

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(delayTime);

		Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
