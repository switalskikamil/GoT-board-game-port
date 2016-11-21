using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float scrollSpeed;
	public float zoomSpeed;
	public float camMaxPos;
	public float campMinPos;
	public float maxLeft;
	public float maxRight;
	public float minZoom;
	public float maxZoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.y <= 0) {
			moveCamera(Vector3.forward);
		}
		else 
		if (Input.mousePosition.y >= Screen.height-1) {
			moveCamera(Vector3.back);
		}
		else 
		if (Input.mousePosition.x >= Screen.width-1) {
			moveCamera(Vector3.left);
		}
		else 
		if (Input.mousePosition.x <= 0) {
			//Debug.Log(Input.mousePosition.x.ToString());
			moveCamera(Vector3.right);
		}
		float v = Input.GetAxis("Mouse ScrollWheel");

		if (v != 0) {
			zoomCamera(v);
		}

		//cast a ray into mouse position
		//RaycastHit hit = new RaycastHit();
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//if ray hits something - check is it tile
		/*if (Physics.Raycast(ray,out hit)) {
			Debug.Log("hit!");
			if (hit.transform.GetComponentInParent<SpriteRenderer>() != null) {
				//flip clicked tile and add it to list of picked up tiles
				Color c =  hit.transform.GetComponentInParent<SpriteRenderer>().color;
				Color n = new Color(c.r,c.g,c.b,1);
				hit.transform.GetComponentInParent<SpriteRenderer>().color = n;
			}
		}*/

	}

	private void moveCamera(Vector3 dir) {
		//transform.Translate(dir* Time.deltaTime * scrollSpeed, Space.Self);
		Vector3 oldPos = transform.position;
		float moveBy = scrollSpeed * Time.deltaTime;


		Vector3 newPos = new Vector3(oldPos.x + (moveBy * dir.x), oldPos.y + (moveBy * dir.y), oldPos.z + (moveBy * dir.z));

		if (newPos.z >= camMaxPos && newPos.z <= campMinPos && newPos.x <= maxLeft && newPos.x >= maxRight) transform.position = newPos;
	}

	private void zoomCamera(float val) {
		Vector3 oldPos = transform.position;
		float moveBy = zoomSpeed * Time.deltaTime * -1;
	
		Vector3 newPos = new Vector3(oldPos.x , oldPos.y + (moveBy * val), oldPos.z );

		if (newPos.y >= maxZoom && newPos.y <= minZoom) transform.position = newPos;

	}

	public float getCameraZoomLvl() {
		return transform.position.y/minZoom;
	}
}
