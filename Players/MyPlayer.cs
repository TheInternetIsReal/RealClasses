using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using RealClasses.Classes;
using RealClasses.Abilities;
using System.Collections.Generic;
using RealClasses.Experience;


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
        public Exp exp;

        //A list of all current abilities instantiated on the player. Used to shove things like PreUpdate() to them since inheriting from ModPlayer was a fail
        //This list is called in every method below that is required by an ability later on
        //Ability will have matching functions ready to be triggerred later (say PreUpdate())
        public List<Ability> ActiveAbilities = new List<Ability>();

        //A list to hold NPCs that the player is invuln to currently. Used by skills like Leap
        public List<NPC> InvulnNPCs = new List<NPC>();
        


        ////Methods////

        //Change class. Cleanup stuff and reset abilities
        public void ChangeClass(string className)
        {
            //If a class is set
            if (playerClass != null)
            {
                //Cleanup current ability hooks
                ActiveAbilities.Clear();
                //Cleanup cooldown bar
                ModContent.GetInstance<RealClasses>().CooldownBar.Cleanup();
            }

            //Class choice and isntantiation
            if (className == "Test")
            {
                playerClass = new TestClass(player, 100);
            }
            else if (className == "Warrior")
            {
                playerClass = new WarriorClass(player, 100);
            }
            else if (className == "Zealot")
            {
                playerClass = new ZealotClass(player, 100);
            }
            else if (className == "Rogue")
            {
                playerClass = new RogueClass(player, 100);
            }
            else if (className == "Summoner")
            {
                playerClass = new SummonerClass(player, 100);
            }

            //Exp setup
            exp = new Exp();
        }
        

        ///Hooks///

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

        //Happens every frame
        public override void PreUpdate()
        {
            //DO class related things/hooks
            if (playerClass != null)
            {
                //Do I need to shove these seperately or should they be in Abilty's PreUpdate?
                //Run passives here since there's no sense of Update() in the Passive class
                playerClass.DoPassives();
                //Do cooldowns here since there's no sense of Update() in the Ability class
                playerClass.DoCooldowns();
                //Give UI buttons their hotkeys through a daisy chain since there is no sense of Update() in the Ability class
                playerClass.GiveHotKeys();
            }

            //PreUpdate shove to Ability children
            foreach (Ability ability in ActiveAbilities)
            {
                ability.PreUpdate();
            }

            //Set out of combat
            if (outOfCombatCounter == 0)
            {
                outOfCombat = true;
                outOfCombatCounter = 0;
            }
            else outOfCombatCounter--;
        }

        //On key presses
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            foreach (ModHotKey hotKey in RealClasses.hotKeys)
            {
                if (hotKey.JustPressed)
                {
                    if (playerClass != null)
                    {
                        playerClass.UseAbility(hotKey);
                    }
                }
            }
        }

        //Can the player be hit by NPC? Defaults to true
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            //Check if player is currently invuln to any NPCs. Set from skills like Leap. Will need to change this from dumb Clear() to specific removals klater
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

        //Drawing an effect on the character. Used by some abilities
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            foreach (Ability ability in ActiveAbilities)
            {
                ability.DrawEffects(drawInfo, ref  r, ref g, ref b, ref a, ref fullBright);
            }
        }
    }
}
