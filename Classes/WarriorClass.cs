using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;
using static Terraria.ModLoader.ModContent;

namespace RealClasses.Classes
{
    public class WarriorClass : PlayerClass
    {
        int level;
        Player player;// = new Player();
        //Ability placeholders
        public IAbility primaryAbility;
        public IAbility secondaryAbility;
        public IPassive primaryPassive;

        //Constructor. Needs player reference and level to set skills and stats correctly (when progression is a thing)
        public WarriorClass(Player player,  int level)
        {
            this.player = player;
            this.level = level;
            //Set ability manually for now
            primaryAbility = new BerserkAbility();
            primaryPassive = new LifeRegenPassive();
            //Fill up cooldown bar with abilities
            ModContent.GetInstance<RealClasses>().CooldownBar.SetButtons(primaryAbility.abilityButton, primaryAbility.abilityButton, primaryAbility.abilityButton, primaryAbility.abilityButton, primaryPassive.passiveButton);
        }

        public override void DoPassives()
        {
            primaryPassive.DoPassive(player);
        }

        public override void DoCooldowns()
        {

        }

        public override void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                primaryAbility.UseAbility(player);
            }
            else if (key == RealClasses.ability2)
            {
                primaryAbility.UseAbility(player);
            }
        }     
    }
}
