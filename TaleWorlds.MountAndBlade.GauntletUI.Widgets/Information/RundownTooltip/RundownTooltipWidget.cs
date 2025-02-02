using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information.RundownTooltip
{
	// Token: 0x0200013F RID: 319
	public class RundownTooltipWidget : TooltipWidget
	{
		// Token: 0x06001100 RID: 4352 RVA: 0x0002F8DC File Offset: 0x0002DADC
		public RundownTooltipWidget(UIContext context) : base(context)
		{
			this.RefreshOnNextLateUpdate();
			this._animationDelayInFrames = 2;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0002F968 File Offset: 0x0002DB68
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.LineContainerWidget != null)
			{
				GridLayout gridLayout = this.LineContainerWidget.GridLayout;
				bool flag = this._lastCheckedColumnWidths.Count != gridLayout.ColumnWidths.Count;
				bool flag2 = false;
				for (int i = 0; i < this._lastCheckedColumnWidths.Count; i++)
				{
					float num = this._lastCheckedColumnWidths[i];
					float num2 = (i < gridLayout.ColumnWidths.Count) ? gridLayout.ColumnWidths[i] : -1f;
					if (MathF.Abs(num - num2) > 1E-05f)
					{
						flag2 = true;
						break;
					}
				}
				if (flag || flag2)
				{
					this._lastCheckedColumnWidths = gridLayout.ColumnWidths;
					RundownColumnDividerCollectionWidget dividerCollectionWidget = this.DividerCollectionWidget;
					if (dividerCollectionWidget == null)
					{
						return;
					}
					dividerCollectionWidget.Refresh(gridLayout.ColumnWidths);
				}
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002FA30 File Offset: 0x0002DC30
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			GridLayout gridLayout = this.LineContainerWidget.GridLayout;
			for (int i = 0; i < this.LineContainerWidget.ChildCount; i++)
			{
				RundownLineWidget rundownLineWidget = this.LineContainerWidget.GetChild(i) as RundownLineWidget;
				int num = i / this.LineContainerWidget.RowCount;
				rundownLineWidget.RefreshValueOffset((num < gridLayout.ColumnWidths.Count) ? gridLayout.ColumnWidths[num] : -1f);
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0002FAAC File Offset: 0x0002DCAC
		private void Refresh()
		{
			RundownTooltipWidget.ValueCategorization valueCategorizationAsInt = (RundownTooltipWidget.ValueCategorization)this.ValueCategorizationAsInt;
			if (this.LineContainerWidget != null)
			{
				List<RundownLineWidget> list = new List<RundownLineWidget>();
				float num = 0f;
				float num2 = 0f;
				using (List<Widget>.Enumerator enumerator = this.LineContainerWidget.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RundownLineWidget rundownLineWidget;
						if ((rundownLineWidget = (enumerator.Current as RundownLineWidget)) != null)
						{
							list.Add(rundownLineWidget);
							float value = rundownLineWidget.Value;
							if (value < num)
							{
								num = value;
							}
							if (value > num2)
							{
								num2 = value;
							}
						}
					}
				}
				foreach (RundownLineWidget rundownLineWidget2 in list)
				{
					float value2 = rundownLineWidget2.Value;
					Brush brush = rundownLineWidget2.ValueTextWidget.Brush;
					Color fontColor = this._defaultValueColor;
					if (valueCategorizationAsInt != RundownTooltipWidget.ValueCategorization.None)
					{
						float num3 = (value2 < 0f) ? num : num2;
						float ratio = MathF.Abs(value2 / num3);
						float num4 = (float)((valueCategorizationAsInt == RundownTooltipWidget.ValueCategorization.LargeIsBetter) ? 1 : -1) * value2;
						fontColor = Color.Lerp(this._defaultValueColor, (num4 < 0f) ? this._negativeValueColor : this._positiveValueColor, ratio);
					}
					brush.FontColor = fontColor;
				}
			}
			this._willRefreshThisFrame = false;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002FC00 File Offset: 0x0002DE00
		private void RefreshOnNextLateUpdate()
		{
			if (!this._willRefreshThisFrame)
			{
				this._willRefreshThisFrame = true;
				base.EventManager.AddLateUpdateAction(this, delegate(float _)
				{
					this.Refresh();
				}, 1);
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0002FC2A File Offset: 0x0002DE2A
		private void OnLineContainerEventFire(Widget widget, string eventName, object[] args)
		{
			if (eventName == "ItemAdd" || eventName == "ItemRemove")
			{
				this.RefreshOnNextLateUpdate();
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x0002FC54 File Offset: 0x0002DE54
		[Editor(false)]
		public GridWidget LineContainerWidget
		{
			get
			{
				return this._lineContainerWidget;
			}
			set
			{
				if (value != this._lineContainerWidget)
				{
					if (this._lineContainerWidget != null)
					{
						this._lineContainerWidget.EventFire -= this.OnLineContainerEventFire;
					}
					this._lineContainerWidget = value;
					base.OnPropertyChanged<GridWidget>(value, "LineContainerWidget");
					this.RefreshOnNextLateUpdate();
					if (this._lineContainerWidget != null)
					{
						this._lineContainerWidget.EventFire += this.OnLineContainerEventFire;
					}
				}
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0002FCC1 File Offset: 0x0002DEC1
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0002FCC9 File Offset: 0x0002DEC9
		[Editor(false)]
		public RundownColumnDividerCollectionWidget DividerCollectionWidget
		{
			get
			{
				return this._dividerCollectionWidget;
			}
			set
			{
				if (value != this._dividerCollectionWidget)
				{
					this._dividerCollectionWidget = value;
					base.OnPropertyChanged<RundownColumnDividerCollectionWidget>(value, "DividerCollectionWidget");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0002FCED File Offset: 0x0002DEED
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x0002FCF5 File Offset: 0x0002DEF5
		[Editor(false)]
		public int ValueCategorizationAsInt
		{
			get
			{
				return this._valueCategorizationAsInt;
			}
			set
			{
				if (value != this._valueCategorizationAsInt)
				{
					this._valueCategorizationAsInt = value;
					base.OnPropertyChanged(value, "ValueCategorizationAsInt");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x040007CF RID: 1999
		private readonly Color _defaultValueColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040007D0 RID: 2000
		private readonly Color _negativeValueColor = new Color(0.8352941f, 0.12941177f, 0.12941177f, 1f);

		// Token: 0x040007D1 RID: 2001
		private readonly Color _positiveValueColor = new Color(0.38039216f, 0.7490196f, 0.33333334f, 1f);

		// Token: 0x040007D2 RID: 2002
		private bool _willRefreshThisFrame;

		// Token: 0x040007D3 RID: 2003
		private IReadOnlyList<float> _lastCheckedColumnWidths = new List<float>();

		// Token: 0x040007D4 RID: 2004
		private GridWidget _lineContainerWidget;

		// Token: 0x040007D5 RID: 2005
		private RundownColumnDividerCollectionWidget _dividerCollectionWidget;

		// Token: 0x040007D6 RID: 2006
		private int _valueCategorizationAsInt;

		// Token: 0x020001B9 RID: 441
		private enum ValueCategorization
		{
			// Token: 0x040009E6 RID: 2534
			None,
			// Token: 0x040009E7 RID: 2535
			LargeIsBetter,
			// Token: 0x040009E8 RID: 2536
			SmallIsBetter
		}
	}
}
