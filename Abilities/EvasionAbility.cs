using Terraria;
using Terraria.ModLoader;
using RealClasses.Buffs;
using RealClasses.UI.AbilityButtons;
using RealClasses.Players;

namespace RealClasses.Abilities
{
    public class EvasionAbility : Ability
    {
        protected bool canBeHit;
        protected bool evading;
        protected float opacity;

        public EvasionAbility(Player player) : base(player)
        {
            //Get instance to the berserk UI button
            abilityButton = new EvasionButton();
            this.player = player;
            cooldownCounter = 0;
            cooldown = 500;
            duration = 180;
            durationCounter = 0;
        }

        public override void UseAbility(Player player)
        {
            //If cooled down
            if (cooldownCounter == 0)
            {
                abilityButton.SetCooldown(cooldown);
                cooldownCounter = cooldown;

                evading = true;
                durationCounter = duration;
                player.GetModPlayer<MyPlayer>().canBeHit = false;            
                player.AddBuff(ModContent.BuffType<StealthBuff>(), 180, false); //Just a ui element, doesn't do anything to player
            }
        }

        public override void PreUpdate()
        {
            if (evading == true)
            {
                if (durationCounter > 0)
                {
                    //Look stealthy      
                    opacity = 255;
                    //Be stealthy
                    //...Actually be invincible for now...
                    player.GetModPlayer<MyPlayer>().canBeHit = false;
                    //Be quick
                    player.moveSpeed = 1.4f;
                }
                //Cleanup
                else if (durationCounter == 0)
                {
                    player.ClearBuff(ModContent.BuffType<StealthBuff>());
                    opacity = 1;
                    player.GetModPlayer<MyPlayer>().canBeHit = true;
                    durationCounter = duration;
                    evading = false;
                }
                durationCounter--;
            }    
        }

        public override void PostUpdateRunSpeeds()
        {
            //Run faster if evading
            if (evading == true)
            {
                player.maxRunSpeed = player.maxRunSpeed * 2f;
                player.moveSpeed = player.moveSpeed * 2f;
                player.accRunSpeed = player.accRunSpeed * 2f;
                player.runAcceleration = player.runAcceleration * 2f;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (evading == true)
            {
                a = opacity;
                fullBright = false;
            }
        }
    }
}
