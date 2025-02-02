using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000020 RID: 32
	public interface ILanguage
	{
		// Token: 0x0600012E RID: 302
		IEnumerable<char> GetForbiddenStartOfLineCharacters();

		// Token: 0x0600012F RID: 303
		bool IsCharacterForbiddenAtStartOfLine(char character);

		// Token: 0x06000130 RID: 304
		IEnumerable<char> GetForbiddenEndOfLineCharacters();

		// Token: 0x06000131 RID: 305
		bool IsCharacterForbiddenAtEndOfLine(char character);

		// Token: 0x06000132 RID: 306
		string GetLanguageID();

		// Token: 0x06000133 RID: 307
		string GetDefaultFontName();

		// Token: 0x06000134 RID: 308
		Font GetDefaultFont();

		// Token: 0x06000135 RID: 309
		char GetLineSeperatorChar();

		// Token: 0x06000136 RID: 310
		bool DoesLanguageRequireSpaceForNewline();

		// Token: 0x06000137 RID: 311
		bool FontMapHasKey(string keyFontName);

		// Token: 0x06000138 RID: 312
		Font GetMappedFont(string keyFontName);
	}
}
