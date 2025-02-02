using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C8 RID: 200
	public class MultiplayerClassLoadoutTroopSubclassButtonWidget : ButtonWidget
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x0001D91D File Offset: 0x0001BB1D
		public MultiplayerClassLoadoutTroopSubclassButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001D928 File Offset: 0x0001BB28
		private void UpdateIcon()
		{
			if (string.IsNullOrEmpty(this.TroopType) || this._iconWidget == null)
			{
				return;
			}
			Sprite sprite = base.Context.SpriteData.GetSprite("General\\compass\\" + this.TroopType);
			foreach (Style style in this.IconWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Sprite = sprite;
				}
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			Widget parentWidget = base.ParentWidget;
			if (parentWidget == null)
			{
				return;
			}
			parentWidget.SetState(base.CurrentState);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001D9F3 File Offset: 0x0001BBF3
		public override void SetState(string stateName)
		{
			base.SetState(stateName);
			if (this.PerksNavigationScopeTargeter != null)
			{
				this.PerksNavigationScopeTargeter.IsScopeEnabled = (stateName == "Selected");
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001DA1A File Offset: 0x0001BC1A
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0001DA22 File Offset: 0x0001BC22
		[DataSourceProperty]
		public string TroopType
		{
			get
			{
				return this._troopType;
			}
			set
			{
				if (value != this._troopType)
				{
					this._troopType = value;
					base.OnPropertyChanged<string>(value, "TroopType");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001DA4B File Offset: 0x0001BC4B
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0001DA53 File Offset: 0x0001BC53
		[DataSourceProperty]
		public BrushWidget IconWidget
		{
			get
			{
				return this._iconWidget;
			}
			set
			{
				if (value != this._iconWidget)
				{
					this._iconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "IconWidget");
					this.UpdateIcon();
				}
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001DA77 File Offset: 0x0001BC77
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0001DA7F File Offset: 0x0001BC7F
		public NavigationScopeTargeter PerksNavigationScopeTargeter
		{
			get
			{
				return this._perksNavigationScopeTargeter;
			}
			set
			{
				if (value != this._perksNavigationScopeTargeter)
				{
					this._perksNavigationScopeTargeter = value;
					base.OnPropertyChanged<NavigationScopeTargeter>(value, "PerksNavigationScopeTargeter");
					if (this._perksNavigationScopeTargeter != null)
					{
						this._perksNavigationScopeTargeter.IsScopeEnabled = false;
					}
				}
			}
		}

		// Token: 0x040004C7 RID: 1223
		private string _troopType;

		// Token: 0x040004C8 RID: 1224
		private BrushWidget _iconWidget;

		// Token: 0x040004C9 RID: 1225
		private NavigationScopeTargeter _perksNavigationScopeTargeter;
	}
}
