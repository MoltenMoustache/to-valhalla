using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour {

    public ClickableTile groundTile;
    public int tileX;
    public int tileY;

	// Use this for initialization
	void Start () {
        tileX = groundTile.GetComponent<ClickableTile>().tileX;
        tileY = groundTile.GetComponent<ClickableTile>().tileY;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
