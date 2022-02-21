using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTracker : MonoBehaviour
{
    public Reward rew;
    public int id; //level id

    public int totalEssence;
    public int uniqueEssence;

    void Start()
    {
        if (!LevelCheck(rew))
        {
            uniqueEssence = rew.unique_essence;
            totalEssence += uniqueEssence;
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
}
