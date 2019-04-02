using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : MonoBehaviour
{
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Character>().movementSpeed += 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
