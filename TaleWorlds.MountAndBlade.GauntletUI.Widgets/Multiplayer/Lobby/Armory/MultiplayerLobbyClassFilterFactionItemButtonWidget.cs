using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B3 RID: 179
	public class MultiplayerLobbyClassFilterFactionItemButtonWidget : ButtonWidget
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0001AE98 File Offset: 0x00019098
		public MultiplayerLobbyClassFilterFactionItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001AEAC File Offset: 0x000190AC
		private void OnCultureChanged()
		{
			if (this.Culture == null)
			{
				return;
			}
			string name = this.BaseBrushName + "." + this.Culture[0].ToString().ToUpper() + this.Culture.Substring(1).ToLower();
			base.Brush = base.Context.BrushFactory.GetBrush(name);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001AF14 File Offset: 0x00019114
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0001AF1C File Offset: 0x0001911C
		[Editor(false)]
		public string BaseBrushName
		{
			get
			{
				return this._baseBrushName;
			}
			set
			{
				if (value != this._baseBrushName)
				{
					this._baseBrushName = value;
					base.OnPropertyChanged<string>(value, "BaseBrushName");
					this.OnCultureChanged();
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001AF45 File Offset: 0x00019145
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0001AF4D File Offset: 0x0001914D
		[Editor(false)]
		public string Culture
		{
			get
			{
				return this._culture;
			}
			set
			{
				if (this._culture != value)
				{
					this._culture = value;
					base.OnPropertyChanged<string>(value, "Culture");
					this.OnCultureChanged();
				}
			}
		}

		// Token: 0x04000452 RID: 1106
		private string _baseBrushName = "MPLobby.ClassFilter.FactionButton";

		// Token: 0x04000453 RID: 1107
		private string _culture;
	}
}
