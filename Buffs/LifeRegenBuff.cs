using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;

namespace RealClasses.Buffs
{
    public class LifeRegenBuff : ModBuff
    {
        public override void SetDefaults()
        //Set the defaults of the buff
        {
            DisplayName.SetDefault("LifeRegen");
            Description.SetDefault("You regenerate life quicker when out of combat. Slows down the closer you are to full health.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
             //if (player.GetModPlayer<MyPlayer>().outOfCombat)
            //{
                //Regen life
                player.lifeRegen = (player.statLifeMax2 - player.statLife) / 10;
                player.buffTime[buffIndex] = 18000;
            //}
        }
    }
}
