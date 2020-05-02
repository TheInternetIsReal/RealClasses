﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;

namespace RealClasses.Buffs.Berserk
{
    public class BerserkBuff5 : ModBuff
    {
        public override void SetDefaults()
        //Set the defaults of the buff
        {
            DisplayName.SetDefault("Berserk");
            Description.SetDefault("+25% damage. Pay 10% health to deal 5% more damage. Stacks 5 times");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Boost players damage by 5%      
            //player.allDamage = (player.allDamage + (float)player.GetModPlayer<MyPlayer>().dmgBoost);
            player.allDamage = player.allDamage + 0.25f;
        }
    }
}
