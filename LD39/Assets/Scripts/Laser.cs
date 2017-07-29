using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	Tile myTile;
	string facing;
	float offset = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(Tile tile, string face){
		myTile = tile;
		facing = face;
		//facing and posistion
		if ((facing == "up" || facing == "down") && gameObject.transform.rotation.z!=90) {
			gameObject.transform.Rotate (0, 0, 90);
		}
		if ((facing == "left" || facing == "right") && gameObject.transform.rotation.z!=0) {
			gameObject.transform.Rotate (0, 0, 0);
		}

		Vector3 pos = new Vector3 (0, 0, -1);
		pos.x = tile.Xpos;
		pos.y = tile.Ypos;

		if (facing == "right") {
			pos.y += offset;
		} else if (facing == "left") {
			pos.y -= offset;
		}else if (facing == "up") {
			pos.x -= offset;
		}else if (facing == "down") {
			pos.x += offset;
		}

		transform.position = pos;

	}

	public string Facing {
		get {
			return facing;
		}
		set {
			facing = value;
		}
	}
}
