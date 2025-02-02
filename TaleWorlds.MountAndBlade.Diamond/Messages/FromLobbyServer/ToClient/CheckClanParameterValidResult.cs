using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class CheckClanParameterValidResult : FunctionResult
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000222B File Offset: 0x0000042B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002233 File Offset: 0x00000433
		[JsonProperty]
		public bool IsValid { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002244 File Offset: 0x00000444
		[JsonProperty]
		public StringValidationError Error { get; private set; }

		// Token: 0x06000033 RID: 51 RVA: 0x0000224D File Offset: 0x0000044D
		public CheckClanParameterValidResult()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002255 File Offset: 0x00000455
		public CheckClanParameterValidResult(bool isValid, StringValidationError error)
		{
			this.IsValid = isValid;
			this.Error = error;
		}
	}
}
