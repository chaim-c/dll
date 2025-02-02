using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000D6 RID: 214
	public class EducationGainedAttributeItemVM : ViewModel
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x0004CE80 File Offset: 0x0004B080
		public EducationGainedAttributeItemVM(CharacterAttribute attributeObj)
		{
			this._attributeObj = attributeObj;
			TextObject nameExtended = this._attributeObj.Name;
			TextObject desc = this._attributeObj.Description;
			this.Hint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("STR1", nameExtended);
				GameTexts.SetVariable("STR2", desc);
				return GameTexts.FindText("str_string_newline_string", null).ToString();
			});
			this.SetValue(0, 0);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004CEE1 File Offset: 0x0004B0E1
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0004CEEC File Offset: 0x0004B0EC
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.HasIncreasedInCurrentStage = (gainedFromCurrentStage > 0);
			GameTexts.SetVariable("LEFT", this._attributeObj.Name);
			GameTexts.SetVariable("RIGHT", gainedFromOtherStages + gainedFromCurrentStage);
			this.NameText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0004CF3B File Offset: 0x0004B13B
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x0004CF43 File Offset: 0x0004B143
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004CF61 File Offset: 0x0004B161
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0004CF69 File Offset: 0x0004B169
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004CF8C File Offset: 0x0004B18C
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0004CF94 File Offset: 0x0004B194
		[DataSourceProperty]
		public bool HasIncreasedInCurrentStage
		{
			get
			{
				return this._hasIncreasedInCurrentStage;
			}
			set
			{
				if (value != this._hasIncreasedInCurrentStage)
				{
					this._hasIncreasedInCurrentStage = value;
					base.OnPropertyChangedWithValue(value, "HasIncreasedInCurrentStage");
				}
			}
		}

		// Token: 0x04000945 RID: 2373
		private readonly CharacterAttribute _attributeObj;

		// Token: 0x04000946 RID: 2374
		private string _nameText;

		// Token: 0x04000947 RID: 2375
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000948 RID: 2376
		private BasicTooltipViewModel _hint;
	}
}
