using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000128 RID: 296
	public class KingdomTributeIconWidget : Widget
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0002A402 File Offset: 0x00028602
		public KingdomTributeIconWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002A40B File Offset: 0x0002860B
		public void UpdateIcons(int tribute)
		{
			if (this.PayIcon != null && this.ReceiveIcon != null)
			{
				this.PayIcon.IsVisible = (tribute > 0);
				this.ReceiveIcon.IsVisible = (tribute < 0);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x0002A43B File Offset: 0x0002863B
		public int Tribute
		{
			set
			{
				this.UpdateIcons(value);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0002A444 File Offset: 0x00028644
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0002A44C File Offset: 0x0002864C
		public Widget PayIcon
		{
			get
			{
				return this._payIcon;
			}
			set
			{
				if (value != this._payIcon)
				{
					this._payIcon = value;
				}
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002A45E File Offset: 0x0002865E
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0002A466 File Offset: 0x00028666
		public Widget ReceiveIcon
		{
			get
			{
				return this._receiveIcon;
			}
			set
			{
				if (value != this._receiveIcon)
				{
					this._receiveIcon = value;
				}
			}
		}

		// Token: 0x04000705 RID: 1797
		private Widget _payIcon;

		// Token: 0x04000706 RID: 1798
		private Widget _receiveIcon;
	}
}
