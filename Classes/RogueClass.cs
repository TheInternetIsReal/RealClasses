using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Players;

namespace RealClasses.Classes
{
    class RogueClass : PlayerClass
    {
        public RogueClass(Player player, int level) : base(player, level)
        {
            this.player = player;
            this.level = level;

            //Set abilities and hotkeys manually for now. Set it to active so player hooks work on it. Add ability and their buttons to lists for work later
            //Should be taking in and working over a list of abilities later
            ability1 = new EvasionAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability1);
            buttons.Add(ability1.GetButton());
            abilities.Add(ability1);

            //Give each ability a ModHotKey if they are available
            SetHotKeys();

            //Fill up cooldown bar with abilities
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(buttons);
        }

    }
}
