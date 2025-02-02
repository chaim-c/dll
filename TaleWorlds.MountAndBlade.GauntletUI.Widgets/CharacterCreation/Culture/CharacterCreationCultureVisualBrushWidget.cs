using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation.Culture
{
	// Token: 0x0200017F RID: 383
	public class CharacterCreationCultureVisualBrushWidget : BrushWidget
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000360CE File Offset: 0x000342CE
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000360D6 File Offset: 0x000342D6
		public bool UseSmallVisuals { get; set; } = true;

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000360DF File Offset: 0x000342DF
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x000360E7 File Offset: 0x000342E7
		public ParallaxItemBrushWidget Layer1Widget { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000360F0 File Offset: 0x000342F0
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000360F8 File Offset: 0x000342F8
		public ParallaxItemBrushWidget Layer2Widget { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00036101 File Offset: 0x00034301
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00036109 File Offset: 0x00034309
		public ParallaxItemBrushWidget Layer3Widget { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00036112 File Offset: 0x00034312
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0003611A File Offset: 0x0003431A
		public ParallaxItemBrushWidget Layer4Widget { get; set; }

		// Token: 0x060013CB RID: 5067 RVA: 0x00036123 File Offset: 0x00034323
		public CharacterCreationCultureVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0003613C File Offset: 0x0003433C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isFirstFrame)
			{
				this._alphaTarget = (float)(string.IsNullOrEmpty(this.CurrentCultureId) ? 0 : 1);
				this.SetGlobalAlphaRecursively(this._alphaTarget);
				ParallaxItemBrushWidget layer1Widget = this.Layer1Widget;
				if (layer1Widget != null)
				{
					layer1Widget.RegisterBrushStatesOfWidget();
				}
				ParallaxItemBrushWidget layer2Widget = this.Layer2Widget;
				if (layer2Widget != null)
				{
					layer2Widget.RegisterBrushStatesOfWidget();
				}
				ParallaxItemBrushWidget layer3Widget = this.Layer3Widget;
				if (layer3Widget != null)
				{
					layer3Widget.RegisterBrushStatesOfWidget();
				}
				ParallaxItemBrushWidget layer4Widget = this.Layer4Widget;
				if (layer4Widget != null)
				{
					layer4Widget.RegisterBrushStatesOfWidget();
				}
				this._isFirstFrame = false;
			}
			this.SetGlobalAlphaRecursively(Mathf.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, this._alphaTarget, dt * 10f));
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000361EC File Offset: 0x000343EC
		private void SetCultureVisual(string newCultureId)
		{
			if (string.IsNullOrEmpty(newCultureId))
			{
				this._alphaTarget = 0f;
				return;
			}
			if (this.UseSmallVisuals)
			{
				using (Dictionary<string, Style>.ValueCollection.Enumerator enumerator = base.Brush.Styles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Style style = enumerator.Current;
						StyleLayer[] layers = style.GetLayers();
						for (int i = 0; i < layers.Length; i++)
						{
							layers[i].Sprite = base.Context.SpriteData.GetSprite("CharacterCreation\\Culture\\" + newCultureId);
						}
					}
					goto IL_CE;
				}
			}
			ParallaxItemBrushWidget layer1Widget = this.Layer1Widget;
			if (layer1Widget != null)
			{
				layer1Widget.SetState(newCultureId);
			}
			ParallaxItemBrushWidget layer2Widget = this.Layer2Widget;
			if (layer2Widget != null)
			{
				layer2Widget.SetState(newCultureId);
			}
			ParallaxItemBrushWidget layer3Widget = this.Layer3Widget;
			if (layer3Widget != null)
			{
				layer3Widget.SetState(newCultureId);
			}
			ParallaxItemBrushWidget layer4Widget = this.Layer4Widget;
			if (layer4Widget != null)
			{
				layer4Widget.SetState(newCultureId);
			}
			IL_CE:
			this._alphaTarget = 1f;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x000362E4 File Offset: 0x000344E4
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x000362EC File Offset: 0x000344EC
		[Editor(false)]
		public string CurrentCultureId
		{
			get
			{
				return this._currentCultureId;
			}
			set
			{
				if (this._currentCultureId != value)
				{
					this._currentCultureId = value;
					base.OnPropertyChanged<string>(value, "CurrentCultureId");
					this.SetCultureVisual(value);
					this.SetGlobalAlphaRecursively(1f);
				}
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00036321 File Offset: 0x00034521
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00036329 File Offset: 0x00034529
		[Editor(false)]
		public bool IsBig
		{
			get
			{
				return this._isBig;
			}
			set
			{
				if (this._isBig != value)
				{
					this._isBig = value;
					base.OnPropertyChanged(value, "IsBig");
				}
			}
		}

		// Token: 0x04000903 RID: 2307
		private float _alphaTarget;

		// Token: 0x04000904 RID: 2308
		private bool _isFirstFrame = true;

		// Token: 0x04000905 RID: 2309
		private string _currentCultureId;

		// Token: 0x04000906 RID: 2310
		private bool _isBig;
	}
}
