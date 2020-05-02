using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;

namespace RealClasses.Buffs
{
    public class StealthBuff : ModBuff
    {
        //Need to turn this into a real stealth skill by setting NPC's target to something else or nothing if single player
        // player.aggro is key, lowering it makes them see you as farther away than you are, raising it works the opposite way
        public override void SetDefaults()
        //Set the defaults of the buff
        {
            DisplayName.SetDefault("Evasion");
            Description.SetDefault("You move twice as fast and evade all attacks for 3 seconds");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Look stealthy      
            player.GetModPlayer<MyPlayer>().opacity = 255;
            //Be stealthy
            //...Actually be invincible for now...
            player.GetModPlayer<MyPlayer>().canBeHit = false;
            //Be quick
            player.moveSpeed = 3;

            //Put opacity back to normal 1 frame before the buff wears off
            if (player.buffTime[buffIndex] < 2)
            {
                player.GetModPlayer<MyPlayer>().opacity = 1;
                player.GetModPlayer<MyPlayer>().canBeHit = true;
            }

            //Lose enemies targeting with player.aggro
        }
    }
}
