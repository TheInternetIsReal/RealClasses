using Terraria;
using Terraria.ModLoader;
using RealClasses.Buffs;
using RealClasses.Players;
using Terraria.UI;
using RealClasses.UI.AbilityButtons;
using RealClasses.UI;
using System.Collections.Generic;

namespace RealClasses.Abilities
{
    public class EvasionAbility : IAbility
    {
        //For IAbility
        public AbilityButton abilityButton { get; set; }
        public int Cooldown  // read-write instance property
        {
            get
            {
                return _cooldown;
            }
            set
            {
                _cooldown = value;
            }
        }
        public int _cooldown = 500;
        public int cooldownCounter = 0;
        public string help;

        public EvasionAbility()
        {
            //Get instance to the berserk UI button
            abilityButton = new EvasionButton();
        }

        public EvasionAbility(List<string> assignedKey)
        {
            //Get instance to the berserk UI button
            abilityButton = new EvasionButton();
            help = assignedKey[0];
        }

        public void GiveHotKey(string hotKey)
        {
            abilityButton.hotKey = hotKey;
        }

        public void DoCooldown()
        {
            if (cooldownCounter == 0)
            {
                //continue
            }
            else cooldownCounter--;
        }

        public void UseAbility(Player player)
        {
            if (cooldownCounter == 0)
            {
                abilityButton.cooldown = Cooldown;
                cooldownCounter = Cooldown;
                //If cooled down
                //if (player.GetModPlayer<MyPlayer>().stealthCDCounter == 0)
                //{
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
                        //player.GetModPlayer<MyPlayer>().stealthCDCounter = player.GetModPlayer<MyPlayer>().stealthCD;
                    }
                //}
            }
        }
    }
}
