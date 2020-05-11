using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;

namespace RealClasses.Classes
{
    class RogueClass : PlayerClass
    {
        public RogueClass(Player player, int level) : base(player, level)
        {
            this.player = player;
            this.level = level;
            //Set ability manually for now
            ability1 = new EvasionAbility(player);
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), ability1.GetButton());
        }

        //Remove this when he gets passives
        public override void DoPassives()
        {

        }
    }
}
