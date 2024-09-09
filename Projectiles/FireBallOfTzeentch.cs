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
	public class FireBallOfTzeentch : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("the blue fires of tzeentch"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 1;
            Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 400;
			Projectile.light = 2f;
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
            SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.5f).WithPitchOffset(8f), Projectile.position);
            //Work in progress
            int expandVelocity = 1;
            Vector2 velocity1 = new Vector2(expandVelocity, expandVelocity);
            Vector2 velocity2 = new Vector2(expandVelocity, -expandVelocity);
            Vector2 velocity3 = new Vector2(-expandVelocity, expandVelocity);
            Vector2 velocity4 = new Vector2(-expandVelocity, -expandVelocity);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity1, ModContent.ProjectileType<Projectiles.FireSparkPink>(), Projectile.damage/2, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity2, ModContent.ProjectileType<Projectiles.FireSparkBlue>(), Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity3, ModContent.ProjectileType<Projectiles.FireSparkPink>(), Projectile.damage / 2, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity4, ModContent.ProjectileType<Projectiles.FireSparkBlue>(), Projectile.damage, 0, Projectile.owner);
            if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y) 
			{ 
				Projectile.velocity.Y = -oldVelocity.Y/2; 
			}
			

			if(bounce >= maxBounce)
			{
				return true;
			}
            else
			{
                return false;
            }
            
        }
        //Boolean to see if projetile has hit an enemy. This helps homing only take effect before projectile hits an enemy.
        Boolean hasHit = false;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hasHit = true;
            SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.5f).WithPitchOffset(8f), Projectile.position);
            Projectile.velocity.X = Projectile.velocity.X * 1.5f;
            Projectile.velocity.Y = Projectile.velocity.Y * 1.5f;
            int expandVelocity = 1;
            Vector2 velocity1 = new Vector2(expandVelocity, expandVelocity);
            Vector2 velocity2 = new Vector2(expandVelocity, -expandVelocity);
            Vector2 velocity3 = new Vector2(-expandVelocity, expandVelocity);
            Vector2 velocity4 = new Vector2(-expandVelocity, -expandVelocity);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity1, ModContent.ProjectileType<Projectiles.FireSparkPink>(), Projectile.damage / 2, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity2, ModContent.ProjectileType<Projectiles.FireSparkBlue>(), Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity3, ModContent.ProjectileType<Projectiles.FireSparkPink>(), Projectile.damage / 2, 0, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, velocity4, ModContent.ProjectileType<Projectiles.FireSparkBlue>(), Projectile.damage, 0, Projectile.owner);
            //base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
		{
			Projectile.velocity.Y += 0.05f;
            //conditions for homing to take place
            if (hasHit!=true)
            {
                /*
                 * Start of homing code
                 * Credit: Redigit & AwesomePerson159
                 */
                float num132 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y)); //pythag
                float num133 = Projectile.localAI[0];
                if (num133 == 0f) //if
                {
                    Projectile.localAI[0] = num132;
                    num133 = num132;
                }
                float num134 = Projectile.position.X;
                float num135 = Projectile.position.Y;
                float num136 = 300f;
                bool flag3 = false; //flag to check if the projectile can hit an enemy (basically if code later is actually hit or not)
                int num137 = 0; //number to decide ai value of projectile
                if (Projectile.ai[1] == 0f) //if no target aquired (i think)
                {
                    for (int num138 = 0; num138 < 200; num138++) //basicaly finding the nearest possible target
                    {
                        if (Main.npc[num138].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num138 + 1)))
                        {
                            float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2); //checking distance between target and projectile
                            float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                            float num141 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num139) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num140);
                            if (num141 < num136 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                            {
                                num136 = num141; //actually changing temp velocity values if the projectile can reach the enemy and if it is the closest enemy
                                num134 = num139;
                                num135 = num140;
                                flag3 = true; //flag set?
                                num137 = num138;
                            }
                        }
                    }
                    if (flag3)
                    {
                        Projectile.ai[1] = (float)(num137 + 1); //sets ai to the available target?
                    }
                    flag3 = false; //flag is now false
                }
                if (Projectile.ai[1] > 0f) //if a target is already aquired || will try to change velocity to the appropriate values
                {
                    int num142 = (int)(Projectile.ai[1] - 1f); //gets the target that is currently locked on 
                    if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                    {
                        float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                        if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144) < 1000f)
                        {
                            flag3 = true; //flag to check target still available
                            num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                            num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                        }
                    }
                    else //when target not available
                    {
                        Projectile.ai[1] = 0f; //target is no longer available so we set this appropriately
                    }
                }
                if (!Projectile.friendly)
                {
                    flag3 = false;
                }
                if (flag3) //if target is still available then we actually set the velocities
                {
                    float num145 = num133;
                    Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num146 = num134 - vector10.X;
                    float num147 = num135 - vector10.Y;
                    float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                    num148 = num145 / num148;
                    num146 *= num148;
                    num147 *= num148;
                    int num149 = 8;
                    Projectile.velocity.X = (Projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                    Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
                }
                /*
                 * End of homing AI
                 *
                 */
            }

        Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Pink, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = false;
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.013f;
            int trail = Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,DustID.Firework_Blue,0f,0f,0, default(Color), 1f);
			Main.dust[trail].noGravity = false;
			Main.dust[trail].velocity *= 0.2f;
			Main.dust[trail].scale = (float)Main.rand.Next(80,115) * 0.013f;
		}
	}
}