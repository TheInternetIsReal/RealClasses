using Terraria;
using Terraria.ModLoader;
using RealClasses.Buffs;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    public class EvasionAbility : Ability
    {
        public EvasionAbility()
        {
            //Get instance to the berserk UI button
            abilityButton = new EvasionButton();
            cooldownCounter = 0;
            cooldown = 500;
        }

        public override void UseAbility(Player player)
        {
            if (cooldownCounter == 0)
            {
                abilityButton.SetCooldown(cooldown);
                cooldownCounter = cooldown;
                player.AddBuff(ModContent.BuffType<StealthBuff>(), 180, false);
            }
        }
    }
}
