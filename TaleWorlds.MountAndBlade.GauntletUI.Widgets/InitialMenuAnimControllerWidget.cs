using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000026 RID: 38
	public class InitialMenuAnimControllerWidget : Widget
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007658 File Offset: 0x00005858
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00007660 File Offset: 0x00005860
		public bool IsAnimEnabled { get; set; }

		// Token: 0x060001F1 RID: 497 RVA: 0x00007669 File Offset: 0x00005869
		public InitialMenuAnimControllerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007674 File Offset: 0x00005874
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.IsAnimEnabled)
			{
				if (!this._isInitialized)
				{
					Widget optionsList = this.OptionsList;
					bool flag;
					if (optionsList == null)
					{
						flag = false;
					}
					else
					{
						List<Widget> children = optionsList.Children;
						int? num = (children != null) ? new int?(children.Count) : null;
						int num2 = 0;
						flag = (num.GetValueOrDefault() > num2 & num != null);
					}
					if (flag)
					{
						this.OptionsList.Children.ForEach(delegate(Widget x)
						{
							x.SetGlobalAlphaRecursively(0f);
						});
						this._totalOptionCount = this.OptionsList.Children.Count;
						this._isInitialized = true;
					}
				}
				if (this._isInitialized && !this._isFinalized && this.OptionsList != null)
				{
					this._timer += dt;
					if (this._timer >= this.InitialWaitTime + (float)this._currentOptionIndex * this.WaitTimeBetweenOptions)
					{
						Widget child = this.OptionsList.GetChild(this._currentOptionIndex);
						if (child != null)
						{
							child.SetState("Activated");
						}
						this._currentOptionIndex++;
					}
					for (int i = 0; i < this._currentOptionIndex; i++)
					{
						float num3 = this.InitialWaitTime + this.WaitTimeBetweenOptions * (float)i;
						float num4 = num3 + this.OptionFadeInTime;
						if (this._timer < num4)
						{
							float alphaFactor = MathF.Clamp((this._timer - num3) / (num4 - num3), 0f, 1f);
							Widget child2 = this.OptionsList.GetChild(i);
							if (child2 != null)
							{
								child2.SetGlobalAlphaRecursively(alphaFactor);
							}
						}
					}
					this._isFinalized = (this._timer > this.InitialWaitTime + this.WaitTimeBetweenOptions * (float)(this._totalOptionCount - 1) + this.OptionFadeInTime);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007844 File Offset: 0x00005A44
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000784C File Offset: 0x00005A4C
		[Editor(false)]
		public Widget OptionsList
		{
			get
			{
				return this._optionsList;
			}
			set
			{
				if (this._optionsList != value)
				{
					this._optionsList = value;
					base.OnPropertyChanged<Widget>(value, "OptionsList");
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000786A File Offset: 0x00005A6A
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007872 File Offset: 0x00005A72
		[Editor(false)]
		public float InitialWaitTime
		{
			get
			{
				return this._initialWaitTime;
			}
			set
			{
				if (this._initialWaitTime != value)
				{
					this._initialWaitTime = value;
					base.OnPropertyChanged(value, "InitialWaitTime");
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007890 File Offset: 0x00005A90
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00007898 File Offset: 0x00005A98
		[Editor(false)]
		public float WaitTimeBetweenOptions
		{
			get
			{
				return this._waitTimeBetweenOptions;
			}
			set
			{
				if (this._waitTimeBetweenOptions != value)
				{
					this._waitTimeBetweenOptions = value;
					base.OnPropertyChanged(value, "WaitTimeBetweenOptions");
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000078B6 File Offset: 0x00005AB6
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000078BE File Offset: 0x00005ABE
		[Editor(false)]
		public float OptionFadeInTime
		{
			get
			{
				return this._optionFadeInTime;
			}
			set
			{
				if (this._optionFadeInTime != value)
				{
					this._optionFadeInTime = value;
					base.OnPropertyChanged(value, "OptionFadeInTime");
				}
			}
		}

		// Token: 0x040000EC RID: 236
		private bool _isInitialized;

		// Token: 0x040000ED RID: 237
		private bool _isFinalized;

		// Token: 0x040000EE RID: 238
		private int _currentOptionIndex;

		// Token: 0x040000EF RID: 239
		private int _totalOptionCount;

		// Token: 0x040000F0 RID: 240
		private float _timer;

		// Token: 0x040000F1 RID: 241
		private Widget _optionsList;

		// Token: 0x040000F2 RID: 242
		private float _initialWaitTime;

		// Token: 0x040000F3 RID: 243
		private float _waitTimeBetweenOptions;

		// Token: 0x040000F4 RID: 244
		private float _optionFadeInTime;
	}
}
