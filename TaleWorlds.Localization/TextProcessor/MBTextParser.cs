using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization.Expressions;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x02000028 RID: 40
	internal class MBTextParser
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000050A7 File Offset: 0x000032A7
		internal TextExpression LookAheadFirst
		{
			get
			{
				return this._lookaheadFirst;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000050AF File Offset: 0x000032AF
		internal TextExpression LookAheadSecond
		{
			get
			{
				return this._lookaheadSecond;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000050B7 File Offset: 0x000032B7
		internal TextExpression LookAheadThird
		{
			get
			{
				return this._lookaheadThird;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000050C0 File Offset: 0x000032C0
		private TextExpression GetSimpleToken(TokenType tokenType, string strValue)
		{
			if (tokenType == TokenType.Text)
			{
				return new SimpleText(strValue);
			}
			if (tokenType == TokenType.Number)
			{
				return new SimpleNumberExpression(strValue);
			}
			if (tokenType == TokenType.Identifier)
			{
				return new VariableExpression(strValue, null);
			}
			if (tokenType == TokenType.LanguageMarker)
			{
				return new LangaugeMarkerExpression(strValue);
			}
			if (tokenType == TokenType.textId)
			{
				return new TextIdExpression(strValue);
			}
			if (tokenType == TokenType.QualifiedIdentifier)
			{
				return new QualifiedIdentifierExpression(strValue);
			}
			if (tokenType == TokenType.ParameterWithAttribute)
			{
				return new ParameterWithAttributeExpression(strValue);
			}
			if (tokenType == TokenType.StartsWith)
			{
				return new StartsWithExpression(strValue);
			}
			return new SimpleToken(tokenType, strValue);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005138 File Offset: 0x00003338
		private void LoadSequenceStack(List<MBTextToken> tokens)
		{
			this._symbolSequence = new Stack<TextExpression>();
			for (int i = tokens.Count - 1; i >= 0; i--)
			{
				TextExpression simpleToken = this.GetSimpleToken(tokens[i].TokenType, tokens[i].Value);
				this._symbolSequence.Push(simpleToken);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000518E File Offset: 0x0000338E
		private void PushToken(TextExpression token)
		{
			this._symbolSequence.Push(token);
			this.UpdateLookAheads();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000051A4 File Offset: 0x000033A4
		private void UpdateLookAheads()
		{
			if (this._symbolSequence.Count == 0)
			{
				this._lookaheadFirst = SimpleToken.SequenceTerminator;
			}
			else
			{
				this._lookaheadFirst = this._symbolSequence.Peek();
			}
			if (this._symbolSequence.Count < 2)
			{
				this._lookaheadSecond = SimpleToken.SequenceTerminator;
			}
			else
			{
				TextExpression item = this._symbolSequence.Pop();
				this._lookaheadSecond = this._symbolSequence.Peek();
				this._symbolSequence.Push(item);
			}
			if (this._symbolSequence.Count < 3)
			{
				this._lookaheadThird = SimpleToken.SequenceTerminator;
				return;
			}
			TextExpression item2 = this._symbolSequence.Pop();
			TextExpression item3 = this._symbolSequence.Pop();
			this._lookaheadThird = this._symbolSequence.Peek();
			this._symbolSequence.Push(item3);
			this._symbolSequence.Push(item2);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000527B File Offset: 0x0000347B
		private void DiscardToken()
		{
			if (this._symbolSequence.Count > 0)
			{
				this._symbolSequence.Pop();
			}
			this.UpdateLookAheads();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000052A0 File Offset: 0x000034A0
		private void DiscardToken(TokenType tokenType)
		{
			if (this._lookaheadFirst.TokenType != tokenType)
			{
				MBTextManager.ThrowLocalizationError(string.Format("Unxpected token: {1} while expecting: {0}", tokenType.ToString().ToUpper(), this._lookaheadFirst.RawValue));
			}
			this.DiscardToken();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052F0 File Offset: 0x000034F0
		private void Statements()
		{
			TextExpression rootExpressions = this.GetRootExpressions();
			this._queryModel.AddRootExpression(rootExpressions);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005310 File Offset: 0x00003510
		private bool IsRootExpression(TokenType tokenType)
		{
			return tokenType == TokenType.Text || tokenType == TokenType.SimpleExpression || tokenType == TokenType.ConditionalExpression || tokenType == TokenType.textId || tokenType == TokenType.SelectionExpression || tokenType == TokenType.MultiStatement || tokenType == TokenType.FieldExpression || tokenType == TokenType.LanguageMarker;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000533C File Offset: 0x0000353C
		private void GetRootExpressionsImp(List<TextExpression> expList)
		{
			for (;;)
			{
				if (!this.RunRootGrammarRulesExceptCollapse())
				{
					if (!this.IsRootExpression(this.LookAheadFirst.TokenType))
					{
						break;
					}
					TextExpression lookAheadFirst = this.LookAheadFirst;
					this.DiscardToken();
					expList.Add(lookAheadFirst);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000537C File Offset: 0x0000357C
		private TextExpression GetRootExpressions()
		{
			List<TextExpression> list = new List<TextExpression>();
			this.GetRootExpressionsImp(list);
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new MultiStatement(list);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000053B7 File Offset: 0x000035B7
		private bool RunRootGrammarRulesExceptCollapse()
		{
			return this.CheckSimpleStatement() || this.CheckConditionalStatement() || this.CheckSelectionStatement() || this.CheckFieldStatement();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000053DC File Offset: 0x000035DC
		private bool CollapseStatements()
		{
			if (!this.IsRootExpression(this.LookAheadFirst.TokenType) || this.LookAheadFirst.TokenType == TokenType.MultiStatement)
			{
				return false;
			}
			List<TextExpression> list = new List<TextExpression>();
			TextExpression lookAheadFirst = this.LookAheadFirst;
			this.DiscardToken();
			list.Add(lookAheadFirst);
			bool flag = false;
			while (!flag)
			{
				while (this.RunRootGrammarRulesExceptCollapse())
				{
				}
				if (this.IsRootExpression(this.LookAheadFirst.TokenType))
				{
					TextExpression lookAheadFirst2 = this.LookAheadFirst;
					this.DiscardToken();
					list.Add(lookAheadFirst2);
				}
				else
				{
					flag = true;
				}
			}
			this.PushToken(new MultiStatement(list));
			return true;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005470 File Offset: 0x00003670
		private bool CheckSimpleStatement()
		{
			if (this.LookAheadFirst.TokenType != TokenType.OpenBraces)
			{
				return false;
			}
			this.DiscardToken(TokenType.OpenBraces);
			bool flag = false;
			while (!flag)
			{
				flag = !this.DoExpressionRules();
			}
			TokenType tokenType = this.LookAheadFirst.TokenType;
			if (this.IsArithmeticExpression(tokenType))
			{
				TextExpression token = new SimpleExpression(this.LookAheadFirst);
				this.DiscardToken();
				this.DiscardToken(TokenType.CloseBraces);
				this.PushToken(token);
			}
			else
			{
				this.DiscardToken(TokenType.CloseBraces);
			}
			return true;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000054EC File Offset: 0x000036EC
		private bool CheckFieldStatement()
		{
			if (this.LookAheadFirst.TokenType != TokenType.FieldStarter)
			{
				return false;
			}
			this.DiscardToken(TokenType.FieldStarter);
			bool flag = false;
			while (!flag)
			{
				flag = !this.DoExpressionRules();
			}
			if (this.LookAheadFirst.TokenType != TokenType.Identifier)
			{
				Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "CheckFieldStatement", 289);
				return false;
			}
			TextExpression lookAheadFirst = this.LookAheadFirst;
			this.DiscardToken(TokenType.Identifier);
			this.DiscardToken(TokenType.CloseBraces);
			TextExpression textExpression = this.GetRootExpressions();
			if (textExpression == null)
			{
				textExpression = new SimpleToken(TokenType.Text, "");
			}
			this.DiscardToken(TokenType.FieldFinalizer);
			FieldExpression token = new FieldExpression(lookAheadFirst, textExpression);
			this.PushToken(token);
			return true;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000559C File Offset: 0x0000379C
		private bool CheckConditionalStatement()
		{
			if (this.LookAheadFirst.TokenType != TokenType.ConditionStarter)
			{
				return false;
			}
			bool flag = false;
			List<TextExpression> list = new List<TextExpression>();
			List<TextExpression> list2 = new List<TextExpression>();
			while (!flag)
			{
				TokenType tokenType = this.LookAheadFirst.TokenType;
				if (this.LookAheadFirst.TokenType == TokenType.ConditionStarter || this.LookAheadFirst.TokenType == TokenType.ConditionFollowUp)
				{
					this.DiscardToken();
					while (this.DoExpressionRules())
					{
					}
					tokenType = this.LookAheadFirst.TokenType;
					if (!this.IsArithmeticExpression(tokenType))
					{
						Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "CheckConditionalStatement", 336);
						return false;
					}
					list.Add(this.LookAheadFirst);
					this.DiscardToken();
					this.DiscardToken(TokenType.CloseBraces);
				}
				else
				{
					if (tokenType != TokenType.ConditionSeperator && tokenType != TokenType.Seperator)
					{
						MBTextManager.ThrowLocalizationError("Can not parse the text: " + this.LookAheadFirst);
						return false;
					}
					this.DiscardToken();
					flag = true;
				}
				TextExpression textExpression = this.GetRootExpressions();
				if (textExpression == null)
				{
					textExpression = new SimpleToken(TokenType.Text, "");
				}
				list2.Add(textExpression);
			}
			while (!flag)
			{
			}
			this.DiscardToken(TokenType.ConditionFinalizer);
			ConditionExpression token = new ConditionExpression(list, list2);
			this.PushToken(token);
			return true;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000056D0 File Offset: 0x000038D0
		private bool CheckSelectionStatement()
		{
			if (this.LookAheadFirst.TokenType != TokenType.SelectionStarter)
			{
				return false;
			}
			this.DiscardToken(TokenType.SelectionStarter);
			while (this.DoExpressionRules())
			{
			}
			TokenType tokenType = this.LookAheadFirst.TokenType;
			if (!this.IsArithmeticExpression(tokenType))
			{
				Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "CheckSelectionStatement", 382);
				return false;
			}
			TextExpression lookAheadFirst = this.LookAheadFirst;
			this.DiscardToken();
			this.DiscardToken(TokenType.CloseBraces);
			bool flag = false;
			List<TextExpression> list = new List<TextExpression>();
			for (;;)
			{
				TextExpression textExpression = this.GetRootExpressions();
				if (textExpression == null)
				{
					textExpression = new SimpleToken(TokenType.Text, "");
				}
				list.Add(textExpression);
				TokenType tokenType2 = this.LookAheadFirst.TokenType;
				if (tokenType2 == TokenType.SelectionSeperator)
				{
					this.DiscardToken();
				}
				else
				{
					if (tokenType2 != TokenType.SelectionFinalizer)
					{
						break;
					}
					flag = true;
					this.DiscardToken();
				}
				if (flag)
				{
					goto Block_7;
				}
			}
			Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "CheckSelectionStatement", 414);
			return false;
			Block_7:
			SelectionExpression token = new SelectionExpression(lookAheadFirst, list);
			this.PushToken(token);
			return true;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000057E0 File Offset: 0x000039E0
		private bool DoExpressionRules()
		{
			return this.ConsumeArrayAccessExpression() || this.ConsumeFunction() || this.ConsumeMarkerOccuranceExpression() || this.ConsumeNegativeAritmeticExpression() || this.ConsumeParenthesisExpression() || this.ConsumeInnerAritmeticExpression() || this.ConsumeOuterAritmeticExpression() || this.ConsumeComparisonExpression();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005830 File Offset: 0x00003A30
		private bool ConsumeFunction()
		{
			if (this.LookAheadFirst.TokenType != TokenType.FunctionIdentifier)
			{
				return false;
			}
			string functionName = this.LookAheadFirst.RawValue.Substring(0, this.LookAheadFirst.RawValue.Length - 1);
			this.DiscardToken();
			bool flag = false;
			List<TextExpression> list = new List<TextExpression>();
			while (this.LookAheadFirst.TokenType != TokenType.CloseParenthesis && !flag)
			{
				if (list.Count > 0)
				{
					this.DiscardToken(TokenType.Comma);
				}
				while (this.DoExpressionRules())
				{
				}
				TokenType tokenType = this.LookAheadFirst.TokenType;
				if (!this.IsArithmeticExpression(tokenType))
				{
					Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "ConsumeFunction", 472);
					return false;
				}
				list.Add(this.LookAheadFirst);
				this.DiscardToken();
			}
			this.DiscardToken(TokenType.CloseParenthesis);
			FunctionCall token = new FunctionCall(functionName, list);
			this.PushToken(token);
			return true;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005918 File Offset: 0x00003B18
		private bool ConsumeMarkerOccuranceExpression()
		{
			if (this.LookAheadFirst.TokenType == TokenType.Identifier && this.LookAheadSecond.TokenType == TokenType.MarkerOccuranceIdentifier)
			{
				VariableExpression innerExpression = this.LookAheadFirst as VariableExpression;
				TextExpression lookAheadSecond = this.LookAheadSecond;
				this.DiscardToken();
				this.DiscardToken();
				MarkerOccuranceTextExpression token = new MarkerOccuranceTextExpression(lookAheadSecond.RawValue.Substring(2), innerExpression);
				this.PushToken(token);
				return true;
			}
			return false;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005980 File Offset: 0x00003B80
		private bool ConsumeArrayAccessExpression()
		{
			if (this.LookAheadFirst.TokenType == TokenType.Identifier && this.LookAheadSecond.TokenType == TokenType.OpenBrackets)
			{
				TextExpression lookAheadFirst = this.LookAheadFirst;
				this.DiscardToken();
				this.DiscardToken(TokenType.OpenBrackets);
				while (this.DoExpressionRules())
				{
				}
				TokenType tokenType = this.LookAheadFirst.TokenType;
				if (this.IsArithmeticExpression(tokenType))
				{
					TextExpression lookAheadFirst2 = this.LookAheadFirst;
					this.DiscardToken();
					this.DiscardToken(TokenType.CloseBrackets);
					ArrayReference token = new ArrayReference(lookAheadFirst.RawValue, lookAheadFirst2);
					this.PushToken(token);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005A0C File Offset: 0x00003C0C
		private bool ConsumeNegativeAritmeticExpression()
		{
			if (this.LookAheadFirst.TokenType == TokenType.Minus)
			{
				this.ConsumeAritmeticOperation();
				TokenType tokenType = this.LookAheadFirst.TokenType;
				if (this.IsArithmeticExpression(tokenType))
				{
					ArithmeticExpression token = new ArithmeticExpression(ArithmeticOperation.Subtract, new SimpleToken(TokenType.Number, "0"), this.LookAheadFirst);
					this.PushToken(token);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005A68 File Offset: 0x00003C68
		private bool ConsumeParenthesisExpression()
		{
			if (this.LookAheadFirst.TokenType != TokenType.OpenParenthesis)
			{
				return false;
			}
			this.DiscardToken(TokenType.OpenParenthesis);
			while (this.DoExpressionRules())
			{
			}
			TokenType tokenType = this.LookAheadFirst.TokenType;
			if (this.IsArithmeticExpression(tokenType))
			{
				ParanthesisExpression token = new ParanthesisExpression(this.LookAheadFirst);
				this.DiscardToken();
				this.DiscardToken(TokenType.CloseParenthesis);
				this.PushToken(token);
				return true;
			}
			this.DiscardToken(TokenType.CloseParenthesis);
			return true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005AD8 File Offset: 0x00003CD8
		private bool IsArithmeticExpression(TokenType t)
		{
			return t == TokenType.ArithmeticProduct || t == TokenType.ArithmeticSum || t == TokenType.Identifier || t == TokenType.QualifiedIdentifier || t == TokenType.MarkerOccuranceExpression || t == TokenType.ParameterWithMarkerOccurance || t == TokenType.ParameterWithMultipleMarkerOccurances || t == TokenType.StartsWith || t == TokenType.Number || t == TokenType.ParenthesisExpression || t == TokenType.ComparisonExpression || t == TokenType.FunctionCall || t == TokenType.FunctionParam || t == TokenType.ArrayAccess || t == TokenType.ParameterWithAttribute;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005B34 File Offset: 0x00003D34
		private bool ConsumeInnerAritmeticExpression()
		{
			TokenType tokenType = this.LookAheadFirst.TokenType;
			TokenType tokenType2 = this.LookAheadSecond.TokenType;
			TokenType tokenType3 = this.LookAheadThird.TokenType;
			if (this.IsArithmeticExpression(tokenType) && (tokenType2 == TokenType.Multiply || tokenType2 == TokenType.Divide))
			{
				TextExpression lookAheadFirst = this.LookAheadFirst;
				this.DiscardToken();
				ArithmeticOperation op = this.ConsumeAritmeticOperation();
				if (!this.IsArithmeticExpression(this.LookAheadFirst.TokenType))
				{
					while (this.DoExpressionRules())
					{
					}
				}
				TextExpression lookAheadFirst2 = this.LookAheadFirst;
				this.DiscardToken();
				ArithmeticExpression token = new ArithmeticExpression(op, lookAheadFirst, lookAheadFirst2);
				this.PushToken(token);
				return true;
			}
			return false;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BCC File Offset: 0x00003DCC
		private bool ConsumeOuterAritmeticExpression()
		{
			TokenType tokenType = this.LookAheadFirst.TokenType;
			TokenType tokenType2 = this.LookAheadSecond.TokenType;
			if (this.IsArithmeticExpression(tokenType) && (tokenType2 == TokenType.Plus || tokenType2 == TokenType.Minus))
			{
				TextExpression lookAheadFirst = this.LookAheadFirst;
				this.DiscardToken();
				ArithmeticOperation op = this.ConsumeAritmeticOperation();
				while (this.DoExpressionRules())
				{
				}
				if (this.IsArithmeticExpression(this.LookAheadFirst.TokenType))
				{
					TextExpression lookAheadFirst2 = this.LookAheadFirst;
					this.DiscardToken();
					ArithmeticExpression token = new ArithmeticExpression(op, lookAheadFirst, lookAheadFirst2);
					this.PushToken(token);
					return true;
				}
				Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "ConsumeOuterAritmeticExpression", 646);
			}
			return false;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005C80 File Offset: 0x00003E80
		private ArithmeticOperation ConsumeAritmeticOperation()
		{
			ArithmeticOperation result = (this.LookAheadFirst.TokenType == TokenType.Plus) ? ArithmeticOperation.Add : ((this.LookAheadFirst.TokenType == TokenType.Minus) ? ArithmeticOperation.Subtract : ((this.LookAheadFirst.TokenType == TokenType.Multiply) ? ArithmeticOperation.Multiply : ((this.LookAheadFirst.TokenType == TokenType.Divide) ? ArithmeticOperation.Divide : ArithmeticOperation.Add)));
			this.DiscardToken();
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005CDC File Offset: 0x00003EDC
		private bool ConsumeComparisonExpression()
		{
			TokenType tokenType = this.LookAheadFirst.TokenType;
			TokenType tokenType2 = this.LookAheadSecond.TokenType;
			if (!this.IsArithmeticExpression(tokenType) || !this.IsComparisonOperator(tokenType2))
			{
				return false;
			}
			TextExpression lookAheadFirst = this.LookAheadFirst;
			this.DiscardToken();
			ComparisonOperation comparisonOp = this.GetComparisonOp(tokenType2);
			this.DiscardToken();
			while (this.DoExpressionRules())
			{
			}
			if (!this.IsArithmeticExpression(this.LookAheadFirst.TokenType))
			{
				Debug.FailedAssert("Can not parse the text: " + this.LookAheadFirst, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\TextProcessor\\MbTextParser.cs", "ConsumeComparisonExpression", 690);
				return false;
			}
			TextExpression lookAheadFirst2 = this.LookAheadFirst;
			this.DiscardToken();
			ComparisonExpression token = new ComparisonExpression(comparisonOp, lookAheadFirst, lookAheadFirst2);
			this.PushToken(token);
			return true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005D96 File Offset: 0x00003F96
		private bool IsComparisonOperator(TokenType tokenType)
		{
			return tokenType == TokenType.Equals || tokenType == TokenType.NotEquals || tokenType == TokenType.GreaterThan || tokenType == TokenType.GreaterOrEqual || tokenType == TokenType.GreaterThan || tokenType == TokenType.LessOrEqual || tokenType == TokenType.LessThan;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DB7 File Offset: 0x00003FB7
		private BooleanOperation GetBooleanOp(TokenType tokenType)
		{
			if (tokenType == TokenType.Or)
			{
				return BooleanOperation.Or;
			}
			if (tokenType == TokenType.And)
			{
				return BooleanOperation.And;
			}
			if (tokenType != TokenType.Not)
			{
				return BooleanOperation.And;
			}
			return BooleanOperation.Not;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005DCC File Offset: 0x00003FCC
		private ComparisonOperation GetComparisonOp(TokenType tokenType)
		{
			if (tokenType == TokenType.Equals)
			{
				return ComparisonOperation.Equals;
			}
			if (tokenType == TokenType.NotEquals)
			{
				return ComparisonOperation.NotEquals;
			}
			if (tokenType == TokenType.GreaterThan)
			{
				return ComparisonOperation.GreaterThan;
			}
			if (tokenType == TokenType.GreaterOrEqual)
			{
				return ComparisonOperation.GreaterOrEqual;
			}
			if (tokenType == TokenType.GreaterThan)
			{
				return ComparisonOperation.GreaterThan;
			}
			if (tokenType == TokenType.LessOrEqual)
			{
				return ComparisonOperation.LessOrEqual;
			}
			if (tokenType != TokenType.LessThan)
			{
				return ComparisonOperation.Equals;
			}
			return ComparisonOperation.LessThan;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005DFA File Offset: 0x00003FFA
		private MBTextModel ParseInternal(List<MBTextToken> tokens)
		{
			this.LoadSequenceStack(tokens);
			this.UpdateLookAheads();
			this._queryModel = new MBTextModel();
			this.Statements();
			this.DiscardToken(TokenType.SequenceTerminator);
			return this._queryModel;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005E28 File Offset: 0x00004028
		internal static MBTextModel Parse(List<MBTextToken> tokens)
		{
			return new MBTextParser().ParseInternal(tokens);
		}

		// Token: 0x0400005A RID: 90
		private Stack<TextExpression> _symbolSequence;

		// Token: 0x0400005B RID: 91
		private TextExpression _lookaheadFirst;

		// Token: 0x0400005C RID: 92
		private TextExpression _lookaheadSecond;

		// Token: 0x0400005D RID: 93
		private TextExpression _lookaheadThird;

		// Token: 0x0400005E RID: 94
		private MBTextModel _queryModel;
	}
}
