using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RealClasses.Players;
using RealClasses.Projectiles;
using Microsoft.Xna.Framework;

namespace RealClasses.Abilities
{
    class HealBombAbility : IAbility
    {
        //For abilities
        public float projSpeed = 5;  
        Vector2 playerOffset = new Vector2(12, 24); //Center on chest height and away from bosy
        Vector2 projSpawn = new Vector2(); //Where the projectile will be spawned (kind of, see below...)
        Vector2 normalizedLine = new Vector2(); //Used in conjunction with projSpawnOffset to spawn the projectile a little in front of the player
        public int projSpawnOffset = 100; //Frames ahead in time to spawn the projectile
        Vector2 normalizedLineSpeed = new Vector2(); //Used to make sure every projectile moves at the same spped and is multiplied by speed to speed them up

        public void UseAbility(Player player)
        {
            //If cooled down
            if (player.GetModPlayer<MyPlayer>().healBombCDCounter == 0)
            {
                if (player.ownedProjectileCounts[ProjectileType<Projectiles.HealBombProjectile>()] > 0)
                {
                    //continue
                    player.GetModPlayer<MyPlayer>().popHealBomb = true;
                }
                else
                {
                    //Set the projectile spawn at the player position but near his chest and in front of him
                    projSpawn = player.position + playerOffset;
                    //Get a normalized set of coords so the projectiles don't go faster the farther the mouse is away from the player
                    normalizedLine = Vector2.Normalize((Main.MouseWorld - projSpawn));
                    //Multiply the normalized angle/velocity by your desired speed from above
                    normalizedLineSpeed = normalizedLine * projSpeed;

                    //Spawn the projectile. The following sets the spawn point, then moves it along the line a bit by muliplying the NORMALIZED X velocity (not with speed!!) by an arbitrary projSpawnOffset.
                    //projSpawnOffset is essentially a frame value where the projectile will spawn X frames into the future on the give line. This simply spawns the projectile in front of the player instead of
                    //in the middle of their body (it has a long tail and the hitbox is on the very front tip which is where it spawns normally, putting the tail in the player).
                    Projectile.NewProjectile(projSpawn.X + normalizedLine.X * projSpawnOffset, projSpawn.Y + normalizedLine.Y * projSpawnOffset, normalizedLineSpeed.X, normalizedLineSpeed.Y, ModContent.ProjectileType<HealBombProjectile>(), 100, 0f, Main.myPlayer, 0f, 0f);

                    //Cooldown is reset over in the projectile code
                }
            }
        }
    }
}
