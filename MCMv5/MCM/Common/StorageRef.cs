using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x0200001B RID: 27
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class StorageRef : IRef, INotifyPropertyChanged
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000093 RID: 147 RVA: 0x00003CC4 File Offset: 0x00001EC4
		// (remove) Token: 0x06000094 RID: 148 RVA: 0x00003CFC File Offset: 0x00001EFC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D31 File Offset: 0x00001F31
		[Nullable(1)]
		public Type Type { [NullableContext(1)] get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003D39 File Offset: 0x00001F39
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003D44 File Offset: 0x00001F44
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				bool flag = !object.Equals(this._value, value);
				if (flag)
				{
					this._value = value;
					this.OnPropertyChanged("Value");
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D7A File Offset: 0x00001F7A
		public StorageRef(object value)
		{
			this._value = value;
			Type type = (value != null) ? value.GetType() : null;
			if (type == null)
			{
				throw new Exception("Value can't be null!");
			}
			this.Type = type;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003DAC File Offset: 0x00001FAC
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000027 RID: 39
		private object _value;
	}
}
