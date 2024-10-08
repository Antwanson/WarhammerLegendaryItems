﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WarhammerLegendaryItems.Items.Placeable
{
	public class ChaoticOre : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
            ItemID.Sets.ItemIconPulse[Item.type] = true; // Makes the item have a 'pulse' animation
                                                         // This ore can spawn in slime bodies like other pre-boss ores. (copper, tin, iron, etch)
                                                         // It will drop in amount from 3 to 13.
            ItemID.Sets.OreDropsFromSlime[Type] = (3, 13);
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ChaoticOre>());
			Item.width = 12;
			Item.height = 12;
			Item.value = 3000;
		}
	}
}