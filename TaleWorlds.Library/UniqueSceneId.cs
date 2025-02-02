using System;
using System.Text.RegularExpressions;

namespace TaleWorlds.Library
{
	// Token: 0x0200009A RID: 154
	public class UniqueSceneId
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00011250 File Offset: 0x0000F450
		public string UniqueToken { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00011258 File Offset: 0x0000F458
		public string Revision { get; }

		// Token: 0x06000538 RID: 1336 RVA: 0x00011260 File Offset: 0x0000F460
		public UniqueSceneId(string uniqueToken, string revision)
		{
			if (uniqueToken == null)
			{
				throw new ArgumentNullException("uniqueToken");
			}
			this.UniqueToken = uniqueToken;
			if (revision == null)
			{
				throw new ArgumentNullException("revision");
			}
			this.Revision = revision;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00011294 File Offset: 0x0000F494
		public string Serialize()
		{
			return string.Format(":ut[{0}]{1}:rev[{2}]{3}", new object[]
			{
				this.UniqueToken.Length,
				this.UniqueToken,
				this.Revision.Length,
				this.Revision
			});
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000112EC File Offset: 0x0000F4EC
		public static bool TryParse(string uniqueMapId, out UniqueSceneId identifiers)
		{
			identifiers = null;
			if (uniqueMapId == null)
			{
				return false;
			}
			Match match = UniqueSceneId.IdentifierPattern.Value.Match(uniqueMapId);
			if (match.Success)
			{
				identifiers = new UniqueSceneId(match.Groups[1].Value, match.Groups[2].Value);
				return true;
			}
			return false;
		}

		// Token: 0x04000189 RID: 393
		private static readonly Lazy<Regex> IdentifierPattern = new Lazy<Regex>(() => new Regex("^:ut\\[\\d+\\](.*):rev\\[\\d+\\](.*)$", RegexOptions.Compiled));
	}
}
