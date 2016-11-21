using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SupplyPanelControl : MonoBehaviour {


	public Image[] track_00 = new Image[6];
	public Image[] track_01 = new Image[6];
	public Image[] track_02 = new Image[6];
	public Image[] track_03 = new Image[6];
	public Image[] track_04 = new Image[6];
	public Image[] track_05 = new Image[6];
	public Image[] track_06 = new Image[6];
	



	// Use this for initialization
	void Start () {

	

		refreshSupplyTrack();
	}
	
	// Update is called once per frame
	void Update () {

	}


	/*
	 * Depending on number of barrels owned by a player,
	 * he will have a certain position on a supply track
	 * however this track is not refreshed automaticaly
	 * after losing or obtaining any barrel. This track is 
	 * only updated once a Supply Card from Westereos Deck
	 * has been drawn. Thats why during refresh action, this
	 * method will only check current state of supplyPos field
	 * and not count barrels.
	 */
	public void refreshSupplyTrack() {
		clearCrestsFromTracks();

		//next free position on a track with ID
		int[] nextPosOnTrack = new int[7]; //values from 0-5, default all = 5
		for (int i = 0; i < 7; i++) nextPosOnTrack[i] = 5;


		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject p in players) {
			int suppPosition = p.GetComponent<Player>().supplyPos;
			int posID = nextPosOnTrack[suppPosition];

			if (suppPosition == 0) {
				track_00[posID].enabled = true;
				track_00[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			}
			else if (suppPosition == 1) {
				track_01[posID].enabled = true;
				track_01[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			} 
			else if (suppPosition == 2) {
				track_02[posID].enabled = true;
				track_02[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			}
			else if (suppPosition == 3) {
				track_03[posID].enabled = true;
				track_03[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			}
			else if (suppPosition == 4) {
				track_04[posID].enabled = true;
				track_04[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			}
			else if (suppPosition == 5) {
				track_05[posID].enabled = true;
				track_05[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			}
			else if (suppPosition == 6) {
				track_06[posID].enabled = true;
				track_06[posID].sprite = p.GetComponent<Player>().getHouseCrest();
			} 
			//this.GetType().GetField("track_0"+
			nextPosOnTrack[suppPosition]--;
		}

	}


	private void clearCrestsFromTracks() {
		for (int i = 0; i < 6; i++) {
			track_00[i].enabled = false;
			track_01[i].enabled = false;
			track_02[i].enabled = false;
			track_03[i].enabled = false;
			track_04[i].enabled = false;
			track_05[i].enabled = false;
			track_06[i].enabled = false;
		}
	}




}
