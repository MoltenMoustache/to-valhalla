//  To Valhalla!
//  >Version 0.1<

//  Player Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float playerMaxHealth;
    float playerCurrentHealth;

    [SerializeField]
    GameObject inventoryUI;
    bool inventoryIsOpen;

    [SerializeField]
    float hitLength;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRoll();

        HandleInventory();

        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleAttack();
        }
    }

    void HandleRoll()
    {
        
    }

    void HandleAttack()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");
        RaycastHit2D raycast = Physics2D.Raycast(this.gameObject.transform.position, this.transform.forward, hitLength);
        Debug.DrawRay(this.gameObject.transform.position, this.transform.forward, Color.green);

        if(raycast)
        {
            Debug.LogWarning(raycast.transform.name);
        }

    }

    void HandleInventory()
    {
        if (!inventoryIsOpen && Input.GetKeyDown(KeyCode.I))
        {
            inventoryIsOpen = true;
            inventoryUI.SetActive(true);
            Time.timeScale = 0f;

        }
        else if (inventoryIsOpen && (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)))
        {
            inventoryIsOpen = false;
            inventoryUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    #region healthfunctions
    //use this function to damage the player, as opposed to doing it directly
    public void TakeDamage(float dmg)
    {
        //reduces current health by the damage amount taken
        dmg -= EquipmentManager.instance.totalArmourRating;
        if (dmg <= 0)
        {
            return;
        }
        else
        {
            playerCurrentHealth -= dmg;
        }
        

        //ensures player health can not fall below 0
        if(playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0.0f;
        }

        //debug message to display health and damage taken
        Debug.Log("Player took " + dmg + " damage! Current health: " + playerCurrentHealth);
    }

    //use this function to heal the player, as opposed to doing it directly
    public void HealPlayer(float heal)
    {
        playerCurrentHealth += heal;
        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        Debug.Log("Player healed by " + heal + ". Current health: " + playerCurrentHealth);
    }
    #endregion
}
