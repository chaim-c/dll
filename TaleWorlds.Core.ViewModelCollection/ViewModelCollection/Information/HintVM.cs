using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000017 RID: 23
	public class HintVM : TooltipBaseVM
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00004217 File Offset: 0x00002417
		public HintVM(Type type, object[] args) : base(type, args)
		{
			base.InvokeRefreshData<HintVM>(this);
			base.IsActive = true;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000423A File Offset: 0x0000243A
		protected override void OnFinalizeInternal()
		{
			base.IsActive = false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004244 File Offset: 0x00002444
		public static void RefreshGenericHintTooltip(HintVM hint, object[] args)
		{
			string text = args[0] as string;
			hint.Text = text;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004261 File Offset: 0x00002461
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00004269 File Offset: 0x00002469
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this._text != value)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x04000070 RID: 112
		private string _text = "";
	}
}
