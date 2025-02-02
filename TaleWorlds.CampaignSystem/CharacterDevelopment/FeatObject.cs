using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x02000350 RID: 848
	public sealed class FeatObject : PropertyObject
	{
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000CDD0D File Offset: 0x000CBF0D
		public static MBReadOnlyList<FeatObject> All
		{
			get
			{
				return Campaign.Current.AllFeats;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000CDD19 File Offset: 0x000CBF19
		// (set) Token: 0x0600302E RID: 12334 RVA: 0x000CDD21 File Offset: 0x000CBF21
		public float EffectBonus { get; private set; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000CDD2A File Offset: 0x000CBF2A
		// (set) Token: 0x06003030 RID: 12336 RVA: 0x000CDD32 File Offset: 0x000CBF32
		public FeatObject.AdditionType IncrementType { get; private set; }

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000CDD3B File Offset: 0x000CBF3B
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x000CDD43 File Offset: 0x000CBF43
		public bool IsPositive { get; private set; }

		// Token: 0x06003033 RID: 12339 RVA: 0x000CDD4C File Offset: 0x000CBF4C
		public FeatObject(string stringId) : base(stringId)
		{
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000CDD55 File Offset: 0x000CBF55
		public void Initialize(string name, string description, float effectBonus, bool isPositiveEffect, FeatObject.AdditionType incrementType)
		{
			base.Initialize(new TextObject(name, null), new TextObject(description, null));
			this.EffectBonus = effectBonus;
			this.IncrementType = incrementType;
			this.IsPositive = isPositiveEffect;
			base.AfterInitialized();
		}

		// Token: 0x02000699 RID: 1689
		public enum AdditionType
		{
			// Token: 0x04001B7E RID: 7038
			Add,
			// Token: 0x04001B7F RID: 7039
			AddFactor
		}
	}
}
