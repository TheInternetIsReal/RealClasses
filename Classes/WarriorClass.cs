using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;

namespace RealClasses.Classes
{
    public class WarriorClass : PlayerClass
    {
        //Ability placeholders

        //Constructor. Needs player reference and level to set skills and stats correctly (when progression is a thing)
        public WarriorClass(Player player,  int level) : base(player, level)
        {
            this.player = player;
            this.level = level;

            //Set abilities and hotkeys manually for now. Set it to active so player hooks work on it. Add ability and their buttons to lists for work later
            //Should be taking in and working over a list of abilities later
            ability1 = new BerserkAbility(player);
            player.GetModPlayer<MyPlayer>().ActiveAbilities.Add(ability1);
            buttons.Add(ability1.GetButton());
            abilities.Add(ability1);

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
