using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.UI;

namespace RealClasses.Classes
{
    public class PlayerClass
    {
        //Things required to be a class in this game
        Player player;
        string classString;
        int level;
        List<string> abilities = new List<string>();
        List<string> passives = new List<string>();
        IAbility primaryAbility;
        public CooldownBar cooldownBar;
        //Stats to come here...

        //Default constructor. Until a class is set...
        public PlayerClass()
        {
            this.classString = "None";
        }

        //Get player reference and level (likely from file later)
        public PlayerClass(Player player, int level)
        {
            this.player = player;
            this.level = level;
        }

        public virtual void GiveHotKeys()
        {

        }

        //Passives. Called each frame from MyPlayer
        public virtual void DoPassives()
        {

        }

        //Active Abilities
        public virtual void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                //Do an ability
            }
        }

        //Countdown CD's
        public virtual void DoCooldowns()
        {
            //Where are cooldowns going to live? I'd rather not call the player to get it. It should be in ability code.
            //How would I count things per frame without PreUpdate() or AI()?
        }

        //Function to build current abiltiies and skill when the player becomes the class, based off level
        //This should likely be saved in a file and loaded later on
    }
}