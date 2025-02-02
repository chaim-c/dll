using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x02000089 RID: 137
	public class MultiplayerScoreboardEndOfBattlePanelWidget : Widget
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001611F File Offset: 0x0001431F
		public MultiplayerScoreboardEndOfBattlePanelWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00016134 File Offset: 0x00014334
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._isFinished || !this._isStarted)
			{
				return;
			}
			this._timePassed += dt;
			if (this._timePassed >= this.SecondDelay)
			{
				this._isFinished = true;
				this.SetState("Opened");
				base.Context.TwoDimensionContext.PlaySound(this._openedSoundEvent);
				return;
			}
			if (this._timePassed >= this.FirstDelay && !this._isPreStateFinished)
			{
				this._isPreStateFinished = true;
				this.SetState("PreOpened");
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000161C6 File Offset: 0x000143C6
		public void StartAnimation()
		{
			this._isStarted = true;
			this._isFinished = false;
			this._isPreStateFinished = false;
			this._timePassed = 0f;
			base.AddState("PreOpened");
			base.AddState("Opened");
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000161FE File Offset: 0x000143FE
		private void Reset()
		{
			this._isStarted = false;
			this._isPreStateFinished = false;
			this._isFinished = false;
			this.SetState("Default");
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00016220 File Offset: 0x00014420
		private void AvailableUpdated()
		{
			if (this.IsAvailable)
			{
				this.StartAnimation();
				return;
			}
			this.Reset();
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00016237 File Offset: 0x00014437
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001623F File Offset: 0x0001443F
		[Editor(false)]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (value != this._isAvailable)
				{
					this._isAvailable = value;
					base.OnPropertyChanged(value, "IsAvailable");
					this.AvailableUpdated();
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00016263 File Offset: 0x00014463
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001626B File Offset: 0x0001446B
		[Editor(false)]
		public float FirstDelay
		{
			get
			{
				return this._firstDelay;
			}
			set
			{
				if (value != this._firstDelay)
				{
					this._firstDelay = value;
					base.OnPropertyChanged(value, "FirstDelay");
				}
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00016289 File Offset: 0x00014489
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00016291 File Offset: 0x00014491
		[Editor(false)]
		public float SecondDelay
		{
			get
			{
				return this._secondDelay;
			}
			set
			{
				if (value != this._secondDelay)
				{
					this._secondDelay = value;
					base.OnPropertyChanged(value, "SecondDelay");
				}
			}
		}

		// Token: 0x04000356 RID: 854
		private bool _isStarted;

		// Token: 0x04000357 RID: 855
		private bool _isPreStateFinished;

		// Token: 0x04000358 RID: 856
		private bool _isFinished;

		// Token: 0x04000359 RID: 857
		private float _timePassed;

		// Token: 0x0400035A RID: 858
		private string _openedSoundEvent = "panels/scoreboard_flags";

		// Token: 0x0400035B RID: 859
		private bool _isAvailable;

		// Token: 0x0400035C RID: 860
		private float _firstDelay;

		// Token: 0x0400035D RID: 861
		private float _secondDelay;
	}
}
