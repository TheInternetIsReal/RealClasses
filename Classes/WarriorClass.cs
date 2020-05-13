using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using static Terraria.ModLoader.ModContent;

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
            //Set ability manually for now
            ability1 = new BerserkAbility(player);
            primaryPassive = new LifeRegenPassive();
            //Fill up cooldown bar with abilities
            //ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), primaryPassive.GetButton());
        }

        public override void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                ability1.UseAbility(player);
            }
            else if (key == RealClasses.ability2)
            {
                ability1.UseAbility(player);
            }
        }     
    }
}
