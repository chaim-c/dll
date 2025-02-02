using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x02000018 RID: 24
	[NullableContext(2)]
	[Nullable(0)]
	public class PropertyRef : IRef, INotifyPropertyChanged, IEquatable<PropertyRef>
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00003798 File Offset: 0x00001998
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x000037D0 File Offset: 0x000019D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003805 File Offset: 0x00001A05
		[Nullable(1)]
		public PropertyInfo PropertyInfo { [NullableContext(1)] get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000380D File Offset: 0x00001A0D
		[Nullable(1)]
		public object Instance { [NullableContext(1)] get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003815 File Offset: 0x00001A15
		[Nullable(1)]
		public Type Type
		{
			[NullableContext(1)]
			get
			{
				return this.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003822 File Offset: 0x00001A22
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003838 File Offset: 0x00001A38
		public object Value
		{
			get
			{
				return this.PropertyInfo.GetValue(this.Instance);
			}
			set
			{
				bool canWrite = this.PropertyInfo.CanWrite;
				if (canWrite)
				{
					this.PropertyInfo.SetValue(this.Instance, value);
					this.OnPropertyChanged("Value");
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003876 File Offset: 0x00001A76
		[NullableContext(1)]
		public PropertyRef(PropertyInfo propInfo, object instance)
		{
			this.PropertyInfo = propInfo;
			this.Instance = instance;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000388E File Offset: 0x00001A8E
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000038AC File Offset: 0x00001AAC
		public bool Equals(PropertyRef other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				result = (flag2 || (this.PropertyInfo.Equals(other.PropertyInfo) && this.Instance.Equals(other.Instance)));
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003900 File Offset: 0x00001B00
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = obj.GetType() != base.GetType();
					result = (!flag3 && this.Equals((PropertyRef)obj));
				}
			}
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003950 File Offset: 0x00001B50
		public override int GetHashCode()
		{
			int hash = 269;
			hash = hash * 47 + this.PropertyInfo.GetHashCode();
			return hash * 47 + this.Instance.GetHashCode();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000398B File Offset: 0x00001B8B
		public static bool operator ==(PropertyRef left, PropertyRef right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003994 File Offset: 0x00001B94
		public static bool operator !=(PropertyRef left, PropertyRef right)
		{
			return !object.Equals(left, right);
		}
	}
}
