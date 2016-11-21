using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class blindScroll : MonoBehaviour {

	public float speed = 10;
	public Image blind;
	public Vector3 dest;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 c = blind.rectTransform.anchoredPosition;
		blind.rectTransform.anchoredPosition = Vector3.MoveTowards(c, dest, speed * Time.deltaTime);
	}
}
