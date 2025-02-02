﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200007C RID: 124
	public struct ExplainedNumber
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000494A7 File Offset: 0x000476A7
		public float ResultNumber
		{
			get
			{
				return MathF.Clamp(this.BaseNumber + this.BaseNumber * this._sumOfFactors, this.LimitMinValue, this.LimitMaxValue);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000494CE File Offset: 0x000476CE
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x000494D6 File Offset: 0x000476D6
		public float BaseNumber { get; private set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000494DF File Offset: 0x000476DF
		public bool IncludeDescriptions
		{
			get
			{
				return this._explainer != null;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000494EA File Offset: 0x000476EA
		public float LimitMinValue
		{
			get
			{
				if (this._limitMinValue == null)
				{
					return float.MinValue;
				}
				return this._limitMinValue.Value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0004950B File Offset: 0x0004770B
		public float LimitMaxValue
		{
			get
			{
				if (this._limitMaxValue == null)
				{
					return float.MaxValue;
				}
				return this._limitMaxValue.Value;
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0004952C File Offset: 0x0004772C
		public ExplainedNumber(float baseNumber = 0f, bool includeDescriptions = false, TextObject baseText = null)
		{
			this.BaseNumber = baseNumber;
			this._explainer = (includeDescriptions ? new ExplainedNumber.StatExplainer() : null);
			this._sumOfFactors = 0f;
			this._limitMinValue = new float?(float.MinValue);
			this._limitMaxValue = new float?(float.MaxValue);
			if (this._explainer != null && !this.BaseNumber.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._explainer.AddLine((baseText ?? ExplainedNumber.BaseText).ToString(), this.BaseNumber, ExplainedNumber.StatExplainer.OperationType.Base);
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000495BC File Offset: 0x000477BC
		public string GetExplanations()
		{
			if (this._explainer == null)
			{
				return "";
			}
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "GetExplanations");
			foreach (ValueTuple<string, float> valueTuple in this._explainer.GetLines(this.BaseNumber, this.ResultNumber))
			{
				string value = string.Format("{0} : {1}{2:0.##}\n", valueTuple.Item1, (valueTuple.Item2 > 0.001f) ? "+" : "", valueTuple.Item2);
				mbstringBuilder.Append<string>(value);
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00049684 File Offset: 0x00047884
		[return: TupleElementNames(new string[]
		{
			"name",
			"number"
		})]
		public List<ValueTuple<string, float>> GetLines()
		{
			if (this._explainer == null)
			{
				return new List<ValueTuple<string, float>>();
			}
			return this._explainer.GetLines(this.BaseNumber, this.ResultNumber);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000496AC File Offset: 0x000478AC
		public void AddFromExplainedNumber(ExplainedNumber explainedNumber, TextObject baseText)
		{
			if (explainedNumber._explainer != null && this._explainer != null)
			{
				if (explainedNumber._explainer.BaseLine != null && explainedNumber._explainer.BaseLine != null && !explainedNumber.BaseNumber.ApproximatelyEqualsTo(0f, 1E-05f))
				{
					float number = explainedNumber._explainer.BaseLine.Value.Number + explainedNumber._explainer.BaseLine.Value.Number * explainedNumber._sumOfFactors;
					this._explainer.AddLine(((baseText != null) ? baseText.ToString() : null) ?? ExplainedNumber.BaseText.ToString(), number, ExplainedNumber.StatExplainer.OperationType.Add);
				}
				foreach (ExplainedNumber.StatExplainer.ExplanationLine explanationLine in explainedNumber._explainer.Lines)
				{
					if (explanationLine.OperationType == ExplainedNumber.StatExplainer.OperationType.Add)
					{
						float number2 = explanationLine.Number + explanationLine.Number * explainedNumber._sumOfFactors;
						this._explainer.AddLine(explanationLine.Name, number2, explanationLine.OperationType);
					}
				}
			}
			this.BaseNumber += explainedNumber.ResultNumber;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00049804 File Offset: 0x00047A04
		public void Add(float value, TextObject description = null, TextObject variable = null)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this.BaseNumber += value;
			if (description != null && this._explainer != null && !value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				if (variable != null)
				{
					description.SetTextVariable("A0", variable);
				}
				this._explainer.AddLine(description.ToString(), value, ExplainedNumber.StatExplainer.OperationType.Add);
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00049874 File Offset: 0x00047A74
		public void AddFactor(float value, TextObject description = null)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this._sumOfFactors += value;
			if (description != null && this._explainer != null && !value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._explainer.AddLine(description.ToString(), MathF.Round(value, 3) * 100f, ExplainedNumber.StatExplainer.OperationType.Multiply);
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x000498DE File Offset: 0x00047ADE
		public void LimitMin(float minValue)
		{
			this._limitMinValue = new float?(minValue);
			if (this._explainer != null)
			{
				this._explainer.AddLine(ExplainedNumber.LimitMinText.ToString(), minValue, ExplainedNumber.StatExplainer.OperationType.LimitMin);
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0004990B File Offset: 0x00047B0B
		public void LimitMax(float maxValue)
		{
			this._limitMaxValue = new float?(maxValue);
			if (this._explainer != null)
			{
				this._explainer.AddLine(ExplainedNumber.LimitMaxText.ToString(), maxValue, ExplainedNumber.StatExplainer.OperationType.LimitMax);
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00049938 File Offset: 0x00047B38
		public void Clamp(float minValue, float maxValue)
		{
			this.LimitMin(minValue);
			this.LimitMax(maxValue);
		}

		// Token: 0x04000523 RID: 1315
		private static readonly TextObject LimitMinText = new TextObject("{=GNalaRaN}Minimum", null);

		// Token: 0x04000524 RID: 1316
		private static readonly TextObject LimitMaxText = new TextObject("{=cfjTtxWv}Maximum", null);

		// Token: 0x04000525 RID: 1317
		private static readonly TextObject BaseText = new TextObject("{=basevalue}Base", null);

		// Token: 0x04000527 RID: 1319
		private float? _limitMinValue;

		// Token: 0x04000528 RID: 1320
		private float? _limitMaxValue;

		// Token: 0x04000529 RID: 1321
		private ExplainedNumber.StatExplainer _explainer;

		// Token: 0x0400052A RID: 1322
		private float _sumOfFactors;

		// Token: 0x020004C3 RID: 1219
		private class StatExplainer
		{
			// Token: 0x17000D94 RID: 3476
			// (get) Token: 0x060042F1 RID: 17137 RVA: 0x00145285 File Offset: 0x00143485
			// (set) Token: 0x060042F2 RID: 17138 RVA: 0x0014528D File Offset: 0x0014348D
			public List<ExplainedNumber.StatExplainer.ExplanationLine> Lines { get; private set; } = new List<ExplainedNumber.StatExplainer.ExplanationLine>();

			// Token: 0x17000D95 RID: 3477
			// (get) Token: 0x060042F3 RID: 17139 RVA: 0x00145296 File Offset: 0x00143496
			// (set) Token: 0x060042F4 RID: 17140 RVA: 0x0014529E File Offset: 0x0014349E
			public ExplainedNumber.StatExplainer.ExplanationLine? BaseLine { get; private set; }

			// Token: 0x17000D96 RID: 3478
			// (get) Token: 0x060042F5 RID: 17141 RVA: 0x001452A7 File Offset: 0x001434A7
			// (set) Token: 0x060042F6 RID: 17142 RVA: 0x001452AF File Offset: 0x001434AF
			public ExplainedNumber.StatExplainer.ExplanationLine? LimitMinLine { get; private set; }

			// Token: 0x17000D97 RID: 3479
			// (get) Token: 0x060042F7 RID: 17143 RVA: 0x001452B8 File Offset: 0x001434B8
			// (set) Token: 0x060042F8 RID: 17144 RVA: 0x001452C0 File Offset: 0x001434C0
			public ExplainedNumber.StatExplainer.ExplanationLine? LimitMaxLine { get; private set; }

			// Token: 0x060042F9 RID: 17145 RVA: 0x001452CC File Offset: 0x001434CC
			[return: TupleElementNames(new string[]
			{
				"name",
				"number"
			})]
			public List<ValueTuple<string, float>> GetLines(float baseNumber, float resultNumber)
			{
				List<ValueTuple<string, float>> list = new List<ValueTuple<string, float>>();
				if (this.BaseLine != null)
				{
					list.Add(new ValueTuple<string, float>(this.BaseLine.Value.Name, this.BaseLine.Value.Number));
				}
				foreach (ExplainedNumber.StatExplainer.ExplanationLine explanationLine in this.Lines)
				{
					float num = explanationLine.Number;
					if (explanationLine.OperationType == ExplainedNumber.StatExplainer.OperationType.Multiply)
					{
						num = baseNumber * num * 0.01f;
					}
					list.Add(new ValueTuple<string, float>(explanationLine.Name, num));
				}
				if (this.LimitMinLine != null && this.LimitMinLine.Value.Number > resultNumber)
				{
					list.Add(new ValueTuple<string, float>(this.LimitMinLine.Value.Name, this.LimitMinLine.Value.Number));
				}
				if (this.LimitMaxLine != null && this.LimitMaxLine.Value.Number < resultNumber)
				{
					list.Add(new ValueTuple<string, float>(this.LimitMaxLine.Value.Name, this.LimitMaxLine.Value.Number));
				}
				return list;
			}

			// Token: 0x060042FA RID: 17146 RVA: 0x00145440 File Offset: 0x00143640
			public void AddLine(string name, float number, ExplainedNumber.StatExplainer.OperationType opType)
			{
				ExplainedNumber.StatExplainer.ExplanationLine explanationLine = new ExplainedNumber.StatExplainer.ExplanationLine(name, number, opType);
				if (opType == ExplainedNumber.StatExplainer.OperationType.Add || opType == ExplainedNumber.StatExplainer.OperationType.Multiply)
				{
					int num = -1;
					for (int i = 0; i < this.Lines.Count; i++)
					{
						if (this.Lines[i].Name.Equals(name) && this.Lines[i].OperationType == opType)
						{
							num = i;
							break;
						}
					}
					if (num < 0)
					{
						this.Lines.Add(explanationLine);
						return;
					}
					explanationLine = new ExplainedNumber.StatExplainer.ExplanationLine(name, number + this.Lines[num].Number, opType);
					this.Lines[num] = explanationLine;
					return;
				}
				else
				{
					if (opType == ExplainedNumber.StatExplainer.OperationType.Base)
					{
						this.BaseLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
						return;
					}
					if (opType == ExplainedNumber.StatExplainer.OperationType.LimitMin)
					{
						this.LimitMinLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
						return;
					}
					if (opType == ExplainedNumber.StatExplainer.OperationType.LimitMax)
					{
						this.LimitMaxLine = new ExplainedNumber.StatExplainer.ExplanationLine?(explanationLine);
					}
					return;
				}
			}

			// Token: 0x02000786 RID: 1926
			public enum OperationType
			{
				// Token: 0x04001F62 RID: 8034
				Base,
				// Token: 0x04001F63 RID: 8035
				Add,
				// Token: 0x04001F64 RID: 8036
				Multiply,
				// Token: 0x04001F65 RID: 8037
				LimitMin,
				// Token: 0x04001F66 RID: 8038
				LimitMax
			}

			// Token: 0x02000787 RID: 1927
			public readonly struct ExplanationLine
			{
				// Token: 0x06005A25 RID: 23077 RVA: 0x00184DA3 File Offset: 0x00182FA3
				public ExplanationLine(string name, float number, ExplainedNumber.StatExplainer.OperationType operationType)
				{
					this.Name = name;
					this.Number = number;
					this.OperationType = operationType;
				}

				// Token: 0x04001F67 RID: 8039
				public readonly float Number;

				// Token: 0x04001F68 RID: 8040
				public readonly string Name;

				// Token: 0x04001F69 RID: 8041
				public readonly ExplainedNumber.StatExplainer.OperationType OperationType;
			}
		}
	}
}
