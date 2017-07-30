using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

	Tile myTile;

	WorldManager manager;

	public int orientation;

	// Use this for initialization
	void Start () {
		manager = Camera.main.GetComponent<WorldManager> ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(Tile startTile){
		//set the tile to start in
		myTile = startTile;
		Vector3 pos = new Vector3(0,0,-1);
		pos.x = myTile.Xpos;
		pos.y = myTile.Ypos;
		transform.position = pos;
		orientation = 1;
	}

	public void rotate(){
		float rotation = 90f;
		StartCoroutine( rotateMe (rotation));
	}

	public int moveMirror(string direction){
		Tile neighbor = manager.getNeighbor (myTile, direction);
		if (neighbor.getObject() != "none") {
			return 0;
		}
		manager.resetLasers ();
		myTile.changeObject ("none",null);
		neighbor.changeObject("mirror",gameObject);
		Vector3 origPos = transform.position;
		Vector3 newPos = new Vector3(0,0,0);
		newPos.x = neighbor.Xpos;
		newPos.y = neighbor.Ypos;
		newPos.z = origPos.z;
		this.myTile = neighbor;


		StartCoroutine(moveMe(origPos, newPos));
		return 1;

	}

	IEnumerator moveMe(Vector3 origPos, Vector3 newPos){

		float elapsedTime = 0;
		float time = 0.3f;
		while (elapsedTime < time) {
			transform.position = Vector3.Lerp (origPos, newPos, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		manager.laserManager.redrawLasers ();
		transform.position = newPos;
	}

	IEnumerator rotateMe(float rotation){
		float elapsedTime = 0;
		float time = 0.1f;
		Quaternion origRot = gameObject.transform.rotation;
		Quaternion newRot = origRot;
		Vector3 rot = new Vector3(0,0,0);
		rot.z = newRot.eulerAngles.z+rotation;
		newRot.eulerAngles = rot;
		if (orientation == 1) {
			orientation = 0;
		} else {
			orientation = 1;
		}

		while (elapsedTime < time) {
			transform.rotation = Quaternion.Lerp (origRot, newRot, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		manager.laserManager.redrawLasers ();
		transform.rotation = newRot;
	}
}
