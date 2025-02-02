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
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedPropertyInfo : PropertyInfo, INotifyPropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600024B RID: 587 RVA: 0x00009B50 File Offset: 0x00007D50
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x00009B88 File Offset: 0x00007D88
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600024D RID: 589 RVA: 0x00009BBD File Offset: 0x00007DBD
		public WrappedPropertyInfo(PropertyInfo actualPropertyInfo, object instance)
		{
			this._propertyInfoImplementation = actualPropertyInfo;
			this._instance = instance;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009BD5 File Offset: 0x00007DD5
		public override PropertyAttributes Attributes
		{
			get
			{
				return this._propertyInfoImplementation.Attributes;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00009BE2 File Offset: 0x00007DE2
		public override bool CanRead
		{
			get
			{
				return this._propertyInfoImplementation.CanRead;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009BEF File Offset: 0x00007DEF
		public override bool CanWrite
		{
			get
			{
				return this._propertyInfoImplementation.CanWrite;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00009BFC File Offset: 0x00007DFC
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._propertyInfoImplementation.CustomAttributes;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00009C09 File Offset: 0x00007E09
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009C16 File Offset: 0x00007E16
		[Nullable(2)]
		public override MethodInfo GetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.GetMethod;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009C23 File Offset: 0x00007E23
		public override MemberTypes MemberType
		{
			get
			{
				return this._propertyInfoImplementation.MemberType;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009C30 File Offset: 0x00007E30
		public override int MetadataToken
		{
			get
			{
				return this._propertyInfoImplementation.MetadataToken;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009C3D File Offset: 0x00007E3D
		public override Module Module
		{
			get
			{
				return this._propertyInfoImplementation.Module;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009C4A File Offset: 0x00007E4A
		public override string Name
		{
			get
			{
				return this._propertyInfoImplementation.Name;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009C57 File Offset: 0x00007E57
		public override Type PropertyType
		{
			get
			{
				return this._propertyInfoImplementation.PropertyType;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00009C64 File Offset: 0x00007E64
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009C71 File Offset: 0x00007E71
		[Nullable(2)]
		public override MethodInfo SetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.SetMethod;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009C7E File Offset: 0x00007E7E
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return (from m in this._propertyInfoImplementation.GetAccessors(nonPublic)
			select new WrappedMethodInfo(m, this._instance)).Cast<MethodInfo>().ToArray<MethodInfo>();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009CA7 File Offset: 0x00007EA7
		[NullableContext(2)]
		public override object GetConstantValue()
		{
			return this._propertyInfoImplementation.GetConstantValue();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009CC3 File Offset: 0x00007EC3
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009CD1 File Offset: 0x00007ED1
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._propertyInfoImplementation.GetCustomAttributesData();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo getMethod = this._propertyInfoImplementation.GetGetMethod(nonPublic);
			return (getMethod == null) ? null : new WrappedMethodInfo(getMethod, this._instance);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009D11 File Offset: 0x00007F11
		public override ParameterInfo[] GetIndexParameters()
		{
			return this._propertyInfoImplementation.GetIndexParameters();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009D1E File Offset: 0x00007F1E
		public override Type[] GetOptionalCustomModifiers()
		{
			return this._propertyInfoImplementation.GetOptionalCustomModifiers();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009D2B File Offset: 0x00007F2B
		[NullableContext(2)]
		public override object GetRawConstantValue()
		{
			return this._propertyInfoImplementation.GetRawConstantValue();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009D38 File Offset: 0x00007F38
		public override Type[] GetRequiredCustomModifiers()
		{
			return this._propertyInfoImplementation.GetRequiredCustomModifiers();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009D48 File Offset: 0x00007F48
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo setMethod = this._propertyInfoImplementation.GetSetMethod(nonPublic);
			return (setMethod == null) ? null : new WrappedMethodInfo(setMethod, this._instance);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009D79 File Offset: 0x00007F79
		[NullableContext(2)]
		public override object GetValue(object obj, object[] index)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, index);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009D8D File Offset: 0x00007F8D
		[NullableContext(2)]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, invokeAttr, binder, index, culture);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009DA6 File Offset: 0x00007FA6
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009DB5 File Offset: 0x00007FB5
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

		// Token: 0x0600026A RID: 618 RVA: 0x00009DEF File Offset: 0x00007FEF
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

		// Token: 0x0600026B RID: 619 RVA: 0x00009E2F File Offset: 0x0000802F
		[NullableContext(2)]
		public override string ToString()
		{
			return this._propertyInfoImplementation.ToString();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009E3C File Offset: 0x0000803C
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

		// Token: 0x0600026D RID: 621 RVA: 0x00009E9E File Offset: 0x0000809E
		public override int GetHashCode()
		{
			return this._propertyInfoImplementation.GetHashCode();
		}

		// Token: 0x040000A2 RID: 162
		private readonly object _instance;

		// Token: 0x040000A3 RID: 163
		private readonly PropertyInfo _propertyInfoImplementation;
	}
}
