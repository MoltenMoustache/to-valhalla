using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] Classes;
    [SerializeField]
    Transform spawnLocale;
    GameObject classToSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < 1001; i++)
            {
            GenerateCharacter();

            }
        }
    }

    #region character generation
    public void GenerateCharacter()
    {
        //int str, int dex, int con, int intel, int wis, int cha, string name
        bool isMale;
        int gender = Random.Range(0, 2);
        if(gender == 0)
        {
            isMale = false;
        }
        else
        {
            isMale = true;
        }

        // randomly selected character from array of Classes
        classToSpawn = Instantiate(Classes[Random.Range(0, Classes.Length)], spawnLocale);
        Tilemap.instance.allUnits.Add(classToSpawn);
        classToSpawn.GetComponent<Character>().tileX = (int)classToSpawn.transform.position.x;
        classToSpawn.GetComponent<Character>().tileY = (int)classToSpawn.transform.position.y;
        classToSpawn.GetComponent<Character>().map = Tilemap.instance;

        // calls GenerateName() function depending on gender and assigns generated string to character name
        string a_name = GenerateName(isMale);
        classToSpawn.GetComponent<Character>().characterName = a_name;
        
        // generates six stats and sorts them from highest to smallest
        int statOne = Random.Range(3, 18);
        int statTwo = Random.Range(3, 18);
        int statThree = Random.Range(3, 18);
        int statFour = Random.Range(3, 18);
        int statFive = Random.Range(3, 18);
        int statSix = Random.Range(3, 18);

        int[] a_stats = { statOne, statTwo, statThree, statFour, statFive, statSix };
        int tempInt;

        for (int i = 0; i < a_stats.Length; i++)
        {
            for (int k = 0; k < a_stats.Length; k++)
            {
                if(a_stats[i] < a_stats[k])
                {
                    tempInt = a_stats[i];
                    a_stats[i] = a_stats[k];
                    a_stats[k] = tempInt;
                }
            }
        }

        // Generates six stats and assigns them dependent on class
        switch (classToSpawn.GetComponent<Character>().characterClass)
        {
            case Class.Cleric:
                classToSpawn.GetComponent<Character>().name = "Cleric";

                classToSpawn.GetComponent<Character>().i_wis = a_stats[5];
                classToSpawn.GetComponent<Character>().i_con = a_stats[4];
                classToSpawn.GetComponent<Character>().i_str = a_stats[3];
                classToSpawn.GetComponent<Character>().i_dex = a_stats[2];
                classToSpawn.GetComponent<Character>().i_cha = a_stats[1];
                classToSpawn.GetComponent<Character>().i_int = a_stats[0];
                break;
            case Class.Fighter:
                classToSpawn.GetComponent<Character>().name = "Fighter";

                classToSpawn.GetComponent<Character>().i_dex = a_stats[5];
                classToSpawn.GetComponent<Character>().i_con = a_stats[4];
                classToSpawn.GetComponent<Character>().i_str = a_stats[3];
                classToSpawn.GetComponent<Character>().i_wis = a_stats[2];
                classToSpawn.GetComponent<Character>().i_cha = a_stats[1];
                classToSpawn.GetComponent<Character>().i_int = a_stats[0];
                break;
            case Class.Wizard:
                classToSpawn.GetComponent<Character>().name = "Wizard";

                classToSpawn.GetComponent<Character>().i_int = a_stats[5];
                classToSpawn.GetComponent<Character>().i_con = a_stats[4];
                classToSpawn.GetComponent<Character>().i_dex = a_stats[3];
                classToSpawn.GetComponent<Character>().i_wis = a_stats[2];
                classToSpawn.GetComponent<Character>().i_cha = a_stats[1];
                classToSpawn.GetComponent<Character>().i_str = a_stats[0];
                break;
            case Class.Rogue:

                classToSpawn.GetComponent<Character>().name = "Rogue";

                classToSpawn.GetComponent<Character>().i_dex = a_stats[5];
                classToSpawn.GetComponent<Character>().i_con = a_stats[4];
                classToSpawn.GetComponent<Character>().i_int = a_stats[3];
                classToSpawn.GetComponent<Character>().i_cha = a_stats[2];
                classToSpawn.GetComponent<Character>().i_wis = a_stats[1];
                classToSpawn.GetComponent<Character>().i_str = a_stats[0];
                break;
        }

        // randomly selects race from Race enums
        Race a_race = (Race)Random.Range(0, (int)Race.Halfling);
        // assigns randomly selected race to character
        classToSpawn.GetComponent<Character>().characterRace = a_race;

        // increases character stats dependent on race
        switch (a_race)
        {
            case Race.Human:
                classToSpawn.GetComponent<Character>().characterRace = Race.Human;

                classToSpawn.GetComponent<Character>().i_str += 1;
                classToSpawn.GetComponent<Character>().i_dex += 1;
                classToSpawn.GetComponent<Character>().i_con += 1;
                classToSpawn.GetComponent<Character>().i_int += 1;
                classToSpawn.GetComponent<Character>().i_wis += 1;
                classToSpawn.GetComponent<Character>().i_cha += 1;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
            case Race.HalfElf:
                classToSpawn.GetComponent<Character>().i_cha += 2;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
            case Race.Goblin:
                classToSpawn.GetComponent<Character>().i_dex += 2;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
            case Race.Tiefling:
                classToSpawn.GetComponent<Character>().i_cha += 2;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
            case Race.HalfOrc:
                classToSpawn.GetComponent<Character>().i_str += 2;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
            case Race.Dwarf:
                classToSpawn.GetComponent<Character>().i_con += 1;
                classToSpawn.GetComponent<Character>().i_str += 1;
                classToSpawn.GetComponent<Character>().movementSpeed = 25;
                break;
            case Race.Gnome:
                classToSpawn.GetComponent<Character>().i_dex += 1;
                classToSpawn.GetComponent<Character>().i_int += 1;
                classToSpawn.GetComponent<Character>().movementSpeed = 25;
                break;
            case Race.Halfling:
                classToSpawn.GetComponent<Character>().i_dex += 1;
                classToSpawn.GetComponent<Character>().i_wis += 1;
                classToSpawn.GetComponent<Character>().movementSpeed = 25;
                break;
            case Race.Elf:
                classToSpawn.GetComponent<Character>().i_int += 2;
                classToSpawn.GetComponent<Character>().movementSpeed = 30;
                break;
        }

        int conMod = 0;
        int j = classToSpawn.GetComponent<Character>().i_con;
        // if the con score is above 10
        if (j > 10)
        {
            // if the con score is 12 or more
            while(j >= 12)
            {
                // increment con mod
                conMod++;
                // reduce con score
                j -= 2;
            }
        }

        // if con score is below 10
        if (j < 10)
        {
            // if the con score is 8 or less
            while (j <= 8)
            {
                // decrement con mod
                conMod--;
                // increase con score
                j += 2;
            }
        }

        // sets character's starting max health to 8 + their con mod
        classToSpawn.GetComponent<Character>().maxHealth = (8 + conMod);

        // sets the character's gender
        classToSpawn.GetComponent<Character>().i_isMale = isMale;
    }

    string GenerateName(bool a_isMale)
    {
        LastName cha_lastName;
        FirstNameMale cha_firstNameMale = 0;
        FirstNameFemale cha_firstNameFemale = 0;

        string name;
        cha_lastName = (LastName)Random.Range(0, 8);

        if (a_isMale)
        {
            cha_firstNameMale = (FirstNameMale)Random.Range(0, 2);
            name = cha_firstNameMale.ToString() + " " + cha_lastName.ToString();
        }
        else
        {
            cha_firstNameFemale = (FirstNameFemale)Random.Range(0, 3);
            name = cha_firstNameFemale.ToString() + " " + cha_lastName.ToString();
        }
        
        return name;
    }

    public enum LastName
    {
        Moten,
        Cottell,
        Jansen,
        Ragnarson,
        Aramathi,
        Venke,
        Graves,
        Grimm,
    }

    public enum FirstNameMale
    {
        Clayton,
        Danthor,
    }

    public enum FirstNameFemale
    {
        Betje,
        Althea,
        Phola,
    }
    #endregion
}