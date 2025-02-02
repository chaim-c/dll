using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x0200007F RID: 127
	public class ItemMenuTooltipPropertyVM : TooltipProperty
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x0002CAAE File Offset: 0x0002ACAE
		public ItemMenuTooltipPropertyVM()
		{
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002CAB6 File Offset: 0x0002ACB6
		public ItemMenuTooltipPropertyVM(string definition, string value, int textHeight, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(definition, value, textHeight, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CACC File Offset: 0x0002ACCC
		public ItemMenuTooltipPropertyVM(string definition, Func<string> _valueFunc, int textHeight, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(definition, _valueFunc, textHeight, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CAE2 File Offset: 0x0002ACE2
		public ItemMenuTooltipPropertyVM(Func<string> _definitionFunc, Func<string> _valueFunc, int textHeight, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(_definitionFunc, _valueFunc, textHeight, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		public ItemMenuTooltipPropertyVM(Func<string> _definitionFunc, Func<string> _valueFunc, object[] valueArgs, int textHeight, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(_definitionFunc, _valueFunc, valueArgs, textHeight, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002CB10 File Offset: 0x0002AD10
		public ItemMenuTooltipPropertyVM(string definition, string value, int textHeight, Color color, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None) : base(definition, value, textHeight, color, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CB28 File Offset: 0x0002AD28
		public ItemMenuTooltipPropertyVM(string definition, Func<string> _valueFunc, int textHeight, Color color, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(definition, _valueFunc, textHeight, color, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CB40 File Offset: 0x0002AD40
		public ItemMenuTooltipPropertyVM(Func<string> _definitionFunc, Func<string> _valueFunc, int textHeight, Color color, bool onlyShowWhenExtended = false, HintViewModel propertyHint = null) : base(_definitionFunc, _valueFunc, textHeight, color, onlyShowWhenExtended, TooltipProperty.TooltipPropertyFlags.None)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CB58 File Offset: 0x0002AD58
		public ItemMenuTooltipPropertyVM(TooltipProperty property, HintViewModel propertyHint = null) : base(property)
		{
			this.PropertyHint = propertyHint;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002CB68 File Offset: 0x0002AD68
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0002CB70 File Offset: 0x0002AD70
		[DataSourceProperty]
		public HintViewModel PropertyHint
		{
			get
			{
				return this._propertyHint;
			}
			set
			{
				if (value != this._propertyHint)
				{
					this._propertyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PropertyHint");
				}
			}
		}

		// Token: 0x0400052A RID: 1322
		private HintViewModel _propertyHint;
	}
}
