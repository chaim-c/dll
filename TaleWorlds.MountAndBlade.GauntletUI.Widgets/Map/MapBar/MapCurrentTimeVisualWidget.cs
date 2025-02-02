using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x0200011C RID: 284
	public class MapCurrentTimeVisualWidget : Widget
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00029134 File Offset: 0x00027334
		public MapCurrentTimeVisualWidget(UIContext context) : base(context)
		{
			base.AddState("Disabled");
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00029148 File Offset: 0x00027348
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				return;
			}
			this.SetState("Default");
			bool isSelected = false;
			bool isSelected2 = false;
			bool isSelected3 = false;
			switch (this.CurrentTimeState)
			{
			case 0:
			case 6:
				isSelected3 = true;
				break;
			case 1:
			case 3:
				isSelected = true;
				break;
			case 2:
			case 4:
			case 5:
				isSelected2 = true;
				break;
			}
			this.PlayButton.IsSelected = isSelected;
			this.FastForwardButton.IsSelected = isSelected2;
			this.PauseButton.IsSelected = isSelected3;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000291DA File Offset: 0x000273DA
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x000291E2 File Offset: 0x000273E2
		[Editor(false)]
		public int CurrentTimeState
		{
			get
			{
				return this._currenTimeState;
			}
			set
			{
				if (this._currenTimeState != value)
				{
					this._currenTimeState = value;
					base.OnPropertyChanged(value, "CurrentTimeState");
				}
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00029200 File Offset: 0x00027400
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00029208 File Offset: 0x00027408
		[Editor(false)]
		public ButtonWidget FastForwardButton
		{
			get
			{
				return this._fastForwardButton;
			}
			set
			{
				if (this._fastForwardButton != value)
				{
					this._fastForwardButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FastForwardButton");
				}
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00029226 File Offset: 0x00027426
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0002922E File Offset: 0x0002742E
		[Editor(false)]
		public ButtonWidget PlayButton
		{
			get
			{
				return this._playButton;
			}
			set
			{
				if (this._playButton != value)
				{
					this._playButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "PlayButton");
				}
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002924C File Offset: 0x0002744C
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00029254 File Offset: 0x00027454
		[Editor(false)]
		public ButtonWidget PauseButton
		{
			get
			{
				return this._pauseButton;
			}
			set
			{
				if (this._pauseButton != value)
				{
					this._pauseButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "PauseButton");
				}
			}
		}

		// Token: 0x040006C9 RID: 1737
		private int _currenTimeState;

		// Token: 0x040006CA RID: 1738
		private ButtonWidget _fastForwardButton;

		// Token: 0x040006CB RID: 1739
		private ButtonWidget _playButton;

		// Token: 0x040006CC RID: 1740
		private ButtonWidget _pauseButton;
	}
}
