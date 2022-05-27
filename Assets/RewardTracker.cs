using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTracker : MonoBehaviour
{
    public Reward rew;
    public int id; //level id

    public int uniqueEssence;
    public int clearEssence;
    public int destructionEssence;
    public int totalEssence;
   

    void Start()
    {
        if (!LevelCheck(rew))
        {
            uniqueEssence = rew.unique_essence;
            clearEssence = rew.clear_essence;
            
        } else
        {

        }
    }

    bool LevelCheck(Reward rew)
    {
        id = rew.id;
        int progress = PlayerPrefs.GetInt("levelProgress", 0);

        if (id < progress)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddDestructionBonus(int bonus)
    {
        destructionEssence += bonus;
    }
}
