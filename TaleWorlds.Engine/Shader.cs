using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000085 RID: 133
	public sealed class Shader : Resource
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x0000B1AC File Offset: 0x000093AC
		internal Shader(UIntPtr ptr) : base(ptr)
		{
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public static Shader GetFromResource(string shaderName)
		{
			return EngineApplicationInterface.IShader.GetFromResource(shaderName);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0000B1C2 File Offset: 0x000093C2
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IShader.GetName(base.Pointer);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public ulong GetMaterialShaderFlagMask(string flagName, bool showErrors = true)
		{
			return EngineApplicationInterface.IShader.GetMaterialShaderFlagMask(base.Pointer, flagName, showErrors);
		}
	}
}
