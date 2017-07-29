using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

	int xpos;
	int ypos;

	string theObject = "none";

	public Tile(int x, int y){
		this.xpos = x;
		this.ypos = y;
	}

	public void changeObject(string newObject){
		this.theObject = newObject;
	}

	public string getObject(){
		return this.theObject;
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
