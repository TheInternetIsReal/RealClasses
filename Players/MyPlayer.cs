using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using RealClasses.Classes;
using RealClasses.Abilities;
using System.Collections.Generic;


namespace RealClasses.Players
{
    public class MyPlayer : ModPlayer
    {
        //// Variables////
        ///60 frames = 1 second
        public bool firstFrame = true;
        public float dmgBoost = 0;
        public bool outOfCombat = true;
        public int outOfCombatFrames = 480;
        public int outOfCombatCounter = 0;
        public float opacity = 1f;
        public bool canBeHit = true;
        //Can I use projectile kill() later?
        public bool popHealBomb = false;
        public bool preHurt = true;

        //For class choice (possibly do these in Mod.Load later)
        public PlayerClass playerClass;

        //A list of all current abilities instantiated on the player. Used to shove things like PreUpdate() to them since inheriting from ModPlayer was a fail
        //This list is called in every method below that is required by an ability later on
        //Ability will have matching functions ready to be triggerred later (say PreUpdate())
        public List<Ability> ActiveAbilities = new List<Ability>();

        //A list to hold NPCs that the player is invuln to currently. Used by skills like Leap
        public List<NPC> InvulnNPCs = new List<NPC>();


        ////Methods////

        public override void Initialize()
        {
            //This happens during character select screen. Possible a good place to load things if not in RealClasses.Load()
        }

        //When hitting an NPC
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {         
            //Set in combat
            outOfCombat = false;
            outOfCombatCounter = outOfCombatFrames;
        }

        //When hit by anything
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            //Set in combat
            outOfCombat = false;
            outOfCombatCounter = outOfCombatFrames;
        }

        //Can the player be hit by NPC? Defaults to true
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            //Check if player is currently invuln to any NPCs. Set from skills like Leap
            foreach (NPC npcs in InvulnNPCs)
                {
                return false;
                }
            return canBeHit;
        }

        //Not used by any abilities currently
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            //ModifyHitByNPC shove to Ability children
            foreach (Ability ability in ActiveAbilities)
            {
                ability.ModifyHitByNPC(npc, ref damage, ref crit);
            }
        }

        //For movement in stuff like Evasion
        public override void PostUpdateRunSpeeds()
        {
            foreach (Ability ability in ActiveAbilities)
            {
                ability.PostUpdateRunSpeeds();
            }
        }

        //Drawing an effect on the character? Used by some abilities
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            foreach (Ability ability in ActiveAbilities)
            {
                ability.DrawEffects(drawInfo, ref  r, ref g, ref b, ref a, ref fullBright);
            }
        }

        //On key presses
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            foreach (ModHotKey hotKey in RealClasses.hotKeys)
            {
                if (hotKey.JustPressed)
                {
                    playerClass.UseAbility(hotKey);
                }
            }


            /*
            if (RealClasses.ability1.JustPressed)
            {
                playerClass.UseAbility(RealClasses.ability1);
            }
            else if (RealClasses.ability2.JustPressed)
            {
                playerClass.UseAbility(RealClasses.ability2);
            }
            else if (RealClasses.ability3.JustPressed)
            {
                playerClass.UseAbility(RealClasses.ability3);
            }
            else if (RealClasses.ability4.JustPressed)
            {
                playerClass.UseAbility(RealClasses.ability4);
            }
            */

        }

        //Happens every frame
        public override void PreUpdate()
        {
            #region Init
            if (firstFrame == true)
            {
                //Cleanup abilities
                ActiveAbilities.Clear();
                //Cleanup cooldown bar
                ModContent.GetInstance<RealClasses>().CooldownBar.Cleanup();

                //Setting to test class. Abilities and cooldown bar will be constructed
                playerClass = new TestClass(player, 100);

                //Critical to set them to default here...
                //else playerClass = new PlayerClass(player, 0);
                firstFrame = false;
            }
            #endregion Init

            //PreUpdate shove to Ability children
            foreach (Ability ability in ActiveAbilities)
            {
                ability.PreUpdate();
            }

            //Do I need to shove these seperately or should they be in Abilty's PreUpdate?
            //Run passives here since there's no sense of Update() in the Passive class
            playerClass.DoPassives();
            //Do cooldowns here since there's no sense of Update() in the Ability class
            playerClass.DoCooldowns();
            //Give UI buttons their hotkeys through a daisy chain since there is no sense of Update() in the Ability class
            playerClass.GiveHotKeys();

            //Set out of combat
            if (outOfCombatCounter == 0)
            {
                outOfCombat = true;
                outOfCombatCounter = 0;
            }
            else outOfCombatCounter--;
        }
    }
}
