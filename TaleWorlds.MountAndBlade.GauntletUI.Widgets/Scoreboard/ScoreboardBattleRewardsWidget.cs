using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard
{
	// Token: 0x0200004F RID: 79
	public class ScoreboardBattleRewardsWidget : Widget
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public ScoreboardBattleRewardsWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000DA2F File Offset: 0x0000BC2F
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._isAnimationActive)
			{
				this.UpdateAnimation(dt);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public void StartAnimation()
		{
			this._isAnimationActive = true;
			this._animationTimePassed = 0f;
			this._animationLastItemIndex = -1;
			this.ItemContainer.SetState("Opened");
			for (int i = 0; i < this.ItemContainer.ChildCount; i++)
			{
				Widget child = this.ItemContainer.GetChild(i);
				child.IsVisible = false;
				child.AddState("Opened");
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public void Reset()
		{
			for (int i = 0; i < this.ItemContainer.ChildCount; i++)
			{
				this.ItemContainer.GetChild(i).IsVisible = false;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		private void UpdateAnimation(float dt)
		{
			if (this._animationTimePassed >= this.AnimationDelay + this.AnimationInterval * (float)this.ItemContainer.ChildCount)
			{
				return;
			}
			if (this._animationTimePassed >= this.AnimationDelay)
			{
				int num = MathF.Floor((this._animationTimePassed - this.AnimationDelay) / this.AnimationInterval);
				if (num != this._animationLastItemIndex && num < this.ItemContainer.ChildCount)
				{
					for (int i = this._animationLastItemIndex + 1; i <= num; i++)
					{
						Widget child = this.ItemContainer.GetChild(i);
						child.IsVisible = true;
						child.SetState("Opened");
					}
				}
			}
			this._animationTimePassed += dt;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000DB99 File Offset: 0x0000BD99
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000DBA1 File Offset: 0x0000BDA1
		[Editor(false)]
		public float AnimationDelay
		{
			get
			{
				return this._animationDelay;
			}
			set
			{
				if (this._animationDelay != value)
				{
					this._animationDelay = value;
					base.OnPropertyChanged(value, "AnimationDelay");
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000DBBF File Offset: 0x0000BDBF
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
		[Editor(false)]
		public float AnimationInterval
		{
			get
			{
				return this._animationInterval;
			}
			set
			{
				if (this._animationInterval != value)
				{
					this._animationInterval = value;
					base.OnPropertyChanged(value, "AnimationInterval");
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000DBED File Offset: 0x0000BDED
		[Editor(false)]
		public Widget ItemContainer
		{
			get
			{
				return this._itemContainer;
			}
			set
			{
				if (this._itemContainer != value)
				{
					this._itemContainer = value;
					base.OnPropertyChanged<Widget>(value, "ItemContainer");
				}
			}
		}

		// Token: 0x040001D3 RID: 467
		private bool _isAnimationActive;

		// Token: 0x040001D4 RID: 468
		private float _animationTimePassed;

		// Token: 0x040001D5 RID: 469
		private int _animationLastItemIndex;

		// Token: 0x040001D6 RID: 470
		private float _animationDelay = 1f;

		// Token: 0x040001D7 RID: 471
		private float _animationInterval = 0.25f;

		// Token: 0x040001D8 RID: 472
		private Widget _itemContainer;
	}
}
