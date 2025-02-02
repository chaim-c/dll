using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000133 RID: 307
	[NullableContext(1)]
	[Nullable(0)]
	internal class ApplicationVersionRange : IEquatable<ApplicationVersionRange>
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00019710 File Offset: 0x00017910
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ApplicationVersionRange);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001971C File Offset: 0x0001791C
		public static ApplicationVersionRange Empty
		{
			get
			{
				return new ApplicationVersionRange();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00019723 File Offset: 0x00017923
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001972B File Offset: 0x0001792B
		public ApplicationVersion Min { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00019734 File Offset: 0x00017934
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001973C File Offset: 0x0001793C
		public ApplicationVersion Max { get; set; }

		// Token: 0x060007DE RID: 2014 RVA: 0x00019745 File Offset: 0x00017945
		public ApplicationVersionRange()
		{
			this.Min = ApplicationVersion.Empty;
			this.Max = ApplicationVersion.Empty;
			base..ctor();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00019765 File Offset: 0x00017965
		public ApplicationVersionRange(ApplicationVersion min, ApplicationVersion max)
		{
			this.Min = ApplicationVersion.Empty;
			this.Max = ApplicationVersion.Empty;
			base..ctor();
			this.Max = max;
			this.Min = min;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00019795 File Offset: 0x00017995
		[NullableContext(2)]
		public bool IsSame(ApplicationVersionRange other)
		{
			return this.Min.IsSame((other != null) ? other.Min : null) && this.Max.IsSame((other != null) ? other.Max : null);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000197CA File Offset: 0x000179CA
		[NullableContext(2)]
		public bool IsSameWithChangeSet(ApplicationVersionRange other)
		{
			return this.Min.IsSameWithChangeSet((other != null) ? other.Min : null) && this.Max.IsSameWithChangeSet((other != null) ? other.Max : null);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000197FF File Offset: 0x000179FF
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.Min, this.Max);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00019818 File Offset: 0x00017A18
		public static bool TryParse(string versionRangeAsString, out ApplicationVersionRange versionRange)
		{
			versionRange = ApplicationVersionRange.Empty;
			bool flag = string.IsNullOrWhiteSpace(versionRangeAsString);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				versionRangeAsString = versionRangeAsString.Replace(" ", string.Empty);
				int index = versionRangeAsString.IndexOf('-');
				bool flag2 = index < 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					string minAsString = versionRangeAsString.Substring(0, index);
					string maxAsString = versionRangeAsString.Substring(index + 1, versionRangeAsString.Length - 1 - index);
					ApplicationVersion min;
					ApplicationVersion max;
					bool flag3 = ApplicationVersion.TryParse(minAsString, out min, true) && ApplicationVersion.TryParse(maxAsString, out max, false);
					if (flag3)
					{
						versionRange = new ApplicationVersionRange
						{
							Min = min,
							Max = max
						};
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000198CA File Offset: 0x00017ACA
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Min = ");
			builder.Append(this.Min);
			builder.Append(", Max = ");
			builder.Append(this.Max);
			return true;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00019904 File Offset: 0x00017B04
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ApplicationVersionRange left, ApplicationVersionRange right)
		{
			return !(left == right);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00019910 File Offset: 0x00017B10
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ApplicationVersionRange left, ApplicationVersionRange right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00019926 File Offset: 0x00017B26
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Max>k__BackingField);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00019966 File Offset: 0x00017B66
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApplicationVersionRange);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00019974 File Offset: 0x00017B74
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ApplicationVersionRange other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000199D7 File Offset: 0x00017BD7
		[CompilerGenerated]
		protected ApplicationVersionRange(ApplicationVersionRange original)
		{
			this.Min = original.<Min>k__BackingField;
			this.Max = original.<Max>k__BackingField;
		}
	}
}
