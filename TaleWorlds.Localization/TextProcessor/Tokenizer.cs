﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TaleWorlds.Library;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x02000030 RID: 48
	internal class Tokenizer
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public Tokenizer()
		{
			this._tokenDefinitions = new TokenDefinition[]
			{
				new TokenDefinition(TokenType.ConditionSeperator, "{\\?}", 1),
				new TokenDefinition(TokenType.ConditionStarter, "{\\?", 1),
				new TokenDefinition(TokenType.ConditionFinalizer, "{\\\\\\?}", 1),
				new TokenDefinition(TokenType.FieldStarter, "{@", 1),
				new TokenDefinition(TokenType.FieldFinalizer, "{\\\\@}", 1),
				new TokenDefinition(TokenType.SelectionSeperator, "{#}", 1),
				new TokenDefinition(TokenType.SelectionFinalizer, "{\\\\#}", 1),
				new TokenDefinition(TokenType.SelectionStarter, "{#", 1),
				new TokenDefinition(TokenType.Seperator, "{\\:}", 1),
				new TokenDefinition(TokenType.ConditionFollowUp, "{\\:\\?", 1),
				new TokenDefinition(TokenType.LanguageMarker, "{\\.[a-zA-Z_^%][a-zA-Z\\d_]*}", 1),
				new TokenDefinition(TokenType.textId, "{=[a-zA-Z\\d_\\!\\*][a-zA-Z\\d_\\.]*}", 1),
				new TokenDefinition(TokenType.CloseBraces, "}", 1),
				new TokenDefinition(TokenType.OpenBraces, "{", 1),
				new TokenDefinition(TokenType.Minus, "\\-", 1),
				new TokenDefinition(TokenType.Multiply, "\\*", 1),
				new TokenDefinition(TokenType.Plus, "\\+", 1),
				new TokenDefinition(TokenType.Divide, "\\/", 1),
				new TokenDefinition(TokenType.Comma, ",", 1),
				new TokenDefinition(TokenType.CloseParenthesis, "\\)", 1),
				new TokenDefinition(TokenType.OpenParenthesis, "\\(", 1),
				new TokenDefinition(TokenType.CloseBrackets, "\\]", 1),
				new TokenDefinition(TokenType.OpenBrackets, "\\[", 1),
				new TokenDefinition(TokenType.ParameterWithMarkerOccurance, "\\$\\d+\\!.[a-zA-Z_][a-zA-Z\\d_]*", 1),
				new TokenDefinition(TokenType.ParameterWithMultipleMarkerOccurances, "\\$\\d+\\!\\.\\[([a-zA-Z]*)\\,([a-zA-Z]*\\,)*([a-zA-Z]+)\\]", 1),
				new TokenDefinition(TokenType.ParameterWithAttribute, "\\$\\d+\\.[a-zA-Z_][a-zA-Z\\d_]*", 1),
				new TokenDefinition(TokenType.StartsWith, "\\$\\d+\\([a-zA-Z_][a-zA-Z\\d_]*\\)", 1),
				new TokenDefinition(TokenType.StartsWith, "\\$\\d+\\(([a-zA-Z\\d_])*(,([a-zA-Z\\d_])*)*\\)", 1),
				new TokenDefinition(TokenType.FunctionParam, "\\$\\d+", 2),
				new TokenDefinition(TokenType.Number, "\\d+", 2),
				new TokenDefinition(TokenType.Match, "match", 1),
				new TokenDefinition(TokenType.QualifiedIdentifier, "[a-zA-Z_][a-zA-Z\\d_]*\\.[a-zA-Z_][a-zA-Z\\d_]*", 1),
				new TokenDefinition(TokenType.FunctionIdentifier, "[a-zA-Z_][a-zA-Z\\d_]*\\(", 1),
				new TokenDefinition(TokenType.Identifier, "[a-zA-Z_][a-zA-Z\\d_]*", 1),
				new TokenDefinition(TokenType.MarkerOccuranceIdentifier, "\\!.[a-zA-Z_][a-zA-Z\\d_]*", 1),
				new TokenDefinition(TokenType.Equals, "==", 1),
				new TokenDefinition(TokenType.NotEquals, "!=", 1),
				new TokenDefinition(TokenType.GreaterOrEqual, ">=", 1),
				new TokenDefinition(TokenType.LessOrEqual, "<=", 1),
				new TokenDefinition(TokenType.GreaterThan, ">", 1),
				new TokenDefinition(TokenType.LessThan, "<", 1),
				new TokenDefinition(TokenType.And, "and", 1),
				new TokenDefinition(TokenType.Or, "or", 1),
				new TokenDefinition(TokenType.Not, "not", 1)
			};
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000070AC File Offset: 0x000052AC
		public List<MBTextToken> Tokenize(string text)
		{
			List<MBTextToken> list = new List<MBTextToken>(2);
			this.FindTokenMatchesAndText(text, list);
			list.Add(new MBTextToken(TokenType.SequenceTerminator));
			return list;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000070D8 File Offset: 0x000052D8
		private void FindTokenMatchesAndText(string text, List<MBTextToken> mbTokenMatches)
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "FindTokenMatchesAndText");
			int i = 0;
			while (i < text.Length)
			{
				if (text[i] == '{')
				{
					if (mbstringBuilder.Length > 0)
					{
						string value = mbstringBuilder.ToStringAndRelease();
						mbstringBuilder.Initialize(16, "FindTokenMatchesAndText");
						mbTokenMatches.Add(new MBTextToken(TokenType.Text, value));
					}
					int num = this.FindExpressionEnd(text, i + 1);
					if (!this.FindTokenMatches(text, i, num, mbTokenMatches))
					{
						mbTokenMatches.Clear();
						string value2 = mbstringBuilder.ToStringAndRelease();
						mbTokenMatches.Add(new MBTextToken(TokenType.Text, value2));
						return;
					}
					i = num;
				}
				else
				{
					mbstringBuilder.Append(text[i]);
					i++;
				}
			}
			string text2 = mbstringBuilder.ToStringAndRelease();
			if (text2.Length > 0)
			{
				mbTokenMatches.Add(new MBTextToken(TokenType.Text, text2));
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000071B8 File Offset: 0x000053B8
		private int FindExpressionEnd(string text, int startIndex)
		{
			int num = startIndex;
			int num2 = 1;
			while (num < text.Length && num2 > 0)
			{
				char c = text[num];
				if (c == '{')
				{
					num2++;
				}
				else if (c == '}')
				{
					num2--;
				}
				num++;
			}
			return num;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000071FC File Offset: 0x000053FC
		private bool FindTokenMatches(string text, int beginIndex, int endIndex, List<MBTextToken> mbTokenMatches)
		{
			int num = this._tokenDefinitions.Length;
			int i = beginIndex;
			while (i < endIndex)
			{
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					TokenDefinition tokenDefinition = this._tokenDefinitions[j];
					Match match = tokenDefinition.CheckMatch(text, i);
					if (match != null)
					{
						int num2 = match.Index + match.Length;
						if (num2 != i)
						{
							mbTokenMatches.Add(new MBTextToken(tokenDefinition.TokenType, match.Value));
							i = num2;
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					MBTextManager.ThrowLocalizationError(string.Concat(new object[]
					{
						"Unexpected token at position ",
						i,
						" in:",
						text
					}));
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000A7 RID: 167
		private readonly TokenDefinition[] _tokenDefinitions;
	}
}
