using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {

	WorldManager manager;
	public GameObject laserPrefab;
	int goal = 0;
	int generators;
	int gX;
	int gY;
	Tile genTile;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(){
		manager = Camera.main.GetComponent<WorldManager> ();
		generators = 1;
	}

	public void redrawLasers(){
		genTile = manager.GenRed.GetComponent<Generator> ().myTile;
		destroyLasers ();
		resetLasers ();
		goal = 0;
		bool stopped = false;
		int iter=0;
		Tile curTile = genTile;
		Tile nextTile;
		GameObject laser;
		string laserFace = manager.GenRed.GetComponent<Generator> ().orientation;
		for (int i = 0; i < generators; i++) {
			while (!stopped) {

				laser = (GameObject)Instantiate (laserPrefab, new Vector3 (0, 0, -2), Quaternion.identity);
				laser.GetComponent<Laser>().init (curTile,laserFace);
				laser.transform.SetParent (gameObject.transform);
				curTile.Laser = 1;
				nextTile = manager.getNeighbor (curTile, laserFace);
				//Debug.Log(nextTile.getObject ());
				//Debug.Log (nextTile.Xpos + " " + nextTile.Ypos);
				if (nextTile.getObject () == "wall" || nextTile.getObject () == "goal" || nextTile.getObject () == "generator") {
					if (nextTile.getObject () == "goal") {
						goal = 1;
					} else {
						goal = 0;
					}
					manager.Goal = goal;
					break;
				}
				if (nextTile.getObject () == "mirror") {
					//check the mirror orientation
					GameObject mirror = nextTile.getGO();
					if (mirror.GetComponent<Mirror> ().orientation == 1) {
						if (laserFace == "up") {
							laserFace = "right";
						} else if (laserFace == "down") {
							laserFace = "left";
						} else if (laserFace == "right") {
							laserFace = "up";
						} else if (laserFace == "left")
							laserFace = "down";
					} else {
						if (laserFace == "up") {
							laserFace = "left";
						}else if (laserFace == "down") {
							laserFace = "right";
						}else if (laserFace == "right") {
							laserFace = "down";
						}else if (laserFace == "left") {
							laserFace = "up";
						}

					}


				}

				//Debug.Log (laserFace);
				iter += 1;
				if (iter == 20)
					break;
				curTile = nextTile;
			}
		}
	}

	void resetLasers(){
		manager.resetLasers ();
	}

	public void destroyLasers(){
		int childCount = this.gameObject.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
		}
	}
}
