using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using rail;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace WarhammerLegendaryItems.Projectiles
{
	public class QuicksilverArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Warpstone Bullet"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 12;
            Projectile.aiStyle = 0;
            Projectile.width = 21;
			Projectile.height = 54;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 400;
			//Projectile.light = 2f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.scale = .6f;
            Projectile.extraUpdates = 1;
			
		}
		
        //Boolean to see if projetile has hit an enemy. This helps homing only take effect before projectile hits an enemy.
        Boolean hasHit = false;
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hasHit = true;

            //sound and effects
            SoundEngine.PlaySound(SoundID.DD2_DarkMageSummonSkeleton.WithVolumeScale(0.5f).WithPitchOffset(5f), Projectile.position);
            for (var i = 0; i < 6; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Yellow, 0f, 0f, 0, default(Color), 1f);
            }
            target.AddBuff(BuffID.Confused,30);
            spawnTentacle();
            //base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
		{
            //Rotation
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            //Gravity
            Projectile.velocity.Y += 0.005f;

            //Lighting
            //Lighting.AddLight(Projectile.position, 0.4f, 0.9f, 0.4f);
            //Lighting.Brightness(1, 1);

            //Particle
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkSlime, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = false;
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.013f;
            /* Feeling cute might delete later :)
            int trail = Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,DustID.Firework_Blue,0f,0f,0, default(Color), 1f);
			Main.dust[trail].noGravity = false;
			Main.dust[trail].velocity *= 0.2f;
			Main.dust[trail].scale = (float)Main.rand.Next(80,115) * 0.013f;
            */
		}
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_DarkMageSummonSkeleton.WithVolumeScale(0.5f).WithPitchOffset(5f), Projectile.position);
            for(var i =0; i < 6; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Yellow, 0f, 0f, 0, default(Color), 1f);
            }
            //int randomNumber = random.Next(1, 10);
            spawnTentacle();
        }

        int bounce = 0;
        int maxBounce = 0;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounce++;
            //SoundEngine.PlaySound(SoundID.DD2_LightningBugDeath.WithVolumeScale(0.5f).WithPitchOffset(5f), Projectile.position);
            for (var i = 0; i < 6; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Yellow, 0f, 0f, 0, default(Color), 1f);
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y / 2;
            }


            if (bounce >= maxBounce)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void spawnTentacle()
        {
            Vector2 tentVelocity = new Vector2(0,0);
            Random random = new Random();
            for (var i = 0; i < 6;i++)
            {
                tentVelocity.X = random.Next(-5, 5);
                tentVelocity.Y = random.Next(-5, 5);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, tentVelocity, ProjectileID.ShadowFlame, Projectile.damage, 0, Projectile.owner);

            }
        }
    }
}