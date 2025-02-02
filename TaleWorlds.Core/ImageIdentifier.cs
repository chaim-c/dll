﻿using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Core
{
	// Token: 0x02000085 RID: 133
	public class ImageIdentifier
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001B0C9 File Offset: 0x000192C9
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001B0D1 File Offset: 0x000192D1
		public ImageIdentifierType ImageTypeCode { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001B0DA File Offset: 0x000192DA
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001B0E2 File Offset: 0x000192E2
		public string AdditionalArgs { get; private set; }

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B0EB File Offset: 0x000192EB
		public ImageIdentifier(ImageIdentifierType imageType = ImageIdentifierType.Null)
		{
			this.ImageTypeCode = imageType;
			this.Id = "";
			this.AdditionalArgs = "";
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001B110 File Offset: 0x00019310
		public ImageIdentifier(ItemObject itemObject, string bannerCode = "")
		{
			this.ImageTypeCode = ImageIdentifierType.Item;
			this.Id = itemObject.StringId;
			this.AdditionalArgs = bannerCode;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001B132 File Offset: 0x00019332
		public ImageIdentifier(CharacterCode characterCode)
		{
			this.ImageTypeCode = ImageIdentifierType.Character;
			this.Id = characterCode.Code;
			this.AdditionalArgs = "";
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001B158 File Offset: 0x00019358
		public ImageIdentifier(CraftingPiece craftingPiece, string pieceUsageId)
		{
			this.ImageTypeCode = ImageIdentifierType.CraftingPiece;
			this.Id = ((craftingPiece != null) ? (craftingPiece.StringId + "$" + pieceUsageId) : "");
			this.AdditionalArgs = "";
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001B193 File Offset: 0x00019393
		public ImageIdentifier(BannerCode bannerCode, bool nineGrid = false)
		{
			this.ImageTypeCode = (nineGrid ? ImageIdentifierType.BannerCodeNineGrid : ImageIdentifierType.BannerCode);
			this.Id = ((bannerCode != null) ? bannerCode.Code : "");
			this.AdditionalArgs = "";
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001B1CF File Offset: 0x000193CF
		public ImageIdentifier(PlayerId playerId, int forcedAvatarIndex)
		{
			this.ImageTypeCode = ImageIdentifierType.MultiplayerAvatar;
			this.Id = playerId.ToString();
			this.AdditionalArgs = string.Format("{0}", forcedAvatarIndex);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001B208 File Offset: 0x00019408
		public ImageIdentifier(Banner banner)
		{
			this.ImageTypeCode = ImageIdentifierType.BannerCode;
			this.AdditionalArgs = "";
			if (banner != null)
			{
				BannerCode bannerCode = BannerCode.CreateFrom(banner);
				this.Id = bannerCode.Code;
				return;
			}
			this.Id = "";
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001B24F File Offset: 0x0001944F
		public ImageIdentifier(ImageIdentifier code)
		{
			this.ImageTypeCode = code.ImageTypeCode;
			this.Id = code.Id;
			this.AdditionalArgs = code.AdditionalArgs;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001B27B File Offset: 0x0001947B
		public ImageIdentifier(string id, ImageIdentifierType type, string additionalArgs = "")
		{
			this.ImageTypeCode = type;
			this.Id = id;
			this.AdditionalArgs = additionalArgs;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001B298 File Offset: 0x00019498
		public bool Equals(ImageIdentifier target)
		{
			return target != null && this.ImageTypeCode == target.ImageTypeCode && this.Id.Equals(target.Id) && this.AdditionalArgs.Equals(target.AdditionalArgs);
		}

		// Token: 0x040003ED RID: 1005
		public string Id;
	}
}
