using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;

namespace RealClasses.Classes
{
    class RogueClass : PlayerClass
    {
        int level;
        Player player = new Player();
        List<string> abilities = new List<string>();
        List<string> passives = new List<string>();
        IAbility primaryAbility;

        //Get player reference and level (likely from file later)
        public RogueClass(Player player, int level)
        {
            this.player = player;
            this.level = level;
            //Set ability manually for now
            primaryAbility = new EvasionAbility();
        }

        //Passives. Called each frame from MyPlayer
        public override void DoPassives()
        {

        }

        //Active Abilities
        public override void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                primaryAbility.UseAbility(player);
            }
        }
    }
}
