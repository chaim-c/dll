﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x0200034A RID: 842
	public sealed class PerkObject : PropertyObject
	{
		// Token: 0x06002F86 RID: 12166 RVA: 0x000C3BF9 File Offset: 0x000C1DF9
		internal static void AutoGeneratedStaticCollectObjectsPerkObject(object o, List<object> collectedObjects)
		{
			((PerkObject)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000C3C07 File Offset: 0x000C1E07
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000C3C10 File Offset: 0x000C1E10
		public static MBReadOnlyList<PerkObject> All
		{
			get
			{
				return Campaign.Current.AllPerks;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000C3C1C File Offset: 0x000C1E1C
		// (set) Token: 0x06002F8A RID: 12170 RVA: 0x000C3C24 File Offset: 0x000C1E24
		public SkillObject Skill { get; private set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000C3C2D File Offset: 0x000C1E2D
		// (set) Token: 0x06002F8C RID: 12172 RVA: 0x000C3C35 File Offset: 0x000C1E35
		public float RequiredSkillValue { get; private set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000C3C3E File Offset: 0x000C1E3E
		// (set) Token: 0x06002F8E RID: 12174 RVA: 0x000C3C46 File Offset: 0x000C1E46
		public PerkObject AlternativePerk { get; private set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000C3C4F File Offset: 0x000C1E4F
		// (set) Token: 0x06002F90 RID: 12176 RVA: 0x000C3C57 File Offset: 0x000C1E57
		public SkillEffect.PerkRole PrimaryRole { get; private set; }

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000C3C60 File Offset: 0x000C1E60
		// (set) Token: 0x06002F92 RID: 12178 RVA: 0x000C3C68 File Offset: 0x000C1E68
		public SkillEffect.PerkRole SecondaryRole { get; private set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000C3C71 File Offset: 0x000C1E71
		// (set) Token: 0x06002F94 RID: 12180 RVA: 0x000C3C79 File Offset: 0x000C1E79
		public float PrimaryBonus { get; private set; }

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000C3C82 File Offset: 0x000C1E82
		// (set) Token: 0x06002F96 RID: 12182 RVA: 0x000C3C8A File Offset: 0x000C1E8A
		public float SecondaryBonus { get; private set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000C3C93 File Offset: 0x000C1E93
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x000C3C9B File Offset: 0x000C1E9B
		public SkillEffect.EffectIncrementType PrimaryIncrementType { get; private set; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000C3CA4 File Offset: 0x000C1EA4
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000C3CAC File Offset: 0x000C1EAC
		public SkillEffect.EffectIncrementType SecondaryIncrementType { get; private set; }

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000C3CB5 File Offset: 0x000C1EB5
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x000C3CBD File Offset: 0x000C1EBD
		public TroopUsageFlags PrimaryTroopUsageMask { get; private set; }

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000C3CC6 File Offset: 0x000C1EC6
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x000C3CCE File Offset: 0x000C1ECE
		public TroopUsageFlags SecondaryTroopUsageMask { get; private set; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000C3CD7 File Offset: 0x000C1ED7
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000C3CDF File Offset: 0x000C1EDF
		public TextObject PrimaryDescription { get; private set; }

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000C3CE8 File Offset: 0x000C1EE8
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000C3CF0 File Offset: 0x000C1EF0
		public TextObject SecondaryDescription { get; private set; }

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000C3CF9 File Offset: 0x000C1EF9
		public bool IsTrash
		{
			get
			{
				return base.Name == null || base.Description == null || this.Skill == null;
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000C3D16 File Offset: 0x000C1F16
		public PerkObject(string stringId) : base(stringId)
		{
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C3D20 File Offset: 0x000C1F20
		public void Initialize(string name, SkillObject skill, int requiredSkillValue, PerkObject alternativePerk, string primaryDescription, SkillEffect.PerkRole primaryRole, float primaryBonus, SkillEffect.EffectIncrementType incrementType, string secondaryDescription = "", SkillEffect.PerkRole secondaryRole = SkillEffect.PerkRole.None, float secondaryBonus = 0f, SkillEffect.EffectIncrementType secondaryIncrementType = SkillEffect.EffectIncrementType.Invalid, TroopUsageFlags primaryTroopUsageMask = TroopUsageFlags.Undefined, TroopUsageFlags secondaryTroopUsageMask = TroopUsageFlags.Undefined)
		{
			this.PrimaryDescription = new TextObject(primaryDescription, null);
			this.SecondaryDescription = new TextObject(secondaryDescription, null);
			PerkHelper.SetDescriptionTextVariable(this.PrimaryDescription, primaryBonus, incrementType);
			TextObject textObject;
			if (secondaryDescription != "")
			{
				PerkHelper.SetDescriptionTextVariable(this.SecondaryDescription, secondaryBonus, secondaryIncrementType);
				textObject = GameTexts.FindText("str_string_newline_newline_string", null);
				textObject.SetTextVariable("STR1", this.PrimaryDescription);
				textObject.SetTextVariable("STR2", this.SecondaryDescription);
			}
			else
			{
				textObject = this.PrimaryDescription.CopyTextObject();
			}
			textObject.SetTextVariable("newline", "\n");
			base.Initialize(new TextObject(name, null), textObject);
			this.Skill = skill;
			this.RequiredSkillValue = (float)requiredSkillValue;
			this.AlternativePerk = alternativePerk;
			if (alternativePerk != null)
			{
				alternativePerk.AlternativePerk = this;
			}
			this.PrimaryRole = primaryRole;
			this.SecondaryRole = secondaryRole;
			this.PrimaryBonus = primaryBonus;
			this.SecondaryBonus = secondaryBonus;
			this.PrimaryIncrementType = incrementType;
			this.SecondaryIncrementType = ((secondaryIncrementType == SkillEffect.EffectIncrementType.Invalid) ? this.PrimaryIncrementType : secondaryIncrementType);
			this.PrimaryTroopUsageMask = primaryTroopUsageMask;
			this.SecondaryTroopUsageMask = secondaryTroopUsageMask;
			base.AfterInitialized();
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C3E48 File Offset: 0x000C2048
		public override string ToString()
		{
			return base.Name.ToString();
		}
	}
}
