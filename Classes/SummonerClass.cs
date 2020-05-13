using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;

namespace RealClasses.Classes
{
    class SummonerClass : PlayerClass
    {
        public SummonerClass(Player player, int level) : base(player, level)
        {
            this.player = player;
            this.level = level;
            ability1 = new DemonAbility(player);
            //ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), ability1.GetButton(), ability1.GetButton());
        }

        //Passives. Called each frame from MyPlayer
        public override void DoPassives()
        {

        }
    }
}
