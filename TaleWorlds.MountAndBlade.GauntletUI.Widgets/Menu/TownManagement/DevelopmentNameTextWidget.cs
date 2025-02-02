using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000FC RID: 252
	public class DevelopmentNameTextWidget : TextWidget
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x0002518D File Offset: 0x0002338D
		public DevelopmentNameTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000251A8 File Offset: 0x000233A8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this.IsInQueue)
			{
				this.SetState(base.ParentWidget.CurrentState);
			}
			else
			{
				this.SetState("Selected");
			}
			this.HandleAnim(dt);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000251E0 File Offset: 0x000233E0
		private void HandleAnim(float dt)
		{
			switch (this._currentState)
			{
			case DevelopmentNameTextWidget.AnimState.Start:
				this._currentAlphaTarget = 0f;
				this._currentState = DevelopmentNameTextWidget.AnimState.DownName;
				break;
			case DevelopmentNameTextWidget.AnimState.DownName:
				if ((double)base.ReadOnlyBrush.TextAlphaFactor < 0.01)
				{
					this._currentAlphaTarget = 1f;
					base.Text = this.MaxText;
					this._currentState = DevelopmentNameTextWidget.AnimState.UpMax;
				}
				break;
			case DevelopmentNameTextWidget.AnimState.UpMax:
				if ((double)base.ReadOnlyBrush.TextAlphaFactor > 0.99)
				{
					this._currentAlphaTarget = 0f;
					this._currentState = DevelopmentNameTextWidget.AnimState.StayMax;
					this._stayMaxTotalTime = 0f;
				}
				break;
			case DevelopmentNameTextWidget.AnimState.StayMax:
				this._stayMaxTotalTime += dt;
				if (this._stayMaxTotalTime >= this.MaxTextStayTime)
				{
					this._currentAlphaTarget = 0f;
					this._currentState = DevelopmentNameTextWidget.AnimState.DownMax;
				}
				break;
			case DevelopmentNameTextWidget.AnimState.DownMax:
				if ((double)base.ReadOnlyBrush.TextAlphaFactor < 0.01)
				{
					this._currentAlphaTarget = 1f;
					this._currentState = DevelopmentNameTextWidget.AnimState.UpName;
					base.Text = this.NameText;
				}
				break;
			case DevelopmentNameTextWidget.AnimState.UpName:
				if ((double)base.ReadOnlyBrush.TextAlphaFactor > 0.99)
				{
					this._currentState = DevelopmentNameTextWidget.AnimState.Idle;
					base.Text = this.NameText;
				}
				break;
			}
			if (this._currentState != DevelopmentNameTextWidget.AnimState.Idle && this._currentState != DevelopmentNameTextWidget.AnimState.StayMax)
			{
				base.Brush.TextAlphaFactor = Mathf.Lerp(base.ReadOnlyBrush.TextAlphaFactor, this._currentAlphaTarget, dt * 15f);
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00025378 File Offset: 0x00023578
		public void StartMaxTextAnimation()
		{
			DevelopmentNameTextWidget.AnimState currentState = this._currentState;
			if (currentState > DevelopmentNameTextWidget.AnimState.StayMax)
			{
				int num = currentState - DevelopmentNameTextWidget.AnimState.DownMax;
				this._currentState = DevelopmentNameTextWidget.AnimState.Start;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002539D File Offset: 0x0002359D
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x000253A5 File Offset: 0x000235A5
		[Editor(false)]
		public string MaxText
		{
			get
			{
				return this._maxText;
			}
			set
			{
				if (this._maxText != value)
				{
					this._maxText = value;
					base.OnPropertyChanged<string>(value, "MaxText");
				}
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x000253C8 File Offset: 0x000235C8
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x000253D0 File Offset: 0x000235D0
		[Editor(false)]
		public float MaxTextStayTime
		{
			get
			{
				return this._maxTextStayTime;
			}
			set
			{
				if (this._maxTextStayTime != value)
				{
					this._maxTextStayTime = value;
					base.OnPropertyChanged(value, "MaxTextStayTime");
				}
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000253EE File Offset: 0x000235EE
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000253F6 File Offset: 0x000235F6
		[Editor(false)]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (this._nameText != value)
				{
					this._nameText = value;
					base.OnPropertyChanged<string>(value, "NameText");
					base.Text = this.NameText;
				}
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00025425 File Offset: 0x00023625
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0002542D File Offset: 0x0002362D
		[Editor(false)]
		public bool IsInQueue
		{
			get
			{
				return this._isInQueue;
			}
			set
			{
				if (this._isInQueue != value)
				{
					this._isInQueue = value;
					base.OnPropertyChanged(value, "IsInQueue");
				}
			}
		}

		// Token: 0x04000621 RID: 1569
		private float _currentAlphaTarget;

		// Token: 0x04000622 RID: 1570
		private float _stayMaxTotalTime;

		// Token: 0x04000623 RID: 1571
		private DevelopmentNameTextWidget.AnimState _currentState = DevelopmentNameTextWidget.AnimState.Idle;

		// Token: 0x04000624 RID: 1572
		private float _maxTextStayTime = 1f;

		// Token: 0x04000625 RID: 1573
		private bool _isInQueue;

		// Token: 0x04000626 RID: 1574
		private string _maxText;

		// Token: 0x04000627 RID: 1575
		private string _nameText;

		// Token: 0x020001AD RID: 429
		public enum AnimState
		{
			// Token: 0x040009B1 RID: 2481
			Start,
			// Token: 0x040009B2 RID: 2482
			DownName,
			// Token: 0x040009B3 RID: 2483
			UpMax,
			// Token: 0x040009B4 RID: 2484
			StayMax,
			// Token: 0x040009B5 RID: 2485
			DownMax,
			// Token: 0x040009B6 RID: 2486
			UpName,
			// Token: 0x040009B7 RID: 2487
			Idle
		}
	}
}
