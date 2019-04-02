using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {
    #region singleton
    public static ClickManager instance = null;
    void Awake()
    {
        instance = this;

    }
    #endregion 
    //The materials
    public Material normalMaterial;
    public Material highlightMaterial;
    public Material selectedMaterial;

    //All currently selected units
    [System.NonSerialized]
    public List<GameObject> selectedUnits = new List<GameObject>();

    //We have hovered above this unit, so we can deselect it next update
    //and dont have to loop through all units
    GameObject highlightThisUnit;

    void Start()
    {

    }

    void Update()
    {
        //Select one or several units by clicking or draging the mouse
        SelectUnits();

        //Highlight by hovering with mouse above a unit which is not selected
        HighlightUnit();
    }

    //Select units with click or by draging the mouse
    void SelectUnits()
    {
        //Select one unit with left mouse and deselect all units with left mouse by clicking on what's not a unit
        if (Input.GetMouseButtonUp(0))
        {
            //Deselect all units
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].GetComponent<MeshRenderer>().material = normalMaterial;
            }

            //Clear the list with selected units
            

            //Try to select a new unit
            RaycastHit hit;
            //Fire ray from camera
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
            {
                //Did we hit a friendly unit?
                if (hit.collider.tag == "Friendly")
                {
                    //selectedUnits.Clear();
                    GameObject activeUnit = hit.collider.gameObject;
                    //Set this unit to selected
                    activeUnit.GetComponent<MeshRenderer>().material = selectedMaterial;
                    //Add it to the list of selected units, which is now just 1 unit
                    selectedUnits.Add(activeUnit);
                    Tilemap.instance.selectedUnit = activeUnit.GetComponentInParent<Character>().gameObject;
                    activeUnit.GetComponent<Character>().currentPath = null;
                }
            }
        }
    }

    //Highlight a unit when mouse is above it
    void HighlightUnit()
    {
        //Change material on the latest unit we highlighted
        if (highlightThisUnit != null)
        {
            //But make sure the unit we want to change material on is not selected
            bool isSelected = false;
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i] == highlightThisUnit)
                {
                    isSelected = true;
                    break;
                }
            }

            if (!isSelected)
            {
                highlightThisUnit.GetComponent<MeshRenderer>().material = normalMaterial;
            }

            highlightThisUnit = null;
        }

        //Fire a ray from the mouse position to get the unit we want to highlight
        RaycastHit hit;
        //Fire ray from camera
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200f))
        {
            //Did we hit a friendly unit?
            if (hit.collider.tag == "Friendly")
            {
                //Get the object we hit
                GameObject currentObj = hit.collider.gameObject;

                //Highlight this unit if it's not selected
                bool isSelected = false;
                for (int i = 0; i < selectedUnits.Count; i++)
                {
                    if (selectedUnits[i] == currentObj)
                    {
                        isSelected = true;
                        break;
                    }
                }

                if (!isSelected)
                {
                    highlightThisUnit = currentObj;

                    highlightThisUnit.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
        }
    }
    }
