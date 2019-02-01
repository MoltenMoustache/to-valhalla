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

    //currency
    public int runes;

    //rage
    public bool isRaged = false;
    [SerializeField]
    float maxRageTime;
    float rageTime;

    //skills
    public SkillSO rage;
    public SkillSO fires;
    public SkillSO ice;

    //has unlocked skills
    public bool unlockedRage;
    public bool unlockedFire;
    public bool unlockedFrost;
    
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

        if (isRaged)
        {
            Rage();
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
}
