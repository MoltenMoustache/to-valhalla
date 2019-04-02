using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int i_str, i_dex, i_int, i_con, i_wis, i_cha;
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public Class characterClass;
    public Race characterRace;
    public bool i_isMale;
    public float movementSpeed;

    public Tilemap map;
    public int tileX;
    public int tileY;
    public List<Node> currentPath = null;

    public List<Item> characterInventory = new List<Item>();
    public Item equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPath != null)
        {
            int currentNode = 0;
            while (currentNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileToWorldCoord(currentPath[currentNode].x, currentPath[currentNode].y) + new Vector3(0, 0, -1);
                Vector3 end = map.TileToWorldCoord(currentPath[currentNode + 1].x, currentPath[currentNode+1].y) + new Vector3(0, 0, -1);
                Debug.DrawLine(start, end, Color.red);

                currentNode++;
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            MoveToNextTile();
        }
    }

    public void MoveToNextTile()
    {
        float remainingMovement = (int)(movementSpeed / 5);

        while (remainingMovement > 0)
        {
            if (currentPath == null)
                return;

            // Get cost from current tile to next tile
            remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

            // Move us to the next tile in the sequence
            tileX = currentPath[1].x;
            tileY = currentPath[1].y;
            
            transform.position = map.TileToWorldCoord(tileX, tileY) + new Vector3(0, 0, -0.75f);   // Update our unity world position
            // Remove the old "current" tile
            currentPath.RemoveAt(0);

            if (currentPath.Count == 1)
            {
                // We only have one tile left in the path, and that tile MUST be our ultimate
                // destination -- and we are standing on it!
                // So let's just clear our pathfinding info.
                currentPath = null;
            }
        }
    }
}


public enum Race
{
    Human,
    HalfElf,
    HalfOrc,
    Goblin,
    Tiefling,
    Elf,
    Dwarf,
    Gnome,
    Halfling,
}


public enum Class
{
    Cleric,
    Wizard,
    Fighter,
    Barbarian,
    Warlock,
    Bard,
    Artificer,
    Ranger,
    Rogue,
}
