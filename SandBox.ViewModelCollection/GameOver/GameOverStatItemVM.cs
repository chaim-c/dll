using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x02000039 RID: 57
	public class GameOverStatItemVM : ViewModel
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00012ED3 File Offset: 0x000110D3
		public GameOverStatItemVM(StatItem item)
		{
			this._item = item;
			this.RefreshValues();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00012EE8 File Offset: 0x000110E8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DefinitionText = GameTexts.FindText("str_game_over_stat_item", this._item.ID).ToString();
			this.ValueText = this._item.Value;
			this.StatTypeAsString = Enum.GetName(typeof(StatItem.StatType), this._item.Type);
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00012F51 File Offset: 0x00011151
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00012F59 File Offset: 0x00011159
		[DataSourceProperty]
		public string DefinitionText
		{
			get
			{
				return this._definitionText;
			}
			set
			{
				if (value != this._definitionText)
				{
					this._definitionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DefinitionText");
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00012F7C File Offset: 0x0001117C
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00012F84 File Offset: 0x00011184
		[DataSourceProperty]
		public string ValueText
		{
			get
			{
				return this._valueText;
			}
			set
			{
				if (value != this._valueText)
				{
					this._valueText = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueText");
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00012FA7 File Offset: 0x000111A7
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00012FAF File Offset: 0x000111AF
		[DataSourceProperty]
		public string StatTypeAsString
		{
			get
			{
				return this._statTypeAsString;
			}
			set
			{
				if (value != this._statTypeAsString)
				{
					this._statTypeAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "StatTypeAsString");
				}
			}
		}

		// Token: 0x0400022F RID: 559
		private readonly StatItem _item;

		// Token: 0x04000230 RID: 560
		private string _definitionText;

		// Token: 0x04000231 RID: 561
		private string _valueText;

		// Token: 0x04000232 RID: 562
		private string _statTypeAsString;
	}
}
