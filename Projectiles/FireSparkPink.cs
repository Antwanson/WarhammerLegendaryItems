using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using rail;

namespace WarhammerLegendaryItems.Projectiles
{
	public class FireSparkPink : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("the blue fires of tzeentch"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 1;
            Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 8;
			Projectile.timeLeft = 150;
			Projectile.light = .5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;

        }
		int bounce = 0;
		int maxBounce = 3;
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			bounce++;
            
				Projectile.velocity.X = 0;
				Projectile.velocity.Y = 0; 
			
                return false;
            
            
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
		{
			Projectile.velocity.Y += 0.1f;
			
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.013f;
		}
	}
}