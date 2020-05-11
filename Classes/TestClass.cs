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

        //Constructor. Use base because those live in the parent and aren't above ^
        public TestClass(Player player, int level) : base(player, level)
        {
            this.player = player;
            this.level = level;

            //Set abilities and hotkeys manually for now. First one is an experiment from PlayerClass. This is really what sets each class apart so far
            ability1 = new LeapAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability1);
            ability2 = new EvasionAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability2);
            ability3 = new HealBombAbility(player);
            ability4 = new DemonAbility(player);
            primaryPassive = new LifeRegenPassive();

            //Fill up cooldown bar with abilities
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(ability1.GetButton(), ability2.GetButton(), ability3.GetButton(), ability4.GetButton(), primaryPassive.GetButton());
        }

        public override void GiveHotKeys()
        {
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

        }

        public override void DoCooldowns()
        {
            ability1.DoCooldown();
            ability2.DoCooldown();
            ability3.DoCooldown();
            ability4.DoCooldown();
        }

        public override void DoPassives()
        {
            primaryPassive.DoPassive(player);
        }

        public override void UseAbility(ModHotKey key)
        {
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
        }
    }
}
