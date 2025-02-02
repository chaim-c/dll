using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000086 RID: 134
	public class MultiplayerTeamPlayerAvatarButtonWidget : ButtonWidget
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x000158BF File Offset: 0x00013ABF
		public MultiplayerTeamPlayerAvatarButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000158D3 File Offset: 0x00013AD3
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized && this.AvatarImage != null)
			{
				this._originalAvatarImageAlpha = this.AvatarImage.ReadOnlyBrush.GlobalAlphaFactor;
				this.UpdateGlobalAlpha();
				this._isInitialized = true;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00015910 File Offset: 0x00013B10
		private void UpdateGlobalAlpha()
		{
			if (this._isInitialized)
			{
				float num = this.IsDead ? this.DeathAlphaFactor : 1f;
				float globalAlphaFactor = num * this._originalAvatarImageAlpha;
				this.SetGlobalAlphaRecursively(num);
				this.AvatarImage.Brush.GlobalAlphaFactor = globalAlphaFactor;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001595C File Offset: 0x00013B5C
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00015964 File Offset: 0x00013B64
		[DataSourceProperty]
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (this._isDead != value)
				{
					this._isDead = value;
					base.OnPropertyChanged(value, "IsDead");
					this.UpdateGlobalAlpha();
				}
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00015988 File Offset: 0x00013B88
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00015990 File Offset: 0x00013B90
		[DataSourceProperty]
		public float DeathAlphaFactor
		{
			get
			{
				return this._deathAlphaFactor;
			}
			set
			{
				if (this._deathAlphaFactor != value)
				{
					this._deathAlphaFactor = value;
					base.OnPropertyChanged(value, "DeathAlphaFactor");
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000159AE File Offset: 0x00013BAE
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x000159B6 File Offset: 0x00013BB6
		[DataSourceProperty]
		public ImageIdentifierWidget AvatarImage
		{
			get
			{
				return this._avatarImage;
			}
			set
			{
				if (this._avatarImage != value)
				{
					this._avatarImage = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "AvatarImage");
				}
			}
		}

		// Token: 0x0400033D RID: 829
		private bool _isInitialized;

		// Token: 0x0400033E RID: 830
		private float _originalAvatarImageAlpha = 1f;

		// Token: 0x0400033F RID: 831
		private bool _isDead;

		// Token: 0x04000340 RID: 832
		private float _deathAlphaFactor;

		// Token: 0x04000341 RID: 833
		private ImageIdentifierWidget _avatarImage;
	}
}
