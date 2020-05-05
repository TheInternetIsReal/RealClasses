using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using RealClasses.Classes;
using RealClasses.Buffs;

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

        //For class choice (possibly do these in Mod.Load later)
        public string classString = "None";
        public PlayerClass playerClass;


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

        }

        //Happens every frame
        public override void PreUpdate()
        {
            #region Init
            if (firstFrame == true)
            {
                //Cleanup cooldown bar
                ModContent.GetInstance<RealClasses>().CooldownBar.Cleanup();

                //Set default class of none to mkae sure tehre are no errors
                playerClass = new TestClass(player, 100);
                //Set player class. Add if cod ehere later to choose class
                //if (player.name == "Unhallowed")
                //{
                //    playerClass = new WarriorClass(player, 100);
                //}
                //else if (player.name == "Greek" || player.name == "M4sterSplint3r")
                //{
                //    playerClass = new RogueClass(player, 100);
                //}
                //else if (player.name == "J3bacha")
                //{
                //    playerClass = new ZealotClass(player, 100);
                //}
                //else if (player.name == "Dawg")
                //{
                //    playerClass = new TestClass(player, 100);
                //}

                //Critical to set them to default here...
                //else playerClass = new PlayerClass(player, 0);
                firstFrame = false;
            }
            #endregion Init

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
