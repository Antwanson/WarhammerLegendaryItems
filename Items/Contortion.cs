using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarhammerLegendaryItems.Items
{
	public class Contortion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Avelorn Bow"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("Shines with elven magic");
		}

		public override void SetDefaults()
		{
			Item.damage = 250;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 72;
			Item.height = 160;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 5;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = 1;
			Item.shootSpeed = 12f;
			Item.useAmmo = AmmoID.Arrow;
			Item.noMelee = true;

		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = new Vector2(velocity.X * 1, velocity.Y * 1);
            position += offset;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            { // or ProjectileID.WoodenArrowFriendly
                type = ModContent.ProjectileType<Projectiles.QuicksilverArrow>(); // or ProjectileID.FireArrow;
            }

        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
        public override Vector2? HoldoutOffset()
        {
			Vector2 Offset = new Vector2(-10,3);

            return Offset;
        }
    }
}