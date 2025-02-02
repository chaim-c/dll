using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000DA RID: 218
	public class EducationOptionVM : StringItemWithActionVM
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0004DC1F File Offset: 0x0004BE1F
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x0004DC27 File Offset: 0x0004BE27
		public string OptionEffect { get; private set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0004DC30 File Offset: 0x0004BE30
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0004DC38 File Offset: 0x0004BE38
		public string OptionDescription { get; private set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0004DC41 File Offset: 0x0004BE41
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0004DC49 File Offset: 0x0004BE49
		public EducationCampaignBehavior.EducationCharacterProperties[] CharacterProperties { get; private set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0004DC52 File Offset: 0x0004BE52
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x0004DC5A File Offset: 0x0004BE5A
		public string ActionID { get; private set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004DC63 File Offset: 0x0004BE63
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0004DC6B File Offset: 0x0004BE6B
		public ValueTuple<CharacterAttribute, int>[] OptionAttributes { get; private set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0004DC74 File Offset: 0x0004BE74
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0004DC7C File Offset: 0x0004BE7C
		public ValueTuple<SkillObject, int>[] OptionSkills { get; private set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0004DC85 File Offset: 0x0004BE85
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0004DC8D File Offset: 0x0004BE8D
		public ValueTuple<SkillObject, int>[] OptionFocusPoints { get; private set; }

		// Token: 0x0600145E RID: 5214 RVA: 0x0004DC98 File Offset: 0x0004BE98
		public EducationOptionVM(Action<object> onExecute, string optionId, TextObject optionText, TextObject optionDescription, TextObject optionEffect, bool isSelected, ValueTuple<CharacterAttribute, int>[] optionAttributes, ValueTuple<SkillObject, int>[] optionSkills, ValueTuple<SkillObject, int>[] optionFocusPoints, EducationCampaignBehavior.EducationCharacterProperties[] characterProperties) : base(onExecute, optionText.ToString(), optionId)
		{
			this.IsSelected = isSelected;
			this.CharacterProperties = characterProperties;
			this._optionTextObject = optionText;
			this._optionDescriptionObject = optionDescription;
			this._optionEffectObject = optionEffect;
			this.OptionAttributes = optionAttributes;
			this.OptionSkills = optionSkills;
			this.OptionFocusPoints = optionFocusPoints;
			this.RefreshValues();
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004DCF8 File Offset: 0x0004BEF8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OptionEffect = this._optionEffectObject.ToString();
			this.OptionDescription = this._optionDescriptionObject.ToString();
			base.ActionText = this._optionTextObject.ToString();
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004DD33 File Offset: 0x0004BF33
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0004DD3B File Offset: 0x0004BF3B
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x04000975 RID: 2421
		private readonly TextObject _optionTextObject;

		// Token: 0x04000976 RID: 2422
		private readonly TextObject _optionDescriptionObject;

		// Token: 0x04000977 RID: 2423
		private readonly TextObject _optionEffectObject;

		// Token: 0x04000978 RID: 2424
		private bool _isSelected;
	}
}
