using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014D RID: 333
	public class EncyclopediaHeroTraitVisualWidget : Widget
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x00030FEF File Offset: 0x0002F1EF
		public EncyclopediaHeroTraitVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		private void SetVisual(string traitCode, int value)
		{
			if (!string.IsNullOrEmpty(traitCode))
			{
				string name = string.Concat(new object[]
				{
					"SPGeneral\\SPTraits\\",
					traitCode.ToLower(),
					"_",
					value
				});
				base.Sprite = base.Context.SpriteData.GetSprite(name);
				base.Sprite = base.Context.SpriteData.GetSprite(name);
				if (value < 0)
				{
					base.Color = new Color(0.738f, 0.113f, 0.113f, 1f);
					return;
				}
				base.Color = new Color(0.992f, 0.75f, 0.33f, 1f);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x000310AF File Offset: 0x0002F2AF
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x000310B7 File Offset: 0x0002F2B7
		[Editor(false)]
		public string TraitId
		{
			get
			{
				return this._traitId;
			}
			set
			{
				if (this._traitId != value)
				{
					this._traitId = value;
					base.OnPropertyChanged<string>(value, "TraitId");
					this.SetVisual(value, this.TraitValue);
				}
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000310E7 File Offset: 0x0002F2E7
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x000310EF File Offset: 0x0002F2EF
		[Editor(false)]
		public int TraitValue
		{
			get
			{
				return this._traitValue;
			}
			set
			{
				if (this._traitValue != value)
				{
					this._traitValue = value;
					base.OnPropertyChanged(value, "TraitValue");
					this.SetVisual(this.TraitId, value);
				}
			}
		}

		// Token: 0x04000811 RID: 2065
		private string _traitId;

		// Token: 0x04000812 RID: 2066
		private int _traitValue;
	}
}
