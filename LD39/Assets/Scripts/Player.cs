﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	int xpos;
	int ypos;

	Tile myTile;

	//i'd rather not have this here, but it works for now
	//FIXME
	Tile newTile;
	Vector3 moveTo;
	float tol = 0.0001f;
	float inputCooldown = 0.1f;
	float time=-1;

	WorldManager manager;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		move ();
	}

	public void init(){
		manager = Camera.main.GetComponent<WorldManager> ();
		//set player position
		this.xpos = 5;
		this.ypos = 5; 

		setupPlayer ();
	}

	void move(){


		if (myTile == null) {
			return;
		}
			
		if (time > 0f) {
			time -= Time.deltaTime;
			return;
		}

		if (Input.GetKeyDown (KeyCode.W)) {

			newTile = manager.getNeighbor (myTile, "up");

		} else if (Input.GetKeyDown (KeyCode.S)) {

			newTile = manager.getNeighbor (myTile, "down");

		} else if (Input.GetKeyDown (KeyCode.A)) {

			newTile = manager.getNeighbor (myTile, "left");

		} else if (Input.GetKeyDown (KeyCode.D)) {

			newTile = manager.getNeighbor (myTile, "right");

		} else {
			newTile = null;
		}

		if ( newTile != null ) {
			movePlayer();
			time=inputCooldown;
		}

	}

	void movePlayer(){

		//check what is in the tile

		string obj = newTile.getObject ();
		Debug.Log (obj);
		if (obj == "wall") {
			return;
		}
		if (obj == "mirror") {
			//move the mirror too if you can
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
		moveTo = newPos;


		StartCoroutine(moveMe(origPos, newPos));

	}

	IEnumerator moveMe(Vector3 origPos, Vector3 newPos){

		float elapsedTime = 0;
		float time = 0.12f;
		while (elapsedTime < time) {
			transform.position = Vector3.Lerp (origPos, newPos, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = newPos;
	}

	void setupPlayer(){
		Vector3 pos = transform.position;
		pos.x = this.xpos;
		pos.y = this.ypos;
		transform.position = pos;
		myTile = manager.getTile (xpos, ypos);
		newTile = myTile;
		moveTo = pos;
	}


}