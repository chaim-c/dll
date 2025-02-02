using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000084 RID: 132
	public class MultiplayerPlayerBadgeVisualWidget : Widget
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x00015772 File Offset: 0x00013972
		public MultiplayerPlayerBadgeVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001577B File Offset: 0x0001397B
		private void UpdateVisual(string badgeId)
		{
			if (badgeId == "badge_official_server_admin")
			{
				badgeId = "badge_taleworlds_dev";
			}
			base.Sprite = base.Context.SpriteData.GetSprite("MPPlayerBadges\\" + badgeId);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000157B2 File Offset: 0x000139B2
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._hasForcedSize)
			{
				base.ScaledSuggestedWidth = this._forcedWidth * base._inverseScaleToUse;
				base.ScaledSuggestedHeight = this._forcedHeight * base._inverseScaleToUse;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000157E9 File Offset: 0x000139E9
		public void SetForcedSize(float width, float height)
		{
			this._forcedWidth = width;
			this._forcedHeight = height;
			this._hasForcedSize = true;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00015800 File Offset: 0x00013A00
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x00015808 File Offset: 0x00013A08
		public string BadgeId
		{
			get
			{
				return this._badgeId;
			}
			set
			{
				if (value != this._badgeId)
				{
					this._badgeId = value;
					base.OnPropertyChanged<string>(value, "BadgeId");
					this.UpdateVisual(value);
				}
			}
		}

		// Token: 0x04000335 RID: 821
		private float _forcedWidth;

		// Token: 0x04000336 RID: 822
		private float _forcedHeight;

		// Token: 0x04000337 RID: 823
		private bool _hasForcedSize;

		// Token: 0x04000338 RID: 824
		private string _badgeId;
	}
}
