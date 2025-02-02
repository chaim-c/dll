using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000100 RID: 256
	public readonly struct ClanCardSelectionItemInfo
	{
		// Token: 0x06001822 RID: 6178 RVA: 0x00059154 File Offset: 0x00057354
		public ClanCardSelectionItemInfo(object identifier, TextObject title, ImageIdentifier image, CardSelectionItemSpriteType spriteType, string spriteName, string spriteLabel, IEnumerable<ClanCardSelectionItemPropertyInfo> properties, bool isDisabled, TextObject disabledReason, TextObject actionResult)
		{
			this.Identifier = identifier;
			this.Title = title;
			this.Image = image;
			this.SpriteType = spriteType;
			this.SpriteName = spriteName;
			this.SpriteLabel = spriteLabel;
			this.Properties = properties;
			this.IsSpecialActionItem = false;
			this.SpecialActionText = null;
			this.IsDisabled = isDisabled;
			this.DisabledReason = disabledReason;
			this.ActionResult = actionResult;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000591DC File Offset: 0x000573DC
		public ClanCardSelectionItemInfo(TextObject specialActionText, bool isDisabled, TextObject disabledReason, TextObject actionResult)
		{
			this.Identifier = null;
			this.Title = null;
			this.Image = null;
			this.SpriteType = CardSelectionItemSpriteType.None;
			this.SpriteName = null;
			this.SpriteLabel = null;
			this.Properties = null;
			this.IsSpecialActionItem = true;
			this.SpecialActionText = specialActionText;
			this.IsDisabled = isDisabled;
			this.DisabledReason = disabledReason;
			this.ActionResult = actionResult;
		}

		// Token: 0x04000B49 RID: 2889
		public readonly object Identifier;

		// Token: 0x04000B4A RID: 2890
		public readonly TextObject Title;

		// Token: 0x04000B4B RID: 2891
		public readonly ImageIdentifier Image;

		// Token: 0x04000B4C RID: 2892
		public readonly CardSelectionItemSpriteType SpriteType;

		// Token: 0x04000B4D RID: 2893
		public readonly string SpriteName;

		// Token: 0x04000B4E RID: 2894
		public readonly string SpriteLabel;

		// Token: 0x04000B4F RID: 2895
		public readonly IEnumerable<ClanCardSelectionItemPropertyInfo> Properties;

		// Token: 0x04000B50 RID: 2896
		public readonly bool IsSpecialActionItem;

		// Token: 0x04000B51 RID: 2897
		public readonly TextObject SpecialActionText;

		// Token: 0x04000B52 RID: 2898
		public readonly bool IsDisabled;

		// Token: 0x04000B53 RID: 2899
		public readonly TextObject DisabledReason;

		// Token: 0x04000B54 RID: 2900
		public readonly TextObject ActionResult;
	}
}
