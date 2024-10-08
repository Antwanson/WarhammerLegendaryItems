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

namespace WarhammerLegendaryItems.Projectiles
{
	public class WidowStorm : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("the madness of Khaine"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 0;
            Projectile.width = 400;
			Projectile.height = 200;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 99999;
			Projectile.timeLeft = 350;
			Projectile.light = 2f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;

            Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.knockBack = 5;
			
			
		}
        
        
        //public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        //{
        //    base.OnHitNPC(target, damage, knockback, crit);
        //}
        Boolean hasHit = false;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (hasHit == false)
            {
                Projectile.velocity.X = (float)Main.rand.Next(-115, 115) * 0.03f;
                Projectile.velocity.Y = 0;
                hasHit = true;
            }
            return false;
        }
        public override void AI()
		{
            Lighting.AddLight(Projectile.position, Color.PaleVioletRed.ToVector3());
            Projectile.velocity.Y += 0.05f;
            int trail = Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,DustID.RedTorch,0f,0f,0, default(Color), 1f);
			Main.dust[trail].noGravity = false;
			Main.dust[trail].velocity *= 0.2f;
			Main.dust[trail].scale = (float)Main.rand.Next(80,115) * 0.020f;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = false;
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.020f;
        }
	}
}