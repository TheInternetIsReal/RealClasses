using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;
using RealClasses.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.UI;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    class HealBombAbility : Ability
    {
        //Ability specific
        public float projSpeed = 5;  
        Vector2 playerOffset = new Vector2(12, 24); //Center on chest height and away from body
        Vector2 projSpawn; //Where the projectile will be spawned (kind of, see next line)
        public int projSpawnOffset = 100; //Frames ahead in time to spawn the projectile
        Vector2 normalizedLine; //Normalizes the line between the projectiel and target (mouse) so that it always flies the same speed
        Vector2 normalizedLineSpeed; //normalized line multiplied by the speed you desire

        public HealBombAbility(Player player) : base(player)
        {
            //Get instance to the berserk UI button
            abilityButton = new HealBombButton();
            cooldownCounter = 0;
            cooldown = 480;
        }

        public override void UseAbility(Player player)
        {

            if (player.ownedProjectileCounts[ProjectileType<Projectiles.HealBombProjectile>()] > 0)
            {
                //This needs to be handled better. How can I directly destroy this projectile?
                player.GetModPlayer<MyPlayer>().popHealBomb = true;
            }
            else if (cooldownCounter == 0)
            {
                abilityButton.SetCooldown(cooldown);
                cooldownCounter = cooldown;

                //Set the projectile spawn at the player position but near his chest and in front of him
                projSpawn = player.position + playerOffset;
                //Get a normalized set of velocity so the projectile doesn't go faster the farther the mouse is away from the player
                normalizedLine = Vector2.Normalize((Main.MouseWorld - projSpawn));
                //Multiply the normalized velocity by your desired speed from above to speed it up
                normalizedLineSpeed = normalizedLine * projSpeed;

                //Spawn the projectile ahead of the actual spawn point X times the amount the velocity. If X equal 10 and velocity is (2, 2) then spawn it + (20, 20) pixels 
                //from the projSpawn along the intended line. What this does is spawns the projectile farther in front of the player so that the tail of the sprite isn't
                //behind him
                Projectile.NewProjectile(
                    projSpawn.X + projSpawnOffset *normalizedLine.X, 
                    projSpawn.Y + projSpawnOffset * normalizedLine.Y, 
                    normalizedLineSpeed.X, 
                    normalizedLineSpeed.Y, 
                    ModContent.ProjectileType<HealBombProjectile>(), 
                    100, 0f, Main.myPlayer, 0f, 0f);
            }
        }
    }
}
