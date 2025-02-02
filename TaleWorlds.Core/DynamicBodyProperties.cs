using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public struct DynamicBodyProperties
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0001681E File Offset: 0x00014A1E
		public DynamicBodyProperties(float age, float weight, float build)
		{
			this.Age = age;
			this.Weight = weight;
			this.Build = build;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00016838 File Offset: 0x00014A38
		public static bool operator ==(DynamicBodyProperties a, DynamicBodyProperties b)
		{
			return a == b || (a != null && b != null && (a.Age == b.Age && a.Weight == b.Weight) && a.Build == b.Build);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00016893 File Offset: 0x00014A93
		public static bool operator !=(DynamicBodyProperties a, DynamicBodyProperties b)
		{
			return !(a == b);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001689F File Offset: 0x00014A9F
		public bool Equals(DynamicBodyProperties other)
		{
			return this.Age.Equals(other.Age) && this.Weight.Equals(other.Weight) && this.Build.Equals(other.Build);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000168DA File Offset: 0x00014ADA
		public override bool Equals(object obj)
		{
			return obj != null && obj is DynamicBodyProperties && this.Equals((DynamicBodyProperties)obj);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000168F7 File Offset: 0x00014AF7
		public override int GetHashCode()
		{
			return (this.Age.GetHashCode() * 397 ^ this.Weight.GetHashCode()) * 397 ^ this.Build.GetHashCode();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00016928 File Offset: 0x00014B28
		public override string ToString()
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(150, "ToString");
			mbstringBuilder.Append<string>("age=\"");
			mbstringBuilder.Append<string>(this.Age.ToString("0.##"));
			mbstringBuilder.Append<string>("\" weight=\"");
			mbstringBuilder.Append<string>(this.Weight.ToString("0.####"));
			mbstringBuilder.Append<string>("\" build=\"");
			mbstringBuilder.Append<string>(this.Build.ToString("0.####"));
			mbstringBuilder.Append<string>("\" ");
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x0400031A RID: 794
		public const float MaxAge = 128f;

		// Token: 0x0400031B RID: 795
		public const float MaxAgeTeenager = 21f;

		// Token: 0x0400031C RID: 796
		public float Age;

		// Token: 0x0400031D RID: 797
		public float Weight;

		// Token: 0x0400031E RID: 798
		public float Build;

		// Token: 0x0400031F RID: 799
		public static readonly DynamicBodyProperties Invalid;

		// Token: 0x04000320 RID: 800
		public static readonly DynamicBodyProperties Default = new DynamicBodyProperties(20f, 0.5f, 0.5f);
	}
}
