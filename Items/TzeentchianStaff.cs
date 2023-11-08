using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace WarhammerLegendaryItems.Items
{
	public class TzeentchianStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tzeentchian Staff"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("Is it looking at me?");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.mana = 8;
			Item.DamageType = DamageClass.Magic;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.knockBack = 9;
			Item.value = 10000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.FireBallOfTzeentch>();
			Item.shootSpeed = 13;
			Item.noMelee = true;
			

		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 offset = new Vector2(velocity.X * 9, velocity.Y * 9);
			position += offset;
			Vector2 secondVelocity = Vector2.Zero;
			secondVelocity.X = velocity.X*(float).9;
			secondVelocity.Y = velocity.Y*(float).9;
			Vector2 thirdVelocity = Vector2.Zero;
			thirdVelocity.X = velocity.X * (float).8;
			thirdVelocity.Y = velocity.Y * (float).8;
			Vector2 fourthVelocity = Vector2.Zero;
			fourthVelocity.X = velocity.X*(float)1.1;
			fourthVelocity.Y = velocity.Y*(float)1.1;
			Vector2 fifthVelocity = Vector2.Zero;
			fifthVelocity.X = velocity.X*(float)1.2;
			fifthVelocity.Y = velocity.Y*(float)1.2;
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            //Projectile.NewProjectile(source, position, secondVelocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, thirdVelocity, type, damage, knockback, player.whoAmI);
            //Projectile.NewProjectile(source, position, fourthVelocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, fifthVelocity, type, damage, knockback, player.whoAmI);
            return false;

        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}