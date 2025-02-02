using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D2 RID: 210
	public class CompassWidget : Widget
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x0001E55D File Offset: 0x0001C75D
		public CompassWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001E566 File Offset: 0x0001C766
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.HandleHorizontalPositioning();
			this.HandleMarkerPositioning();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001E57C File Offset: 0x0001C77C
		private void HandleHorizontalPositioning()
		{
			if (this.ItemContainerPanel.ChildCount <= 0)
			{
				return;
			}
			List<List<Widget>> list = new List<List<Widget>>();
			float valueFrom = 0f;
			float num = this.ItemContainerPanel.ParentWidget.MeasuredSize.X * base._inverseScaleToUse - 50f;
			for (int i = 0; i < this.ItemContainerPanel.ChildCount; i++)
			{
				CompassElementWidget compassElementWidget = this.ItemContainerPanel.GetChild(i) as CompassElementWidget;
				if (!compassElementWidget.IsHidden)
				{
					float amount = (compassElementWidget.Position + 1f) * 0.5f;
					compassElementWidget.MarginLeft = MBMath.Lerp(valueFrom, num, amount, 1E-05f);
					bool flag = false;
					if (list.Count > 0)
					{
						List<Widget> list2 = list[list.Count - 1];
						int j = list2.Count - 1;
						while (j >= 0)
						{
							if (Math.Abs(list2[j].MarginLeft - compassElementWidget.MarginLeft) < 10f)
							{
								flag = true;
								compassElementWidget.MarginLeft = list[list.Count - 1][list[list.Count - 1].Count - 1].MarginLeft + 10f;
								list[list.Count - 1].Add(compassElementWidget);
								if (compassElementWidget.MarginLeft > num)
								{
									float marginLeft = compassElementWidget.MarginLeft;
									compassElementWidget.MarginLeft = num;
									float num2 = marginLeft - compassElementWidget.MarginLeft;
									for (int k = 1; k < list2.Count; k++)
									{
										int index = list2.Count - 1 - k;
										list2[index].MarginLeft -= num2;
									}
									break;
								}
								break;
							}
							else
							{
								j--;
							}
						}
					}
					if (!flag)
					{
						list.Add(new List<Widget>());
						list[list.Count - 1].Add(compassElementWidget);
					}
				}
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001E764 File Offset: 0x0001C964
		private void HandleMarkerPositioning()
		{
			if (this.MarkerContainerPanel.ChildCount <= 0)
			{
				return;
			}
			float valueFrom = 0f;
			float valueTo = this.MarkerContainerPanel.ParentWidget.MeasuredSize.X * base._inverseScaleToUse;
			for (int i = 0; i < this.MarkerContainerPanel.ChildCount; i++)
			{
				CompassMarkerTextWidget compassMarkerTextWidget = this.MarkerContainerPanel.GetChild(i) as CompassMarkerTextWidget;
				float num = (compassMarkerTextWidget.Position + 1f) * 0.5f;
				compassMarkerTextWidget.MarginLeft = MBMath.Lerp(valueFrom, valueTo, num, 1E-05f) - compassMarkerTextWidget.Size.X * 0.5f;
				compassMarkerTextWidget.IsHidden = (MBMath.ApproximatelyEquals(num, 0f, 1E-05f) || MBMath.ApproximatelyEquals(num, 1f, 1E-05f));
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001E835 File Offset: 0x0001CA35
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0001E83D File Offset: 0x0001CA3D
		[DataSourceProperty]
		public Widget ItemContainerPanel
		{
			get
			{
				return this._itemContainerPanel;
			}
			set
			{
				if (this._itemContainerPanel != value)
				{
					this._itemContainerPanel = value;
					base.OnPropertyChanged<Widget>(value, "ItemContainerPanel");
				}
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001E85B File Offset: 0x0001CA5B
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0001E863 File Offset: 0x0001CA63
		[DataSourceProperty]
		public Widget MarkerContainerPanel
		{
			get
			{
				return this._markerContainerPanel;
			}
			set
			{
				if (this._markerContainerPanel != value)
				{
					this._markerContainerPanel = value;
					base.OnPropertyChanged<Widget>(value, "MarkerContainerPanel");
				}
			}
		}

		// Token: 0x040004E8 RID: 1256
		private Widget _itemContainerPanel;

		// Token: 0x040004E9 RID: 1257
		private Widget _markerContainerPanel;
	}
}
