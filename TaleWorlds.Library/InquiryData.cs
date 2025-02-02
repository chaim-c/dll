using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200003A RID: 58
	public class InquiryData
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000731C File Offset: 0x0000551C
		public InquiryData(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action affirmativeAction, Action negativeAction, string soundEventPath = "", float expireTime = 0f, Action timeoutAction = null, Func<ValueTuple<bool, string>> isAffirmativeOptionEnabled = null, Func<ValueTuple<bool, string>> isNegativeOptionEnabled = null)
		{
			this.TitleText = titleText;
			this.Text = text;
			this.IsAffirmativeOptionShown = isAffirmativeOptionShown;
			this.IsNegativeOptionShown = isNegativeOptionShown;
			this.GetIsAffirmativeOptionEnabled = isAffirmativeOptionEnabled;
			this.GetIsNegativeOptionEnabled = isNegativeOptionEnabled;
			this.AffirmativeText = affirmativeText;
			this.NegativeText = negativeText;
			this.AffirmativeAction = affirmativeAction;
			this.NegativeAction = negativeAction;
			this.SoundEventPath = soundEventPath;
			this.ExpireTime = expireTime;
			this.TimeoutAction = timeoutAction;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007394 File Offset: 0x00005594
		public void SetText(string text)
		{
			this.Text = text;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000739D File Offset: 0x0000559D
		public void SetTitleText(string titleText)
		{
			this.TitleText = titleText;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000073A6 File Offset: 0x000055A6
		public void SetAffirmativeAction(Action newAffirmativeAction)
		{
			this.AffirmativeAction = newAffirmativeAction;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000073B0 File Offset: 0x000055B0
		public bool HasSameContentWith(object other)
		{
			InquiryData inquiryData;
			return (inquiryData = (other as InquiryData)) != null && (this.TitleText == inquiryData.TitleText && this.Text == inquiryData.Text && this.IsAffirmativeOptionShown == inquiryData.IsAffirmativeOptionShown && this.IsNegativeOptionShown == inquiryData.IsNegativeOptionShown && this.GetIsAffirmativeOptionEnabled == inquiryData.GetIsAffirmativeOptionEnabled && this.GetIsNegativeOptionEnabled == inquiryData.GetIsNegativeOptionEnabled && this.AffirmativeText == inquiryData.AffirmativeText && this.NegativeText == inquiryData.NegativeText && this.AffirmativeAction == inquiryData.AffirmativeAction && this.NegativeAction == inquiryData.NegativeAction && this.SoundEventPath == inquiryData.SoundEventPath && this.ExpireTime == inquiryData.ExpireTime) && this.TimeoutAction == inquiryData.TimeoutAction;
		}

		// Token: 0x04000097 RID: 151
		public string TitleText;

		// Token: 0x04000098 RID: 152
		public string Text;

		// Token: 0x04000099 RID: 153
		public readonly float ExpireTime;

		// Token: 0x0400009A RID: 154
		public readonly bool IsAffirmativeOptionShown;

		// Token: 0x0400009B RID: 155
		public readonly bool IsNegativeOptionShown;

		// Token: 0x0400009C RID: 156
		public readonly string AffirmativeText;

		// Token: 0x0400009D RID: 157
		public readonly string NegativeText;

		// Token: 0x0400009E RID: 158
		public readonly string SoundEventPath;

		// Token: 0x0400009F RID: 159
		public Action AffirmativeAction;

		// Token: 0x040000A0 RID: 160
		public readonly Action NegativeAction;

		// Token: 0x040000A1 RID: 161
		public readonly Action TimeoutAction;

		// Token: 0x040000A2 RID: 162
		public readonly Func<ValueTuple<bool, string>> GetIsAffirmativeOptionEnabled;

		// Token: 0x040000A3 RID: 163
		public readonly Func<ValueTuple<bool, string>> GetIsNegativeOptionEnabled;
	}
}
