using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator
{
	// Token: 0x0200006D RID: 109
	public class FaceGenPropertyVM : ViewModel
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00020C27 File Offset: 0x0001EE27
		public int KeyTimePoint { get; }

		// Token: 0x06000887 RID: 2183 RVA: 0x00020C30 File Offset: 0x0001EE30
		public FaceGenPropertyVM(int keyNo, double min, double max, TextObject name, int keyTimePoint, int tabId, double value, Action<int, float, bool, bool> updateFace, Action addCommand, Action resetSliderPrevValuesCommand, bool isEnabled = true, bool isDiscrete = false)
		{
			this._calledFromInit = true;
			this._updateFace = updateFace;
			this._addCommand = addCommand;
			this._nameObj = name;
			this._resetSliderPrevValuesCommand = resetSliderPrevValuesCommand;
			this.KeyNo = keyNo;
			this.Min = (float)min;
			this.Max = (float)max;
			this.KeyTimePoint = keyTimePoint;
			this.TabID = tabId;
			this._initialValue = (float)value;
			this.Value = (float)value;
			this.PrevValue = -1.0;
			this.IsEnabled = isEnabled;
			this.IsDiscrete = isDiscrete;
			this._calledFromInit = false;
			this.RefreshValues();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public void Reset()
		{
			this._updateOnValueChange = false;
			this.Value = this._initialValue;
			this._updateOnValueChange = true;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00020D08 File Offset: 0x0001EF08
		public void Randomize()
		{
			this._updateOnValueChange = false;
			float num = 0.5f * (MBRandom.RandomFloat + MBRandom.RandomFloat);
			this.Value = num * (this.Max - this.Min) + this.Min;
			this._updateOnValueChange = true;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00020D51 File Offset: 0x0001EF51
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._nameObj.ToString();
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00020D6A File Offset: 0x0001EF6A
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00020D72 File Offset: 0x0001EF72
		[DataSourceProperty]
		public float Min
		{
			get
			{
				return this._min;
			}
			set
			{
				if (value != this._min)
				{
					this._min = value;
					base.OnPropertyChangedWithValue(value, "Min");
				}
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00020D90 File Offset: 0x0001EF90
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00020D98 File Offset: 0x0001EF98
		[DataSourceProperty]
		public int TabID
		{
			get
			{
				return this._tabID;
			}
			set
			{
				if (value != this._tabID)
				{
					this._tabID = value;
					base.OnPropertyChangedWithValue(value, "TabID");
				}
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00020DB6 File Offset: 0x0001EFB6
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00020DBE File Offset: 0x0001EFBE
		[DataSourceProperty]
		public float Max
		{
			get
			{
				return this._max;
			}
			set
			{
				if (value != this._max)
				{
					this._max = value;
					base.OnPropertyChangedWithValue(value, "Max");
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00020DDC File Offset: 0x0001EFDC
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		[DataSourceProperty]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((double)MathF.Abs(value - this._value) > ((this.KeyNo == -16) ? 0.006000000052154064 : 0.06))
				{
					if (!this._calledFromInit && this.PrevValue < 0.0 && this._updateOnValueChange)
					{
						this._addCommand();
					}
					this._resetSliderPrevValuesCommand();
					if (this.KeyNo >= 0)
					{
						this.PrevValue = (double)this._value;
					}
					this._value = value;
					base.OnPropertyChangedWithValue(value, "Value");
					Action<int, float, bool, bool> updateFace = this._updateFace;
					if (updateFace == null)
					{
						return;
					}
					updateFace(this.KeyNo, value, this._calledFromInit, this._updateOnValueChange);
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00020EA5 File Offset: 0x0001F0A5
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00020EAD File Offset: 0x0001F0AD
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00020EF6 File Offset: 0x0001F0F6
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00020EFE File Offset: 0x0001F0FE
		[DataSourceProperty]
		public bool IsDiscrete
		{
			get
			{
				return this._isDiscrete;
			}
			set
			{
				if (value != this._isDiscrete)
				{
					this._isDiscrete = value;
					base.OnPropertyChangedWithValue(value, "IsDiscrete");
				}
			}
		}

		// Token: 0x040003F4 RID: 1012
		public int KeyNo;

		// Token: 0x040003F5 RID: 1013
		public double PrevValue = -1.0;

		// Token: 0x040003F6 RID: 1014
		private bool _updateOnValueChange = true;

		// Token: 0x040003F7 RID: 1015
		private readonly TextObject _nameObj;

		// Token: 0x040003F8 RID: 1016
		private readonly Action<int, float, bool, bool> _updateFace;

		// Token: 0x040003F9 RID: 1017
		private readonly Action _resetSliderPrevValuesCommand;

		// Token: 0x040003FA RID: 1018
		private readonly Action _addCommand;

		// Token: 0x040003FB RID: 1019
		private readonly bool _calledFromInit;

		// Token: 0x040003FC RID: 1020
		private readonly float _initialValue;

		// Token: 0x040003FD RID: 1021
		private int _tabID = -1;

		// Token: 0x040003FE RID: 1022
		private string _name;

		// Token: 0x040003FF RID: 1023
		private float _value;

		// Token: 0x04000400 RID: 1024
		private float _max;

		// Token: 0x04000401 RID: 1025
		private float _min;

		// Token: 0x04000402 RID: 1026
		private bool _isEnabled;

		// Token: 0x04000403 RID: 1027
		private bool _isDiscrete;
	}
}
