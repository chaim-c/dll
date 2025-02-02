﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C7 RID: 199
	[NullableContext(1)]
	[Nullable(0)]
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>, IConvertible
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D134 File Offset: 0x0002B334
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return AsyncUtils.CompletedTask;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				return writer.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					return writer.WriteValueAsync(value3, cancellationToken);
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					return writer.WriteValueAsync(value4, cancellationToken);
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					return writer.WriteValueAsync(value5, cancellationToken);
				}
				value2 = this._value;
				if (value2 is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					return writer.WriteValueAsync(bigInteger, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToInt64(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					return writer.WriteValueAsync(value6, cancellationToken);
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					return writer.WriteValueAsync(value7, cancellationToken);
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					return writer.WriteValueAsync(value8, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDouble(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				return writer.WriteValueAsync((value9 != null) ? value9.ToString() : null, cancellationToken);
			}
			case JTokenType.Boolean:
				return writer.WriteValueAsync(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture), cancellationToken);
			case JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					return writer.WriteValueAsync(value10, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				return writer.WriteRawValueAsync((value11 != null) ? value11.ToString() : null, cancellationToken);
			}
			case JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])this._value, cancellationToken);
			case JTokenType.Guid:
				return writer.WriteValueAsync((this._value != null) ? ((Guid?)this._value) : null, cancellationToken);
			case JTokenType.Uri:
				return writer.WriteValueAsync((Uri)this._value, cancellationToken);
			case JTokenType.TimeSpan:
				return writer.WriteValueAsync((this._value != null) ? ((TimeSpan?)this._value) : null, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D43A File Offset: 0x0002B63A
		[NullableContext(2)]
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D450 File Offset: 0x0002B650
		public JValue(JValue other) : this(other.Value, other.Type)
		{
			base.CopyAnnotations(this, other);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002D46C File Offset: 0x0002B66C
		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002D47B File Offset: 0x0002B67B
		public JValue(decimal value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002D48A File Offset: 0x0002B68A
		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002D499 File Offset: 0x0002B699
		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002D4A8 File Offset: 0x0002B6A8
		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002D4B7 File Offset: 0x0002B6B7
		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D4C6 File Offset: 0x0002B6C6
		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002D4D6 File Offset: 0x0002B6D6
		public JValue(DateTimeOffset value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002D4E6 File Offset: 0x0002B6E6
		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D4F6 File Offset: 0x0002B6F6
		[NullableContext(2)]
		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D500 File Offset: 0x0002B700
		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D510 File Offset: 0x0002B710
		[NullableContext(2)]
		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D528 File Offset: 0x0002B728
		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D538 File Offset: 0x0002B738
		[NullableContext(2)]
		public JValue(object value) : this(value, JValue.GetValueType(null, value))
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002D55C File Offset: 0x0002B75C
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0002D582 File Offset: 0x0002B782
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002D588 File Offset: 0x0002B788
		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002D614 File Offset: 0x0002B814
		[NullableContext(2)]
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string strA = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string strB = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(strA, strB);
			}
			case JTokenType.Integer:
				if (objA is BigInteger)
				{
					BigInteger i = (BigInteger)objA;
					return JValue.CompareBigInteger(i, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i2 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i2, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is BigInteger)
				{
					BigInteger i3 = (BigInteger)objA;
					return JValue.CompareBigInteger(i3, objB);
				}
				if (objB is BigInteger)
				{
					BigInteger i4 = (BigInteger)objB;
					return -JValue.CompareBigInteger(i4, objA);
				}
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool value = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(value);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime value2;
					if (objB is DateTimeOffset)
					{
						value2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						value2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(value2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset other;
				if (objB is DateTimeOffset)
				{
					other = (DateTimeOffset)objB;
				}
				else
				{
					other = new DateTimeOffset(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(other);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid value3 = (Guid)objB;
				return guid.CompareTo(value3);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan value4 = (TimeSpan)objB;
				return timeSpan.CompareTo(value4);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002D940 File Offset: 0x0002BB40
		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002D978 File Offset: 0x0002BB78
		[NullableContext(2)]
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == ExpressionType.Add || operation == ExpressionType.AddAssign))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is BigInteger || objB is BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_393;
							}
							goto IL_DE;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_CE;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_393;
						}
						goto IL_BE;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_393;
						}
						goto IL_DE;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_CE;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_393;
					}
					goto IL_BE;
				}
				result = bigInteger + bigInteger2;
				return true;
				IL_BE:
				result = bigInteger - bigInteger2;
				return true;
				IL_CE:
				result = bigInteger * bigInteger2;
				return true;
				IL_DE:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal d = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal d2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_393;
							}
							goto IL_1AD;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_19D;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_393;
						}
						goto IL_18D;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_393;
						}
						goto IL_1AD;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_19D;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_393;
					}
					goto IL_18D;
				}
				result = d + d2;
				return true;
				IL_18D:
				result = d - d2;
				return true;
				IL_19D:
				result = d * d2;
				return true;
				IL_1AD:
				result = d / d2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_393;
							}
							goto IL_278;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_26A;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_393;
						}
						goto IL_25C;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_393;
						}
						goto IL_278;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_26A;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_393;
					}
					goto IL_25C;
				}
				result = num + num2;
				return true;
				IL_25C:
				result = num - num2;
				return true;
				IL_26A:
				result = num * num2;
				return true;
				IL_278:
				result = num / num2;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num3 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num4 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= ExpressionType.Subtract)
				{
					if (operation <= ExpressionType.Divide)
					{
						if (operation != ExpressionType.Add)
						{
							if (operation != ExpressionType.Divide)
							{
								goto IL_393;
							}
							goto IL_385;
						}
					}
					else
					{
						if (operation == ExpressionType.Multiply)
						{
							goto IL_377;
						}
						if (operation != ExpressionType.Subtract)
						{
							goto IL_393;
						}
						goto IL_369;
					}
				}
				else if (operation <= ExpressionType.DivideAssign)
				{
					if (operation != ExpressionType.AddAssign)
					{
						if (operation != ExpressionType.DivideAssign)
						{
							goto IL_393;
						}
						goto IL_385;
					}
				}
				else
				{
					if (operation == ExpressionType.MultiplyAssign)
					{
						goto IL_377;
					}
					if (operation != ExpressionType.SubtractAssign)
					{
						goto IL_393;
					}
					goto IL_369;
				}
				result = num3 + num4;
				return true;
				IL_369:
				result = num3 - num4;
				return true;
				IL_377:
				result = num3 * num4;
				return true;
				IL_385:
				result = num3 / num4;
				return true;
			}
			IL_393:
			result = null;
			return false;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002DD1C File Offset: 0x0002BF1C
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002DD24 File Offset: 0x0002BF24
		public static JValue CreateComment([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002DD2D File Offset: 0x0002BF2D
		public static JValue CreateString([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002DD36 File Offset: 0x0002BF36
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002DD40 File Offset: 0x0002BF40
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002DD4C File Offset: 0x0002BF4C
		[NullableContext(2)]
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is BigInteger)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002DE50 File Offset: 0x0002C050
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002DE86 File Offset: 0x0002C086
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002DE8E File Offset: 0x0002C08E
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0002DE98 File Offset: 0x0002C098
		[Nullable(2)]
		public new object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
			[NullableContext(2)]
			set
			{
				object value2 = this._value;
				Type left = (value2 != null) ? value2.GetType() : null;
				Type right = (value != null) ? value.GetType() : null;
				if (left != right)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002DEEC File Offset: 0x0002C0EC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					writer.WriteValue(value3);
					return;
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					writer.WriteValue(value4);
					return;
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					writer.WriteValue(value5);
					return;
				}
				value2 = this._value;
				if (value2 is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value2;
					writer.WriteValue(bigInteger);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					writer.WriteValue(value6);
					return;
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					writer.WriteValue(value7);
					return;
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					writer.WriteValue(value8);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				writer.WriteValue((value9 != null) ? value9.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					writer.WriteValue(value10);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				writer.WriteRawValue((value11 != null) ? value11.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : null);
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002E20C File Offset: 0x0002C40C
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002E23E File Offset: 0x0002C43E
		[NullableContext(2)]
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002E24C File Offset: 0x0002C44C
		public override bool Equals(object obj)
		{
			JValue jvalue = obj as JValue;
			return jvalue != null && this.Equals(jvalue);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002E26C File Offset: 0x0002C46C
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002E283 File Offset: 0x0002C483
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E29E File Offset: 0x0002C49E
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public string ToString([Nullable(2)] string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E2F6 File Offset: 0x0002C4F6
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E304 File Offset: 0x0002C504
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object objB;
			JTokenType valueType;
			if (jvalue != null)
			{
				objB = jvalue.Value;
				valueType = ((this._valueType == JTokenType.String && this._valueType != jvalue._valueType) ? jvalue._valueType : this._valueType);
			}
			else
			{
				objB = obj;
				valueType = this._valueType;
			}
			return JValue.Compare(valueType, this._value, objB);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E365 File Offset: 0x0002C565
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare((this._valueType == JTokenType.String && this._valueType != obj._valueType) ? obj._valueType : this._valueType, this._value, obj._value);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
		TypeCode IConvertible.GetTypeCode()
		{
			if (this._value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = this._value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002E3D2 File Offset: 0x0002C5D2
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return (bool)this;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E3DA File Offset: 0x0002C5DA
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return (char)this;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002E3E2 File Offset: 0x0002C5E2
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return (sbyte)this;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002E3EA File Offset: 0x0002C5EA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return (byte)this;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002E3F2 File Offset: 0x0002C5F2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return (short)this;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002E3FA File Offset: 0x0002C5FA
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return (ushort)this;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002E402 File Offset: 0x0002C602
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002E40A File Offset: 0x0002C60A
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return (uint)this;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002E412 File Offset: 0x0002C612
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return (long)this;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002E41A File Offset: 0x0002C61A
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return (ulong)this;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002E422 File Offset: 0x0002C622
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return (float)this;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002E42B File Offset: 0x0002C62B
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002E434 File Offset: 0x0002C634
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return (decimal)this;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002E43C File Offset: 0x0002C63C
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return (DateTime)this;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002E444 File Offset: 0x0002C644
		[return: Nullable(2)]
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return base.ToObject(conversionType);
		}

		// Token: 0x0400039D RID: 925
		private JTokenType _valueType;

		// Token: 0x0400039E RID: 926
		[Nullable(2)]
		private object _value;

		// Token: 0x020001CF RID: 463
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x06000FC3 RID: 4035 RVA: 0x00045184 File Offset: 0x00043384
			public override bool TryConvert(JValue instance, ConvertBinder binder, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x06000FC4 RID: 4036 RVA: 0x000451F4 File Offset: 0x000433F4
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				JValue jvalue = arg as JValue;
				object objB = (jvalue != null) ? jvalue.Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= ExpressionType.NotEqual)
				{
					if (operation <= ExpressionType.LessThanOrEqual)
					{
						if (operation != ExpressionType.Add)
						{
							switch (operation)
							{
							case ExpressionType.Divide:
								break;
							case ExpressionType.Equal:
								result = (JValue.Compare(instance.Type, instance.Value, objB) == 0);
								return true;
							case ExpressionType.ExclusiveOr:
							case ExpressionType.Invoke:
							case ExpressionType.Lambda:
							case ExpressionType.LeftShift:
								goto IL_18D;
							case ExpressionType.GreaterThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) > 0);
								return true;
							case ExpressionType.GreaterThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) >= 0);
								return true;
							case ExpressionType.LessThan:
								result = (JValue.Compare(instance.Type, instance.Value, objB) < 0);
								return true;
							case ExpressionType.LessThanOrEqual:
								result = (JValue.Compare(instance.Type, instance.Value, objB) <= 0);
								return true;
							default:
								goto IL_18D;
							}
						}
					}
					else if (operation != ExpressionType.Multiply)
					{
						if (operation != ExpressionType.NotEqual)
						{
							goto IL_18D;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= ExpressionType.AddAssign)
				{
					if (operation != ExpressionType.Subtract && operation != ExpressionType.AddAssign)
					{
						goto IL_18D;
					}
				}
				else if (operation != ExpressionType.DivideAssign && operation != ExpressionType.MultiplyAssign && operation != ExpressionType.SubtractAssign)
				{
					goto IL_18D;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_18D:
				result = null;
				return false;
			}
		}
	}
}
