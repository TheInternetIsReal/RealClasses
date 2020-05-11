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
    }
}
