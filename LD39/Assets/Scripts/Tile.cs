using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

	int xpos;
	int ypos;

	string theObject = "none";
	GameObject goObject;
	int laser = 0;

	public Tile(int x, int y){
		this.xpos = x;
		this.ypos = y;
		goObject = null;
	}

	public void changeObject(string newObject, GameObject go){
		this.theObject = newObject;
		this.goObject = go;
	}

	public string getObject(){
		return this.theObject;
	}

	public GameObject getGO(){
		return this.goObject;
	}

	public void changeGO(GameObject go){
		goObject = go;
	}

	public int Xpos {
		get {
			return xpos;
		}
		set {
			xpos = value;
		}
	}

	public int Ypos {
		get {
			return ypos;
		}
		set {
			ypos = value;
		}
	}

	public int Laser {
		get {
			return laser;
		}
		set {
			laser = value;
		}
	}
}
