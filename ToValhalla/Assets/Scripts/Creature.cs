using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int creatureMaxHealth;
    protected int creatureCurrentHealth;
    public float moveSpeed;
    float defaultMoveSpeed;
    public float maxStunTime;
    float stunTime;

    public Transform target;

    void Awake()
    {
        creatureCurrentHealth = creatureMaxHealth;
        defaultMoveSpeed = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        StunCreature();
    }

    protected virtual void HandleMovement()
    {
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

    }

    public virtual void DamageCreature(int dmg)
    {
        creatureCurrentHealth -= dmg;
        stunTime = maxStunTime;
        if(creatureCurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void StunCreature()
    {
        if(stunTime <= 0)
        {
            moveSpeed = defaultMoveSpeed;
        }
        else
        {
            moveSpeed = 0f;
            stunTime -= Time.deltaTime;
        }
    }
}
