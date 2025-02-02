using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000164 RID: 356
	public class ConversationPersuasionProgressRichTextWidget : RichTextWidget
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00032BFD File Offset: 0x00030DFD
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00032C05 File Offset: 0x00030E05
		public float FadeInTime { get; set; } = 1f;

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00032C0E File Offset: 0x00030E0E
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00032C16 File Offset: 0x00030E16
		public float FadeOutTime { get; set; } = 1f;

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00032C1F File Offset: 0x00030E1F
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00032C27 File Offset: 0x00030E27
		public float StayTime { get; set; } = 2.5f;

		// Token: 0x0600129A RID: 4762 RVA: 0x00032C30 File Offset: 0x00030E30
		public ConversationPersuasionProgressRichTextWidget(UIContext context) : base(context)
		{
			base.PropertyChanged += this.OnSelfPropertyChanged;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00032C84 File Offset: 0x00030E84
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._startTime == -1f)
			{
				this.SetGlobalAlphaRecursively(0f);
				return;
			}
			float alphaFactor;
			if (base.EventManager.Time - this._startTime < this.FadeInTime)
			{
				alphaFactor = Mathf.Lerp(0f, 1f, (base.EventManager.Time - this._startTime) / this.FadeInTime);
			}
			else if (base.EventManager.Time - this._startTime < this.StayTime + this.FadeInTime)
			{
				alphaFactor = 1f;
			}
			else
			{
				alphaFactor = Mathf.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, 0f, (base.EventManager.Time - (this._startTime + this.StayTime + this.FadeInTime)) / this.FadeOutTime);
				if (base.ReadOnlyBrush.GlobalAlphaFactor <= 0.001f)
				{
					this._startTime = -1f;
				}
			}
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00032D89 File Offset: 0x00030F89
		private void OnSelfPropertyChanged(PropertyOwnerObject arg1, string propertyName, object newState)
		{
			if (propertyName == "Text" && !string.IsNullOrEmpty(newState as string))
			{
				this._startTime = base.EventManager.Time;
			}
		}

		// Token: 0x04000879 RID: 2169
		private float _startTime = -1f;
	}
}
