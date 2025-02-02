using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000014 RID: 20
	public class ContextMenuItemWidget : Widget
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004BA6 File Offset: 0x00002DA6
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004BAE File Offset: 0x00002DAE
		public Widget TypeIconWidget { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004BB7 File Offset: 0x00002DB7
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00004BBF File Offset: 0x00002DBF
		public ButtonWidget ActionButtonWidget { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public string TypeIconState { get; set; }

		// Token: 0x06000107 RID: 263 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public ContextMenuItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004BEC File Offset: 0x00002DEC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				if (this.TypeIconWidget != null && !string.IsNullOrEmpty(this.TypeIconState))
				{
					this.TypeIconWidget.RegisterBrushStatesOfWidget();
					this.TypeIconWidget.SetState(this.TypeIconState);
				}
				this._isInitialized = true;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004C40 File Offset: 0x00002E40
		protected override void RefreshState()
		{
			base.RefreshState();
			if (!this.CanBeUsed)
			{
				this.SetGlobalAlphaRecursively(0.5f);
				return;
			}
			this.SetGlobalAlphaRecursively(1f);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004C67 File Offset: 0x00002E67
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00004C6F File Offset: 0x00002E6F
		public bool CanBeUsed
		{
			get
			{
				return this._canBeUsed;
			}
			set
			{
				if (value != this._canBeUsed)
				{
					this._canBeUsed = value;
					base.OnPropertyChanged(value, "CanBeUsed");
					this.RefreshState();
				}
			}
		}

		// Token: 0x0400007C RID: 124
		private const float _disabledAlpha = 0.5f;

		// Token: 0x0400007D RID: 125
		private const float _enabledAlpha = 1f;

		// Token: 0x04000081 RID: 129
		private bool _isInitialized;

		// Token: 0x04000082 RID: 130
		private bool _canBeUsed = true;
	}
}
