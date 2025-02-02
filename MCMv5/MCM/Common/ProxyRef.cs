using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x02000019 RID: 25
	[NullableContext(2)]
	[Nullable(0)]
	public class ProxyRef<T> : IRef, INotifyPropertyChanged, IEquatable<ProxyRef<T>>
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000080 RID: 128 RVA: 0x000039A0 File Offset: 0x00001BA0
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x000039D8 File Offset: 0x00001BD8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003A0D File Offset: 0x00001C0D
		[Nullable(1)]
		public Type Type
		{
			[NullableContext(1)]
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003A19 File Offset: 0x00001C19
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003A2C File Offset: 0x00001C2C
		public object Value
		{
			get
			{
				return this._getter();
			}
			set
			{
				T val;
				bool flag;
				if (this._setter != null)
				{
					if (value is T)
					{
						val = (T)((object)value);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this._setter(val);
					this.OnPropertyChanged("Value");
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003A77 File Offset: 0x00001C77
		[NullableContext(1)]
		public ProxyRef(Func<T> getter, [Nullable(new byte[]
		{
			2,
			1
		})] Action<T> setter)
		{
			if (getter == null)
			{
				throw new ArgumentNullException("getter");
			}
			this._getter = getter;
			this._setter = setter;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003A9E File Offset: 0x00001C9E
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003ABC File Offset: 0x00001CBC
		public bool Equals([Nullable(new byte[]
		{
			2,
			1
		})] ProxyRef<T> other)
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
				result = (flag2 || (this._getter.Equals(other._getter) && object.Equals(this._setter, other._setter)));
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B10 File Offset: 0x00001D10
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
					result = (!flag3 && this.Equals((ProxyRef<T>)obj));
				}
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B60 File Offset: 0x00001D60
		public override int GetHashCode()
		{
			int hash = 269;
			hash = hash * 47 + this._getter.GetHashCode();
			bool flag = this._setter != null;
			if (flag)
			{
				hash = hash * 47 + this._setter.GetHashCode();
			}
			return hash;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003BAB File Offset: 0x00001DAB
		public static bool operator ==([Nullable(new byte[]
		{
			2,
			1
		})] ProxyRef<T> left, [Nullable(new byte[]
		{
			2,
			1
		})] ProxyRef<T> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static bool operator !=([Nullable(new byte[]
		{
			2,
			1
		})] ProxyRef<T> left, [Nullable(new byte[]
		{
			2,
			1
		})] ProxyRef<T> right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0400001F RID: 31
		[Nullable(1)]
		private readonly Func<T> _getter;

		// Token: 0x04000020 RID: 32
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private readonly Action<T> _setter;
	}
}
