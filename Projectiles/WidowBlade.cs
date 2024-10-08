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
	public class WidowBlade : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("the madness of Khaine"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 0;
            Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 60;
			Projectile.timeLeft = 500;
			Projectile.light = 0f;
            
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.knockBack = 5;


        }
        
        
        //public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        //{
        //    base.OnHitNPC(target, damage, knockback, crit);
        //}
        public override void AI()
		{
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -38; // These values match the values in SetDefaults
                DrawOriginOffsetY = -0;
                DrawOriginOffsetX = (40-(42/2));
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = 0; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -0; // doesn't change
                DrawOriginOffsetX = -1; // Math works out that this is negative of the other value.
            }
            Lighting.AddLight(Projectile.position, Color.PaleVioletRed.ToVector3());
            int trail = Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,DustID.RedTorch,0f,0f,0, default(Color), 1f);
			Main.dust[trail].noGravity = true;
			Main.dust[trail].velocity *= 0.2f;
			Main.dust[trail].scale = (float)Main.rand.Next(80,115) * 0.013f;
		}
	}
}