using Terraria;
using RealClasses.UI;
using RealClasses.UI.AbilityButtons;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace RealClasses.Abilities
{
    public abstract class Ability
    {
        //Should probably add texture in here and shove it to AbilityButton
        protected Player player;
        protected AbilityButton abilityButton;
        protected int cooldown;
        protected int cooldownCounter;
        protected int duration;
        protected int durationCounter;
        protected ModHotKey hotKey;

        //Must have an ability and a cooldown. Maybe send these in via the method later if needed
        public Ability(Player player)
        {
            abilityButton = new AbilityButton();
            cooldownCounter = 0;
            cooldown = 600;
            this.player = player;
        }

        //Give the ability a hotkey
        public void SetHotKey(ModHotKey hotKey)
        {
            this.hotKey = hotKey;
        }

        public ModHotKey GetHotKey()
        {
            return this.hotKey;
        }

        //Let PlayerClass grab this Ability's AbilityButton safely
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

        //Pseudo PreUpdate shoved from MyPlayer
        public virtual void PreUpdate()
        {

        }

        //Pseudo ModifyHitByNPC shoved from MyPlayer
        public virtual void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {

        }

        public virtual void PostUpdateRunSpeeds()
        {

        }

        public virtual void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

        }
    }
}
