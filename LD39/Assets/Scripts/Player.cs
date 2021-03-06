﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	int xpos;
	int ypos;

	Tile myTile;

	bool disableControls;
	bool disableCheck;

	//i'd rather not have this here, but it works for now
	//FIXME
	Tile newTile;
	float inputCooldown = 0.2f;
	float time=-1;

	WorldManager manager;

	// Use this for initialization
	void Start () {
		disableControls = true;
		disableCheck = false;
	}

	// Update is called once per frame
	void Update () {

		move ();
		rotateMirror ();
		//this is ok for now, but i don't like it
		if (disableCheck == false) {
			//Debug.Log ("checking");
			checkLaser ();
		}
	}

	public void init(Tile startTile){
		manager = Camera.main.GetComponent<WorldManager> ();
		//set player position

		this.xpos = startTile.Xpos;
		this.ypos = startTile.Ypos; 

		setupPlayer ();
	}

	void move(){
		string direction = " ";

		if (disableControls == true) {
			return;
		}
		if (myTile == null) {
			return;
		}
			
		if (time > 0f) {
			time -= Time.deltaTime;
			return;
		}

		if (Input.GetKey (KeyCode.W)) {

			Quaternion tmp = new Quaternion();
			tmp.eulerAngles = new Vector3 (0, 0, 90);
			newTile = manager.getNeighbor (myTile, "up");
			direction = "up";
			gameObject.transform.rotation = tmp;

		} else if (Input.GetKey (KeyCode.S)) {
			Quaternion tmp = new Quaternion();
			tmp.eulerAngles = new Vector3 (0, 0, -90);
			newTile = manager.getNeighbor (myTile, "down");
			direction = "down";
			gameObject.transform.rotation = tmp;


		} else if (Input.GetKey (KeyCode.A)) {
			Quaternion tmp = new Quaternion();
			tmp.eulerAngles = new Vector3 (0, 0, 180);
			newTile = manager.getNeighbor (myTile, "left");
			direction = "left";
			gameObject.transform.rotation = tmp;


		} else if (Input.GetKey (KeyCode.D)) {
			Quaternion tmp = new Quaternion();
			tmp.eulerAngles = new Vector3 (0, 0, 0);
			newTile = manager.getNeighbor (myTile, "right");
			direction = "right";
			gameObject.transform.rotation = tmp;


		} else {
			newTile = null;
			direction = " ";
		}

		if ( newTile != null ) {
			movePlayer(direction);
			time=inputCooldown;
		}

	}

	void movePlayer(string direction){

		//check what is in the tile
		gameObject.GetComponent<Animator> ().SetTrigger("walk");
		string obj = newTile.getObject ();
		if (obj == "wall" || obj == "goal" || obj == "generator") {
			return;
		}
		if (obj == "mirror") {
			//move the mirror too if you can
			int moved = newTile.getGO().GetComponent<Mirror>().moveMirror(direction);
			if (moved == 0) {
				return;
			}
		}
		if (obj == "pickup") {
			//do something with the pickup
		}



		Vector3 origPos = transform.position;
		Vector3 newPos = new Vector3(0,0,0);
		newPos.x = newTile.Xpos;
		newPos.y = newTile.Ypos;
		newPos.z = origPos.z;
		this.myTile = newTile;


		StartCoroutine(moveMe(origPos, newPos));
		//checkLaser ();

	}

	IEnumerator moveMe(Vector3 origPos, Vector3 newPos){

		float elapsedTime = 0;
		float time = 0.2f;

		while (elapsedTime < time) {
			transform.position = Vector3.Lerp (origPos, newPos, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		gameObject.GetComponent<Animator> ().SetTrigger ("stopwalking");
		transform.position = newPos;
	}

	void rotateMirror(){
		if (myTile == null) {
			return;
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			string[] allDirs = new string[4];
			allDirs [0] = "up";
			allDirs [1] = "right";
			allDirs [2] = "down";
			allDirs [3] = "left";
			for (int i = 0; i < 4; i++) {
				Tile facingTile = manager.getNeighbor (myTile,allDirs[i]);
				if (facingTile.getObject() == "mirror") {
					facingTile.getGO ().GetComponent<Mirror> ().rotate ();
				}
			}
			//checkLaser ();

		}
	}

	void setupPlayer(){
		Vector3 pos = transform.position;
		pos.x = this.xpos;
		pos.y = this.ypos;
		transform.position = pos;
		myTile = manager.getTile (xpos, ypos);
		newTile = myTile;
	}

	public void checkLaser(){
		if (myTile == null)
			return;
		if (myTile.Laser == 1) {
			manager.Dead = 1;
			manager.Lives -= 1;
			gameObject.GetComponent<ParticleSystem> ().Play ();
			gameObject.GetComponent<AudioSource> ().Play();
		}
	}

	public bool DisableControls {
		get {
			return disableControls;
		}
		set {
			disableControls = value;
		}
	}

	public bool DisableCheck {
		get {
			return disableCheck;
		}
		set {
			disableCheck = value;
		}
	}
}
