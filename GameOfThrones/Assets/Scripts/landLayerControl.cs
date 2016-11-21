using UnityEngine;
using System.Collections;

public class landLayerControl : MonoBehaviour {

	public Color redish;
	public Color greenish;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseEnter() {
	

		Color n = new Color(redish.r, redish.g, redish.b,0.7f);
		changeSpriteColor(n);
	}

	void OnMouseExit() {


		Color n = new Color(redish.r, redish.g, redish.b,0);
		changeSpriteColor(n);
	}

	private void changeSpriteColor(Color n) {
		gameObject.GetComponent<SpriteRenderer>().color = n;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
