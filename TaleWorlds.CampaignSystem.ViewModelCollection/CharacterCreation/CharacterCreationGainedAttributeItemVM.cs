using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000133 RID: 307
	public class CharacterCreationGainedAttributeItemVM : ViewModel
	{
		// Token: 0x06001DBB RID: 7611 RVA: 0x0006AAAC File Offset: 0x00068CAC
		public CharacterCreationGainedAttributeItemVM(CharacterAttribute attributeObj)
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

		// Token: 0x06001DBC RID: 7612 RVA: 0x0006AB0D File Offset: 0x00068D0D
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0006AB18 File Offset: 0x00068D18
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.HasIncreasedInCurrentStage = (gainedFromCurrentStage > 0);
			GameTexts.SetVariable("LEFT", this._attributeObj.Name);
			GameTexts.SetVariable("RIGHT", gainedFromOtherStages + gainedFromCurrentStage);
			this.NameText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0006AB67 File Offset: 0x00068D67
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0006AB6F File Offset: 0x00068D6F
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0006AB8D File Offset: 0x00068D8D
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0006AB95 File Offset: 0x00068D95
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

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0006ABB8 File Offset: 0x00068DB8
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0006ABC0 File Offset: 0x00068DC0
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

		// Token: 0x04000E05 RID: 3589
		private readonly CharacterAttribute _attributeObj;

		// Token: 0x04000E06 RID: 3590
		private string _nameText;

		// Token: 0x04000E07 RID: 3591
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000E08 RID: 3592
		private BasicTooltipViewModel _hint;
	}
}
