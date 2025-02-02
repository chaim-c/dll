using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace HarmonyLib
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class HarmonyException : Exception
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000A6CD File Offset: 0x000088CD
		internal HarmonyException()
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A6D5 File Offset: 0x000088D5
		internal HarmonyException(string message) : base(message)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A6DE File Offset: 0x000088DE
		internal HarmonyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A6E8 File Offset: 0x000088E8
		protected HarmonyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A6F5 File Offset: 0x000088F5
		internal HarmonyException(Exception innerException, Dictionary<int, CodeInstruction> instructions, int errorOffset) : base("IL Compile Error", innerException)
		{
			this.instructions = instructions;
			this.errorOffset = errorOffset;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A714 File Offset: 0x00008914
		internal static Exception Create(Exception ex, Dictionary<int, CodeInstruction> finalInstructions)
		{
			Match match = Regex.Match(ex.Message.TrimEnd(Array.Empty<char>()), "Reason: Invalid IL code in.+: IL_(\\d{4}): (.+)$");
			if (!match.Success)
			{
				return ex;
			}
			int num = int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
			Regex.Replace(match.Groups[2].Value, " {2,}", " ");
			HarmonyException ex2 = ex as HarmonyException;
			if (ex2 != null)
			{
				ex2.instructions = finalInstructions;
				ex2.errorOffset = num;
				return ex2;
			}
			return new HarmonyException(ex, finalInstructions, num);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A7A5 File Offset: 0x000089A5
		public List<KeyValuePair<int, CodeInstruction>> GetInstructionsWithOffsets()
		{
			List<KeyValuePair<int, CodeInstruction>> list = new List<KeyValuePair<int, CodeInstruction>>();
			list.AddRange(from ins in this.instructions
			orderby ins.Key
			select ins);
			return list;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A7DC File Offset: 0x000089DC
		public List<CodeInstruction> GetInstructions()
		{
			return (from ins in this.instructions
			orderby ins.Key
			select ins.Value).ToList<CodeInstruction>();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A83C File Offset: 0x00008A3C
		public int GetErrorOffset()
		{
			return this.errorOffset;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A844 File Offset: 0x00008A44
		public int GetErrorIndex()
		{
			CodeInstruction item;
			if (this.instructions.TryGetValue(this.errorOffset, out item))
			{
				return this.GetInstructions().IndexOf(item);
			}
			return -1;
		}

		// Token: 0x04000089 RID: 137
		private Dictionary<int, CodeInstruction> instructions;

		// Token: 0x0400008A RID: 138
		private int errorOffset;
	}
}
