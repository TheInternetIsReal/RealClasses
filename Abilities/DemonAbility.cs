using Terraria;
using static Terraria.ModLoader.ModContent;
using RealClasses.Buffs;

namespace RealClasses.Abilities
{
    public class DemonAbility : IAbility
    {
        public void UseAbility(Player player)
        {
            player.statLife = player.statLifeMax2;
            if (player.HasBuff(BuffType<DemonBuff>()))
            {
                player.ClearBuff(BuffType<DemonBuff>());
            }
            else player.AddBuff(BuffType<DemonBuff>(), 3600, false);
        }
    }
}
