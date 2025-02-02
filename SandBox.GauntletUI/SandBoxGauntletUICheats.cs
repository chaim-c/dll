using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI
{
	// Token: 0x02000010 RID: 16
	public static class SandBoxGauntletUICheats
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00007360 File Offset: 0x00005560
		[CommandLineFunctionality.CommandLineArgumentFunction("set_inventory_search_enabled", "ui")]
		public static string SetInventorySearchEnabled(List<string> args)
		{
			string result = "Format is \"ui.set_inventory_search_enabled [1/0]\".";
			GauntletInventoryScreen obj;
			if ((obj = (ScreenManager.TopScreen as GauntletInventoryScreen)) == null)
			{
				return "Inventory screen is not open!";
			}
			if (args.Count != 1)
			{
				return result;
			}
			int num;
			if (int.TryParse(args[0], out num) && (num == 1 || num == 0))
			{
				FieldInfo field = typeof(GauntletInventoryScreen).GetField("_dataSource", BindingFlags.Instance | BindingFlags.NonPublic);
				SPInventoryVM spinventoryVM = (SPInventoryVM)field.GetValue(obj);
				spinventoryVM.IsSearchAvailable = (num == 1);
				field.SetValue(obj, spinventoryVM);
				return "Success";
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000073E8 File Offset: 0x000055E8
		[CommandLineFunctionality.CommandLineArgumentFunction("reload_pieces", "crafting")]
		public static string ReloadCraftingPieces(List<string> strings)
		{
			if (strings.Count != 2)
			{
				return "Usage: crafting.reload_pieces {MODULE_NAME} {XML_NAME}";
			}
			typeof(GauntletCraftingScreen).GetField("_reloadXmlPath", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, new KeyValuePair<string, string>(strings[0], strings[1]));
			return "Reloading crafting pieces...";
		}
	}
}
