using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x0200011E RID: 286
	public class MapInfoSilhouetteWidget : Widget
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x000294A4 File Offset: 0x000276A4
		public MapInfoSilhouetteWidget(UIContext context) : base(context)
		{
			base.AddState("MapScreen");
			base.AddState("InventoryGauntletScreen");
			base.AddState("GauntletPartyScreen");
			base.AddState("GauntletCharacterDeveloperScreen");
			base.AddState("GauntletClanScreen");
			base.AddState("GauntletQuestsScreen");
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x000294FA File Offset: 0x000276FA
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00029504 File Offset: 0x00027704
		[Editor(false)]
		public string CurrentScreen
		{
			get
			{
				return this._currentScreen;
			}
			set
			{
				if (this._currentScreen != value)
				{
					this._currentScreen = value;
					if (base.ContainsState(this._currentScreen))
					{
						this.SetState(this._currentScreen);
					}
					else
					{
						this.SetState("Default");
					}
					base.OnPropertyChanged<string>(value, "CurrentScreen");
				}
			}
		}

		// Token: 0x040006D0 RID: 1744
		private string _currentScreen;
	}
}
