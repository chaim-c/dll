using System;
using System.ComponentModel;

namespace TaleWorlds.Library
{
	// Token: 0x02000045 RID: 69
	public interface IViewModel : INotifyPropertyChanged
	{
		// Token: 0x0600023D RID: 573
		object GetViewModelAtPath(BindingPath path);

		// Token: 0x0600023E RID: 574
		object GetViewModelAtPath(BindingPath path, bool isList);

		// Token: 0x0600023F RID: 575
		object GetPropertyValue(string name);

		// Token: 0x06000240 RID: 576
		object GetPropertyValue(string name, PropertyTypeFeeder propertyTypeFeeder);

		// Token: 0x06000241 RID: 577
		void SetPropertyValue(string name, object value);

		// Token: 0x06000242 RID: 578
		void ExecuteCommand(string commandName, object[] parameters);

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000243 RID: 579
		// (remove) Token: 0x06000244 RID: 580
		event PropertyChangedWithValueEventHandler PropertyChangedWithValue;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000245 RID: 581
		// (remove) Token: 0x06000246 RID: 582
		event PropertyChangedWithBoolValueEventHandler PropertyChangedWithBoolValue;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000247 RID: 583
		// (remove) Token: 0x06000248 RID: 584
		event PropertyChangedWithIntValueEventHandler PropertyChangedWithIntValue;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000249 RID: 585
		// (remove) Token: 0x0600024A RID: 586
		event PropertyChangedWithFloatValueEventHandler PropertyChangedWithFloatValue;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600024B RID: 587
		// (remove) Token: 0x0600024C RID: 588
		event PropertyChangedWithUIntValueEventHandler PropertyChangedWithUIntValue;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600024D RID: 589
		// (remove) Token: 0x0600024E RID: 590
		event PropertyChangedWithColorValueEventHandler PropertyChangedWithColorValue;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600024F RID: 591
		// (remove) Token: 0x06000250 RID: 592
		event PropertyChangedWithDoubleValueEventHandler PropertyChangedWithDoubleValue;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000251 RID: 593
		// (remove) Token: 0x06000252 RID: 594
		event PropertyChangedWithVec2ValueEventHandler PropertyChangedWithVec2Value;
	}
}
