using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public string orientation;
	public Tile myTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(Tile tile, string facing){
		orientation = facing;
		myTile = tile;
		Quaternion origRot = this.transform.rotation;
		Quaternion newRot = origRot;
		Vector3 rot = new Vector3(0,0,0);
		if(orientation == "up")
			rot.z = newRot.eulerAngles.z+90f;
		if(orientation == "right")
			rot.z = newRot.eulerAngles.z+0f;
		if(orientation == "down")
			rot.z = newRot.eulerAngles.z+-90f;
		if(orientation == "left")
			rot.z = newRot.eulerAngles.z+180f;

		newRot.eulerAngles = rot;
		transform.rotation = newRot;
	}
}
