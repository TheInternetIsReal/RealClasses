using Terraria;
using RealClasses.UI;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    public abstract class Ability
    {
        protected AbilityButton abilityButton;
        protected int cooldown;
        protected int cooldownCounter;

        //Must have an ability and a cooldown. Maybe send these in via the method later if needed
        public Ability()
        {
            abilityButton = new AbilityButton();
            cooldownCounter = 0;
            cooldown = 600;
        }

        //Must attach to a UI ability button to draw the texture and cooldown on the cooldown bar
        public virtual AbilityButton GetButton()
        {
            return abilityButton;
        }

        //Must send the hotkey to the ability button so it can draw it
        public virtual void GiveHotKey(string hotKey)
        {
            //Turn this into a setter later when hotKey is made private
            abilityButton.SetHotKey(hotKey);
        }

        //Gotta countdown time to actually lower the cooldown
        public virtual void DoCooldown()
        {
            if (cooldownCounter <= 0)
            {
                abilityButton.SetStack(0);
            }
            else cooldownCounter--;
        }

        //Usually starts like this
        public virtual void UseAbility(Player player)
        {
            abilityButton.SetCooldown(cooldown);
            cooldownCounter = cooldown;
        }
    }
}
