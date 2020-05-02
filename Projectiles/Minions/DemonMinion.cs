using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealClasses.Projectiles.Minions
{
    /*
	 * This minion shows a few mandatory things that make it behave properly. 
	 * Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	 * If the player targets a certain NPC with right-click, it will fly through tiles to it
	 * If it isn't attacking, it will float near the player with minimal movement
	 */
    
    public class DemonMinion : ModProjectile
    {

        public int count = 0;
    
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[projectile.type] = 4;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            // These below are needed for a minion
            // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = false;
        }

        public sealed override void SetDefaults()
        {
            //projectile.width = 55;
            //projectile.height = 42;
            projectile.width = 30;
            projectile.height = 20;
            // Makes the minion go through tiles freely
            projectile.tileCollide = true;
            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            //projectile.friendly = true;
            projectile.friendly = true;
            // Only determines the damage type
            projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            projectile.minionSlots = 1f;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            projectile.penetrate = -1;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return true;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }

        //public DateTime waitTime = DateTime.MinValue;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
                projectile.friendly = false;
        }

        public override void AI()
        {
            //How do I know what projectile.owner is and what it does?
            Player player = Main.player[projectile.owner];
            bool foundTarget = false;

            #region Can attack check
            if (count > 60)
            {
                //Hit
                projectile.friendly = true;
                count = 0;
            }
            else count++;
            #endregion

            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<Buffs.DemonBuff>());
            }
           if (player.HasBuff(BuffType<Buffs.DemonBuff>()))
            {
                //Minion has a constant death timer going. When the buff goes away, this lets it die
                projectile.timeLeft = 2;
            }
            #endregion

            #region General behavior
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            //Caclulate idle position (player center) to minion's center
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();

            //If minion is farther than 50 blocks away (800 / 16)
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 900f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                foundTarget = false;
                projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            //I assume this checks if projectiles are right on top of each other and then moves them...
            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Fix overlap with other minions
                Projectile other = Main.projectile[i];
                if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
                    else projectile.velocity.X += overlapVelocity;

                    if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
                    else projectile.velocity.Y += overlapVelocity;
                }
            }
            #endregion

            #region Find target
            // Starting search distance
            float distanceFromTarget = 800f;
            //Vector2 targetCenter = projectile.position;
            Vector2 targetCenter = player.position;
            //bool foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            //I think this is automatically finding targets for the minions...
            if (player.HasMinionAttackTargetNPC)
            {
                //For target...
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                //float between = Vector2.Distance(npc.Center, projectile.Center);
                float between = Vector2.Distance(npc.Center, player.Center);
                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 800f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }
            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                //Iterate over all NPCs nearby
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    //I guess some NPC's can't be chased?...
                    if (npc.CanBeChasedBy())
                    {
                        //Find distance and then closest NPC, then chase closest if within range (distanceFromTarget)
                        //float between = Vector2.Distance(npc.Center, projectile.Center);
                        float between = Vector2.Distance(npc.Center, player.Center);
                        //bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                        bool closest = Vector2.Distance(player.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        //What's this doing?... Checking for line of sight?
                        bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        //I guess going through tiles loses target?...
                        bool closeThroughWall = between < 100f;
                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            //projectile.friendly = foundTarget;
            #endregion

            #region Movement
            // Default movement parameters (here for attacking)
            float speed = 6f;
            float inertia = 15f;

            if (foundTarget)
            {
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget < 800f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    //Move...
                    projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
                }
            }
            else
            {
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 200f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 24f;
                    inertia = 80f;
                }
                else
                {
                    // Slow down the minion if closer to the player
                    speed = 12f;
                    inertia = 60f;
                }
                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    //To idle position (player)...
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (projectile.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    //This needs to be smarter because there will be constant forces towards the player, not true 0 velocity
                    //projectile.velocity.X = -0.15f;
                    //projectile.velocity.Y = -0.05f;
                    projectile.velocity.X = -0.5f;
                    projectile.velocity.Y = -0.5f;
                }
            }
            #endregion

        #region Animation and visuals
        // So it will lean slightly towards the direction it's moving
        projectile.rotation = projectile.velocity.X * 0.05f;

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 5;
            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }

            // Some visuals here
            Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
            #endregion
        }
    }
}
