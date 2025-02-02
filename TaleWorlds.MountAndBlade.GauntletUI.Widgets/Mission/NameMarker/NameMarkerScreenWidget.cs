using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.NameMarker
{
	// Token: 0x020000ED RID: 237
	public class NameMarkerScreenWidget : Widget
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x00022373 File Offset: 0x00020573
		public NameMarkerScreenWidget(UIContext context) : base(context)
		{
			this._markers = new List<NameMarkerListPanel>();
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00022388 File Offset: 0x00020588
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float end = this.IsMarkersEnabled ? this.TargetAlphaValue : 0f;
			float amount = MathF.Clamp(dt * 10f, 0f, 1f);
			base.AlphaFactor = Mathf.Lerp(base.AlphaFactor, end, amount);
			bool flag = this._markers.Count > 0;
			for (int i = 0; i < this._markers.Count; i++)
			{
				this._markers[i].Update(dt);
				flag &= (this._markers[i].TypeVisualWidget.AlphaFactor > 0f);
			}
			if (flag)
			{
				this._markers.Sort((NameMarkerListPanel m1, NameMarkerListPanel m2) => m1.Rect.Left.CompareTo(m2.Rect.Left));
				for (int j = 0; j < this._markers.Count; j++)
				{
					int num = j + 1;
					while (num < this._markers.Count && this._markers[num].Rect.Left - this._markers[j].Rect.Left <= this._markers[j].Rect.Width)
					{
						if (this._markers[j].Rect.IsOverlapping(this._markers[num].Rect))
						{
							this._markers[num].ScaledPositionXOffset += this._markers[j].Rect.Right - this._markers[num].Rect.Left;
							this._markers[num].UpdateRectangle();
						}
						num++;
					}
				}
				NameMarkerListPanel nameMarkerListPanel = null;
				float num2 = 3600f;
				for (int k = 0; k < this._markers.Count; k++)
				{
					if (this._markers[k].IsInScreenBoundaries)
					{
						NameMarkerListPanel nameMarkerListPanel2 = this._markers[k];
						float num3 = base.EventManager.PageSize.X / 2f;
						float num4 = base.EventManager.PageSize.Y / 2f;
						float num5 = Mathf.Abs(num3 - nameMarkerListPanel2.Rect.CenterX);
						float num6 = Mathf.Abs(num4 - nameMarkerListPanel2.Rect.CenterY);
						float num7 = num5 * num5 + num6 * num6;
						if (num7 < num2)
						{
							num2 = num7;
							nameMarkerListPanel = nameMarkerListPanel2;
						}
					}
				}
				if (nameMarkerListPanel != this._lastFocusedWidget)
				{
					if (this._lastFocusedWidget != null)
					{
						this._lastFocusedWidget.IsFocused = false;
					}
					this._lastFocusedWidget = nameMarkerListPanel;
					if (this._lastFocusedWidget != null)
					{
						this._lastFocusedWidget.IsFocused = true;
					}
				}
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002266C File Offset: 0x0002086C
		private void OnMarkersChanged(Widget widget, string eventName, object[] args)
		{
			NameMarkerListPanel item;
			if (args.Length == 1 && (item = (args[0] as NameMarkerListPanel)) != null)
			{
				if (eventName == "ItemAdd")
				{
					this._markers.Add(item);
					return;
				}
				if (eventName == "ItemRemove")
				{
					this._markers.Remove(item);
				}
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000226BF File Offset: 0x000208BF
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x000226C7 File Offset: 0x000208C7
		public bool IsMarkersEnabled
		{
			get
			{
				return this._isMarkersEnabled;
			}
			set
			{
				if (this._isMarkersEnabled != value)
				{
					this._isMarkersEnabled = value;
					base.OnPropertyChanged(value, "IsMarkersEnabled");
				}
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000226E5 File Offset: 0x000208E5
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x000226ED File Offset: 0x000208ED
		public float TargetAlphaValue
		{
			get
			{
				return this._targetAlphaValue;
			}
			set
			{
				if (this._targetAlphaValue != value)
				{
					this._targetAlphaValue = value;
					base.OnPropertyChanged(value, "TargetAlphaValue");
				}
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002270B File Offset: 0x0002090B
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00022714 File Offset: 0x00020914
		[Editor(false)]
		public Widget MarkersContainer
		{
			get
			{
				return this._markersContainer;
			}
			set
			{
				if (value != this._markersContainer)
				{
					if (this._markersContainer != null)
					{
						this._markersContainer.EventFire += this.OnMarkersChanged;
					}
					this._markersContainer = value;
					if (this._markersContainer != null)
					{
						this._markersContainer.EventFire += this.OnMarkersChanged;
					}
					base.OnPropertyChanged<Widget>(value, "MarkersContainer");
				}
			}
		}

		// Token: 0x040005AE RID: 1454
		private const float MinDistanceToFocusSquared = 3600f;

		// Token: 0x040005AF RID: 1455
		private List<NameMarkerListPanel> _markers;

		// Token: 0x040005B0 RID: 1456
		private NameMarkerListPanel _lastFocusedWidget;

		// Token: 0x040005B1 RID: 1457
		private bool _isMarkersEnabled;

		// Token: 0x040005B2 RID: 1458
		private float _targetAlphaValue;

		// Token: 0x040005B3 RID: 1459
		private Widget _markersContainer;
	}
}
