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
					       "6.....M..W" +
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
							"W5WWW....W" +
							"W2.......W" +
							"WWWWWWWWWW";
	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level3 =  "WWWWWWWW3W" +
							"W...WW...W" +
							"W...8W...W" +
							"W...WW...W" +
							"W..P.WM..W" +
							"W....W...W" +
							"W....W...W" +
							"W.M......W" +
							"W.......MW" +
							"WWWWWWWWWW";
	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level4 =  "WWWWWWWWWW" +
							"WM..P...MW" +
							"W........W" +
							"W...WW...W" +
							"W.M.WW...W" +
							"W...46...W" +
							"W...WW..MW" +
							"W...WW...W" +
							"WM......MW" +
							"WWWWWWWWWW";
	[System.NonSerialized]  //stupid, must be this wey, otherwise you have to change in inspector
	public string level5 =  "WWWWWWWWWW" +
							"W........W" +
							"2......M.W" +
							"W........W" +
							"W...MM.P.W" +
							"W...MM...W" +
							"W........W" +
							"WWWWW.WWWW" +
							"6....M...W" +
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
		} else if (lvl == 3) {
			return level3;
		} else if (lvl == 4) {
			return level4;
		} else if (lvl == 5) {
			return level5;


		} else {
			return level1;
		}


	}
}
