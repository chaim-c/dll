using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x0200001C RID: 28
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class StorageRef<T> : IRef, INotifyPropertyChanged
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600009A RID: 154 RVA: 0x00003DC8 File Offset: 0x00001FC8
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x00003E00 File Offset: 0x00002000
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003E35 File Offset: 0x00002035
		[Nullable(1)]
		public Type Type { [NullableContext(1)] get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003E3D File Offset: 0x0000203D
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003E4C File Offset: 0x0000204C
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				T val;
				bool flag;
				if (value is T)
				{
					val = (T)((object)value);
					flag = !object.Equals(this._value, val);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this._value = val;
					this.OnPropertyChanged("Value");
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003EA0 File Offset: 0x000020A0
		public StorageRef(T value)
		{
			this._value = value;
			ref T ptr = ref value;
			T t = default(T);
			Type type;
			if (t == null)
			{
				t = value;
				ptr = ref t;
				if (t == null)
				{
					type = null;
					goto IL_41;
				}
			}
			type = ptr.GetType();
			IL_41:
			this.Type = (type ?? typeof(T));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F01 File Offset: 0x00002101
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400002A RID: 42
		private T _value;
	}
}
