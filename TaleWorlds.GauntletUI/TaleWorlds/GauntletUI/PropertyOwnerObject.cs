using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200003A RID: 58
	public class PropertyOwnerObject
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000F7D6 File Offset: 0x0000D9D6
		protected void OnPropertyChanged<T>(T value, [CallerMemberName] string propertyName = null) where T : class
		{
			Action<PropertyOwnerObject, string, object> propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, propertyName, value);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		protected void OnPropertyChanged(int value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, int> action = this.intPropertyChanged;
			if (action == null)
			{
				return;
			}
			action(this, propertyName, value);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000F805 File Offset: 0x0000DA05
		protected void OnPropertyChanged(float value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, float> action = this.floatPropertyChanged;
			if (action == null)
			{
				return;
			}
			action(this, propertyName, value);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F81A File Offset: 0x0000DA1A
		protected void OnPropertyChanged(bool value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, bool> action = this.boolPropertyChanged;
			if (action == null)
			{
				return;
			}
			action(this, propertyName, value);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F82F File Offset: 0x0000DA2F
		protected void OnPropertyChanged(Vec2 value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, Vec2> vec2PropertyChanged = this.Vec2PropertyChanged;
			if (vec2PropertyChanged == null)
			{
				return;
			}
			vec2PropertyChanged(this, propertyName, value);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F844 File Offset: 0x0000DA44
		protected void OnPropertyChanged(Vector2 value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, Vector2> vector2PropertyChanged = this.Vector2PropertyChanged;
			if (vector2PropertyChanged == null)
			{
				return;
			}
			vector2PropertyChanged(this, propertyName, value);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000F859 File Offset: 0x0000DA59
		protected void OnPropertyChanged(double value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, double> action = this.doublePropertyChanged;
			if (action == null)
			{
				return;
			}
			action(this, propertyName, value);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F86E File Offset: 0x0000DA6E
		protected void OnPropertyChanged(uint value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, uint> action = this.uintPropertyChanged;
			if (action == null)
			{
				return;
			}
			action(this, propertyName, value);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F883 File Offset: 0x0000DA83
		protected void OnPropertyChanged(Color value, [CallerMemberName] string propertyName = null)
		{
			Action<PropertyOwnerObject, string, Color> colorPropertyChanged = this.ColorPropertyChanged;
			if (colorPropertyChanged == null)
			{
				return;
			}
			colorPropertyChanged(this, propertyName, value);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060003D8 RID: 984 RVA: 0x0000F898 File Offset: 0x0000DA98
		// (remove) Token: 0x060003D9 RID: 985 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public event Action<PropertyOwnerObject, string, object> PropertyChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060003DA RID: 986 RVA: 0x0000F908 File Offset: 0x0000DB08
		// (remove) Token: 0x060003DB RID: 987 RVA: 0x0000F940 File Offset: 0x0000DB40
		public event Action<PropertyOwnerObject, string, bool> boolPropertyChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060003DC RID: 988 RVA: 0x0000F978 File Offset: 0x0000DB78
		// (remove) Token: 0x060003DD RID: 989 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		public event Action<PropertyOwnerObject, string, int> intPropertyChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060003DE RID: 990 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		// (remove) Token: 0x060003DF RID: 991 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public event Action<PropertyOwnerObject, string, float> floatPropertyChanged;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060003E0 RID: 992 RVA: 0x0000FA58 File Offset: 0x0000DC58
		// (remove) Token: 0x060003E1 RID: 993 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public event Action<PropertyOwnerObject, string, Vec2> Vec2PropertyChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060003E2 RID: 994 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		// (remove) Token: 0x060003E3 RID: 995 RVA: 0x0000FB00 File Offset: 0x0000DD00
		public event Action<PropertyOwnerObject, string, Vector2> Vector2PropertyChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060003E4 RID: 996 RVA: 0x0000FB38 File Offset: 0x0000DD38
		// (remove) Token: 0x060003E5 RID: 997 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public event Action<PropertyOwnerObject, string, double> doublePropertyChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060003E6 RID: 998 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		// (remove) Token: 0x060003E7 RID: 999 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		public event Action<PropertyOwnerObject, string, uint> uintPropertyChanged;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060003E8 RID: 1000 RVA: 0x0000FC18 File Offset: 0x0000DE18
		// (remove) Token: 0x060003E9 RID: 1001 RVA: 0x0000FC50 File Offset: 0x0000DE50
		public event Action<PropertyOwnerObject, string, Color> ColorPropertyChanged;
	}
}
