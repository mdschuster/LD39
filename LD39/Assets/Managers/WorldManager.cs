using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	static int mapSize = 10;
	Tile[,] Grid = new Tile[mapSize,mapSize];
	GameObject[,] goGrid = new GameObject[mapSize, mapSize];

	public GameObject floorPrefab;
	public GameObject playerPrefab;
	public GameObject mirrorPrefab;

	public Sprite wall;

	GameObject player;
	GameObject mirror;
	Maps maps;
	public LaserManager laserManager;
	char[] level;

	// Use this for initialization
	void Start () {
		
		//First thing to do is create grid
		loadLevel ();
		defineGrid ();
		displayBoard ();
		displayPlayer ();
		displayMirrors ();
		setupCamera ();
		setupLaserManager ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadLevel(){
		
		maps = Camera.main.GetComponent<Maps> ();
		string slvl = maps.loadLevel ();
		level = slvl.ToCharArray();
	}

	void setupCamera (){
		Vector3 pos = new Vector3 (mapSize / 2, mapSize / 2, -10);
		Camera.main.transform.position = pos;
		Camera.main.orthographicSize = 5.6f;
		Vector3 rot = new Vector3 (0, 0, 90);
		Quaternion qrot = new Quaternion ();
		qrot.eulerAngles = rot;
		Camera.main.transform.rotation = qrot;
	}


	void defineGrid(){
		int idx;
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Grid[i,j] = new Tile(i,j);
				//add wall boundry
				idx = j+i*mapSize;
				if (level [idx] == 'W') {
					Grid [i, j].changeObject ("wall", null);
				} else {
					Grid [i, j].changeObject ("none", null);
				}
			}
		}
	}

	void displayBoard(){
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				goGrid [i, j] = (GameObject)Instantiate (floorPrefab, new Vector3 (i,j,0), Quaternion.identity);
				if (Grid [i, j].getObject () == "wall") {
					goGrid [i, j].GetComponent<SpriteRenderer> ().sprite = wall;
				}
			}
		}
	}

	void displayPlayer(){
		int idx;
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				idx = j+i*mapSize;
				if (level [idx] == 'P') {
					player = (GameObject)Instantiate (playerPrefab, new Vector3 (0, 0, -1), Quaternion.identity);
					player.GetComponent<Player>().init (Grid[i,j]);
					return;
				}
			}
		}

	}

	void displayMirrors(){
		int idx;
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				idx = j + i * mapSize;
				if (level [idx] == 'M') {
					mirror = (GameObject)Instantiate (mirrorPrefab, new Vector3 (0, 0, -1), Quaternion.identity);
					mirror.GetComponent<Mirror> ().init (Grid [i, j]);
					Grid [i, j].changeObject ("mirror", mirror);
				}
			}
		}
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
			neighbor = Grid [tilex-1,tiley ];
				
		} else if (direction == "down") {
			neighbor = Grid [tilex+1,tiley ];
				
		} else if (direction == "right") {
			neighbor = Grid [tilex ,tiley+1];
				
		} else if (direction == "left") {
			neighbor = Grid [tilex ,tiley-1];
				
		} else {
			Debug.LogError ("Unknown Direction for Neighbor");
			neighbor = null;
		}

		return neighbor;

	}

	void setupLaserManager(){
		laserManager = GameObject.FindGameObjectWithTag ("laserManager").GetComponent<LaserManager> ();
		laserManager.init ();
		laserManager.redrawLasers ();
	}
}
