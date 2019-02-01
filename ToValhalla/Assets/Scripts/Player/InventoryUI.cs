﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    GameObject skillTreeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSkillTree()
    {
        skillTreeUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}