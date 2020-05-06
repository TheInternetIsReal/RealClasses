using Terraria;
using static Terraria.ModLoader.ModContent;
using RealClasses.Buffs;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    public class DemonAbility : Ability
    {
        public DemonAbility()
        {
            abilityButton = new DemonButton();
            abilityButton.SetOpaque(true);
            cooldownCounter = 0;
            cooldown = 0;
        }

        public override void UseAbility(Player player)
        {
            if (player.HasBuff(BuffType<DemonBuff>()))
            {
                player.ClearBuff(BuffType<DemonBuff>());
                abilityButton.SetOpaque(true);
            }
            else
            {
                player.AddBuff(BuffType<DemonBuff>(), 3600, false);
                abilityButton.SetOpaque(false);
            }
        }
    }
}
