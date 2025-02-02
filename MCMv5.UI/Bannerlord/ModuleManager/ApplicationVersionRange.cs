using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	internal class ApplicationVersionRange : IEquatable<ApplicationVersionRange>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BA4C File Offset: 0x00009C4C
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ApplicationVersionRange);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000BA58 File Offset: 0x00009C58
		public static ApplicationVersionRange Empty
		{
			get
			{
				return new ApplicationVersionRange();
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BA5F File Offset: 0x00009C5F
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BA67 File Offset: 0x00009C67
		public ApplicationVersion Min { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000BA70 File Offset: 0x00009C70
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000BA78 File Offset: 0x00009C78
		public ApplicationVersion Max { get; set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BA81 File Offset: 0x00009C81
		public ApplicationVersionRange()
		{
			this.Min = ApplicationVersion.Empty;
			this.Max = ApplicationVersion.Empty;
			base..ctor();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BAA1 File Offset: 0x00009CA1
		public ApplicationVersionRange(ApplicationVersion min, ApplicationVersion max)
		{
			this.Min = ApplicationVersion.Empty;
			this.Max = ApplicationVersion.Empty;
			base..ctor();
			this.Max = max;
			this.Min = min;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		[NullableContext(2)]
		public bool IsSame(ApplicationVersionRange other)
		{
			return this.Min.IsSame((other != null) ? other.Min : null) && this.Max.IsSame((other != null) ? other.Max : null);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BB06 File Offset: 0x00009D06
		[NullableContext(2)]
		public bool IsSameWithChangeSet(ApplicationVersionRange other)
		{
			return this.Min.IsSameWithChangeSet((other != null) ? other.Min : null) && this.Max.IsSameWithChangeSet((other != null) ? other.Max : null);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BB3B File Offset: 0x00009D3B
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.Min, this.Max);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BB54 File Offset: 0x00009D54
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

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BC06 File Offset: 0x00009E06
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

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BC40 File Offset: 0x00009E40
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ApplicationVersionRange left, ApplicationVersionRange right)
		{
			return !(left == right);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BC4C File Offset: 0x00009E4C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ApplicationVersionRange left, ApplicationVersionRange right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BC62 File Offset: 0x00009E62
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Max>k__BackingField);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BCA2 File Offset: 0x00009EA2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApplicationVersionRange);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ApplicationVersionRange other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BD13 File Offset: 0x00009F13
		[CompilerGenerated]
		protected ApplicationVersionRange(ApplicationVersionRange original)
		{
			this.Min = original.<Min>k__BackingField;
			this.Max = original.<Max>k__BackingField;
		}
	}
}
