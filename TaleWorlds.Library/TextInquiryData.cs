using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200003B RID: 59
	public class TextInquiryData
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000074C4 File Offset: 0x000056C4
		public TextInquiryData(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action<string> affirmativeAction, Action negativeAction, bool shouldInputBeObfuscated = false, Func<string, Tuple<bool, string>> textCondition = null, string soundEventPath = "", string defaultInputText = "")
		{
			this.TitleText = titleText;
			this.Text = text;
			this.IsAffirmativeOptionShown = isAffirmativeOptionShown;
			this.IsNegativeOptionShown = isNegativeOptionShown;
			this.AffirmativeText = affirmativeText;
			this.NegativeText = negativeText;
			this.AffirmativeAction = affirmativeAction;
			this.NegativeAction = negativeAction;
			this.TextCondition = textCondition;
			this.IsInputObfuscated = shouldInputBeObfuscated;
			this.SoundEventPath = soundEventPath;
			this.DefaultInputText = defaultInputText;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007540 File Offset: 0x00005740
		public bool HasSameContentWith(object other)
		{
			TextInquiryData textInquiryData;
			return (textInquiryData = (other as TextInquiryData)) != null && (this.TitleText == textInquiryData.TitleText && this.Text == textInquiryData.Text && this.IsAffirmativeOptionShown == textInquiryData.IsAffirmativeOptionShown && this.IsNegativeOptionShown == textInquiryData.IsNegativeOptionShown && this.AffirmativeText == textInquiryData.AffirmativeText && this.NegativeText == textInquiryData.NegativeText && this.AffirmativeAction == textInquiryData.AffirmativeAction && this.NegativeAction == textInquiryData.NegativeAction && this.TextCondition == textInquiryData.TextCondition && this.IsInputObfuscated == textInquiryData.IsInputObfuscated && this.SoundEventPath == textInquiryData.SoundEventPath) && this.DefaultInputText == textInquiryData.DefaultInputText;
		}

		// Token: 0x040000A4 RID: 164
		public string TitleText;

		// Token: 0x040000A5 RID: 165
		public string Text = "";

		// Token: 0x040000A6 RID: 166
		public readonly bool IsAffirmativeOptionShown;

		// Token: 0x040000A7 RID: 167
		public readonly bool IsNegativeOptionShown;

		// Token: 0x040000A8 RID: 168
		public readonly bool IsInputObfuscated;

		// Token: 0x040000A9 RID: 169
		public readonly string AffirmativeText;

		// Token: 0x040000AA RID: 170
		public readonly string NegativeText;

		// Token: 0x040000AB RID: 171
		public readonly string SoundEventPath;

		// Token: 0x040000AC RID: 172
		public readonly string DefaultInputText;

		// Token: 0x040000AD RID: 173
		public readonly Action<string> AffirmativeAction;

		// Token: 0x040000AE RID: 174
		public readonly Action NegativeAction;

		// Token: 0x040000AF RID: 175
		public readonly Func<string, Tuple<bool, string>> TextCondition;
	}
}
