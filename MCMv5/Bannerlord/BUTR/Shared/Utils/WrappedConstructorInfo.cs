using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200012A RID: 298
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedConstructorInfo : ConstructorInfo
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x000174E3 File Offset: 0x000156E3
		public WrappedConstructorInfo(ConstructorInfo actualConstructorInfo, object[] args)
		{
			this._constructorInfoImplementation = actualConstructorInfo;
			this._args = args;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000174FB File Offset: 0x000156FB
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._constructorInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00017509 File Offset: 0x00015709
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._constructorInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00017518 File Offset: 0x00015718
		public override ParameterInfo[] GetParameters()
		{
			return this._constructorInfoImplementation.GetParameters();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00017525 File Offset: 0x00015725
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this._constructorInfoImplementation.GetMethodImplementationFlags();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00017532 File Offset: 0x00015732
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._constructorInfoImplementation.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00017546 File Offset: 0x00015746
		public override string Name
		{
			get
			{
				return this._constructorInfoImplementation.Name;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00017553 File Offset: 0x00015753
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._constructorInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00017560 File Offset: 0x00015760
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._constructorInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001756D File Offset: 0x0001576D
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this._constructorInfoImplementation.MethodHandle;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001757A File Offset: 0x0001577A
		public override MethodAttributes Attributes
		{
			get
			{
				return this._constructorInfoImplementation.Attributes;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00017587 File Offset: 0x00015787
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._constructorInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00017596 File Offset: 0x00015796
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._constructorInfoImplementation.Invoke(invokeAttr, binder, this._args, culture);
		}

		// Token: 0x0400022A RID: 554
		private readonly object[] _args;

		// Token: 0x0400022B RID: 555
		private readonly ConstructorInfo _constructorInfoImplementation;
	}
}
