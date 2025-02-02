using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Bannerlord.BUTR.Shared.Utils;

namespace MCM.UI.Utils
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedPropertyInfo : PropertyInfo
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003872 File Offset: 0x00001A72
		public WrappedPropertyInfo(PropertyInfo actualPropertyInfo, object instance, [Nullable(2)] Action onSet = null)
		{
			this._propertyInfoImplementation = actualPropertyInfo;
			this._instance = instance;
			this._onSet = onSet;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003891 File Offset: 0x00001A91
		public override PropertyAttributes Attributes
		{
			get
			{
				return this._propertyInfoImplementation.Attributes;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000389E File Offset: 0x00001A9E
		public override bool CanRead
		{
			get
			{
				return this._propertyInfoImplementation.CanRead;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000038AB File Offset: 0x00001AAB
		public override bool CanWrite
		{
			get
			{
				return this._propertyInfoImplementation.CanWrite;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000038B8 File Offset: 0x00001AB8
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._propertyInfoImplementation.CustomAttributes;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000038C5 File Offset: 0x00001AC5
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000038D2 File Offset: 0x00001AD2
		[Nullable(2)]
		public override MethodInfo GetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.GetMethod;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000038DF File Offset: 0x00001ADF
		public override MemberTypes MemberType
		{
			get
			{
				return this._propertyInfoImplementation.MemberType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000038EC File Offset: 0x00001AEC
		public override int MetadataToken
		{
			get
			{
				return this._propertyInfoImplementation.MetadataToken;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000038F9 File Offset: 0x00001AF9
		public override Module Module
		{
			get
			{
				return this._propertyInfoImplementation.Module;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003906 File Offset: 0x00001B06
		public override string Name
		{
			get
			{
				return this._propertyInfoImplementation.Name;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003913 File Offset: 0x00001B13
		public override Type PropertyType
		{
			get
			{
				return this._propertyInfoImplementation.PropertyType;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003920 File Offset: 0x00001B20
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000392D File Offset: 0x00001B2D
		[Nullable(2)]
		public override MethodInfo SetMethod
		{
			[NullableContext(2)]
			get
			{
				return this._propertyInfoImplementation.SetMethod;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000393A File Offset: 0x00001B3A
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return (from m in this._propertyInfoImplementation.GetAccessors(nonPublic)
			select new WrappedMethodInfo(m, this._instance)).Cast<MethodInfo>().ToArray<MethodInfo>();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003963 File Offset: 0x00001B63
		[NullableContext(2)]
		public override object GetConstantValue()
		{
			return this._propertyInfoImplementation.GetConstantValue();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003970 File Offset: 0x00001B70
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000397F File Offset: 0x00001B7F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._propertyInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000398D File Offset: 0x00001B8D
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._propertyInfoImplementation.GetCustomAttributesData();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000399C File Offset: 0x00001B9C
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo getMethod = this._propertyInfoImplementation.GetGetMethod(nonPublic);
			return (getMethod == null) ? null : new WrappedMethodInfo(getMethod, this._instance);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000039CD File Offset: 0x00001BCD
		public override ParameterInfo[] GetIndexParameters()
		{
			return this._propertyInfoImplementation.GetIndexParameters();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039DA File Offset: 0x00001BDA
		public override Type[] GetOptionalCustomModifiers()
		{
			return this._propertyInfoImplementation.GetOptionalCustomModifiers();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039E7 File Offset: 0x00001BE7
		[NullableContext(2)]
		public override object GetRawConstantValue()
		{
			return this._propertyInfoImplementation.GetRawConstantValue();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000039F4 File Offset: 0x00001BF4
		public override Type[] GetRequiredCustomModifiers()
		{
			return this._propertyInfoImplementation.GetRequiredCustomModifiers();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003A04 File Offset: 0x00001C04
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo setMethod = this._propertyInfoImplementation.GetSetMethod(nonPublic);
			return (setMethod == null) ? null : new WrappedMethodInfo(setMethod, this._instance);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003A35 File Offset: 0x00001C35
		[NullableContext(2)]
		public override object GetValue(object obj, object[] index)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, index);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003A49 File Offset: 0x00001C49
		[NullableContext(2)]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			return this._propertyInfoImplementation.GetValue(this._instance, invokeAttr, binder, index, culture);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003A62 File Offset: 0x00001C62
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._propertyInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003A71 File Offset: 0x00001C71
		[NullableContext(2)]
		public override void SetValue(object obj, object value, object[] index)
		{
			this._propertyInfoImplementation.SetValue(this._instance, value, index);
			Action onSet = this._onSet;
			if (onSet != null)
			{
				onSet();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003A9A File Offset: 0x00001C9A
		[NullableContext(2)]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, [Nullable(1)] CultureInfo culture)
		{
			this._propertyInfoImplementation.SetValue(this._instance, value, invokeAttr, binder, index, culture);
			Action onSet = this._onSet;
			if (onSet != null)
			{
				onSet();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public override string ToString()
		{
			return this._propertyInfoImplementation.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003AD8 File Offset: 0x00001CD8
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

		// Token: 0x06000082 RID: 130 RVA: 0x00003B3A File Offset: 0x00001D3A
		public override int GetHashCode()
		{
			return this._propertyInfoImplementation.GetHashCode();
		}

		// Token: 0x04000021 RID: 33
		private readonly object _instance;

		// Token: 0x04000022 RID: 34
		private readonly PropertyInfo _propertyInfoImplementation;

		// Token: 0x04000023 RID: 35
		[Nullable(2)]
		private readonly Action _onSet;
	}
}
