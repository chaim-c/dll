using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000094 RID: 148
	public class MultiplayerLobbyAnimatedRankChangeWidget : Widget
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x000171B8 File Offset: 0x000153B8
		public MultiplayerLobbyAnimatedRankChangeWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000171D8 File Offset: 0x000153D8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this.IsAnimationRequested)
			{
				return;
			}
			if (this._preAnimationTimeElapsed < this._animationDelay)
			{
				this._preAnimationTimeElapsed += dt;
				return;
			}
			if (this._animationTimeElapsed >= this._animationDuration)
			{
				this.NewRankName.SetGlobalAlphaRecursively(1f);
				this.NewRankSprite.SetGlobalAlphaRecursively(1f);
				this.OldRankName.SetGlobalAlphaRecursively(0f);
				this.OldRankSprite.SetGlobalAlphaRecursively(0f);
				this.NewRankSprite.ScaledSuggestedWidth = base.ScaledSuggestedWidth / 2f;
				this.NewRankSprite.ScaledSuggestedHeight = base.ScaledSuggestedHeight / 2f;
				return;
			}
			float num = MathF.Lerp(0f, 1f, this._animationTimeElapsed / this._animationDuration, 1E-05f);
			this.OldRankSprite.SetGlobalAlphaRecursively(1f - num);
			this.OldRankName.SetGlobalAlphaRecursively(1f - num);
			this.NewRankSprite.SetGlobalAlphaRecursively(num);
			this.NewRankName.SetGlobalAlphaRecursively(num);
			this.NewRankSprite.ScaledSuggestedWidth = MathF.Lerp(base.ScaledSuggestedWidth, base.ScaledSuggestedWidth / 2f, this._animationTimeElapsed / this._animationDuration, 1E-05f);
			this.NewRankSprite.ScaledSuggestedHeight = MathF.Lerp(base.ScaledSuggestedHeight, base.ScaledSuggestedHeight / 2f, this._animationTimeElapsed / this._animationDuration, 1E-05f);
			this._animationTimeElapsed += dt;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00017364 File Offset: 0x00015564
		private void StartAnimation()
		{
			this.NewRankName.SetGlobalAlphaRecursively(0f);
			this.NewRankSprite.SetGlobalAlphaRecursively(0f);
			this.OldRankName.SetGlobalAlphaRecursively(1f);
			this.OldRankSprite.SetGlobalAlphaRecursively(1f);
			this.OldRankSprite.ScaledSuggestedWidth = base.ScaledSuggestedWidth / 2f;
			this.OldRankSprite.ScaledSuggestedHeight = base.ScaledSuggestedHeight / 2f;
			this._preAnimationTimeElapsed = 0f;
			this._animationTimeElapsed = 0f;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000173F5 File Offset: 0x000155F5
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x000173FD File Offset: 0x000155FD
		[Editor(false)]
		public bool IsAnimationRequested
		{
			get
			{
				return this._isAnimationRequested;
			}
			set
			{
				if (value != this._isAnimationRequested)
				{
					this._isAnimationRequested = value;
					base.OnPropertyChanged(value, "IsAnimationRequested");
					this.StartAnimation();
				}
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00017421 File Offset: 0x00015621
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00017429 File Offset: 0x00015629
		[Editor(false)]
		public bool IsPromoted
		{
			get
			{
				return this._isPromoted;
			}
			set
			{
				if (value != this._isPromoted)
				{
					this._isPromoted = value;
					base.OnPropertyChanged(value, "IsPromoted");
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00017447 File Offset: 0x00015647
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001744F File Offset: 0x0001564F
		[Editor(false)]
		public TextWidget OldRankName
		{
			get
			{
				return this._oldRankName;
			}
			set
			{
				if (value != this._oldRankName)
				{
					this._oldRankName = value;
					base.OnPropertyChanged<TextWidget>(value, "OldRankName");
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001746D File Offset: 0x0001566D
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00017475 File Offset: 0x00015675
		[Editor(false)]
		public TextWidget NewRankName
		{
			get
			{
				return this._newRankName;
			}
			set
			{
				if (value != this._newRankName)
				{
					this._newRankName = value;
					base.OnPropertyChanged<TextWidget>(value, "NewRankName");
				}
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00017493 File Offset: 0x00015693
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001749B File Offset: 0x0001569B
		[Editor(false)]
		public MultiplayerLobbyRankItemButtonWidget OldRankSprite
		{
			get
			{
				return this._oldRankSprite;
			}
			set
			{
				if (value != this._oldRankSprite)
				{
					this._oldRankSprite = value;
					base.OnPropertyChanged<MultiplayerLobbyRankItemButtonWidget>(value, "OldRankSprite");
				}
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000174B9 File Offset: 0x000156B9
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000174C1 File Offset: 0x000156C1
		[Editor(false)]
		public MultiplayerLobbyRankItemButtonWidget NewRankSprite
		{
			get
			{
				return this._newRankSprite;
			}
			set
			{
				if (value != this._newRankSprite)
				{
					this._newRankSprite = value;
					base.OnPropertyChanged<MultiplayerLobbyRankItemButtonWidget>(value, "NewRankSprite");
				}
			}
		}

		// Token: 0x0400038F RID: 911
		private float _animationTimeElapsed;

		// Token: 0x04000390 RID: 912
		private float _animationDuration = 0.25f;

		// Token: 0x04000391 RID: 913
		private float _preAnimationTimeElapsed;

		// Token: 0x04000392 RID: 914
		private float _animationDelay = 0.5f;

		// Token: 0x04000393 RID: 915
		private bool _isAnimationRequested;

		// Token: 0x04000394 RID: 916
		private bool _isPromoted;

		// Token: 0x04000395 RID: 917
		private TextWidget _oldRankName;

		// Token: 0x04000396 RID: 918
		private TextWidget _newRankName;

		// Token: 0x04000397 RID: 919
		private MultiplayerLobbyRankItemButtonWidget _oldRankSprite;

		// Token: 0x04000398 RID: 920
		private MultiplayerLobbyRankItemButtonWidget _newRankSprite;
	}
}
