using UnityEngine;
using System;
using System.Collections;

public class Message : MonoBehaviour {

	public Player sender;
	public Player recipient;
	public string msgID;
	public string paramX;
	public string paramY;
	public string paramZ;


	private DateTime msgTime;
	private string msgTranslated;
	private GameObject GC;

	// Use this for initialization
	void Start () {
		GC = GameObject.FindGameObjectWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//sets up message
	//params x,y,z should come in already color-formatted
	//
	public void setMessage(string ID, string x, string y, string z, Player from, Player to) {
		this.msgID = ID;
		this.paramX = x;
		this.paramY = y;
		this.paramZ = z;
		this.sender = from;
		this.recipient = to;
		this.msgTime = DateTime.Now;


		
		msgTranslated = GC.GetComponent<GameControl>().getTranslation(msgID); 

		msgTranslated.Replace("%x",x);
		msgTranslated.Replace("%y",y);
		msgTranslated.Replace("%z",z);
	}

	public string getTimeStamp() {
		return this.msgTime.ToString();
	}
}
