using Terraria;
using Terraria.ModLoader;
using RealClasses.Buffs.Berserk;
using RealClasses.UI;
using Terraria.UI;
using RealClasses.UI.AbilityButtons;
using Microsoft.Xna.Framework;

namespace RealClasses.Abilities
{
    public class BerserkAbility : Ability
    {
        //Ability specific
        int lifeLost;
        Color blood = new Color(255, 51, 51);

        public BerserkAbility(Player player) : base(player)
        {
            //Get instance to the berserk UI button
            abilityButton = new BerserkButton();
            cooldownCounter = 0;
            cooldown = 600;
        }

        //Berserk specific code
        public override void UseAbility(Player player)
        {
            abilityButton.SetCooldown(cooldown);
            cooldownCounter = cooldown;

            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, 4, 0, 0, 150, blood, 1.5f);
            }

            //If the buff is off completely... start the buff on stack one
            if (player.HasBuff(ModContent.BuffType<BerserkBuff1>()) == false && player.HasBuff(ModContent.BuffType<BerserkBuff2>()) == false && player.HasBuff(ModContent.BuffType<BerserkBuff3>()) == false && player.HasBuff(ModContent.BuffType<BerserkBuff4>()) == false && player.HasBuff(ModContent.BuffType<BerserkBuff5>()) == false)
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Start first stack of berserk for 10 seconds
                player.AddBuff(ModContent.BuffType<BerserkBuff1>(), 600, false);
                abilityButton.SetStack(1);
            }
            //If it's already at stack 1, start stack two...
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff1>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Clear 1st stack and start 2nd buff for 8 seconds
                player.ClearBuff(ModContent.BuffType<BerserkBuff1>());
                player.AddBuff(ModContent.BuffType<BerserkBuff2>(), 600, false);
                abilityButton.SetStack(2);
            }
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff2>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Clear 2nd stack start 3rd buff for 8 seconds
                player.ClearBuff(ModContent.BuffType<BerserkBuff2>());
                player.AddBuff(ModContent.BuffType<BerserkBuff3>(), 600, false);
                abilityButton.SetStack(3);
            }
            /*
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff3>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Clear 3rd stack and start 4th buff for 8 seconds
                player.ClearBuff(ModContent.BuffType<BerserkBuff3>());
                player.AddBuff(ModContent.BuffType<BerserkBuff4>(), 600, false);
                abilityButton.SetStack(4);
            }
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff4>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Clear 4th stack and start 5th buff for 8 seconds
                player.ClearBuff(ModContent.BuffType<BerserkBuff4>());
                player.AddBuff(ModContent.BuffType<BerserkBuff5>(), 600, false);
                abilityButton.SetStack(5);
            }
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff5>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Start 5th buff for 6 seconds
                player.AddBuff(ModContent.BuffType<BerserkBuff5>(), 600, false);
                abilityButton.SetStack(5);
            }
            */
            else if (player.HasBuff(ModContent.BuffType<BerserkBuff3>()))
            {
                //Lose 10% of your life
                lifeLost = player.statLifeMax2 / 10;
                player.statLife = player.statLife - (int)lifeLost;

                //Start 5th buff for 6 seconds
                player.AddBuff(ModContent.BuffType<BerserkBuff3>(), 600, false);
                abilityButton.SetStack(3);
            }
        }
    }
}
