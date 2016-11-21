using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pawn : MonoBehaviour {

	public enum PawnType {
		pt_SHIP,
		pt_FOOTMAN,
		pt_KNIGHT,
		pt_SIEGE
	}

	public PawnType pawnType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
