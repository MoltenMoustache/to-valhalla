//  To Valhalla!
//  >Version 0.1<

//  Player Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float playerMaxHealth;
    float playerCurrentHealth;

    [SerializeField]
    GameObject inventoryUI;

    [SerializeField]
    GameObject skillTreeUI;

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
        HandleSpecial();
        HandleInventory();
        HandleSkillTree();

        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleAttack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(20);
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
        if (!inventoryUI.activeSelf && Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(true);
            skillTreeUI.SetActive(false);
            Inventory.instance.onItemChangedCallback.Invoke();
            Time.timeScale = 0f;

        }
        else if (inventoryUI.activeSelf && (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Back")))
        {
            inventoryUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void HandleSkillTree()
    {
        if (Input.GetKeyDown(KeyCode.K) && !skillTreeUI.activeSelf)
        {
            skillTreeUI.SetActive(true);
            inventoryUI.SetActive(false);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.K) && skillTreeUI.activeSelf)
        {
            skillTreeUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void HandleSpecial()
    {
        if (Input.GetButtonDown("Special"))
        {
            if (GameManager.instance.unlockedRage)
            {
                GameManager.instance.UseSpecial();
            }
        }
    }

    #region healthfunctions
    //use this function to damage the player, as opposed to doing it directly
    public void TakeDamage(float dmg)
    {
        //reduces current health by the damage amount taken
        dmg -= EquipmentManager.instance.totalArmourRating;
        if (GameManager.instance.isRaged)
        {
            dmg /= 2;
        }

        if (dmg <= 0)
        {
            dmg = 0;
            return;
        }
        else
        {
            playerCurrentHealth -= dmg;
            Debug.Log(playerCurrentHealth);
        }
        

        //ensures player health can not fall below 0
        if(playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0.0f;
            Debug.LogError("PLAYER DEATH!");
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

    public void IncreaseMaxHealth(float amount)
    {
        playerMaxHealth += amount;
        playerCurrentHealth += amount;
    }

    public void DecreaseMaxHealth(float amount)
    {
        playerMaxHealth -= amount;

        if(playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }
    #endregion
}
