using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem{

    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        if(experience >= experienceToNextLevel)
        {
            //We check here if the character leveled up
            level++;
            experience -= experienceToNextLevel;
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public int GetExperienceNumber()
    {
        return experience;
    }

    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

}
