using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WarhammerLegendaryItems.Items.Placeable;


namespace WarhammerLegendaryItems.Items
{
	public class KhornatePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Khornate Pickaxe"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("Skulls for the skull throne");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 7;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.pick = 195;
			Item.useTurn = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DemonSteelBar>(10);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}