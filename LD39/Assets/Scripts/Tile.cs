using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

	int xpos;
	int ypos;

	string theObject = "none";
	GameObject goObject;

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
}
