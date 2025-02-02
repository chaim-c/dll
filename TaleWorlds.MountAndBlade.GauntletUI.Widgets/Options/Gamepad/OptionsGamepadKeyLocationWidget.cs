using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options.Gamepad
{
	// Token: 0x02000073 RID: 115
	public class OptionsGamepadKeyLocationWidget : Widget
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001275F File Offset: 0x0001095F
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00012767 File Offset: 0x00010967
		public bool ForceVisible { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00012770 File Offset: 0x00010970
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00012778 File Offset: 0x00010978
		public int KeyID { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00012781 File Offset: 0x00010981
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00012789 File Offset: 0x00010989
		public int NormalPositionXOffset { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00012792 File Offset: 0x00010992
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001279A File Offset: 0x0001099A
		public int NormalPositionYOffset { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000127A3 File Offset: 0x000109A3
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000127AB File Offset: 0x000109AB
		public int NormalSizeXOfImage { get; private set; } = -1;

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000127B4 File Offset: 0x000109B4
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x000127BC File Offset: 0x000109BC
		public int NormalSizeYOfImage { get; private set; } = -1;

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000127C5 File Offset: 0x000109C5
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000127CD File Offset: 0x000109CD
		public int CurrentSizeXOfImage { get; private set; } = -1;

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000127D6 File Offset: 0x000109D6
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000127DE File Offset: 0x000109DE
		public int CurrentSizeYOfImage { get; private set; } = -1;

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000127E7 File Offset: 0x000109E7
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x000127EF File Offset: 0x000109EF
		public bool IsKeyToTheLeftOfTheGamepad { get; private set; }

		// Token: 0x06000650 RID: 1616 RVA: 0x000127F8 File Offset: 0x000109F8
		public OptionsGamepadKeyLocationWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00012828 File Offset: 0x00010A28
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._valuesInitialized)
			{
				this.NormalSizeXOfImage = base.ParentWidget.Sprite.Width;
				this.NormalSizeYOfImage = base.ParentWidget.Sprite.Height;
				this.CurrentSizeXOfImage = (int)(base.ParentWidget.SuggestedWidth * base._scaleToUse);
				this.CurrentSizeYOfImage = (int)(base.ParentWidget.SuggestedHeight * base._scaleToUse);
				this._keyNameTextWidgets.Clear();
				using (IEnumerator<Widget> enumerator = base.AllChildren.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TextWidget item;
						if ((item = (enumerator.Current as TextWidget)) != null)
						{
							this._keyNameTextWidgets.Add(item);
						}
					}
				}
				this._keyVisualWidget = (base.AllChildren.FirstOrDefault((Widget c) => c is InputKeyVisualWidget) as InputKeyVisualWidget);
				this._valuesInitialized = true;
				this.IsKeyToTheLeftOfTheGamepad = ((float)this.NormalPositionXOffset < (float)this.NormalSizeXOfImage / 2f);
			}
			float num = base.ParentWidget.SuggestedWidth / (float)this.NormalSizeXOfImage;
			float num2 = base.ParentWidget.SuggestedHeight / (float)this.NormalSizeYOfImage;
			base.PositionXOffset = (float)this.NormalPositionXOffset * num;
			base.PositionYOffset = (float)this.NormalPositionYOffset * num2;
			List<TextWidget> keyNameTextWidgets = this._keyNameTextWidgets;
			if (keyNameTextWidgets != null && keyNameTextWidgets.Count == 1)
			{
				this._keyNameTextWidgets[0].Text = this._actionText;
			}
			base.IsVisible = (!string.IsNullOrEmpty(this._actionText) || this.ForceVisible);
			if (this._valuesInitialized)
			{
				if (this.IsKeyToTheLeftOfTheGamepad)
				{
					this._keyNameTextWidgets.ForEach(delegate(TextWidget t)
					{
						t.ScaledSuggestedWidth = MathF.Abs(this._parentAreaWidget.GlobalPosition.X - this._keyVisualWidget.GlobalPosition.X);
						t.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Right;
					});
					return;
				}
				this._keyNameTextWidgets.ForEach(delegate(TextWidget t)
				{
					t.ScaledSuggestedWidth = this._parentAreaWidget.GlobalPosition.X + this._parentAreaWidget.Size.X - (this._keyVisualWidget.GlobalPosition.X + this._keyVisualWidget.Size.X);
					t.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
				});
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00012A24 File Offset: 0x00010C24
		internal void SetKeyProperties(string actionText, Widget parentAreaWidget)
		{
			this._actionText = actionText;
			List<TextWidget> keyNameTextWidgets = this._keyNameTextWidgets;
			if (keyNameTextWidgets != null && keyNameTextWidgets.Count == 1)
			{
				this._keyNameTextWidgets[0].Text = this._actionText;
			}
			this._parentAreaWidget = parentAreaWidget;
			this._valuesInitialized = false;
		}

		// Token: 0x040002BA RID: 698
		private bool _valuesInitialized;

		// Token: 0x040002BB RID: 699
		private string _actionText;

		// Token: 0x040002BC RID: 700
		private Widget _parentAreaWidget;

		// Token: 0x040002BD RID: 701
		private List<TextWidget> _keyNameTextWidgets = new List<TextWidget>();

		// Token: 0x040002BE RID: 702
		private InputKeyVisualWidget _keyVisualWidget;
	}
}
