using RealClasses.UI.AbilityButtons;
using Terraria;

namespace RealClasses.Passives
{
    public abstract class Passive
    {
        protected AbilityButton passiveButton;

        public Passive()
        {
            passiveButton = new AbilityButton();
        }

        public virtual AbilityButton GetButton()
        {
            return passiveButton;
        }

        public virtual void DoPassive(Player player) { }


    }
}
