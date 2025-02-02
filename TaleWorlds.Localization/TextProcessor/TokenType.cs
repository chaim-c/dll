using System;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002F RID: 47
	internal enum TokenType
	{
		// Token: 0x0400006A RID: 106
		NotDefined,
		// Token: 0x0400006B RID: 107
		And,
		// Token: 0x0400006C RID: 108
		Or,
		// Token: 0x0400006D RID: 109
		Not,
		// Token: 0x0400006E RID: 110
		Equals,
		// Token: 0x0400006F RID: 111
		NotEquals,
		// Token: 0x04000070 RID: 112
		GreaterThan,
		// Token: 0x04000071 RID: 113
		LessThan,
		// Token: 0x04000072 RID: 114
		GreaterOrEqual,
		// Token: 0x04000073 RID: 115
		LessOrEqual,
		// Token: 0x04000074 RID: 116
		Comma,
		// Token: 0x04000075 RID: 117
		OpenBraces,
		// Token: 0x04000076 RID: 118
		CloseBraces,
		// Token: 0x04000077 RID: 119
		OpenParenthesis,
		// Token: 0x04000078 RID: 120
		CloseParenthesis,
		// Token: 0x04000079 RID: 121
		OpenBrackets,
		// Token: 0x0400007A RID: 122
		CloseBrackets,
		// Token: 0x0400007B RID: 123
		Number,
		// Token: 0x0400007C RID: 124
		Identifier,
		// Token: 0x0400007D RID: 125
		VariableExpression,
		// Token: 0x0400007E RID: 126
		MarkerOccuranceIdentifier,
		// Token: 0x0400007F RID: 127
		Match,
		// Token: 0x04000080 RID: 128
		ConditionSeperator,
		// Token: 0x04000081 RID: 129
		ConditionFollowUp,
		// Token: 0x04000082 RID: 130
		Seperator,
		// Token: 0x04000083 RID: 131
		ConditionStarter,
		// Token: 0x04000084 RID: 132
		ConditionFinalizer,
		// Token: 0x04000085 RID: 133
		SelectionSeperator,
		// Token: 0x04000086 RID: 134
		SelectionStarter,
		// Token: 0x04000087 RID: 135
		SelectionFinalizer,
		// Token: 0x04000088 RID: 136
		FieldStarter,
		// Token: 0x04000089 RID: 137
		FieldFinalizer,
		// Token: 0x0400008A RID: 138
		SequenceTerminator,
		// Token: 0x0400008B RID: 139
		Text,
		// Token: 0x0400008C RID: 140
		LanguageMarker,
		// Token: 0x0400008D RID: 141
		UnrecognizedTokenError,
		// Token: 0x0400008E RID: 142
		Plus,
		// Token: 0x0400008F RID: 143
		Minus,
		// Token: 0x04000090 RID: 144
		Multiply,
		// Token: 0x04000091 RID: 145
		Divide,
		// Token: 0x04000092 RID: 146
		ArithmeticProduct,
		// Token: 0x04000093 RID: 147
		ArithmeticSum,
		// Token: 0x04000094 RID: 148
		StringExpression,
		// Token: 0x04000095 RID: 149
		SimpleExpression,
		// Token: 0x04000096 RID: 150
		ConditionalExpression,
		// Token: 0x04000097 RID: 151
		SelectionExpression,
		// Token: 0x04000098 RID: 152
		ParenthesisExpression,
		// Token: 0x04000099 RID: 153
		ArrayAccess,
		// Token: 0x0400009A RID: 154
		MultiStatement,
		// Token: 0x0400009B RID: 155
		ComparisonExpression,
		// Token: 0x0400009C RID: 156
		FieldExpression,
		// Token: 0x0400009D RID: 157
		MarkerOccuranceExpression,
		// Token: 0x0400009E RID: 158
		FunctionIdentifier,
		// Token: 0x0400009F RID: 159
		FunctionCall,
		// Token: 0x040000A0 RID: 160
		FunctionParam,
		// Token: 0x040000A1 RID: 161
		ParameterWithMarkerOccurance,
		// Token: 0x040000A2 RID: 162
		ParameterWithMultipleMarkerOccurances,
		// Token: 0x040000A3 RID: 163
		QualifiedIdentifier,
		// Token: 0x040000A4 RID: 164
		ParameterWithAttribute,
		// Token: 0x040000A5 RID: 165
		StartsWith,
		// Token: 0x040000A6 RID: 166
		textId
	}
}
