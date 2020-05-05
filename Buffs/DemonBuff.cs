using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;

namespace RealClasses.Buffs
{
    public class DemonBuff : ModBuff
    {
        public override void SetDefaults()
        //Set the defaults of the buff
        {
            DisplayName.SetDefault("Demon");
            Description.SetDefault("This demon has taken a liking to you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //If there is already a minion, reset the buff time
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.DemonMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;   
            }
            //Else create the minion
            else
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("DemonMinion"), 100, 0f, player.whoAmI, 0f, 0f);
                //player.DelBuff(buffIndex);
                //buffIndex--;  
            }
        }
    }
}
