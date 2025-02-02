using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E7 RID: 231
	public class OrderOfBattleHeroDropWidget : ButtonWidget
	{
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		public OrderOfBattleHeroDropWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00020EE9 File Offset: 0x0001F0E9
		protected override bool OnPreviewDrop()
		{
			this.HandleSoundEvent();
			return true;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00020EF2 File Offset: 0x0001F0F2
		protected override void OnClick()
		{
			this.HandleSoundEvent();
			base.OnClick();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00020F00 File Offset: 0x0001F100
		private void HandleSoundEvent()
		{
			switch (this.FormationClass)
			{
			case 0:
				break;
			case 1:
				base.EventFired("Infantry", Array.Empty<object>());
				return;
			case 2:
				base.EventFired("Archers", Array.Empty<object>());
				return;
			case 3:
				base.EventFired("Cavalry", Array.Empty<object>());
				return;
			case 4:
				base.EventFired("HorseArchers", Array.Empty<object>());
				return;
			case 5:
				base.EventFired("InfantryArchers", Array.Empty<object>());
				return;
			case 6:
				base.EventFired("CavalryHorseArchers", Array.Empty<object>());
				break;
			default:
				return;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00020F9C File Offset: 0x0001F19C
		protected override bool OnPreviewDragHover()
		{
			return true;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00020F9F File Offset: 0x0001F19F
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		[DataSourceProperty]
		public int FormationClass
		{
			get
			{
				return this._formationClass;
			}
			set
			{
				if (value != this._formationClass)
				{
					this._formationClass = value;
					base.OnPropertyChanged(value, "FormationClass");
				}
			}
		}

		// Token: 0x0400056F RID: 1391
		private int _formationClass;
	}
}
