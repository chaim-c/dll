using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E5 RID: 229
	public class OrderOfBattleHeroButtonWidget : ButtonWidget
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00020B96 File Offset: 0x0001ED96
		public OrderOfBattleHeroButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		private void OnHeroTypeChanged()
		{
			foreach (BrushLayer brushLayer in base.Brush.Layers)
			{
				brushLayer.HueFactor = (float)(this.IsMainHero ? this.MainHeroHueFactor : 0);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00020C08 File Offset: 0x0001EE08
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00020C10 File Offset: 0x0001EE10
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChanged(value, "IsMainHero");
					this.OnHeroTypeChanged();
				}
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00020C34 File Offset: 0x0001EE34
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00020C3C File Offset: 0x0001EE3C
		public int MainHeroHueFactor
		{
			get
			{
				return this._mainHeroHueFactor;
			}
			set
			{
				if (value != this._mainHeroHueFactor)
				{
					this._mainHeroHueFactor = value;
					base.OnPropertyChanged(value, "MainHeroHueFactor");
				}
			}
		}

		// Token: 0x04000568 RID: 1384
		private bool _isMainHero;

		// Token: 0x04000569 RID: 1385
		private int _mainHeroHueFactor;
	}
}
