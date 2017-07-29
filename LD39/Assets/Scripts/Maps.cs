using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {


	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level1 = "WWWWWWWWWW" +
						   "W........W" +
						   "W.....M..W" +
					       "W........W" +
					       "WWW......W" +
					       "WWW......W" +
					       "W.....M..W" +
					       "WWWWW....W" +
					       "WW..P....W" +
					       "WWWWWWWWWW";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string loadLevel(){
		return level1;
	}
}
