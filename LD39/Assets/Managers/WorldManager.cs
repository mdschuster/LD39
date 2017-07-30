using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

	static int mapSize = 10;
	Tile[,] Grid = new Tile[mapSize,mapSize];
	GameObject[,] goGrid = new GameObject[mapSize, mapSize];

	public GameObject floorPrefab;
	public GameObject playerPrefab;
	public GameObject mirrorPrefab;
	public GameObject goalRedPrefab;
	public GameObject genRedPrefab;

	public Sprite wall;

	public GameObject player;
	GameObject mirror;
	GameObject goalRed;
	GameObject genRed;
	GameObject objects;
	SpriteRenderer fadeScreen;
	GameObject textCanvas;
	Maps maps;
	GameObject menu;
	public LaserManager laserManager;
	char[] level;
	int goal = 0;
	int dead = 0;
	int lives=3;
	int levelCounter = 1;
	bool play=false;

	// Use this for initialization
	void Start () {
		
		//First thing to do is create grid
		goal=0;
		objects = GameObject.FindGameObjectWithTag ("objects");
		textCanvas = GameObject.FindGameObjectWithTag ("textCanvas");
		menu = GameObject.FindGameObjectWithTag ("mainMenu");
		loadLevel (1);
		defineGrid ();
		displayBoard ();
		displayOther ();
		displayPlayer ();
		displayMirrors ();

		setupCamera ();
		setupLaserManager ();
		setupFade ();

		//fadeout ();

	}

	public void exitClick(){
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("escape"))
			Application.Quit ();
		if (goal == 1 && dead==0) {
			goal = 0;
			dead = 0;
			win ();
		}
		if (dead == 1) {
			goal = 0;
			dead = 0;
			lose ();
		}
		if (play) {
			fadeout ();
		}
	}

	void loadLevel(int lvl){
		
		maps = Camera.main.GetComponent<Maps> ();
		string slvl = maps.loadLevel (lvl);
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
				if (level [idx] == 'W' ){
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
					Vector3 temp = new Vector3 (i, j, -4);
					goGrid [i, j].transform.position = temp;
				}
				goGrid [i, j].transform.SetParent (objects.transform);
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
					player.transform.SetParent (objects.transform);
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
					mirror = (GameObject)Instantiate (mirrorPrefab, new Vector3 (0, 0, -5), Quaternion.identity);
					mirror.GetComponent<Mirror> ().init (Grid [i, j]);
					Grid [i, j].changeObject ("mirror", mirror);
					mirror.transform.SetParent (objects.transform);
				}
			}
		}
	}

	void displayOther(){
		int idx;
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				idx = j + i * mapSize;
				if (level [idx] == '5'||level [idx] == '6'||level [idx] == '7'||level [idx] == '8') {
					goalRed = (GameObject)Instantiate (goalRedPrefab, new Vector3 (0, 0, -1), Quaternion.identity);
					//mirror.GetComponent<Mirror> ().init (Grid [i, j]);
					Vector3 pos = new Vector3(0,0,-1);
					pos.x = Grid [i, j].Xpos;
					pos.y = Grid [i, j].Ypos;
					goalRed.transform.position = pos;

					Grid [i, j].changeObject ("goal", goalRed);
					goalRed.transform.SetParent (objects.transform);
					if(level[idx]=='6'){ //up
						goalRed.transform.Rotate(new Vector3(0,0,90));
					} else if(level[idx]=='7'){//right
						goalRed.transform.Rotate(new Vector3(0,0,0));
					} else if(level[idx]=='8'){//down
						goalRed.transform.Rotate(new Vector3(0,0,-90));
					} else if(level[idx]=='9'){//left
						goalRed.transform.Rotate(new Vector3(0,0,180));
					}
				}
				//generator        //up                  //right              //down              //left
				if (level [idx] == '1' || level [idx] == '2' || level [idx] == '3' || level [idx] == '4' ) {
					genRed = (GameObject)Instantiate (genRedPrefab, new Vector3 (0, 0, -5), Quaternion.identity);
					//mirror.GetComponent<Mirror> ().init (Grid [i, j]);
					Vector3 pos = new Vector3(0,0,-1);
					pos.x = Grid [i, j].Xpos;
					pos.y = Grid [i, j].Ypos;
					genRed.transform.position = pos;

					Grid [i, j].changeObject ("generator", genRed);

					if(level[idx]=='1'){
						genRed.GetComponent<Generator>().init(Grid[i,j],"up");
					} else if(level[idx]=='2'){
						genRed.GetComponent<Generator>().init(Grid[i,j],"right");
					} else if(level[idx]=='3'){
						genRed.GetComponent<Generator>().init(Grid[i,j],"down");
					} else if(level[idx]=='4'){
						genRed.GetComponent<Generator>().init(Grid[i,j],"left");
					}
					genRed.transform.SetParent (objects.transform);
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

	void setupFade(){
		fadeScreen =GameObject.FindGameObjectWithTag("blackScreen").GetComponent<SpriteRenderer>();
		Color tmp = fadeScreen.color;
		tmp.a = 1;
		fadeScreen.color = tmp;
	}


	void win(){
		player.GetComponent<Player> ().DisableControls = true;
		player.GetComponent<Player> ().DisableCheck = true;
		goal = 0;
		fadein (1);

	}

	void lose(){
		player.GetComponent<Player> ().DisableControls = true;
		player.GetComponent<Player> ().DisableCheck = true;
		dead = 0;
		fadein (0);
		//display gameover screen
	}

	void fadeout(){
		StartCoroutine (fadeMe (-1,-1));
	}

	void fadein(int win){
		StartCoroutine (fadeMe (1,win));
	}

	IEnumerator fadeMe(int direction, int win){
		Color tmp = fadeScreen.color;
		float alpha;
		bool go = true;
		int maxVal;
		if(direction==-1){
			maxVal=0;
			alpha = 1f; 
		} else {
			maxVal=1;
			alpha = 0f;
		}

		while(go){

			alpha += direction*Time.deltaTime*2;
			tmp.a = alpha;
			fadeScreen.color = tmp;
			//Debug.Log (alpha+" "+direction+" "+Time.deltaTime);
			if(direction==-1){
				if (alpha < 0)
					go = false;
			} else {
				//Debug.Log (alpha);
				if (alpha > 1)
					go = false;
			}
			yield return null;
		}

		//activate player
		if (direction == -1) {
			player.GetComponent<Player> ().DisableControls = false;
		} else {
			player.GetComponent<Player> ().DisableControls = true;
		}

		yield return new WaitForSeconds (0.25f);

		if (direction == 1) {
			if (win == 1 && lives > 0) {
				Color darkGreen = new Color(16/255f,101/255f,16/255f,1f);
				textCanvas.GetComponent<Text> ().color = darkGreen;
				textCanvas.GetComponent<Text>().text = "Power Restored\nMoving to Next Section";
				levelCounter += 1;
				resetLevel (levelCounter);
				yield return new WaitForSeconds (2f);
				//remove lose screen
				textCanvas.GetComponent<Text>().text = "";
				fadeout ();
			}
			if (win == 0 && lives > 0) {
				//display lose screen
				Color darkRed = new Color (101 / 255f, 16 / 255f, 16 / 255f, 1f);
				textCanvas.GetComponent<Text> ().color = darkRed;
				textCanvas.GetComponent<Text> ().text = "Watch Out for the Lasers!\nRemaining Lives: " + lives;
					//display lose screen;
				resetLevel (levelCounter);
				yield return new WaitForSeconds (2f);
				//remove lose screen
				textCanvas.GetComponent<Text> ().text = "";
				fadeout ();

			} else if( lives == 0){
				//display lose screen
				Color darkRed = new Color (101 / 255f, 16 / 255f, 16 / 255f, 1f);
				textCanvas.GetComponent<Text> ().color = darkRed;
				textCanvas.GetComponent<Text> ().text = "Game Over";
				resetLevel (levelCounter);
				yield return new WaitForSeconds (2f);
				//remove lose screen
				textCanvas.GetComponent<Text> ().text = "";
				//fadeout ();
				reset ();

			}
		}

	}

	public void reset(){
		lives = 3;
		goal = 0;
		dead = 0;
		levelCounter = 1;
		menu.SetActive(true);
		resetLevel (levelCounter);
	}
		

	public void resetLasers(){
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Grid [i, j].Laser = 0;
			}
		}
	}

	void resetLevel(int lvl){
		destroyAll ();
		goal=0;
		loadLevel (lvl);
		defineGrid ();
		displayBoard ();
		displayOther ();
		displayPlayer ();
		displayMirrors ();
		laserManager.redrawLasers ();

	}

	void destroyAll(){
		//resetLasers ();
		for (int i = 0; i < mapSize; i++) {
			for (int j = 0; j < mapSize; j++) {
				Grid [i, j] = null;
			}
		}

		laserManager.destroyLasers ();

		int childCount = objects.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject.Destroy(objects.transform.GetChild(i).gameObject);
		}




	}

	public void clickPlay(){
		fadeout ();
		menu.SetActive (false);


	}

	public void returnToMenu(){
		
	}

	public GameObject GenRed {
		get {
			return genRed;
		}
		set {
			genRed = value;
		}
	}

	public int Goal {
		get {
			return goal;
		}
		set {
			goal = value;
		}
	}

	public int Dead {
		get {
			return dead;
		}
		set {
			dead = value;
		}
	}

	public int Lives {
		get {
			return lives;
		}
		set {
			lives = value;
		}
	}
}
