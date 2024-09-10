using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;
using System.Net.Mail;
using WarhammerLegendaryItems.Items.Placeable;


namespace WarhammerLegendaryItems.Items
{
	public class Metamorphose : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Sword of Khaine"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("The Widowmaker");
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            

            var line = new TooltipLine(Mod, "Test", "Is that blue thing blinking?")
            {
                OverrideColor = new Color(100, 100, 255)
            };
            tooltips.Add(line);

            // Here we give the item name a rainbow effect.
            /*
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Main.DiscoColor;
                }
            }
            */
            // Here we will remove all tooltips whose title end with ':RemoveMe'
            // One like that is added at the start of this method
            //tooltips.RemoveAll(l => l.Name.EndsWith(":RemoveMe"));

            // Another method of removal can be done if you know the index of the tooltip:
            // tooltips.RemoveAt(index);

            // You can also remove a specific line, if you have access to that object:
            // tooltips.Remove(tooltipLine);
        }
        public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Melee;
			Item.width = 84;
			Item.height = 84;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.knockBack = 13;
			Item.value = 10000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.TzeentchEyeball>();
            Item.shootSpeed = 20;

		}

		public override bool AltFunctionUse(Player player)
		{
			return false;
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Clentaminator_Blue);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal.WithVolumeScale(1f).WithPitchOffset(8f), player.position);
            return true;
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            Lighting.AddLight(player.position, Color.PaleVioletRed.ToVector3());
            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DemonSteelBar>(10);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.Lens, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}