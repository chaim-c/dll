using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TaleWorlds.Library;
using TaleWorlds.Localization.Expressions;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002D RID: 45
	public class TextProcessingContext
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006397 File Offset: 0x00004597
		internal void SetTextVariable(string variableName, TextObject data)
		{
			this._variables[variableName] = data;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000063A8 File Offset: 0x000045A8
		internal TextObject GetRawTextVariable(string variableName, TextObject parent)
		{
			TextObject result = null;
			if (parent != null && parent.GetVariableValue(variableName, out result))
			{
				return result;
			}
			if (!this._variables.ContainsKey(variableName))
			{
				return TextObject.Empty;
			}
			return this._variables[variableName];
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000063E8 File Offset: 0x000045E8
		internal MultiStatement GetVariableValue(string variableName, TextObject parent)
		{
			TextObject textObject = null;
			MBTextModel mbtextModel = null;
			if (parent == null || !parent.GetVariableValue(variableName, out textObject))
			{
				this._variables.TryGetValue(variableName, out textObject);
			}
			if (textObject != null)
			{
				mbtextModel = MBTextParser.Parse(MBTextManager.Tokenizer.Tokenize(textObject.ToStringWithoutClear()));
			}
			if (mbtextModel == null)
			{
				return null;
			}
			if (mbtextModel.RootExpressions.Count == 1 && mbtextModel.RootExpressions[0] is MultiStatement)
			{
				return new MultiStatement((mbtextModel.RootExpressions[0] as MultiStatement).SubStatements);
			}
			return new MultiStatement(mbtextModel.RootExpressions);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000647C File Offset: 0x0000467C
		internal ValueTuple<TextObject, bool> GetVariableValueAsTextObject(string variableName, TextObject parent)
		{
			TextObject textObject = null;
			if (parent != null)
			{
				if (parent.GetVariableValue(variableName, out textObject))
				{
					return new ValueTuple<TextObject, bool>(textObject, true);
				}
				textObject = TextProcessingContext.FindNestedFieldValue(MBTextManager.GetLocalizedText(parent.Value), variableName, parent);
				if (textObject != null && textObject.Length > 0)
				{
					return new ValueTuple<TextObject, bool>(textObject, true);
				}
			}
			if (textObject != null && textObject.Length != 0)
			{
				return new ValueTuple<TextObject, bool>(textObject, true);
			}
			return new ValueTuple<TextObject, bool>(new TextObject("{=!}ERROR: " + variableName + " variable has not been set before.", null), false);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000064F8 File Offset: 0x000046F8
		internal MultiStatement GetArrayAccess(string variableName, int index)
		{
			string key = variableName + ":" + index;
			TextObject textObject;
			if (this._variables.TryGetValue(key, out textObject))
			{
				return new MultiStatement(MBTextParser.Parse(MBTextManager.Tokenizer.Tokenize(textObject.ToStringWithoutClear())).RootExpressions);
			}
			return null;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006548 File Offset: 0x00004748
		private int CountMarkerOccurancesInString(string searchedIdentifier, TextObject parent)
		{
			Regex regex = new Regex("{." + searchedIdentifier + "}");
			TextObject to = parent;
			if (parent.IsLink)
			{
				string key = parent.Attributes.First((KeyValuePair<string, object> x) => !x.Key.Equals("LINK")).Key;
				parent.GetVariableValue(key, out to);
			}
			string input = MBTextManager.ProcessWithoutLanguageProcessor(to);
			if (regex.IsMatch(input))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000065C4 File Offset: 0x000047C4
		internal string GetParameterWithMarkerOccurance(string token, TextObject parent)
		{
			int num = token.IndexOf('!');
			if (num == -1)
			{
				return "";
			}
			string rawValue = token.Substring(0, num);
			string searchedIdentifier = token.Substring(num + 2, token.Length - num - 2);
			TextObject functionParamWithoutEvaluate = this.GetFunctionParamWithoutEvaluate(rawValue);
			TextObject textObject;
			if (((((parent != null) ? parent.Attributes : null) != null && parent.TryGetAttributesValue(functionParamWithoutEvaluate.ToStringWithoutClear(), out textObject)) || this._variables.TryGetValue(functionParamWithoutEvaluate.ToStringWithoutClear(), out textObject)) && textObject.Length > 0)
			{
				return this.CountMarkerOccurancesInString(searchedIdentifier, textObject).ToString();
			}
			return "";
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006660 File Offset: 0x00004860
		internal string GetParameterWithMarkerOccurances(string token, TextObject parent)
		{
			int num = token.IndexOf('!');
			if (num == -1)
			{
				return "";
			}
			string rawValue = token.Substring(0, num);
			int num2 = token.IndexOf('[') + 1;
			int num3 = token.IndexOf(']');
			string[] array = token.Substring(num2, num3 - num2).Split(new char[]
			{
				','
			});
			TextObject functionParamWithoutEvaluate = this.GetFunctionParamWithoutEvaluate(rawValue);
			TextObject textObject;
			if (((((parent != null) ? parent.Attributes : null) != null && parent.TryGetAttributesValue(functionParamWithoutEvaluate.ToStringWithoutClear(), out textObject)) || this._variables.TryGetValue(functionParamWithoutEvaluate.ToStringWithoutClear(), out textObject)) && textObject.Length > 0)
			{
				foreach (string searchedIdentifier in array)
				{
					int num4 = this.CountMarkerOccurancesInString(searchedIdentifier, textObject);
					if (num4 > 0)
					{
						return num4.ToString();
					}
				}
				return "0";
			}
			return "";
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006743 File Offset: 0x00004943
		internal static bool IsDeclaration(string token)
		{
			return token.Length > 1 && token[0] == '@';
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000675B File Offset: 0x0000495B
		internal static bool IsLinkToken(string token)
		{
			return token == ".link" || token == "LINK";
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006777 File Offset: 0x00004977
		internal static bool IsDeclarationFinalizer(string token)
		{
			return token.Length == 2 && (token[0] == '\\' || token[0] == '/') && token[1] == '@';
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000067A8 File Offset: 0x000049A8
		private static TextObject FindNestedFieldValue(string text, string identifier, TextObject parent)
		{
			string[] fieldNames = identifier.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			return new TextObject(TextProcessingContext.GetFieldValue(text, fieldNames, parent), null);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000067D8 File Offset: 0x000049D8
		[return: TupleElementNames(new string[]
		{
			"value",
			"doesValueExist"
		})]
		internal ValueTuple<TextObject, bool> GetQualifiedVariableValue(string token, TextObject parent)
		{
			int num = token.IndexOf('.');
			if (num == -1)
			{
				return this.GetVariableValueAsTextObject(token, parent);
			}
			string text = token.Substring(0, num);
			string text2 = token.Substring(num + 1, token.Length - (num + 1));
			TextObject parent2;
			if (((parent != null) ? parent.Attributes : null) != null && parent.TryGetAttributesValue(text, out parent2))
			{
				ValueTuple<TextObject, bool> qualifiedVariableValue = this.GetQualifiedVariableValue(text2, parent2);
				TextObject item = qualifiedVariableValue.Item1;
				bool item2 = qualifiedVariableValue.Item2;
				if (!item.Equals(TextObject.Empty))
				{
					return new ValueTuple<TextObject, bool>(item, item2);
				}
			}
			else
			{
				TextObject textObject;
				if (this._variables.TryGetValue(text, out textObject) && textObject.Length > 0)
				{
					return new ValueTuple<TextObject, bool>(TextProcessingContext.FindNestedFieldValue(MBTextManager.GetLocalizedText(textObject.Value), text2, textObject), true);
				}
				foreach (KeyValuePair<string, TextObject> keyValuePair in this._variables)
				{
					if (keyValuePair.Key == text && keyValuePair.Value.Attributes != null)
					{
						foreach (KeyValuePair<string, object> keyValuePair2 in keyValuePair.Value.Attributes)
						{
							if (keyValuePair2.Key == text2)
							{
								return new ValueTuple<TextObject, bool>(TextObject.TryGetOrCreateFromObject(keyValuePair2.Value), true);
							}
						}
					}
				}
			}
			return new ValueTuple<TextObject, bool>(TextObject.Empty, false);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006974 File Offset: 0x00004B74
		private static string GetFieldValue(string text, string[] fieldNames, TextObject parent)
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "GetFieldValue");
			bool flag = false;
			while (i < text.Length)
			{
				if (text[i] != '{')
				{
					if (num == fieldNames.Length && num2 == num)
					{
						mbstringBuilder.Append(text[i]);
					}
				}
				else
				{
					string text2 = TextProcessingContext.ReadFirstToken(text, ref i);
					if (TextProcessingContext.IsLinkToken(text2))
					{
						flag = true;
					}
					else if (TextProcessingContext.IsDeclarationFinalizer(text2))
					{
						num--;
						if (num2 > num)
						{
							num2 = num;
						}
					}
					else if (TextProcessingContext.IsDeclaration(text2))
					{
						string strB = text2.Substring(1);
						bool flag2 = num2 == num && num < fieldNames.Length && string.Compare(fieldNames[num], strB, StringComparison.InvariantCultureIgnoreCase) == 0;
						num++;
						if (flag2)
						{
							num2 = num;
						}
					}
					else if (flag)
					{
						TextObject value;
						if (parent.Attributes != null && parent.TryGetAttributesValue(text2, out value))
						{
							return TextProcessingContext.GetFieldValuesFromLinks(fieldNames, value, ref mbstringBuilder);
						}
					}
					else if (num == fieldNames.Length && num2 == num)
					{
						mbstringBuilder.Append<string>("{" + text2 + "}");
					}
				}
				i++;
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006A9C File Offset: 0x00004C9C
		private static string GetFieldValuesFromLinks(string[] fieldNames, TextObject value, ref MBStringBuilder targetString)
		{
			TextObject value2;
			if (fieldNames.Length == 1 && value.TryGetAttributesValue(fieldNames[0], out value2))
			{
				targetString.Append<TextObject>(value2);
				return targetString.ToStringAndRelease();
			}
			targetString.Append<string>(TextProcessingContext.GetFieldValue(MBTextManager.GetLocalizedText(value.Value), fieldNames, null));
			return targetString.ToStringAndRelease();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006AEC File Offset: 0x00004CEC
		internal static string ReadFirstToken(string text, ref int i)
		{
			int num = i;
			while (i < text.Length && text[i] != '}')
			{
				i++;
			}
			int num2 = i - num;
			return text.Substring(num + 1, num2 - 1);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006B2C File Offset: 0x00004D2C
		internal TextObject CallFunction(string functionName, List<TextExpression> functionParams, TextObject parent)
		{
			TextObject[] array = new TextObject[functionParams.Count];
			TextObject[] array2 = new TextObject[functionParams.Count];
			for (int i = 0; i < functionParams.Count; i++)
			{
				array[i] = new TextObject(functionParams[i].EvaluateString(this, parent), null);
				array2[i] = new TextObject(functionParams[i].RawValue, null);
			}
			this._curParams.Push(array);
			this._curParamsWithoutEvaluate.Push(array2);
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			MBTextModel functionBody = this.GetFunctionBody(functionName);
			mbstringBuilder.Initialize(16, "CallFunction");
			if (functionBody != null)
			{
				using (List<TextExpression>.Enumerator enumerator = functionBody.RootExpressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TextExpression textExpression = enumerator.Current;
						mbstringBuilder.Append<string>(textExpression.EvaluateString(this, parent));
					}
					goto IL_E7;
				}
			}
			if (array.Length != 0)
			{
				mbstringBuilder.Append<TextObject>(array[0]);
			}
			IL_E7:
			string value = mbstringBuilder.ToStringAndRelease();
			this._curParams.Pop();
			return new TextObject(value, null);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006C4C File Offset: 0x00004E4C
		public void SetFunction(string functionName, MBTextModel functionBody)
		{
			this._functions[functionName] = functionBody;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006C5B File Offset: 0x00004E5B
		public void ResetFunctions()
		{
			this._functions.Clear();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006C68 File Offset: 0x00004E68
		public MBTextModel GetFunctionBody(string functionName)
		{
			MBTextModel result;
			this._functions.TryGetValue(functionName, out result);
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C88 File Offset: 0x00004E88
		public TextObject GetFunctionParam(string rawValue)
		{
			int num;
			if (!int.TryParse(rawValue.Substring(1), out num))
			{
				return TextObject.Empty;
			}
			if (this._curParams.Count > 0 && this._curParams.Peek().Length > num)
			{
				return this._curParams.Peek()[num];
			}
			return new TextObject("Can't find parameter:" + rawValue, null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public TextObject GetFunctionParamWithoutEvaluate(string rawValue)
		{
			int num;
			if (!int.TryParse(rawValue.Substring(1), out num))
			{
				return TextObject.Empty;
			}
			if (this._curParamsWithoutEvaluate.Count > 0 && this._curParamsWithoutEvaluate.Peek().Length > num)
			{
				return this._curParamsWithoutEvaluate.Peek()[num];
			}
			return new TextObject("Can't find parameter:" + rawValue, null);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006D48 File Offset: 0x00004F48
		internal void ClearAll()
		{
			this._variables.Clear();
		}

		// Token: 0x04000065 RID: 101
		private readonly Dictionary<string, TextObject> _variables = new Dictionary<string, TextObject>(new CaseInsensitiveComparer());

		// Token: 0x04000066 RID: 102
		private readonly Dictionary<string, MBTextModel> _functions = new Dictionary<string, MBTextModel>();

		// Token: 0x04000067 RID: 103
		private readonly Stack<TextObject[]> _curParams = new Stack<TextObject[]>();

		// Token: 0x04000068 RID: 104
		private readonly Stack<TextObject[]> _curParamsWithoutEvaluate = new Stack<TextObject[]>();
	}
}
