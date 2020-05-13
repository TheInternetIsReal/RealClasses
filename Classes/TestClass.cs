using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using RealClasses.Players;

namespace RealClasses.Classes
{
    class TestClass : PlayerClass
    {
        //Ability placeholders
        public Ability ability2;
        public Ability ability3;
        public Ability ability4;
        public Ability ability5;

        //Constructor. Use base because those live in the parent and aren't above ^
        public TestClass(Player player, int level) : base(player, level)
        {
            this.player = player;
            this.level = level;

            //Set abilities and hotkeys manually for now. Set it to active so player hooks work on it. Add ability and their buttons to lists for work later
            ability1 = new LeapAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability1);
            abilities.Add(ability1);
            buttons.Add(ability1.GetButton());         

            ability2 = new EvasionAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability2);
            abilities.Add(ability2);
            buttons.Add(ability2.GetButton());

            ability3 = new HealBombAbility(player);
            buttons.Add(ability3.GetButton());
            abilities.Add(ability3);

            ability4 = new DemonAbility(player);
            buttons.Add(ability4.GetButton());
            abilities.Add(ability4);

            ability5 = new BerserkAbility(player);
            buttons.Add(ability5.GetButton());
            abilities.Add(ability5);

            primaryPassive = new LifeRegenPassive();
            buttons.Add(primaryPassive.GetButton());

            //Give each ability a ModHotKey if they are available
            SetHotKeys();

            //Fill up cooldown bar with abilities
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(buttons);
        }

        //Set hotKey for each ability
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

        //This seems backwards but is required so that the hotkey is udpated on every frame
        public override void GiveHotKeys()
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
            
            /*
            //Give each ability a hotKey over from RealClasses/Controls menu in game
            for (int i = 0; i < abilities.Count; i++)
            {
                //Ensure there are enough hotkeys left to give
                if (i < RealClasses.hotKeys.Count)
                {
                    //If the hotkey is bound
                    if (RealClasses.hotKeys[i].GetAssignedKeys().Count > 0)
                    {
                        //Assign the hotkey
                        abilities[i].GiveHotKey(RealClasses.hotKeys[i].GetAssignedKeys()[0].ToString());
                    }
                }
            }
            */


            /*
            foreach (Ability ability in abilities)
            {
                foreach (ModHotKey hotKey in RealClasses.hotKeys)
                {
                    if (hotKey.GetAssignedKeys().Count > 0)
                    {
                        ability.GiveHotKey(hotKey.GetAssignedKeys()[0].ToString());
                    }
                    else ability.GiveHotKey("Empty");
                }
            }
            */

            /*
            if (RealClasses.ability1.GetAssignedKeys().Count > 0)
            {
                ability1.GiveHotKey(RealClasses.ability1.GetAssignedKeys()[0].ToString());
            }
            else ability1.GiveHotKey("Empty");

            if (RealClasses.ability2.GetAssignedKeys().Count > 0)
            {
                ability2.GiveHotKey(RealClasses.ability2.GetAssignedKeys()[0].ToString());
            }
            else ability2.GiveHotKey("Empty");

            if (RealClasses.ability3.GetAssignedKeys().Count > 0)
            {
                ability3.GiveHotKey(RealClasses.ability3.GetAssignedKeys()[0].ToString());
            }
            else ability3.GiveHotKey("Empty");

            if (RealClasses.ability4.GetAssignedKeys().Count > 0)
            {
                ability4.GiveHotKey(RealClasses.ability4.GetAssignedKeys()[0].ToString());
            }
            else ability4.GiveHotKey("Empty");
            */

        }

        public override void DoCooldowns()
        {
            foreach (Ability ability in abilities)
            {
                ability.DoCooldown();
            }
            //ability1.DoCooldown();
            //ability2.DoCooldown();
            //ability3.DoCooldown();
            //ability4.DoCooldown();
        }

        public override void DoPassives()
        {
            primaryPassive.DoPassive(player);
        }

        public override void UseAbility(ModHotKey hotKey)
        {
            foreach (Ability ability in abilities)
            {
                if (hotKey == ability.GetHotKey())
                ability.UseAbility(player);
            }

            /*
            if (key == RealClasses.ability1)
            {
                ability1.UseAbility(player);
            }
            else if (key == RealClasses.ability2)
            {
                ability2.UseAbility(player);
            }
            else if (key == RealClasses.ability3)
            {
                ability3.UseAbility(player);
            }
            else if (key == RealClasses.ability4)
            {
                ability4.UseAbility(player);
            }
            */
        }
    }
}
