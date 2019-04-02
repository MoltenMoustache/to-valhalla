using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public Tilemap map;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Click!");
            map.MoveSelectedUnit(tileX, tileY);   
        }
    }
}
