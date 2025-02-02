using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Perks
{
	// Token: 0x02000091 RID: 145
	public class MultiplayerPerkPopupWidget : Widget
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00016BD1 File Offset: 0x00014DD1
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00016BD9 File Offset: 0x00014DD9
		public bool ShowAboveContainer { get; set; }

		// Token: 0x060007C3 RID: 1987 RVA: 0x00016BE2 File Offset: 0x00014DE2
		public MultiplayerPerkPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00016BEB File Offset: 0x00014DEB
		public void SetPopupPerksContainer(MultiplayerPerkContainerPanelWidget container)
		{
			this._latestContainer = container;
			base.ApplyActionOnAllChildren(new Action<Widget>(this.SetContainersOfChildren));
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00016C08 File Offset: 0x00014E08
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsVisible && this._latestContainer != null)
			{
				float num = this._latestContainer.GlobalPosition.X - (base.Size.X / 2f - this._latestContainer.Size.X / 2f);
				base.ScaledPositionXOffset = Mathf.Clamp(num - base.EventManager.LeftUsableAreaStart, 0f, base.Context.EventManager.PageSize.X - base.Size.X);
				if (!this.ShowAboveContainer)
				{
					base.ScaledPositionYOffset = this._latestContainer.GlobalPosition.Y + this._latestContainer.Size.Y - base.EventManager.TopUsableAreaStart;
					return;
				}
				base.ScaledPositionYOffset = this._latestContainer.GlobalPosition.Y - base.Size.Y - base.EventManager.TopUsableAreaStart;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00016D14 File Offset: 0x00014F14
		private void SetContainersOfChildren(Widget obj)
		{
			MultiplayerPerkItemToggleWidget multiplayerPerkItemToggleWidget;
			if ((multiplayerPerkItemToggleWidget = (obj as MultiplayerPerkItemToggleWidget)) != null)
			{
				multiplayerPerkItemToggleWidget.ContainerPanel = this._latestContainer;
			}
		}

		// Token: 0x0400037D RID: 893
		private MultiplayerPerkContainerPanelWidget _latestContainer;
	}
}
