using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace WarhammerLegendaryItems.Items
{
	public class SkryreWarplockRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Skryre Warplock Rifle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("Bring ruin-death!\nConverts normal bullets into Warpstone Bullets");
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 32;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.knockBack = .6f;
			Item.value = 12000;
			Item.rare = 3;
			Item.UseSound = SoundID.Item125;
			Item.autoReuse = true;
			Item.shoot = 1;
            //ModContent.ProjectileType<Projectiles.WarplockBullet>();
            Item.useAmmo = AmmoID.Bullet;
			Item.shootSpeed = 9.5f;
			Item.noMelee = true;
			

		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 offset = new Vector2(velocity.X * 5, velocity.Y * 5);
			position += offset;
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            { // or ProjectileID.WoodenArrowFriendly
                type = ModContent.ProjectileType<Projectiles.WarplockBullet>(); // or ProjectileID.FireArrow;
            }

        }


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddIngredient(ItemID.ShadowScale, 2);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
            Recipe altrecipe = CreateRecipe();
            altrecipe.AddIngredient(ItemID.CrimtaneBar, 8);
            altrecipe.AddIngredient(ItemID.TissueSample, 2);
            altrecipe.AddIngredient(ItemID.JungleSpores, 5);
            altrecipe.AddTile(TileID.Anvils);
            altrecipe.Register();
        }
        public override Vector2? HoldoutOffset()
        {
			//First int is left (negative) to right (positive)
			//Second Int is Up (negative) to down (positive)
            Vector2 Offset = new Vector2(-15, 7);

            return Offset;
        }
    }
}