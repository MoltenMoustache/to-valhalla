using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{

    [SerializeField]
    GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
