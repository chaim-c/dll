using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200012C RID: 300
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedPropertyInfo : PropertyInfo, INotifyPropertyChanged
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000759 RID: 1881 RVA: 0x00017814 File Offset: 0x00015A14
		// (remove) Token: 0x0600075A RID: 1882 RVA: 0x0001784C File Offset: 0x00015A4C
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600075B RID: 1883 RVA: 0x00017881 File Offset: 0x00015A81
		public WrappedPropertyInfo(PropertyInfo actualPropertyInfo, object instance)
		{
			this._propertyInfoImplementation = actualPropertyInfo;
			this._instance = instance;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00017899 File Offset: 0x00015A99
		public override PropertyAttributes Attributes
		{
			get
			{
				return this._propertyInfoImplementation.Attributes;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000178A6 File Offset: 0x00015AA6
		public override bool CanRead
		{
			get
			{
				return this._propertyInfoImplementation.CanRead;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x000178B3 File Offset: 0x00015AB3
		public override bool CanWrite
		{
			get
			{
				return this._propertyInfoImplementation.CanWrite;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000178C0 File Offset: 0x00015AC0
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._propertyInfoImplementation.CustomAttributes;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x000178CD File Offset: 0x00015ACD
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000178DA File Offset: 0x00015ADA
		[Nullable(2)]
		public override MethodInfo GetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.GetMethod;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000178E7 File Offset: 0x00015AE7
		public override MemberTypes MemberType
		{
			get
			{
				return this._propertyInfoImplementation.MemberType;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000178F4 File Offset: 0x00015AF4
		public override int MetadataToken
		{
			get
			{
				return this._propertyInfoImplementation.MetadataToken;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00017901 File Offset: 0x00015B01
		public override Module Module
		{
			get
			{
				return this._propertyInfoImplementation.Module;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001790E File Offset: 0x00015B0E
		public override string Name
		{
			get
			{
				return this._propertyInfoImplementation.Name;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001791B File Offset: 0x00015B1B
		public override Type PropertyType
		{
			get
			{
				return this._propertyInfoImplementation.PropertyType;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00017928 File Offset: 0x00015B28
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00017935 File Offset: 0x00015B35
		[Nullable(2)]
		public override MethodInfo SetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.SetMethod;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00017942 File Offset: 0x00015B42
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return (from m in this._propertyInfoImplementation.GetAccessors(nonPublic)
			select new WrappedMethodInfo(m, this._instance)).Cast<MethodInfo>().ToArray<MethodInfo>();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001796B File Offset: 0x00015B6B
		[NullableContext(2)]
		public override object GetConstantValue()
		{
			return this._propertyInfoImplementation.GetConstantValue();
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00017978 File Offset: 0x00015B78
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00017987 File Offset: 0x00015B87
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00017995 File Offset: 0x00015B95
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._propertyInfoImplementation.GetCustomAttributesData();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000179A4 File Offset: 0x00015BA4
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo getMethod = this._propertyInfoImplementation.GetGetMethod(nonPublic);
			return (getMethod == null) ? null : new WrappedMethodInfo(getMethod, this._instance);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000179D5 File Offset: 0x00015BD5
		public override ParameterInfo[] GetIndexParameters()
		{
			return this._propertyInfoImplementation.GetIndexParameters();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000179E2 File Offset: 0x00015BE2
		public override Type[] GetOptionalCustomModifiers()
		{
			return this._propertyInfoImplementation.GetOptionalCustomModifiers();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000179EF File Offset: 0x00015BEF
		[NullableContext(2)]
		public override object GetRawConstantValue()
		{
			return this._propertyInfoImplementation.GetRawConstantValue();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000179FC File Offset: 0x00015BFC
		public override Type[] GetRequiredCustomModifiers()
		{
			return this._propertyInfoImplementation.GetRequiredCustomModifiers();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00017A0C File Offset: 0x00015C0C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo setMethod = this._propertyInfoImplementation.GetSetMethod(nonPublic);
			return (setMethod == null) ? null : new WrappedMethodInfo(setMethod, this._instance);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00017A3D File Offset: 0x00015C3D
		[NullableContext(2)]
		public override object GetValue(object obj, object[] index)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, index);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00017A51 File Offset: 0x00015C51
		[NullableContext(2)]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, invokeAttr, binder, index, culture);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00017A6A File Offset: 0x00015C6A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00017A79 File Offset: 0x00015C79
		[NullableContext(2)]
		public override void SetValue(object obj, object value, object[] index)
		{
			this._propertyInfoImplementation.SetValue(this._instance, value, index);
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(this._propertyInfoImplementation.Name));
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00017AB3 File Offset: 0x00015CB3
		[NullableContext(2)]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			this._propertyInfoImplementation.SetValue(this._instance, value, invokeAttr, binder, index, culture);
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(this._propertyInfoImplementation.Name));
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00017AF3 File Offset: 0x00015CF3
		[NullableContext(2)]
		public override string ToString()
		{
			return this._propertyInfoImplementation.ToString();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00017B00 File Offset: 0x00015D00
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!true)
			{
			}
			WrappedPropertyInfo proxy = obj as WrappedPropertyInfo;
			bool result;
			if (proxy == null)
			{
				PropertyInfo propertyInfo = obj as PropertyInfo;
				if (propertyInfo == null)
				{
					result = this._propertyInfoImplementation.Equals(obj);
				}
				else
				{
					result = this._propertyInfoImplementation.Equals(propertyInfo);
				}
			}
			else
			{
				result = this._propertyInfoImplementation.Equals(proxy._propertyInfoImplementation);
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00017B62 File Offset: 0x00015D62
		public override int GetHashCode()
		{
			return this._propertyInfoImplementation.GetHashCode();
		}

		// Token: 0x0400022F RID: 559
		private readonly object _instance;

		// Token: 0x04000230 RID: 560
		private readonly PropertyInfo _propertyInfoImplementation;
	}
}
