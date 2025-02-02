using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000008 RID: 8
	public class BoolBrushChangerBrushWidget : BrushWidget
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000025A6 File Offset: 0x000007A6
		public BoolBrushChangerBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025AF File Offset: 0x000007AF
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialUpdateHandled)
			{
				this.OnBooleanUpdated();
				this._initialUpdateHandled = true;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D0 File Offset: 0x000007D0
		private void OnBooleanUpdated()
		{
			string name = this.BooleanCheck ? this.TrueBrush : this.FalseBrush;
			Brush brush = base.Context.GetBrush(name);
			BrushWidget brushWidget = this.TargetWidget ?? this;
			brushWidget.Brush = brush;
			if (this.IncludeChildren)
			{
				using (IEnumerator<Widget> enumerator = brushWidget.AllChildren.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BrushWidget brushWidget2;
						if ((brushWidget2 = (enumerator.Current as BrushWidget)) != null)
						{
							brushWidget2.Brush = brush;
						}
					}
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002668 File Offset: 0x00000868
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002670 File Offset: 0x00000870
		[Editor(false)]
		public bool BooleanCheck
		{
			get
			{
				return this._booleanCheck;
			}
			set
			{
				if (this._booleanCheck != value)
				{
					this._booleanCheck = value;
					base.OnPropertyChanged(value, "BooleanCheck");
					this.OnBooleanUpdated();
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002694 File Offset: 0x00000894
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000269C File Offset: 0x0000089C
		[Editor(false)]
		public string TrueBrush
		{
			get
			{
				return this._trueBrush;
			}
			set
			{
				if (this._trueBrush != value)
				{
					this._trueBrush = value;
					base.OnPropertyChanged<string>(value, "TrueBrush");
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026BF File Offset: 0x000008BF
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026C7 File Offset: 0x000008C7
		[Editor(false)]
		public string FalseBrush
		{
			get
			{
				return this._falseBrush;
			}
			set
			{
				if (this._falseBrush != value)
				{
					this._falseBrush = value;
					base.OnPropertyChanged<string>(value, "FalseBrush");
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026EA File Offset: 0x000008EA
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026F2 File Offset: 0x000008F2
		[Editor(false)]
		public BrushWidget TargetWidget
		{
			get
			{
				return this._targetWidget;
			}
			set
			{
				if (this._targetWidget != value)
				{
					this._targetWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "TargetWidget");
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002710 File Offset: 0x00000910
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002718 File Offset: 0x00000918
		[Editor(false)]
		public bool IncludeChildren
		{
			get
			{
				return this._includeChildren;
			}
			set
			{
				if (this._includeChildren != value)
				{
					this._includeChildren = value;
					base.OnPropertyChanged(value, "IncludeChildren");
				}
			}
		}

		// Token: 0x0400000B RID: 11
		private bool _initialUpdateHandled;

		// Token: 0x0400000C RID: 12
		private bool _booleanCheck;

		// Token: 0x0400000D RID: 13
		private string _trueBrush;

		// Token: 0x0400000E RID: 14
		private string _falseBrush;

		// Token: 0x0400000F RID: 15
		private BrushWidget _targetWidget;

		// Token: 0x04000010 RID: 16
		private bool _includeChildren;
	}
}
