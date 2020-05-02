using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Players;

namespace RealClasses.Projectiles
{
    class HealBombProjectile : ModProjectile
    {
        Color healBombColor = new Color(0, 255, 153);
        Vector2 rectSize = new Vector2(256, 256);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heal Bomb");
            //Frames in sprite
            Main.projFrames[projectile.type] = 6;

        }

        public override void SetDefaults()
        {
            projectile.arrow = true;
            //Changing these two ruins the offset already in place - cool
            projectile.width = 1;
            projectile.height = 1;
            //projectile.aiStyle = 0;
            //aiType = ProjectileID.WoodenArrowFriendly;
            //projectile.type = 45;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.scale = 1f;
            projectile.tileCollide = true;
            projectile.penetrate = 5;
            //Usually sprite width
            drawOffsetX = -128;
            //Usually sprite height
            drawOriginOffsetX = 64;
            //This one is iffy but seems to be half of the height or the height. Affects when shooting left
            drawOriginOffsetY = -32;
        }

        //Heal and damage in aoe when it dies
        public override void Kill(int timeLeft)
        {
            //Make pretty dust
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0, 0, 150, healBombColor, 3.0f);

                //Get the owner of the projectile and reset its cooldown
                Player player = Main.player[projectile.owner];
                player.GetModPlayer<MyPlayer>().healBombCDCounter = player.GetModPlayer<MyPlayer>().healBombCD;

            }
            
            //Iterate through all players and heal them if their character rectangle overlaps with the rectangle surrounding the hitbox
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player players = Main.player[i];
                //If you draw a rectangle, it simply draws it right and down from the hitbox (1x1). Gotta center it first.
                Rectangle rectBounds = new Rectangle((int)projectile.position.X - (int)rectSize.X / 2 , (int)projectile.position.Y - (int)rectSize.Y / 2, (int)rectSize.X, (int)rectSize.Y);
                
                //Are the rectangles overlapping?
                if (rectBounds.Intersects(players.getRect()))
                {
                    //Heal life
                    players.statLife += players.statLifeMax2 / 5;
                    //Show the actual heal effect numbers above their heads
                    players.HealEffect(players.statLifeMax2 / 5);
                }
            }
            //Iterate through all players and heal them if their character rectangle overlaps with the rectangle surrounding the hitbox
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npcs = Main.npc[i];
                //If you draw a rectangle, it simply draws it right and down from the hitbox (1x1). Gotta center it first.
                Rectangle rectBounds = new Rectangle((int)projectile.position.X - (int)rectSize.X / 2, (int)projectile.position.Y - (int)rectSize.Y / 2, (int)rectSize.X, (int)rectSize.Y);

                //Are the rectangles overlapping?
                if (rectBounds.Intersects(npcs.getRect()))
                {
                    //Show the actual heal effect numbers above their heads
                    npcs.StrikeNPC(100, 10, 0, false, false, false);
                    //Heal life
                    npcs.life = npcs.life - 50;

                }
            }
        }


        public override void AI()
        {
            //Get the owner of the projectile
            Player player = Main.player[projectile.owner];

            //Rotate the projectile to face the mouse. Setting the offsets at the top is huge here
            projectile.rotation = projectile.velocity.ToRotation();

            //If player hit button again when projectile already exists, kill the projectile
            if (player.GetModPlayer<MyPlayer>().popHealBomb)
            {
                projectile.timeLeft = 1;
                player.GetModPlayer<MyPlayer>().popHealBomb = false;
                projectile.netUpdate = true;
            }

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
        }
    }
}
