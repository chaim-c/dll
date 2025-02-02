using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024D RID: 589
	public struct CompassItemUpdateParams
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x00070036 File Offset: 0x0006E236
		public CompassItemUpdateParams(object item, TargetIconType targetType, Vec3 worldPosition, uint color, uint color2)
		{
			this = default(CompassItemUpdateParams);
			this.Item = item;
			this.TargetType = targetType;
			this.WorldPosition = worldPosition;
			this.Color = color;
			this.Color2 = color2;
			this.IsAttacker = false;
			this.IsAlly = false;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00070072 File Offset: 0x0006E272
		public CompassItemUpdateParams(object item, TargetIconType targetType, Vec3 worldPosition, BannerCode bannerCode, bool isAttacker, bool isAlly)
		{
			this = default(CompassItemUpdateParams);
			this.Item = item;
			this.TargetType = targetType;
			this.WorldPosition = worldPosition;
			this.BannerCode = bannerCode;
			this.IsAttacker = isAttacker;
			this.IsAlly = isAlly;
		}

		// Token: 0x04000BAA RID: 2986
		public readonly object Item;

		// Token: 0x04000BAB RID: 2987
		public readonly TargetIconType TargetType;

		// Token: 0x04000BAC RID: 2988
		public readonly Vec3 WorldPosition;

		// Token: 0x04000BAD RID: 2989
		public readonly uint Color;

		// Token: 0x04000BAE RID: 2990
		public readonly uint Color2;

		// Token: 0x04000BAF RID: 2991
		public readonly BannerCode BannerCode;

		// Token: 0x04000BB0 RID: 2992
		public readonly bool IsAttacker;

		// Token: 0x04000BB1 RID: 2993
		public readonly bool IsAlly;
	}
}
