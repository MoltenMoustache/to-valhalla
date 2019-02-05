using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeUI : MonoBehaviour
{

    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    TextMeshProUGUI runeCount;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRuneCounter();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalPress = Input.GetAxisRaw("Horizontal");
        if(horizontalPress == 1)
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void UpdateRuneCounter()
    {
        runeCount.text = GameManager.instance.runes.ToString();
    }
}
