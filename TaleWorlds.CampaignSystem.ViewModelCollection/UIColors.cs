using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200001D RID: 29
	public static class UIColors
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0001005E File Offset: 0x0000E25E
		public static Color PositiveIndicator
		{
			get
			{
				return UIColors._positiveIndicator;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00010065 File Offset: 0x0000E265
		public static Color NegativeIndicator
		{
			get
			{
				return UIColors._negativeIndicator;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0001006C File Offset: 0x0000E26C
		public static Color Gold
		{
			get
			{
				return UIColors._gold;
			}
		}

		// Token: 0x040000C3 RID: 195
		private static Color _positiveIndicator = Color.FromUint(4285250886U);

		// Token: 0x040000C4 RID: 196
		private static Color _negativeIndicator = Color.FromUint(4290070086U);

		// Token: 0x040000C5 RID: 197
		private static Color _gold = Color.FromUint(4294957447U);
	}
}
