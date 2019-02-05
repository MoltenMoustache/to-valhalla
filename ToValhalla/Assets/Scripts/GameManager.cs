using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public string playerName;

    //currency
    public int runes;

    //UIs
    [Header("UI References")]
    [SerializeField]
    GameObject skillTreeUI;

    //rage
    public bool isRaged = false;
    [SerializeField]
    float maxRageTime;
    float rageTime;

    //skills
    [Header("Skill Object References")]
    public SkillSO rage;
    public SkillSO fires;
    public SkillSO ice;
    public SkillSO vidarsBlood;

    //has unlocked skills
    [Header("Unlocked Skill Booleans")]
    public bool unlockedRage;
    public bool unlockedFire;
    public bool unlockedFrost;
    public bool unlockedVidarsBlood;
    bool usedBlood = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unlockedRage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(rage);
        unlockedFire = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(fires);
        unlockedFrost = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(ice);
        unlockedVidarsBlood = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>().playerSkills.Contains(vidarsBlood);

        if (isRaged)
        {
            Rage();
        }

        if (unlockedVidarsBlood)
        {
            VidarsBlood();
        }

    }

    public void UseSpecial()
    {
        if (unlockedRage)
        {
            isRaged = true;
            if (unlockedFire)
            {
                Debug.Log("Unleash the " + fires.skillName);
            }
            else if (unlockedFrost)
            {
                Debug.Log("Unleash the " + ice.skillName);
            }
        }
        
    }

    void Rage()
    {
        rageTime -= Time.deltaTime;
        if (rageTime <= 0)
        {
            isRaged = false;
            rageTime = maxRageTime;
        }
    }

    public void VidarsBlood()
    {
        if (!usedBlood)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IncreaseMaxHealth(25);
            usedBlood = true;
        }
    }

    public void AddRunes(int amount)
    {
        runes += amount;
        skillTreeUI.GetComponent<SkillTreeUI>().UpdateRuneCounter();
    }

    public void RemoveRunes(int amount)
    {
        runes -= amount;
        skillTreeUI.GetComponent<SkillTreeUI>().UpdateRuneCounter();
    }
}
