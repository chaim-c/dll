﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A82 RID: 2690
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UTF8Encoding : Encoding
	{
		// Token: 0x06006914 RID: 26900 RVA: 0x0016671A File Offset: 0x0016491A
		[__DynamicallyInvokable]
		public UTF8Encoding() : this(false)
		{
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x00166723 File Offset: 0x00164923
		[__DynamicallyInvokable]
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier) : this(encoderShouldEmitUTF8Identifier, false)
		{
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x0016672D File Offset: 0x0016492D
		[__DynamicallyInvokable]
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier, bool throwOnInvalidBytes) : base(65001)
		{
			this.emitUTF8Identifier = encoderShouldEmitUTF8Identifier;
			this.isThrowException = throwOnInvalidBytes;
			if (this.isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x00166758 File Offset: 0x00164958
		internal override void SetDefaultFallbacks()
		{
			if (this.isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("�");
			this.decoderFallback = new DecoderReplacementFallback("�");
		}

		// Token: 0x06006918 RID: 26904 RVA: 0x001667A4 File Offset: 0x001649A4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCount(ptr + index, count, null);
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x0016683C File Offset: 0x00164A3C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(string chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = chars;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, chars.Length, null);
		}

		// Token: 0x0600691A RID: 26906 RVA: 0x00166875 File Offset: 0x00164A75
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetByteCount(chars, count, null);
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x001668B4 File Offset: 0x00164AB4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x001669A8 File Offset: 0x00164BA8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x00166AA4 File Offset: 0x00164CA4
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x00166B14 File Offset: 0x00164D14
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCount(ptr + index, count, null);
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x00166BA7 File Offset: 0x00164DA7
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetCharCount(bytes, count, null);
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x00166BE8 File Offset: 0x00164DE8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			int charCount = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			char[] array;
			char* ptr2;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, null);
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x00166CE4 File Offset: 0x00164EE4
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x00166D54 File Offset: 0x00164F54
		[SecuritySafeCritical]
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x00166DEC File Offset: 0x00164FEC
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars;
			char* ptr2 = ptr + count;
			int num = count;
			int num2 = 0;
			if (baseEncoder != null)
			{
				UTF8Encoding.UTF8Encoder utf8Encoder = (UTF8Encoding.UTF8Encoder)baseEncoder;
				num2 = utf8Encoder.surrogateChar;
				if (utf8Encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = utf8Encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							utf8Encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(chars, ptr2, utf8Encoder, false);
				}
			}
			for (;;)
			{
				if (ptr >= ptr2)
				{
					if (num2 == 0)
					{
						num2 = (int)((encoderFallbackBuffer != null) ? encoderFallbackBuffer.InternalGetNextChar() : '\0');
						if (num2 > 0)
						{
							num++;
							goto IL_14D;
						}
					}
					else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
					{
						num2 = (int)encoderFallbackBuffer.InternalGetNextChar();
						num++;
						if (UTF8Encoding.InRange(num2, 56320, 57343))
						{
							num2 = 65533;
							num++;
							goto IL_169;
						}
						if (num2 <= 0)
						{
							break;
						}
						goto IL_14D;
					}
					if (num2 > 0 && (baseEncoder == null || baseEncoder.MustFlush))
					{
						num++;
						goto IL_169;
					}
					goto IL_3F8;
				}
				else if (num2 > 0)
				{
					int ch = (int)(*ptr);
					num++;
					if (UTF8Encoding.InRange(ch, 56320, 57343))
					{
						num2 = 65533;
						ptr++;
						goto IL_169;
					}
					goto IL_169;
				}
				else
				{
					if (encoderFallbackBuffer != null)
					{
						num2 = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (num2 > 0)
						{
							num++;
							goto IL_14D;
						}
					}
					num2 = (int)(*ptr);
					ptr++;
				}
				IL_14D:
				if (UTF8Encoding.InRange(num2, 55296, 56319))
				{
					num--;
					continue;
				}
				IL_169:
				if (UTF8Encoding.InRange(num2, 55296, 57343))
				{
					if (encoderFallbackBuffer == null)
					{
						if (baseEncoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = baseEncoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(chars, chars + count, baseEncoder, false);
					}
					encoderFallbackBuffer.InternalFallback((char)num2, ref ptr);
					num--;
					num2 = 0;
				}
				else
				{
					if (num2 > 127)
					{
						if (num2 > 2047)
						{
							num++;
						}
						num++;
					}
					if (num < 0)
					{
						goto IL_3F8;
					}
					if (encoderFallbackBuffer != null && (num2 = (int)encoderFallbackBuffer.InternalGetNextChar()) != 0)
					{
						num++;
						goto IL_14D;
					}
					int num3 = UTF8Encoding.PtrDiff(ptr2, ptr);
					if (num3 <= 13)
					{
						char* ptr3 = ptr2;
						while (ptr < ptr3)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								goto IL_14D;
							}
						}
						goto Block_28;
					}
					num3 &= 268435455;
					char* ptr4 = ptr + num3 - 7;
					IL_3E8:
					while (ptr < ptr4)
					{
						num2 = (int)(*ptr);
						ptr++;
						if (num2 > 127)
						{
							if (num2 > 2047)
							{
								if ((num2 & 63488) == 55296)
								{
									goto IL_39A;
								}
								num++;
							}
							num++;
						}
						if ((ptr & 2) != 0)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								if (num2 > 2047)
								{
									if ((num2 & 63488) == 55296)
									{
										goto IL_39A;
									}
									num++;
								}
								num++;
							}
						}
						while (ptr < ptr4)
						{
							num2 = *(int*)ptr;
							int num4 = *(int*)(ptr + 2);
							if (((num2 | num4) & -8323200) != 0)
							{
								if (((num2 | num4) & -134154240) != 0)
								{
									goto IL_38B;
								}
								if ((num2 & -8388608) != 0)
								{
									num++;
								}
								if ((num2 & 65408) != 0)
								{
									num++;
								}
								if ((num4 & -8388608) != 0)
								{
									num++;
								}
								if ((num4 & 65408) != 0)
								{
									num++;
								}
							}
							ptr += 4;
							num2 = *(int*)ptr;
							num4 = *(int*)(ptr + 2);
							if (((num2 | num4) & -8323200) != 0)
							{
								if (((num2 | num4) & -134154240) != 0)
								{
									goto IL_38B;
								}
								if ((num2 & -8388608) != 0)
								{
									num++;
								}
								if ((num2 & 65408) != 0)
								{
									num++;
								}
								if ((num4 & -8388608) != 0)
								{
									num++;
								}
								if ((num4 & 65408) != 0)
								{
									num++;
								}
							}
							ptr += 4;
							continue;
							IL_38B:
							num2 = (int)((ushort)num2);
							ptr++;
							if (num2 > 127)
							{
								goto IL_39A;
							}
							goto IL_3E8;
						}
						break;
						IL_39A:
						if (num2 > 2047)
						{
							if (UTF8Encoding.InRange(num2, 55296, 57343))
							{
								int ch2 = (int)(*ptr);
								if (num2 > 56319 || !UTF8Encoding.InRange(ch2, 56320, 57343))
								{
									ptr--;
									break;
								}
								ptr++;
							}
							num++;
						}
						num++;
					}
					num2 = 0;
				}
			}
			num--;
			Block_28:
			IL_3F8:
			if (num < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
			}
			return num;
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x00167206 File Offset: 0x00165406
		[SecurityCritical]
		private unsafe static int PtrDiff(char* a, char* b)
		{
			return (int)((uint)((long)((a - b) / 1 * 2)) >> 1);
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x00167211 File Offset: 0x00165411
		[SecurityCritical]
		private unsafe static int PtrDiff(byte* a, byte* b)
		{
			return (int)((long)(a - b));
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x0016721A File Offset: 0x0016541A
		private static bool InRange(int ch, int start, int end)
		{
			return ch - start <= end - start;
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x00167228 File Offset: 0x00165428
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			UTF8Encoding.UTF8Encoder utf8Encoder = null;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars;
			byte* ptr2 = bytes;
			char* ptr3 = ptr + charCount;
			byte* ptr4 = ptr2 + byteCount;
			int num = 0;
			if (baseEncoder != null)
			{
				utf8Encoder = (UTF8Encoding.UTF8Encoder)baseEncoder;
				num = utf8Encoder.surrogateChar;
				if (utf8Encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = utf8Encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && utf8Encoder.m_throwOnOverflow)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							utf8Encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(chars, ptr3, utf8Encoder, true);
				}
			}
			for (;;)
			{
				if (ptr >= ptr3)
				{
					if (num == 0)
					{
						num = (int)((encoderFallbackBuffer != null) ? encoderFallbackBuffer.InternalGetNextChar() : '\0');
						if (num > 0)
						{
							goto IL_151;
						}
					}
					else if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
					{
						int num2 = num;
						num = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (UTF8Encoding.InRange(num, 56320, 57343))
						{
							num = num + (num2 << 10) + -56613888;
							goto IL_167;
						}
						if (num > 0)
						{
							goto IL_151;
						}
						goto IL_4BE;
					}
					if (num <= 0)
					{
						goto IL_4BE;
					}
					if (utf8Encoder == null)
					{
						goto IL_167;
					}
					if (utf8Encoder.MustFlush)
					{
						goto IL_167;
					}
					goto IL_4BE;
				}
				else if (num > 0)
				{
					int num3 = (int)(*ptr);
					if (UTF8Encoding.InRange(num3, 56320, 57343))
					{
						num = num3 + (num << 10) + -56613888;
						ptr++;
						goto IL_167;
					}
					goto IL_167;
				}
				else
				{
					if (encoderFallbackBuffer != null)
					{
						num = (int)encoderFallbackBuffer.InternalGetNextChar();
						if (num > 0)
						{
							goto IL_151;
						}
					}
					num = (int)(*ptr);
					ptr++;
				}
				IL_151:
				if (UTF8Encoding.InRange(num, 55296, 56319))
				{
					continue;
				}
				IL_167:
				if (UTF8Encoding.InRange(num, 55296, 57343))
				{
					if (encoderFallbackBuffer == null)
					{
						if (baseEncoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = baseEncoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(chars, ptr3, baseEncoder, true);
					}
					encoderFallbackBuffer.InternalFallback((char)num, ref ptr);
					num = 0;
				}
				else
				{
					int num4 = 1;
					if (num > 127)
					{
						if (num > 2047)
						{
							if (num > 65535)
							{
								num4++;
							}
							num4++;
						}
						num4++;
					}
					if (ptr2 != ptr4 - num4)
					{
						break;
					}
					if (num <= 127)
					{
						*ptr2 = (byte)num;
					}
					else
					{
						int num5;
						if (num <= 2047)
						{
							num5 = (int)((byte)(-64 | num >> 6));
						}
						else
						{
							if (num <= 65535)
							{
								num5 = (int)((byte)(-32 | num >> 12));
							}
							else
							{
								*ptr2 = (byte)(-16 | num >> 18);
								ptr2++;
								num5 = (-128 | (num >> 12 & 63));
							}
							*ptr2 = (byte)num5;
							ptr2++;
							num5 = (-128 | (num >> 6 & 63));
						}
						*ptr2 = (byte)num5;
						ptr2++;
						*ptr2 = (byte)(-128 | (num & 63));
					}
					ptr2++;
					if (encoderFallbackBuffer != null && (num = (int)encoderFallbackBuffer.InternalGetNextChar()) != 0)
					{
						goto IL_151;
					}
					int num6 = UTF8Encoding.PtrDiff(ptr3, ptr);
					int num7 = UTF8Encoding.PtrDiff(ptr4, ptr2);
					if (num6 <= 13)
					{
						if (num7 >= num6)
						{
							char* ptr5 = ptr3;
							while (ptr < ptr5)
							{
								num = (int)(*ptr);
								ptr++;
								if (num > 127)
								{
									goto IL_151;
								}
								*ptr2 = (byte)num;
								ptr2++;
							}
							goto Block_37;
						}
						num = 0;
					}
					else
					{
						if (num7 < num6)
						{
							num6 = num7;
						}
						char* ptr6 = ptr + num6 - 5;
						while (ptr < ptr6)
						{
							num = (int)(*ptr);
							ptr++;
							if (num <= 127)
							{
								*ptr2 = (byte)num;
								ptr2++;
								if ((ptr & 2) != 0)
								{
									num = (int)(*ptr);
									ptr++;
									if (num > 127)
									{
										goto IL_3DD;
									}
									*ptr2 = (byte)num;
									ptr2++;
								}
								while (ptr < ptr6)
								{
									num = *(int*)ptr;
									int num8 = *(int*)(ptr + 2);
									if (((num | num8) & -8323200) == 0)
									{
										*ptr2 = (byte)num;
										ptr2[1] = (byte)(num >> 16);
										ptr += 4;
										ptr2[2] = (byte)num8;
										ptr2[3] = (byte)(num8 >> 16);
										ptr2 += 4;
									}
									else
									{
										num = (int)((ushort)num);
										ptr++;
										if (num <= 127)
										{
											*ptr2 = (byte)num;
											ptr2++;
											break;
										}
										goto IL_3DD;
									}
								}
								continue;
							}
							IL_3DD:
							int num9;
							if (num <= 2047)
							{
								num9 = (-64 | num >> 6);
							}
							else
							{
								if (!UTF8Encoding.InRange(num, 55296, 57343))
								{
									num9 = (-32 | num >> 12);
								}
								else
								{
									if (num > 56319)
									{
										ptr--;
										break;
									}
									num9 = (int)(*ptr);
									ptr++;
									if (!UTF8Encoding.InRange(num9, 56320, 57343))
									{
										ptr -= 2;
										break;
									}
									num = num9 + (num << 10) + -56613888;
									*ptr2 = (byte)(-16 | num >> 18);
									ptr2++;
									num9 = (-128 | (num >> 12 & 63));
								}
								*ptr2 = (byte)num9;
								ptr6--;
								ptr2++;
								num9 = (-128 | (num >> 6 & 63));
							}
							*ptr2 = (byte)num9;
							ptr6--;
							ptr2++;
							*ptr2 = (byte)(-128 | (num & 63));
							ptr2++;
						}
						num = 0;
					}
				}
			}
			if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
			{
				encoderFallbackBuffer.MovePrevious();
				if (num > 65535)
				{
					encoderFallbackBuffer.MovePrevious();
				}
			}
			else
			{
				ptr--;
				if (num > 65535)
				{
					ptr--;
				}
			}
			base.ThrowBytesOverflow(utf8Encoder, ptr2 == bytes);
			num = 0;
			goto IL_4BE;
			Block_37:
			num = 0;
			IL_4BE:
			if (utf8Encoder != null)
			{
				utf8Encoder.surrogateChar = num;
				utf8Encoder.m_charsUsed = (int)((long)(ptr - chars));
			}
			return (int)((long)(ptr2 - bytes));
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x00167714 File Offset: 0x00165914
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			byte* ptr = bytes;
			byte* ptr2 = ptr + count;
			int num = count;
			int num2 = 0;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (baseDecoder != null)
			{
				UTF8Encoding.UTF8Decoder utf8Decoder = (UTF8Encoding.UTF8Decoder)baseDecoder;
				num2 = utf8Decoder.bits;
				num -= num2 >> 30;
			}
			IL_27:
			while (ptr < ptr2)
			{
				if (num2 == 0)
				{
					num2 = (int)(*ptr);
					ptr++;
					goto IL_10A;
				}
				int num3 = (int)(*ptr);
				ptr++;
				if ((num3 & -64) != 128)
				{
					ptr--;
					num += num2 >> 30;
				}
				else
				{
					num2 = (num2 << 6 | (num3 & 63));
					if ((num2 & 536870912) == 0)
					{
						if ((num2 & 268435456) != 0)
						{
							if ((num2 & 8388608) != 0 || UTF8Encoding.InRange(num2 & 496, 16, 256))
							{
								continue;
							}
						}
						else if ((num2 & 992) != 0)
						{
							if ((num2 & 992) != 864)
							{
								continue;
							}
						}
					}
					else
					{
						if ((num2 & 270467072) == 268435456)
						{
							num--;
							goto IL_180;
						}
						goto IL_180;
					}
				}
				IL_C7:
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, null);
				}
				num += this.FallbackInvalidByteSequence(ptr, num2, decoderFallbackBuffer);
				num2 = 0;
				continue;
				IL_180:
				int num4 = UTF8Encoding.PtrDiff(ptr2, ptr);
				if (num4 <= 13)
				{
					byte* ptr3 = ptr2;
					while (ptr < ptr3)
					{
						num2 = (int)(*ptr);
						ptr++;
						if (num2 > 127)
						{
							goto IL_10A;
						}
					}
					num2 = 0;
					break;
				}
				byte* ptr4 = ptr + num4 - 7;
				while (ptr < ptr4)
				{
					num2 = (int)(*ptr);
					ptr++;
					if (num2 <= 127)
					{
						if ((ptr & 1) != 0)
						{
							num2 = (int)(*ptr);
							ptr++;
							if (num2 > 127)
							{
								goto IL_24D;
							}
						}
						if ((ptr & 2) != 0)
						{
							num2 = (int)(*(ushort*)ptr);
							if ((num2 & 32896) != 0)
							{
								goto IL_239;
							}
							ptr += 2;
						}
						while (ptr < ptr4)
						{
							num2 = *(int*)ptr;
							int num5 = *(int*)(ptr + 4);
							if (((num2 | num5) & -2139062144) != 0)
							{
								goto IL_239;
							}
							ptr += 8;
							if (ptr >= ptr4)
							{
								break;
							}
							num2 = *(int*)ptr;
							num5 = *(int*)(ptr + 4);
							if (((num2 | num5) & -2139062144) != 0)
							{
								goto IL_239;
							}
							ptr += 8;
						}
						break;
						IL_239:
						num2 &= 255;
						ptr++;
						if (num2 <= 127)
						{
							continue;
						}
					}
					IL_24D:
					int num6 = (int)(*ptr);
					ptr++;
					if ((num2 & 64) != 0 && (num6 & -64) == 128)
					{
						num6 &= 63;
						if ((num2 & 32) != 0)
						{
							num6 |= (num2 & 15) << 6;
							if ((num2 & 16) != 0)
							{
								num2 = (int)(*ptr);
								if (!UTF8Encoding.InRange(num6 >> 4, 1, 16) || (num2 & -64) != 128)
								{
									goto IL_319;
								}
								num6 = (num6 << 6 | (num2 & 63));
								num2 = (int)ptr[1];
								if ((num2 & -64) != 128)
								{
									goto IL_319;
								}
								ptr += 2;
								num--;
							}
							else
							{
								num2 = (int)(*ptr);
								if ((num6 & 992) == 0 || (num6 & 992) == 864 || (num2 & -64) != 128)
								{
									goto IL_319;
								}
								ptr++;
								num--;
							}
						}
						else if ((num2 & 30) == 0)
						{
							goto IL_319;
						}
						num--;
						continue;
					}
					IL_319:
					ptr -= 2;
					num2 = 0;
					goto IL_27;
				}
				num2 = 0;
				continue;
				IL_10A:
				if (num2 <= 127)
				{
					goto IL_180;
				}
				num--;
				if ((num2 & 64) == 0)
				{
					goto IL_C7;
				}
				if ((num2 & 32) != 0)
				{
					if ((num2 & 16) != 0)
					{
						num2 &= 15;
						if (num2 > 4)
						{
							num2 |= 240;
							goto IL_C7;
						}
						num2 |= 1347226624;
						num--;
					}
					else
					{
						num2 = ((num2 & 15) | 1210220544);
						num--;
					}
				}
				else
				{
					num2 &= 31;
					if (num2 <= 1)
					{
						num2 |= 192;
						goto IL_C7;
					}
					num2 |= 8388608;
				}
			}
			if (num2 != 0)
			{
				num += num2 >> 30;
				if (baseDecoder == null || baseDecoder.MustFlush)
				{
					if (decoderFallbackBuffer == null)
					{
						if (baseDecoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = baseDecoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(bytes, null);
					}
					num += this.FallbackInvalidByteSequence(ptr, num2, decoderFallbackBuffer);
				}
			}
			return num;
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x00167A90 File Offset: 0x00165C90
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			byte* ptr = bytes;
			char* ptr2 = chars;
			byte* ptr3 = ptr + byteCount;
			char* ptr4 = ptr2 + charCount;
			int num = 0;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (baseDecoder != null)
			{
				UTF8Encoding.UTF8Decoder utf8Decoder = (UTF8Encoding.UTF8Decoder)baseDecoder;
				num = utf8Decoder.bits;
			}
			IL_2C:
			while (ptr < ptr3)
			{
				if (num == 0)
				{
					num = (int)(*ptr);
					ptr++;
					goto IL_161;
				}
				int num2 = (int)(*ptr);
				ptr++;
				if ((num2 & -64) != 128)
				{
					ptr--;
				}
				else
				{
					num = (num << 6 | (num2 & 63));
					if ((num & 536870912) == 0)
					{
						if ((num & 268435456) != 0)
						{
							if ((num & 8388608) != 0 || UTF8Encoding.InRange(num & 496, 16, 256))
							{
								continue;
							}
						}
						else if ((num & 992) != 0)
						{
							if ((num & 992) != 864)
							{
								continue;
							}
						}
					}
					else
					{
						if ((num & 270467072) > 268435456 && ptr2 < ptr4)
						{
							*ptr2 = (char)((num >> 10 & 2047) + -10304);
							ptr2++;
							num = (num & 1023) + 56320;
							goto IL_1E2;
						}
						goto IL_1E2;
					}
				}
				IL_FD:
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, ptr4);
				}
				if (!this.FallbackInvalidByteSequence(ref ptr, num, decoderFallbackBuffer, ref ptr2))
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
					num = 0;
					break;
				}
				num = 0;
				continue;
				IL_1E2:
				if (ptr2 >= ptr4)
				{
					num &= 2097151;
					if (num > 127)
					{
						if (num > 2047)
						{
							if (num >= 56320 && num <= 57343)
							{
								ptr--;
								ptr2--;
							}
							else if (num > 65535)
							{
								ptr--;
							}
							ptr--;
						}
						ptr--;
					}
					ptr--;
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
					num = 0;
					break;
				}
				*ptr2 = (char)num;
				ptr2++;
				int num3 = UTF8Encoding.PtrDiff(ptr4, ptr2);
				int num4 = UTF8Encoding.PtrDiff(ptr3, ptr);
				if (num4 > 13)
				{
					if (num3 < num4)
					{
						num4 = num3;
					}
					char* ptr5 = ptr2 + num4 - 7;
					while (ptr2 < ptr5)
					{
						num = (int)(*ptr);
						ptr++;
						if (num <= 127)
						{
							*ptr2 = (char)num;
							ptr2++;
							if ((ptr & 1) != 0)
							{
								num = (int)(*ptr);
								ptr++;
								if (num > 127)
								{
									goto IL_3FC;
								}
								*ptr2 = (char)num;
								ptr2++;
							}
							if ((ptr & 2) != 0)
							{
								num = (int)(*(ushort*)ptr);
								if ((num & 32896) != 0)
								{
									goto IL_3DA;
								}
								*ptr2 = (char)(num & 127);
								ptr += 2;
								ptr2[1] = (char)(num >> 8 & 127);
								ptr2 += 2;
							}
							while (ptr2 < ptr5)
							{
								num = *(int*)ptr;
								int num5 = *(int*)(ptr + 4);
								if (((num | num5) & -2139062144) != 0)
								{
									goto IL_3DA;
								}
								*ptr2 = (char)(num & 127);
								ptr2[1] = (char)(num >> 8 & 127);
								ptr2[2] = (char)(num >> 16 & 127);
								ptr2[3] = (char)(num >> 24 & 127);
								ptr += 8;
								ptr2[4] = (char)(num5 & 127);
								ptr2[5] = (char)(num5 >> 8 & 127);
								ptr2[6] = (char)(num5 >> 16 & 127);
								ptr2[7] = (char)(num5 >> 24 & 127);
								ptr2 += 8;
							}
							break;
							IL_3DA:
							num &= 255;
							ptr++;
							if (num <= 127)
							{
								*ptr2 = (char)num;
								ptr2++;
								continue;
							}
						}
						IL_3FC:
						int num6 = (int)(*ptr);
						ptr++;
						if ((num & 64) != 0 && (num6 & -64) == 128)
						{
							num6 &= 63;
							if ((num & 32) != 0)
							{
								num6 |= (num & 15) << 6;
								if ((num & 16) != 0)
								{
									num = (int)(*ptr);
									if (!UTF8Encoding.InRange(num6 >> 4, 1, 16) || (num & -64) != 128)
									{
										goto IL_53E;
									}
									num6 = (num6 << 6 | (num & 63));
									num = (int)ptr[1];
									if ((num & -64) != 128)
									{
										goto IL_53E;
									}
									ptr += 2;
									num = (num6 << 6 | (num & 63));
									*ptr2 = (char)((num >> 10 & 2047) + -10304);
									ptr2++;
									num = (num & 1023) + -9216;
									ptr5--;
								}
								else
								{
									num = (int)(*ptr);
									if ((num6 & 992) == 0 || (num6 & 992) == 864 || (num & -64) != 128)
									{
										goto IL_53E;
									}
									ptr++;
									num = (num6 << 6 | (num & 63));
									ptr5--;
								}
							}
							else
							{
								num &= 31;
								if (num <= 1)
								{
									goto IL_53E;
								}
								num = (num << 6 | num6);
							}
							*ptr2 = (char)num;
							ptr2++;
							ptr5--;
							continue;
						}
						IL_53E:
						ptr -= 2;
						num = 0;
						goto IL_2C;
					}
					num = 0;
					continue;
				}
				if (num3 < num4)
				{
					num = 0;
					continue;
				}
				byte* ptr6 = ptr3;
				while (ptr < ptr6)
				{
					num = (int)(*ptr);
					ptr++;
					if (num > 127)
					{
						goto IL_161;
					}
					*ptr2 = (char)num;
					ptr2++;
				}
				num = 0;
				break;
				IL_161:
				if (num <= 127)
				{
					goto IL_1E2;
				}
				if ((num & 64) == 0)
				{
					goto IL_FD;
				}
				if ((num & 32) != 0)
				{
					if ((num & 16) != 0)
					{
						num &= 15;
						if (num > 4)
						{
							num |= 240;
							goto IL_FD;
						}
						num |= 1347226624;
					}
					else
					{
						num = ((num & 15) | 1210220544);
					}
				}
				else
				{
					num &= 31;
					if (num <= 1)
					{
						num |= 192;
						goto IL_FD;
					}
					num |= 8388608;
				}
			}
			if (num != 0 && (baseDecoder == null || baseDecoder.MustFlush))
			{
				if (decoderFallbackBuffer == null)
				{
					if (baseDecoder == null)
					{
						decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
					}
					else
					{
						decoderFallbackBuffer = baseDecoder.FallbackBuffer;
					}
					decoderFallbackBuffer.InternalInitialize(bytes, ptr4);
				}
				if (!this.FallbackInvalidByteSequence(ref ptr, num, decoderFallbackBuffer, ref ptr2))
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(baseDecoder, ptr2 == chars);
				}
				num = 0;
			}
			if (baseDecoder != null)
			{
				UTF8Encoding.UTF8Decoder utf8Decoder2 = (UTF8Encoding.UTF8Decoder)baseDecoder;
				utf8Decoder2.bits = num;
				baseDecoder.m_bytesUsed = (int)((long)(ptr - bytes));
			}
			return UTF8Encoding.PtrDiff(ptr2, chars);
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x00168074 File Offset: 0x00166274
		[SecurityCritical]
		private unsafe bool FallbackInvalidByteSequence(ref byte* pSrc, int ch, DecoderFallbackBuffer fallback, ref char* pTarget)
		{
			byte* ptr = pSrc;
			byte[] bytesUnknown = this.GetBytesUnknown(ref ptr, ch);
			if (!fallback.InternalFallback(bytesUnknown, pSrc, ref pTarget))
			{
				pSrc = ptr;
				return false;
			}
			return true;
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x001680A4 File Offset: 0x001662A4
		[SecurityCritical]
		private unsafe int FallbackInvalidByteSequence(byte* pSrc, int ch, DecoderFallbackBuffer fallback)
		{
			byte[] bytesUnknown = this.GetBytesUnknown(ref pSrc, ch);
			return fallback.InternalFallback(bytesUnknown, pSrc);
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x001680C8 File Offset: 0x001662C8
		[SecurityCritical]
		private unsafe byte[] GetBytesUnknown(ref byte* pSrc, int ch)
		{
			byte[] result;
			if (ch < 256 && ch >= 0)
			{
				pSrc--;
				result = new byte[]
				{
					(byte)ch
				};
			}
			else if ((ch & 402653184) == 0)
			{
				pSrc--;
				result = new byte[]
				{
					(byte)((ch & 31) | 192)
				};
			}
			else if ((ch & 268435456) != 0)
			{
				if ((ch & 8388608) != 0)
				{
					pSrc -= 3;
					result = new byte[]
					{
						(byte)((ch >> 12 & 7) | 240),
						(byte)((ch >> 6 & 63) | 128),
						(byte)((ch & 63) | 128)
					};
				}
				else if ((ch & 131072) != 0)
				{
					pSrc -= 2;
					result = new byte[]
					{
						(byte)((ch >> 6 & 7) | 240),
						(byte)((ch & 63) | 128)
					};
				}
				else
				{
					pSrc--;
					result = new byte[]
					{
						(byte)((ch & 7) | 240)
					};
				}
			}
			else if ((ch & 8388608) != 0)
			{
				pSrc -= 2;
				result = new byte[]
				{
					(byte)((ch >> 6 & 15) | 224),
					(byte)((ch & 63) | 128)
				};
			}
			else
			{
				pSrc--;
				result = new byte[]
				{
					(byte)((ch & 15) | 224)
				};
			}
			return result;
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x0016821B File Offset: 0x0016641B
		[__DynamicallyInvokable]
		public override Decoder GetDecoder()
		{
			return new UTF8Encoding.UTF8Decoder(this);
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00168223 File Offset: 0x00166423
		[__DynamicallyInvokable]
		public override Encoder GetEncoder()
		{
			return new UTF8Encoding.UTF8Encoder(this);
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0016822C File Offset: 0x0016642C
		[__DynamicallyInvokable]
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 3L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x0016829C File Offset: 0x0016649C
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)byteCount + 1L;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x00168305 File Offset: 0x00166505
		[__DynamicallyInvokable]
		public override byte[] GetPreamble()
		{
			if (this.emitUTF8Identifier)
			{
				return new byte[]
				{
					239,
					187,
					191
				};
			}
			return EmptyArray<byte>.Value;
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x00168328 File Offset: 0x00166528
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			UTF8Encoding utf8Encoding = value as UTF8Encoding;
			return utf8Encoding != null && (this.emitUTF8Identifier == utf8Encoding.emitUTF8Identifier && base.EncoderFallback.Equals(utf8Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf8Encoding.DecoderFallback);
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x00168375 File Offset: 0x00166575
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + 65001 + (this.emitUTF8Identifier ? 1 : 0);
		}

		// Token: 0x04002F1B RID: 12059
		private const int UTF8_CODEPAGE = 65001;

		// Token: 0x04002F1C RID: 12060
		private bool emitUTF8Identifier;

		// Token: 0x04002F1D RID: 12061
		private bool isThrowException;

		// Token: 0x04002F1E RID: 12062
		private const int FinalByte = 536870912;

		// Token: 0x04002F1F RID: 12063
		private const int SupplimentarySeq = 268435456;

		// Token: 0x04002F20 RID: 12064
		private const int ThreeByteSeq = 134217728;

		// Token: 0x02000CBE RID: 3262
		internal sealed class UTF8EncodingSealed : UTF8Encoding
		{
			// Token: 0x060071B2 RID: 29106 RVA: 0x00187072 File Offset: 0x00185272
			public UTF8EncodingSealed(bool encoderShouldEmitUTF8Identifier) : base(encoderShouldEmitUTF8Identifier)
			{
			}
		}

		// Token: 0x02000CBF RID: 3263
		[Serializable]
		internal class UTF8Encoder : EncoderNLS, ISerializable
		{
			// Token: 0x060071B3 RID: 29107 RVA: 0x0018707B File Offset: 0x0018527B
			public UTF8Encoder(UTF8Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x060071B4 RID: 29108 RVA: 0x00187084 File Offset: 0x00185284
			internal UTF8Encoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				this.surrogateChar = (int)info.GetValue("surrogateChar", typeof(int));
				try
				{
					this.m_fallback = (EncoderFallback)info.GetValue("m_fallback", typeof(EncoderFallback));
				}
				catch (SerializationException)
				{
					this.m_fallback = null;
				}
			}

			// Token: 0x060071B5 RID: 29109 RVA: 0x00187124 File Offset: 0x00185324
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
				info.AddValue("surrogateChar", this.surrogateChar);
				info.AddValue("m_fallback", this.m_fallback);
				info.AddValue("storedSurrogate", this.surrogateChar > 0);
				info.AddValue("mustFlush", false);
			}

			// Token: 0x060071B6 RID: 29110 RVA: 0x00187196 File Offset: 0x00185396
			public override void Reset()
			{
				this.surrogateChar = 0;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700137D RID: 4989
			// (get) Token: 0x060071B7 RID: 29111 RVA: 0x001871B2 File Offset: 0x001853B2
			internal override bool HasState
			{
				get
				{
					return this.surrogateChar != 0;
				}
			}

			// Token: 0x040038D4 RID: 14548
			internal int surrogateChar;
		}

		// Token: 0x02000CC0 RID: 3264
		[Serializable]
		internal class UTF8Decoder : DecoderNLS, ISerializable
		{
			// Token: 0x060071B8 RID: 29112 RVA: 0x001871BD File Offset: 0x001853BD
			public UTF8Decoder(UTF8Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x060071B9 RID: 29113 RVA: 0x001871C8 File Offset: 0x001853C8
			internal UTF8Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.bits = (int)info.GetValue("wbits", typeof(int));
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this.bits = 0;
					this.m_fallback = null;
				}
			}

			// Token: 0x060071BA RID: 29114 RVA: 0x0018726C File Offset: 0x0018546C
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
				info.AddValue("wbits", this.bits);
				info.AddValue("m_fallback", this.m_fallback);
				info.AddValue("bits", 0);
				info.AddValue("trailCount", 0);
				info.AddValue("isSurrogate", false);
				info.AddValue("byteSequence", 0);
			}

			// Token: 0x060071BB RID: 29115 RVA: 0x001872EA File Offset: 0x001854EA
			public override void Reset()
			{
				this.bits = 0;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700137E RID: 4990
			// (get) Token: 0x060071BC RID: 29116 RVA: 0x00187306 File Offset: 0x00185506
			internal override bool HasState
			{
				get
				{
					return this.bits != 0;
				}
			}

			// Token: 0x040038D5 RID: 14549
			internal int bits;
		}
	}
}
