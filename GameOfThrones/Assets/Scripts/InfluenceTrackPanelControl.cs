using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfluenceTrackPanelControl : MonoBehaviour {

	public enum TrackTtype {
		ttIronThrone,
		ttKingsCourt,
		ttFiefdoms
	}
	

	public Image[] ironThroneTrack = new Image[6];
	public Image[] kingsCourtTrack = new Image[6];
	public Image[] fiefdomsTrack = new Image[6];

	// Use this for initialization
	void Start () {
	

		refreshTracks(TrackTtype.ttIronThrone);
		refreshTracks(TrackTtype.ttKingsCourt);
		refreshTracks(TrackTtype.ttFiefdoms);
	}
	
	// Update is called once per frame
	void Update () {


	}



	public void refreshTracks(TrackTtype track) {
		if (track == TrackTtype.ttIronThrone) refreshIronThrone();
		else if (track == TrackTtype.ttKingsCourt) refreshKingsCourt();
		else refreshFiefdoms();
	}

	#region TracksRefreshing

	private void refreshIronThrone() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject p in players) {
			ironThroneTrack[p.GetComponent<Player>().ironThronePos - 1].sprite = p.GetComponent<Player>().getHouseCrest(); 
		}
	}

	private void refreshKingsCourt() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject p in players) {
			kingsCourtTrack[p.GetComponent<Player>().kingsCourtPos - 1].sprite = p.GetComponent<Player>().getHouseCrest(); 
		}
	}

	private void refreshFiefdoms() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		foreach (GameObject p in players) {
			fiefdomsTrack[p.GetComponent<Player>().fiefdomPos - 1].sprite = p.GetComponent<Player>().getHouseCrest(); 
		}
	}
	#endregion
}
