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
	public class TzeentchEyeball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("the blue fires of tzeentch"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}
        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.width = 24;
			Projectile.height = 24;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 400;
			Projectile.light = 2f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			
		}
		int bounce = 0;
		int maxBounce = 0;
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			bounce++;
            SoundEngine.PlaySound(SoundID.MaxMana.WithVolumeScale(1f).WithPitchOffset(8f), Projectile.position);
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;
            return false;

        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
            Vector2 dustPos = new Vector2(Projectile.position.X - 12, Projectile.position.Y - 12);
            SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal.WithVolumeScale(1f).WithPitchOffset(8f), Projectile.position);
            for (int i = 0; i < 20; i++)
            {
                
                int dust = Dust.NewDust(dustPos, Projectile.width + 24, Projectile.height + 24, DustID.Clentaminator_Purple, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = false;
                Main.dust[dust].velocity *= 0.2f;
                Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.017f;
                int trail = Dust.NewDust(dustPos, Projectile.width + 24, Projectile.height + 24, DustID.Clentaminator_Blue, 0f, 0f, 0, default(Color), 1f);
                Main.dust[trail].noGravity = false;
                Main.dust[trail].velocity *= 0.2f;
                Main.dust[trail].scale = (float)Main.rand.Next(80, 115) * 0.017f;
            }

        }
        //Boolean to see if projetile has hit an enemy. This helps homing only take effect before projectile hits an enemy.
        Boolean hasHit = false;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hasHit = true;

            for (int i = 0; i < 10; i++)
            {
                Vector2 dustPos= new Vector2(Projectile.position.X-12, Projectile.position.Y-12);
                int dust = Dust.NewDust(dustPos, Projectile.width + 24, Projectile.height + 24, DustID.Clentaminator_Purple, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = false;
                Main.dust[dust].velocity *= 0.2f;
                Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.017f;
                int trail = Dust.NewDust(dustPos, Projectile.width + 24, Projectile.height + 24, DustID.Clentaminator_Blue, 0f, 0f, 0, default(Color), 1f);
                Main.dust[trail].noGravity = false;
                Main.dust[trail].velocity *= 0.2f;
                Main.dust[trail].scale = (float)Main.rand.Next(80, 115) * 0.017f;
            }
            if (bounce==0) 
            {
                //Projectile.Kill();
                
                Projectile.velocity = new Vector2(0, 0);
                bounce++;
                SoundEngine.PlaySound(SoundID.MaxMana.WithVolumeScale(1f).WithPitchOffset(8f), Projectile.position);
            }
        }
        public override void AI()
		{
            //Projectile.velocity.Y += 0.05f;
            if (Projectile.timeLeft == 370)
            {
                Projectile.velocity = new Vector2(0, 0);
                bounce++;
                SoundEngine.PlaySound(SoundID.MaxMana.WithVolumeScale(1f).WithPitchOffset(8f), Projectile.position);
            }
                //conditions for homing to take place
                if (true) //always taking place (yes redundant)
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
                float num136 = 600f;
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
                if (Projectile.ai[1] > 0f) //if a target is already aquired || will try to change rotation (edited) to the appropriate values
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

                    float subVelX = (1 * (float)(num149 - 1) + num146) / (float)num149;
                    float subVelY = (1 * (float)(num149 - 1) + num147) / (float)num149;
                    if (bounce>=0)
                    {

                    
                        Projectile.rotation = (float)Math.Atan2((double)subVelY, (double)subVelX);
                    }
                    if(Projectile.timeLeft%30 == 0)
                    {
                        Vector2 laserVelocity = new Vector2(subVelX*10, subVelY*10);
                        Vector2 laserPos = new Vector2(Projectile.position.X+12, Projectile.position.Y+12);
                        SoundEngine.PlaySound(SoundID.MaxMana.WithVolumeScale(0.5f).WithPitchOffset(8f), Projectile.position);
                        if (Projectile.timeLeft % 60 == 0)
                        { 
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), laserPos, laserVelocity, ModContent.ProjectileType<Projectiles.FireSparkPink>(), Projectile.damage / 2, 0, Projectile.owner); 
                        }
                        else
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), laserPos, laserVelocity, ModContent.ProjectileType<Projectiles.FireSparkBlue>(), Projectile.damage, 0, Projectile.owner);

                        }
                    }
                }
                /*
                 * End of homing AI
                 *
                 */
            }

        
			
            //int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple, 0f, 0f, 0, default(Color), 1f);
            //Main.dust[dust].noGravity = false;
            //Main.dust[dust].velocity *= 0.2f;
            //Main.dust[dust].scale = (float)Main.rand.Next(80, 115) * 0.007f;
            int trail = Dust.NewDust(Projectile.position,Projectile.width,Projectile.height,DustID.Clentaminator_Blue,0f,0f,0, default(Color), 1f);
			Main.dust[trail].noGravity = false;
			Main.dust[trail].velocity *= 0.2f;
			Main.dust[trail].scale = (float)Main.rand.Next(80,115) * 0.007f;
		}
	}
}