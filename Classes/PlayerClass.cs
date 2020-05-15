using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using Terraria.UI;

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
        protected List<UIElement> buttons;
        protected List<Ability> abilities;
        protected List<Passive> passives;
        //Stats to come?

        //We at least need a player, and level later. This should eventually force 4 abilities and one passive, unless classes can have less than that many skills...
        public PlayerClass(Player player, int level)
        {
            this.player = player;
            this.level = level;
            buttons = new List<UIElement>();
            abilities = new List<Ability>();
            passives = new List<Passive>();
        }

        //Set ModHotKey for each ability
        public void SetHotKeys()
        {
            //Give each ability a hotKey over from RealClasses/Controls menu in game
            for (int i = 0; i < abilities.Count; i++)
            {
                //Ensure there are enough hotkeys left to give else don't give a hotKey
                if (i < RealClasses.hotKeys.Count)
                {
                    //Assign the hotkey
                    abilities[i].SetHotKey(RealClasses.hotKeys[i]);
                }
            }
        }

        //Shoves the ModHotKey binding to the ability button to show on the CooldowBar. Heavily chained but required due to lack of Update() past this point
        //This seems backwards but is required so that the hotkey is udpated on every frame over in the AbilityButtons
        //Maybe assign UIButtons to PlayerClasses instead of of Abilities like is happening today or put this code over in Abilities
        public virtual void GiveHotKeys()
        {
            foreach (Ability ability in abilities)
            {
                //Make sure it has a ModHotKey assigned and that it is bound, else say it's Empty
                if (ability.GetHotKey() != null && ability.GetHotKey().GetAssignedKeys().Count > 0)
                {
                    ability.GiveHotKey(ability.GetHotKey().GetAssignedKeys()[0].ToString());
                }
                else ability.GiveHotKey("Empty");
            }
        }

        //Passives happen every frame. Player tells us to do them every frame and we point the player to the passive we have
        public virtual void DoPassives()
        {
            foreach (Passive passive in passives)
            {
                primaryPassive.DoPassive(player);
            }
        }

        //Player tells our class when a key is pushed and our class decides what ability to activate. Abilities must be declared in the inherited class
        public virtual void UseAbility(ModHotKey hotKey)
        {
            foreach (Ability ability in abilities)
            {
                //Check if the ModHotKey pressed is assigned to the ability (happens back up in SetHotKeys)
                if (hotKey == ability.GetHotKey())
                    ability.UseAbility(player);
            }
        }

        //CDs need to countdown every frame and only player can tell us to do that. We just point him to the ability to cooldown
        public virtual void DoCooldowns()
        {
            foreach (Ability ability in abilities)
            {
                ability.DoCooldown();
            }
        }

        //Function to build current abiltiies and skill when the player becomes the class, based off level
        //This should likely be saved in a file and loaded later on
    }
}