using Terraria;
using Terraria.ModLoader;
using RealClasses.Players;
using RealClasses.Buffs;
using Terraria.UI;
using RealClasses.UI.AbilityButtons;


namespace RealClasses.Passives
{
    public class LifeRegenPassive : IPassive
    {
        //For IPassive
        public AbilityButton passiveButton { get; set; }

        public LifeRegenPassive()
        {
            passiveButton = new LifeRegenButton();
        }

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
            else
            {
                //Clear buff, set button to opaque and set cooldown to out of combat timer
                player.ClearBuff(ModContent.BuffType<LifeRegenBuff>());
                passiveButton.cooldown = player.GetModPlayer<MyPlayer>().outOfCombatCounter;
            }
        }
    }
}
