//  To Valhalla!
//  >Version 0.1<

//  Player Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    public int playerMaxHealth;
    int playerCurrentHealth;

    [Header("UI References")]
    [SerializeField]
    GameObject inventoryUI;

    [SerializeField]
    GameObject skillTreeUI;

    [Header("Attack Variables")]

    float attackCooldown;
    public float maxAttackCooldown;
    public Transform attackPos;
    public float attackRange;
    public LayerMask damageable;
    public int attackDamage;




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
        HandleMeleeAttack();

        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(1);
        }
    }

    void HandleRoll()
    {
        
    }

    void HandleMeleeAttack()
    {
        if(attackCooldown <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Sah-swing! Bata bata bata. Sah-wing bataaa...");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, damageable);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Creature>().DamageCreature(attackDamage);
                    Debug.LogWarning("Hello!");
                }
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void InsertWeaponDetails(float weaponRange, float weaponCooldown)
    {
        attackRange = weaponRange;
        maxAttackCooldown = weaponCooldown;
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
    public void TakeDamage(int dmg)
    {
        //reduces current health by the damage amount taken
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

        if(EquipmentManager.instance.currentArmourRating == 0)
        {
            playerCurrentHealth--;
            Debug.Log(playerCurrentHealth);
        }
        else
        {
            EquipmentManager.instance.DamageArmour();
        }
        

        //ensures player health can not fall below 0
        if(playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
            Debug.LogError("PLAYER DEATH!");
        }

        //debug message to display health and damage taken
        Debug.Log("Player took " + dmg + " damage! Current health: " + playerCurrentHealth);
    }

    //use this function to heal the player, as opposed to doing it directly
    public void HealPlayer(int heal)
    {
        playerCurrentHealth += heal;
        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        Debug.Log("Player healed by " + heal + ". Current health: " + playerCurrentHealth);
    }

    public void IncreaseMaxHealth(int amount)
    {
        playerMaxHealth += amount;
        playerCurrentHealth += amount;
    }

    public void DecreaseMaxHealth(int amount)
    {
        playerMaxHealth -= amount;

        if(playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }
    #endregion
}
