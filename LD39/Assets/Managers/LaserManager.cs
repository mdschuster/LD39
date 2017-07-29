using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {

	GameObject allLasers;
	WorldManager manager;
	public GameObject laserPrefab;
	int generators;
	int gX=2;
	int gY=0;
	string gface = "right";
	Tile genTile;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(){
		manager = Camera.main.GetComponent<WorldManager> ();
		allLasers = gameObject;
		generators = 1;
		genTile = manager.getTile (gX, gY);
	}

	public void redrawLasers(){
		destroyLasers ();
		bool stopped = false;
		int iter=0;
		Tile curTile = genTile;
		Tile nextTile;
		GameObject laser;
		string laserFace = gface;
		float offset = 0.5f;
		for (int i = 0; i < generators; i++) {
			while (!stopped) {

				laser = (GameObject)Instantiate (laserPrefab, new Vector3 (0, 0, -1), Quaternion.identity);
				laser.GetComponent<Laser>().init (curTile,laserFace);
				laser.transform.SetParent (gameObject.transform);
				nextTile = manager.getNeighbor (curTile, laserFace);
				//Debug.Log(nextTile.getObject ());
				//Debug.Log (nextTile.Xpos + " " + nextTile.Ypos);
				if (nextTile.getObject() == "wall") {
					break;
				}
				if (nextTile.getObject () == "mirror") {
					//check the mirror orientation
					GameObject mirror = nextTile.getGO();
					if (mirror.GetComponent<Mirror> ().orientation == 1) {
						Debug.Log (laserFace + " before");
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

	void destroyLasers(){
		int childCount = this.gameObject.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
		}
	}
}
