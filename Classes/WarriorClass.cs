using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.Passives;

namespace RealClasses.Classes
{
    public class WarriorClass : PlayerClass
    {
        int level;
        Player player = new Player();
        //Ability placeholders
        public IAbility primaryAbility;
        public IPassive primaryPassive;


        //Constructor. Needs player reference and level to set skills and stats correctly (when progression is a thing)
        public WarriorClass(Player player,  int level)
        {
            this.player = player;
            this.level = level;
            //Set ability manually for now
            primaryAbility = new BerserkAbility();
            primaryPassive = new LifeRegenPassive();
        }

        public override void DoPassives()
        {
            primaryPassive.DoPassive(player);
        }

        public override void UseAbility(ModHotKey key)
        {
            if (key == RealClasses.ability1)
            {
                primaryAbility.UseAbility(player);
            }
        }
    }
}
