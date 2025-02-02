using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Cecil;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x0200003C RID: 60
	internal class InlineSignature : ICallSiteGenerator
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000ADFA File Offset: 0x00008FFA
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000AE02 File Offset: 0x00009002
		public bool HasThis { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000AE0B File Offset: 0x0000900B
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000AE13 File Offset: 0x00009013
		public bool ExplicitThis { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000AE1C File Offset: 0x0000901C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000AE24 File Offset: 0x00009024
		public CallingConvention CallingConvention { get; set; } = CallingConvention.Winapi;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000AE2D File Offset: 0x0000902D
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000AE35 File Offset: 0x00009035
		public List<object> Parameters { get; set; } = new List<object>();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000AE3E File Offset: 0x0000903E
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000AE46 File Offset: 0x00009046
		public object ReturnType { get; set; } = typeof(void);

		// Token: 0x0600015D RID: 349 RVA: 0x0000AE50 File Offset: 0x00009050
		public override string ToString()
		{
			Type type = this.ReturnType as Type;
			string str;
			if (type == null)
			{
				object returnType = this.ReturnType;
				str = ((returnType != null) ? returnType.ToString() : null);
			}
			else
			{
				str = type.FullDescription();
			}
			return str + " (" + this.Parameters.Join(delegate(object p)
			{
				Type type2 = p as Type;
				if (type2 != null)
				{
					return type2.FullDescription();
				}
				if (p == null)
				{
					return null;
				}
				return p.ToString();
			}, ", ") + ")";
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AEC4 File Offset: 0x000090C4
		internal static TypeReference GetTypeReference(ModuleDefinition module, object param)
		{
			Type type = param as Type;
			TypeReference result;
			if (type == null)
			{
				InlineSignature inlineSignature = param as InlineSignature;
				if (inlineSignature == null)
				{
					InlineSignature.ModifierType modifierType = param as InlineSignature.ModifierType;
					if (modifierType == null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Unsupported inline signature parameter type: ");
						defaultInterpolatedStringHandler.AppendFormatted<object>(param);
						defaultInterpolatedStringHandler.AppendLiteral(" (");
						defaultInterpolatedStringHandler.AppendFormatted((param != null) ? param.GetType().FullDescription() : null);
						defaultInterpolatedStringHandler.AppendLiteral(")");
						throw new NotSupportedException(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					result = modifierType.ToTypeReference(module);
				}
				else
				{
					result = inlineSignature.ToFunctionPointer(module);
				}
			}
			else
			{
				result = module.ImportReference(type);
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AF6C File Offset: 0x0000916C
		Mono.Cecil.CallSite ICallSiteGenerator.ToCallSite(ModuleDefinition module)
		{
			Mono.Cecil.CallSite callSite = new Mono.Cecil.CallSite(InlineSignature.GetTypeReference(module, this.ReturnType))
			{
				HasThis = this.HasThis,
				ExplicitThis = this.ExplicitThis,
				CallingConvention = (MethodCallingConvention)((byte)this.CallingConvention - 1)
			};
			foreach (object param in this.Parameters)
			{
				callSite.Parameters.Add(new ParameterDefinition(InlineSignature.GetTypeReference(module, param)));
			}
			return callSite;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B00C File Offset: 0x0000920C
		private FunctionPointerType ToFunctionPointer(ModuleDefinition module)
		{
			FunctionPointerType functionPointerType = new FunctionPointerType
			{
				ReturnType = InlineSignature.GetTypeReference(module, this.ReturnType),
				HasThis = this.HasThis,
				ExplicitThis = this.ExplicitThis,
				CallingConvention = (MethodCallingConvention)((byte)this.CallingConvention - 1)
			};
			foreach (object param in this.Parameters)
			{
				functionPointerType.Parameters.Add(new ParameterDefinition(InlineSignature.GetTypeReference(module, param)));
			}
			return functionPointerType;
		}

		// Token: 0x0200008F RID: 143
		public class ModifierType
		{
			// Token: 0x060004C6 RID: 1222 RVA: 0x00016C5C File Offset: 0x00014E5C
			public override string ToString()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
				Type type = this.Type as Type;
				string value;
				if (type == null)
				{
					object type2 = this.Type;
					value = ((type2 != null) ? type2.ToString() : null);
				}
				else
				{
					value = type.FullDescription();
				}
				defaultInterpolatedStringHandler.AppendFormatted(value);
				defaultInterpolatedStringHandler.AppendLiteral(" mod");
				defaultInterpolatedStringHandler.AppendFormatted(this.IsOptional ? "opt" : "req");
				defaultInterpolatedStringHandler.AppendLiteral("(");
				Type modifier = this.Modifier;
				defaultInterpolatedStringHandler.AppendFormatted((modifier != null) ? modifier.FullDescription() : null);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x00016D04 File Offset: 0x00014F04
			internal TypeReference ToTypeReference(ModuleDefinition module)
			{
				if (this.IsOptional)
				{
					return new OptionalModifierType(module.ImportReference(this.Modifier), InlineSignature.GetTypeReference(module, this.Type));
				}
				return new RequiredModifierType(module.ImportReference(this.Modifier), InlineSignature.GetTypeReference(module, this.Type));
			}

			// Token: 0x040001A8 RID: 424
			public bool IsOptional;

			// Token: 0x040001A9 RID: 425
			public Type Modifier;

			// Token: 0x040001AA RID: 426
			public object Type;
		}
	}
}
