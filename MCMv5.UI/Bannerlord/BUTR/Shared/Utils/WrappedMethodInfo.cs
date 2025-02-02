using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedMethodInfo : MethodInfo
	{
		// Token: 0x06000225 RID: 549 RVA: 0x000098E9 File Offset: 0x00007AE9
		public WrappedMethodInfo(MethodInfo actualMethodInfo, object instance)
		{
			this._methodInfoImplementation = actualMethodInfo;
			this._instance = instance;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009901 File Offset: 0x00007B01
		public override MethodAttributes Attributes
		{
			get
			{
				return this._methodInfoImplementation.Attributes;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000990E File Offset: 0x00007B0E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this._methodInfoImplementation.CallingConvention;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000991B File Offset: 0x00007B1B
		public override bool ContainsGenericParameters
		{
			get
			{
				return this._methodInfoImplementation.ContainsGenericParameters;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009928 File Offset: 0x00007B28
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._methodInfoImplementation.CustomAttributes;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009935 File Offset: 0x00007B35
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._methodInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009942 File Offset: 0x00007B42
		public override bool IsGenericMethod
		{
			get
			{
				return this._methodInfoImplementation.IsGenericMethod;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000994F File Offset: 0x00007B4F
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this._methodInfoImplementation.IsGenericMethodDefinition;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000995C File Offset: 0x00007B5C
		public override bool IsSecurityCritical
		{
			get
			{
				return this._methodInfoImplementation.IsSecurityCritical;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009969 File Offset: 0x00007B69
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this._methodInfoImplementation.IsSecuritySafeCritical;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009976 File Offset: 0x00007B76
		public override bool IsSecurityTransparent
		{
			get
			{
				return this._methodInfoImplementation.IsSecurityTransparent;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009983 File Offset: 0x00007B83
		public override MemberTypes MemberType
		{
			get
			{
				return this._methodInfoImplementation.MemberType;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009990 File Offset: 0x00007B90
		public override int MetadataToken
		{
			get
			{
				return this._methodInfoImplementation.MetadataToken;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000999D File Offset: 0x00007B9D
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this._methodInfoImplementation.MethodHandle;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000099AA File Offset: 0x00007BAA
		public override MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this._methodInfoImplementation.MethodImplementationFlags;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000099B7 File Offset: 0x00007BB7
		public override Module Module
		{
			get
			{
				return this._methodInfoImplementation.Module;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000099C4 File Offset: 0x00007BC4
		public override string Name
		{
			get
			{
				return this._methodInfoImplementation.Name;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000099D1 File Offset: 0x00007BD1
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._methodInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000099DE File Offset: 0x00007BDE
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this._methodInfoImplementation.ReturnParameter;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000099EB File Offset: 0x00007BEB
		public override Type ReturnType
		{
			get
			{
				return this._methodInfoImplementation.ReturnType;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000099F8 File Offset: 0x00007BF8
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this._methodInfoImplementation.ReturnTypeCustomAttributes;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009A05 File Offset: 0x00007C05
		public override Delegate CreateDelegate(Type delegateType)
		{
			return this._methodInfoImplementation.CreateDelegate(delegateType);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009A13 File Offset: 0x00007C13
		public override Delegate CreateDelegate(Type delegateType, [Nullable(2)] object target)
		{
			return this._methodInfoImplementation.CreateDelegate(delegateType, target);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009A22 File Offset: 0x00007C22
		public override MethodInfo GetBaseDefinition()
		{
			return this._methodInfoImplementation.GetBaseDefinition();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009A2F File Offset: 0x00007C2F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._methodInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009A3D File Offset: 0x00007C3D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._methodInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009A4C File Offset: 0x00007C4C
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._methodInfoImplementation.GetCustomAttributesData();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009A59 File Offset: 0x00007C59
		public override Type[] GetGenericArguments()
		{
			return this._methodInfoImplementation.GetGenericArguments();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009A66 File Offset: 0x00007C66
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this._methodInfoImplementation.GetGenericMethodDefinition();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009A73 File Offset: 0x00007C73
		[NullableContext(2)]
		public override MethodBody GetMethodBody()
		{
			return this._methodInfoImplementation.GetMethodBody();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009A80 File Offset: 0x00007C80
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this._methodInfoImplementation.GetMethodImplementationFlags();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009A8D File Offset: 0x00007C8D
		public override ParameterInfo[] GetParameters()
		{
			return this._methodInfoImplementation.GetParameters();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009A9A File Offset: 0x00007C9A
		[NullableContext(2)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._methodInfoImplementation.Invoke(this._instance, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009AB3 File Offset: 0x00007CB3
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._methodInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009AC2 File Offset: 0x00007CC2
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return this._methodInfoImplementation.MakeGenericMethod(typeArguments);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009AD0 File Offset: 0x00007CD0
		[NullableContext(2)]
		public override string ToString()
		{
			return this._methodInfoImplementation.ToString();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009AE0 File Offset: 0x00007CE0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!true)
			{
			}
			WrappedMethodInfo proxy = obj as WrappedMethodInfo;
			bool result;
			if (proxy == null)
			{
				MethodInfo propertyInfo = obj as MethodInfo;
				if (propertyInfo == null)
				{
					result = this._methodInfoImplementation.Equals(obj);
				}
				else
				{
					result = this._methodInfoImplementation.Equals(propertyInfo);
				}
			}
			else
			{
				result = this._methodInfoImplementation.Equals(proxy._methodInfoImplementation);
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009B42 File Offset: 0x00007D42
		public override int GetHashCode()
		{
			return this._methodInfoImplementation.GetHashCode();
		}

		// Token: 0x0400009F RID: 159
		private readonly object _instance;

		// Token: 0x040000A0 RID: 160
		private readonly MethodInfo _methodInfoImplementation;
	}
}
