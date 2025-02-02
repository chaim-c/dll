using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public sealed class Dropdown<[Nullable(2)] T> : List<T>, IEqualityComparer<Dropdown<T>>, INotifyPropertyChanged, ICloneable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000353B File Offset: 0x0000173B
		public static Dropdown<T> Empty
		{
			get
			{
				return new Dropdown<T>(Enumerable.Empty<T>(), 0);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00003548 File Offset: 0x00001748
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x00003580 File Offset: 0x00001780
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000035B5 File Offset: 0x000017B5
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000035BD File Offset: 0x000017BD
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				this.SetField<int>(ref this._selectedIndex, value, "SelectedIndex");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000035D2 File Offset: 0x000017D2
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000035E0 File Offset: 0x000017E0
		public T SelectedValue
		{
			get
			{
				return base[this.SelectedIndex];
			}
			set
			{
				bool flag = this.SetField<T>(ref this._selectedValue, value, "SelectedValue");
				if (flag)
				{
					int index = base.IndexOf(value);
					bool flag2 = index == -1;
					if (!flag2)
					{
						this.SelectedIndex = index;
					}
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003620 File Offset: 0x00001820
		public Dropdown(IEnumerable<T> values, int selectedIndex) : base(values)
		{
			this.SelectedIndex = selectedIndex;
			bool flag = this.SelectedIndex != 0 && this.SelectedIndex >= base.Count;
			if (flag)
			{
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000365E File Offset: 0x0000185E
		[NullableContext(2)]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000367C File Offset: 0x0000187C
		private bool SetField<[Nullable(2)] TVal>(ref TVal field, TVal value, [Nullable(2)] [CallerMemberName] string propertyName = null)
		{
			bool flag = EqualityComparer<TVal>.Default.Equals(field, value);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				field = value;
				this.OnPropertyChanged(propertyName);
				result = true;
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000036B8 File Offset: 0x000018B8
		public bool Equals([Nullable(new byte[]
		{
			2,
			1
		})] Dropdown<T> x, [Nullable(new byte[]
		{
			2,
			1
		})] Dropdown<T> y)
		{
			int? num = (x != null) ? new int?(x.SelectedIndex) : null;
			int? num2 = (y != null) ? new int?(y.SelectedIndex) : null;
			return num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000371A File Offset: 0x0000191A
		public int GetHashCode(Dropdown<T> obj)
		{
			return obj.SelectedIndex;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003722 File Offset: 0x00001922
		public override int GetHashCode()
		{
			return this.GetHashCode(this);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000372C File Offset: 0x0000192C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			Dropdown<T> dropdown = obj as Dropdown<T>;
			bool flag = dropdown != null;
			bool result;
			if (flag)
			{
				result = this.Equals(this, dropdown);
			}
			else
			{
				result = (this == obj);
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000375C File Offset: 0x0000195C
		public object Clone()
		{
			return new Dropdown<T>(this, this.SelectedIndex);
		}

		// Token: 0x04000018 RID: 24
		private int _selectedIndex;

		// Token: 0x04000019 RID: 25
		[Nullable(2)]
		private T _selectedValue;
	}
}
