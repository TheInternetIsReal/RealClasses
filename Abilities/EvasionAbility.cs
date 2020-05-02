using Terraria;
using Terraria.ModLoader;
using RealClasses.Buffs;
using RealClasses.Players;

namespace RealClasses.Abilities
{
    public class EvasionAbility : IAbility
    {
        public void UseAbility(Player player)
        {
            //If cooled down
            if (player.GetModPlayer<MyPlayer>().stealthCDCounter == 0)
            {
                //Do I really want to let them clear the buff?
                if (player.HasBuff(ModContent.BuffType<StealthBuff>()))
                {
                    player.ClearBuff(ModContent.BuffType<StealthBuff>());
                    //Set opacity back to normal, jesus this code is a mess due to lack of functionality of tModLoader
                    player.GetModPlayer<MyPlayer>().opacity = 1;
                    player.GetModPlayer<MyPlayer>().canBeHit = true;
                }
                else
                {
                    player.AddBuff(ModContent.BuffType<StealthBuff>(), 180, false);
                    player.GetModPlayer<MyPlayer>().stealthCDCounter = player.GetModPlayer<MyPlayer>().stealthCD;
                }
            }
        }
    }
}
