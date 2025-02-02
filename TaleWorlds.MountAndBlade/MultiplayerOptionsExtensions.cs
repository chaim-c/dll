using System;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FB RID: 763
	public static class MultiplayerOptionsExtensions
	{
		// Token: 0x060029A5 RID: 10661 RVA: 0x000A0C10 File Offset: 0x0009EE10
		public static string GetValueText(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			switch (optionType.GetOptionProperty().OptionValueType)
			{
			case MultiplayerOptions.OptionValueType.Bool:
				return optionType.GetBoolValue(mode).ToString();
			case MultiplayerOptions.OptionValueType.Integer:
			case MultiplayerOptions.OptionValueType.Enum:
				return optionType.GetIntValue(mode).ToString();
			case MultiplayerOptions.OptionValueType.String:
				return optionType.GetStrValue(mode);
			default:
				Debug.FailedAssert("Missing OptionValueType for optionType: " + optionType, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerOptions.cs", "GetValueText", 1039);
				return null;
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000A0C90 File Offset: 0x0009EE90
		public static bool GetBoolValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			int num;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out num);
			return num == 1;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000A0CB4 File Offset: 0x0009EEB4
		public static int GetIntValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			int result;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out result);
			return result;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000A0CD8 File Offset: 0x0009EED8
		public static string GetStrValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			string result;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out result);
			return result;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000A0CF9 File Offset: 0x0009EEF9
		public static void SetValue(this MultiplayerOptions.OptionType optionType, bool value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value ? 1 : 0);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000A0D14 File Offset: 0x0009EF14
		public static void SetValue(this MultiplayerOptions.OptionType optionType, int value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000A0D29 File Offset: 0x0009EF29
		public static void SetValue(this MultiplayerOptions.OptionType optionType, string value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000A0D3E File Offset: 0x0009EF3E
		public static int GetMinimumValue(this MultiplayerOptions.OptionType optionType)
		{
			return optionType.GetOptionProperty().BoundsMin;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000A0D4B File Offset: 0x0009EF4B
		public static int GetMaximumValue(this MultiplayerOptions.OptionType optionType)
		{
			return optionType.GetOptionProperty().BoundsMax;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000A0D58 File Offset: 0x0009EF58
		public static MultiplayerOptionsProperty GetOptionProperty(this MultiplayerOptions.OptionType optionType)
		{
			return (MultiplayerOptionsProperty)optionType.GetType().GetField(optionType.ToString()).GetCustomAttributesSafe(typeof(MultiplayerOptionsProperty), false).Single<object>();
		}
	}
}
