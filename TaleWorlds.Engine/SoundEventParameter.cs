using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008A RID: 138
	[EngineStruct("Managed_sound_event_parameter", false)]
	public struct SoundEventParameter
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x0000B967 File Offset: 0x00009B67
		public SoundEventParameter(string paramName, float value)
		{
			this.ParamName = paramName;
			this.Value = value;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000B977 File Offset: 0x00009B77
		public void Update(string paramName, float value)
		{
			this.ParamName = paramName;
			this.Value = value;
		}

		// Token: 0x040001B0 RID: 432
		[CustomEngineStructMemberData("str_id")]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string ParamName;

		// Token: 0x040001B1 RID: 433
		internal float Value;
	}
}
