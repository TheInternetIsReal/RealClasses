using Terraria;
using static Terraria.ModLoader.ModContent;
using RealClasses.Buffs;
using Terraria.UI;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    public class DemonAbility : IAbility
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
        public int _cooldown = 480;
        public int cooldownCounter = 0;

        public DemonAbility()
        {
            //Get instance to the berserk UI button
            abilityButton = new DemonButton();
            abilityButton.opaque = true;
        }

        public DemonAbility(string hotKey)
        {
            //Get instance to the berserk UI button
            abilityButton = new DemonButton(hotKey);
            abilityButton.opaque = true;
            
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
            if (player.HasBuff(BuffType<DemonBuff>()))
            {
                player.ClearBuff(BuffType<DemonBuff>());
                abilityButton.opaque = true;
            }
            else
            {
                player.AddBuff(BuffType<DemonBuff>(), 3600, false);
                abilityButton.opaque = false;
            }
        }
    }
}
