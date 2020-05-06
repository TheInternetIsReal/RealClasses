using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;

namespace RealClasses.Classes
{
    public abstract class PlayerClass
    {
        //This signifies a player class such as warrior, rogue, or archer. It includes logic for instantaiting one ability, one passive ability and the functionality
        //required to operate them: UseAbility, DoPassive, DoCooldowns and SendHotKeys (for UI). Sadly, these all must be triggered in player itself every frame since 
        //PlayerClass does not have access to an Update() style method.

        //Things required to be a class in this game
        protected Player player;
        protected int level;
        protected Ability ability1;
        protected Passive primaryPassive;
        //Stats to come?

        //We at least need a player, and level later. This should eventually force 4 abilities and one passive, unless calsses can have less than that many skills...
        public PlayerClass(Player player, int level)
        {
            this.player = player;
            this.level = level;
        }

        //Shoves the ModHotKey binding to the ability button tos how on the CooldowBar. Heavily chained but required due to lack of Update() past this point
        public virtual void GiveHotKeys()
        {
            if (RealClasses.ability1.GetAssignedKeys().Count > 0)
            {
                ability1.GiveHotKey(RealClasses.ability1.GetAssignedKeys()[0].ToString());
            }
            else ability1.GiveHotKey("Empty");
        }

        //Passives happen every frame. Player tells us to do them every frame and we point the player to the passive we have
        public virtual void DoPassives()
        {
            primaryPassive.DoPassive(player);
        }

        //Player tells our class when a key is pushed and our class decides what ability to activate. Abilities must be declared in the inherited class
        public virtual void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                ability1.UseAbility(player);
            }
        }

        //CDs need to countdown every frame and only player can tell us to do that. We just point him to the ability to cooldown
        public virtual void DoCooldowns()
        {
            ability1.DoCooldown();
        }

        //Function to build current abiltiies and skill when the player becomes the class, based off level
        //This should likely be saved in a file and loaded later on
    }
}