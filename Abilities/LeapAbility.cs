using Terraria;
using RealClasses.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using RealClasses.Players;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    //Lepas into the air, becomes invulnerbale to and damages NPCs he falls on, slams ground and damages enemies nearby. Small invulnerability frames to enemies hit after landing.
    class LeapAbility : Ability
    {
        protected Random rnd = new Random();
        protected bool leaping = false;
        protected Vector2 slamSize = new Vector2(200, 50);
        protected Vector2 safeSize = new Vector2(32, 8);
        protected float xDistance; //For determining which way to shove NPCs
        protected int invulnFrames = 60; //Amount of frames to be invulnerable to enemies tagged after landing
        protected int invulnFramesCounter = 0;
        protected List<NPC> alreadyHit = new List<NPC>(); //Used to not keep hitting enemies as you fall down with them
        protected int fallingDmg;
        protected int slamDmg;

        public LeapAbility(Player player) : base(player)
        {
            //Use placeholder ui button for testing
            abilityButton = new LeapButton();
            cooldownCounter = 0;
            cooldown = 600;
            this.player = player;
            fallingDmg = 50;
            slamDmg = 50;
        }

        //Leap
        public override void UseAbility(Player player)
        {
            if (cooldownCounter == 0)
            {
                abilityButton.SetCooldown(cooldown);
                cooldownCounter = cooldown;

                leaping = true;

                //Leap in the direction player is facing
                if (player.direction == 1)
                {
                    player.velocity.Y = player.velocity.Y - 12;
                    player.velocity.X = player.velocity.X + 4;
                }
                else if (player.direction == -1)
                {
                    player.velocity.Y = player.velocity.Y - 12;
                    player.velocity.X = player.velocity.X - 4;
                }
            }
        }

        public override void PreUpdate()
        {
            //Do leap
            if (leaping == true)
            {
                #region Falling
                //If falling, smash noobs downwards and be invulnerable to them
                if (player.velocity.Y > 0)
                {
                    //Tag NPCs below player to add to invuln list
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npcs = Main.npc[i];

                        //Make a rect to identify invuln enemies below player
                        Rectangle rectSafe = new Rectangle((int)player.Bottom.X - (int)safeSize.X / 2, (int)player.Bottom.Y - (int)safeSize.Y / 2, (int)safeSize.X, (int)safeSize.Y);

                        //If touching
                        if (rectSafe.Intersects(npcs.getRect()))
                        {
                            //If they were already hit while falling once, don't hit them again
                            if (alreadyHit.Contains(npcs))
                            {
                                //continue
                            }
                            else
                            {
                                //Add them to list so the player wil be invuln to them
                                player.GetModPlayer<MyPlayer>().InvulnNPCs.Add(npcs);
                                //Remember who we hit so we don't hit them again when falling
                                alreadyHit.Add(npcs);
                                //Damage them and send them downwards
                                npcs.StrikeNPC(fallingDmg, 1, 0, false, false, false);
                                npcs.velocity.Y = 10;
                            }
                        }
                    }
                }
                #endregion Falling

                #region Touched the ground
                //When player touches the ground, make dust, do damage etc
                if (player.velocity.Y == 0)
                {
                    //No longer leaping so set invuln frames for tagged enemies so player doesn't get shoved around right away
                    leaping = false;
                    invulnFramesCounter = invulnFrames;

                    //Freeze player movement for a frame so the landing feels hefty
                    player.velocity.X = 0;
                    
                    //And make some dust to look like he shook the ground
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(player.position + new Vector2(rnd.Next(-75, 75), 15), player.width, player.height, 4, 0, 0, 150, Color.White, 3);
                    }

                    //Do slam damage and knockup
                    //Draw rectangle centered around player's ankle
                    Rectangle rectBounds = new Rectangle((int)player.Bottom.X - (int)slamSize.X / 2, (int)player.Bottom.Y - (int)slamSize.Y / 2, (int)slamSize.X, (int)slamSize.Y);

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npcs = Main.npc[i];

                        //If npc is hit by rectangle
                        if (rectBounds.Intersects(npcs.getRect()))
                        {
                            //Damage them and pop them up for a frame
                            npcs.StrikeNPC(slamDmg, 1, 0, false, false, false);
                            npcs.velocity.Y = -6;

                            //Check what side of the player they are on and shove them away a bit
                            xDistance = npcs.position.X - player.position.X;
                            if (xDistance < 0)
                            {
                                npcs.velocity.X = -1;
                            }
                            else npcs.velocity.X = 1;                          
                        }
                    }
                }
                #endregion Touched the ground
            }

            #region Reset invuln
            //Clear invuln NPCs after invuln frames wears off
            if (leaping == false)
            {
                if (invulnFramesCounter <= 0)
                {
                    //Probably can't straight up destroy this list when other abilities start populating it too
                    player.GetModPlayer<MyPlayer>().InvulnNPCs.Clear();
                    alreadyHit.Clear();
                }
                else invulnFramesCounter--;
            }
            #endregion Reset invuln
        }
    }
}
