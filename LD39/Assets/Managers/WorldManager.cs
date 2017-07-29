﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	static int mapSize = 10;
	Tile[,] Grid = new Tile[mapSize,mapSize];
	GameObject[,] goGrid = new GameObject[mapSize, mapSize];

	public GameObject floorPrefab;
	public GameObject playerPrefab;

	GameObject player;

	// Use this for initialization
	void Start () {

		//First thing to do is create grid
		defineGrid ();
		displayBoard ();
		displayPlayer ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void defineGrid(){
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Grid[i,j] = new Tile(i,j);
				//add wall boundry
				if (i == mapSize-1 || j == mapSize-1  || i==0 ||j==0) {
					Grid [i, j].changeObject ("wall");
				}
			}
		}
	}

	void displayBoard(){
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				goGrid [i, j] = (GameObject)Instantiate (floorPrefab, new Vector3 (i,j,0), Quaternion.identity);
			}
		}
	}

	void displayPlayer(){
		player = (GameObject)Instantiate (playerPrefab, new Vector3 (0, 0, -1), Quaternion.identity);
		player.GetComponent<Player>().init ();
	}

	public Tile getTile(int x, int y){
		if (Grid [x, y] == null) {
			Debug.LogError ("Return value of getTile is null");
		}
		return Grid [x, y];
	}

	public Tile getNeighbor(Tile tile, string direction){

		//this routine assumes that there is always a one tile border
		//around the map, otherwise the Grid will be out of bounds
		//FIXME
		Tile neighbor;
		int tilex = tile.Xpos;
		int tiley = tile.Ypos;

		if (direction == "up") {
			neighbor = Grid [tilex,tiley + 1];
				
		} else if (direction == "down") {
			neighbor = Grid [tilex,tiley - 1];
				
		} else if (direction == "right") {
			neighbor = Grid [tilex + 1,tiley];
				
		} else if (direction == "left") {
			neighbor = Grid [tilex - 1,tiley];
				
		} else {
			Debug.LogError ("Unknown Direction for Neighbor");
			neighbor = null;
		}

		return neighbor;

	}
}
