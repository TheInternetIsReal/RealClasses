using Terraria;
using Terraria.ModLoader;
using RealClasses.Players;
using RealClasses.Buffs;

namespace RealClasses.Passives
{
    public class LifeRegenPassive : IPassive
    {
        public void DoPassive(Player player)
        {
            if (player.GetModPlayer<MyPlayer>().outOfCombat)
            {
                if (player.HasBuff(ModContent.BuffType<LifeRegenBuff>()))
                {
                    //continue - buff class itself will keep its timer going
                }
                else player.AddBuff(ModContent.BuffType<LifeRegenBuff>(), 999, false);
            }
            else player.ClearBuff(ModContent.BuffType<LifeRegenBuff>());
        }
    }
}
