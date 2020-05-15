using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using RealClasses.Players;
using RealClasses.UI.MenuButtons;

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
            //Should be taking in and working over a list of abilities later
            ability1 = new LeapAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability1);
            abilities.Add(ability1);
            buttons.Add(ability1.GetButton());         

            ability2 = new EvasionAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability2);
            abilities.Add(ability2);
            buttons.Add(ability2.GetButton());

            ability3 = new HealBombAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability3);
            buttons.Add(ability3.GetButton());
            abilities.Add(ability3);

            ability4 = new DemonAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability4);
            buttons.Add(ability4.GetButton());
            abilities.Add(ability4);

            ability5 = new BerserkAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability5);
            buttons.Add(ability5.GetButton());
            abilities.Add(ability5);

            primaryPassive = new LifeRegenPassive();
            buttons.Add(primaryPassive.GetButton());
            passives.Add(primaryPassive);

            //Give each ability a ModHotKey if they are available
            SetHotKeys();

            //Fill up cooldown bar with abilities
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(buttons);
        }
    }
}
