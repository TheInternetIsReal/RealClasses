using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace RealClasses.Buffs
{
    class LeapBuff : ModBuff
    {
        Random rnd = new Random();

        public override void SetDefaults()
        //Set the defaults of the buff
        {
            DisplayName.SetDefault("Leap");
            Description.SetDefault("You deal aoe damage when landing");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Boost players damage by 5%      
            //player.allDamage = (player.allDamage + (float)player.GetModPlayer<MyPlayer>().dmgBoost);
            if (player.velocity.Y == 0)
            {
                player.velocity.X = 0;
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(player.position + new Vector2(rnd.Next(-75,75),15), player.width, player.height, 4, 0, 0, 150, Color.White, 3);
                }
                //Do flat rectangle center on feet/ground and apply damage. Push NPC velocity up into the air a bit for fun, and x=0
                player.ClearBuff(ModContent.BuffType<LeapBuff>());
            }
        }
    }
}
