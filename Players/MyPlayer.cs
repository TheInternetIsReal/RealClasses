using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using RealClasses.Classes;

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
        public int healBombCD = 360;
        public int healBombCDCounter = 0;
        public bool popHealBomb = false;
        public int stealthCD = 720;
        public int stealthCDCounter = 0;

        //For class choice (possibly do these in Mod.Load later)
        public string classString = "None";
        PlayerClass playerClass;


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

        //Can the player be hit by NPC? Defaults to true. Changed by some skills
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return canBeHit;
        }

        //Drawing an effect on the character? Used by some abilities
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            a = opacity;
            fullBright = false;
        }

        //Do things on death. Related to some abilities
        public override void UpdateDead()
        {
            //These are related to the stealth buff bug. If you die with it on (say from an arrown) these won't get reset
            opacity = 1;
            canBeHit = true;
        }

        //On key presses
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (RealClasses.ability1.JustPressed)
            {
                playerClass.UseAbility(RealClasses.ability1);
            }

        }

        //Happens every frame
        public override void PreUpdate()
        {
            #region Init
            if (firstFrame == true)
            {
                //Set player class. Add if cod ehere later to choose class
                if (player.name == "Unhallowed")
                {
                    playerClass = new WarriorClass(player, 100);
                }
                else if (player.name == "Greek" || player.name == "M4sterSplint3r")
                {
                    playerClass = new RogueClass(player, 100);
                }
                else if (player.name == "J3bacha")
                {
                    playerClass = new ZealotClass(player, 100);
                }
                else if (player.name == "Dawg")
                {
                    playerClass = new SummonerClass(player, 100);
                }

                //Critical to set them to default here...
                else playerClass = new PlayerClass(player, 0);
                firstFrame = false;
            }
            #endregion Init

            //Do class passives
            playerClass.DoPassives();

            //Set out of combat
            if (outOfCombatCounter == 0)
            {
                outOfCombat = true;
                outOfCombatCounter = 0;
            }
            else outOfCombatCounter--;

            #region Cooldowns
            //Cooldown counters. Let them tick to zero and hold them there to avoid memory leaks
            //Stealth CD
            if (stealthCDCounter == 0)
            {
                //continue
            }
            else if (stealthCDCounter > 0)
            {
                stealthCDCounter--;          
            }
            //Heal Bomb CD
            if (healBombCDCounter == 0)
            {
                //continue
            }
            else if (healBombCDCounter > 0)
            {
                healBombCDCounter--;
            }
            #endregion Cooldowns
        }
    }
}
