using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000095 RID: 149
	public class MultiplayerLobbyBadgeButtonWidget : ButtonWidget
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x000174DF File Offset: 0x000156DF
		public MultiplayerLobbyBadgeButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000174E8 File Offset: 0x000156E8
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.EventManager.HoveredView == this && Input.IsKeyPressed(InputKey.ControllerRUp))
			{
				this.OnMouseAlternatePressed();
				return;
			}
			if (base.EventManager.HoveredView == this && Input.IsKeyReleased(InputKey.ControllerRUp))
			{
				this.OnMouseAlternateReleased();
			}
		}
	}
}
