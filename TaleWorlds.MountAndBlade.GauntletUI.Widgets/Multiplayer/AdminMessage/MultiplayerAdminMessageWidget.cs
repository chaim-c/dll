using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.AdminMessage
{
	// Token: 0x020000CB RID: 203
	public class MultiplayerAdminMessageWidget : Widget
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001DBD1 File Offset: 0x0001BDD1
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0001DBD9 File Offset: 0x0001BDD9
		public TextWidget MessageTextWidget { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001DBE2 File Offset: 0x0001BDE2
		public float MessageOnScreenStayTime
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0001DBE9 File Offset: 0x0001BDE9
		public float MessageFadeInTime
		{
			get
			{
				return 0.4f;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		public float MessageFadeOutTime
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001DBF7 File Offset: 0x0001BDF7
		public MultiplayerAdminMessageWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001DC00 File Offset: 0x0001BE00
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.ChildCount <= 0)
			{
				this._currentTextOnScreenTime = 0f;
				return;
			}
			this._currentTextOnScreenTime += dt;
			if (this._currentTextOnScreenTime < this.MessageFadeInTime)
			{
				float alphaFactor = MathF.Lerp(0f, 1f, this._currentTextOnScreenTime / this.MessageFadeInTime, 1E-05f);
				base.Children[0].SetGlobalAlphaRecursively(alphaFactor);
				base.Children[0].IsVisible = true;
				return;
			}
			if (this._currentTextOnScreenTime > this.MessageFadeInTime && this._currentTextOnScreenTime < this.MessageOnScreenStayTime + this.MessageFadeInTime)
			{
				base.Children[0].SetGlobalAlphaRecursively(1f);
				return;
			}
			if (this._currentTextOnScreenTime < this.MessageFadeInTime + this.MessageOnScreenStayTime + this.MessageFadeOutTime)
			{
				float alphaFactor2 = MathF.Lerp(1f, 0f, (this._currentTextOnScreenTime - (this.MessageFadeInTime + this.MessageOnScreenStayTime)) / this.MessageFadeOutTime, 1E-05f);
				base.Children[0].SetGlobalAlphaRecursively(alphaFactor2);
				return;
			}
			MultiplayerAdminMessageItemWidget multiplayerAdminMessageItemWidget = base.Children[0] as MultiplayerAdminMessageItemWidget;
			if (multiplayerAdminMessageItemWidget != null)
			{
				multiplayerAdminMessageItemWidget.Remove();
			}
			this._currentTextOnScreenTime = 0f;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001DD4F File Offset: 0x0001BF4F
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
		}

		// Token: 0x040004CF RID: 1231
		private float _currentTextOnScreenTime;
	}
}
