using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000009 RID: 9
	public class BoolStateChangerWidget : Widget
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002736 File Offset: 0x00000936
		public BoolStateChangerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002740 File Offset: 0x00000940
		private void AddState(Widget widget, string state, bool includeChildren)
		{
			widget.AddState(state);
			if (includeChildren)
			{
				for (int i = 0; i < widget.ChildCount; i++)
				{
					this.AddState(widget.GetChild(i), state, true);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002778 File Offset: 0x00000978
		private void SetState(Widget widget, string state, bool includeChildren)
		{
			widget.SetState(state);
			if (includeChildren)
			{
				for (int i = 0; i < widget.ChildCount; i++)
				{
					this.SetState(widget.GetChild(i), state, true);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027B0 File Offset: 0x000009B0
		private void TriggerUpdated()
		{
			string state = this.BooleanCheck ? this.TrueState : this.FalseState;
			Widget widget = this.TargetWidget ?? this;
			this.AddState(widget, state, this.IncludeChildren);
			this.SetState(widget, state, this.IncludeChildren);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002804 File Offset: 0x00000A04
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
					this.TriggerUpdated();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002830 File Offset: 0x00000A30
		[Editor(false)]
		public string TrueState
		{
			get
			{
				return this._trueState;
			}
			set
			{
				if (this._trueState != value)
				{
					this._trueState = value;
					base.OnPropertyChanged<string>(value, "TrueState");
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002853 File Offset: 0x00000A53
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000285B File Offset: 0x00000A5B
		[Editor(false)]
		public string FalseState
		{
			get
			{
				return this._falseState;
			}
			set
			{
				if (this._falseState != value)
				{
					this._falseState = value;
					base.OnPropertyChanged<string>(value, "FalseState");
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000287E File Offset: 0x00000A7E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002886 File Offset: 0x00000A86
		[Editor(false)]
		public Widget TargetWidget
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
					base.OnPropertyChanged<Widget>(value, "TargetWidget");
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028A4 File Offset: 0x00000AA4
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000028AC File Offset: 0x00000AAC
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

		// Token: 0x04000011 RID: 17
		private bool _booleanCheck;

		// Token: 0x04000012 RID: 18
		private string _trueState;

		// Token: 0x04000013 RID: 19
		private string _falseState;

		// Token: 0x04000014 RID: 20
		private Widget _targetWidget;

		// Token: 0x04000015 RID: 21
		private bool _includeChildren;
	}
}
