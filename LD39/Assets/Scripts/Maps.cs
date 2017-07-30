using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {


	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level1 = "WWWWWWWWWW" +
						   "W........W" +
						   "2.....M..W" +
					       "W........W" +
					       "WWW......W" +
					       "WWW......W" +
					       "G.....M..W" +
					       "WWWWW....W" +
					       "WW..P....W" +
					       "WWWWWWWWWW";

	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level2 =  "WWWWWWWWWW" +
							"W..P.....W" +
							"W.....M..W" +
							"W........W" +
							"WMW......W" +
							"W.W......W" +
							"W....M...W" +
							"WGWWW....W" +
							"W2.......W" +
							"WWWWWWWWWW";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string loadLevel(int lvl){
		if (lvl == 1) {
			return level1;
		} else if (lvl == 2) {
			return level2;
		} else {
			return level1;
		}
	}
}
