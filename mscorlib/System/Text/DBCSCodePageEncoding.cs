﻿using System;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A7D RID: 2685
	[Serializable]
	internal class DBCSCodePageEncoding : BaseCodePageEncoding, ISerializable
	{
		// Token: 0x060068BE RID: 26814 RVA: 0x001626C4 File Offset: 0x001608C4
		[SecurityCritical]
		public DBCSCodePageEncoding(int codePage) : this(codePage, codePage)
		{
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x001626CE File Offset: 0x001608CE
		[SecurityCritical]
		internal DBCSCodePageEncoding(int codePage, int dataCodePage)
		{
			this.mapBytesToUnicode = null;
			this.mapUnicodeToBytes = null;
			this.mapCodePageCached = null;
			base..ctor(codePage, dataCodePage);
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x001626F0 File Offset: 0x001608F0
		[SecurityCritical]
		internal DBCSCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			this.mapBytesToUnicode = null;
			this.mapUnicodeToBytes = null;
			this.mapCodePageCached = null;
			base..ctor(0);
			throw new ArgumentNullException("this");
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0016271C File Offset: 0x0016091C
		[SecurityCritical]
		protected unsafe override void LoadManagedCodePage()
		{
			if (this.pCodePage->ByteCount != 2)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[]
				{
					this.CodePage
				}));
			}
			this.bytesUnknown = this.pCodePage->ByteReplace;
			this.charUnknown = this.pCodePage->UnicodeReplace;
			if (base.DecoderFallback.IsMicrosoftBestFitFallback)
			{
				((InternalDecoderBestFitFallback)base.DecoderFallback).cReplacement = this.charUnknown;
			}
			this.byteCountUnknown = 1;
			if (this.bytesUnknown > 255)
			{
				this.byteCountUnknown++;
			}
			byte* sharedMemory = base.GetSharedMemory(262148 + this.iExtraBytes);
			this.mapBytesToUnicode = (char*)sharedMemory;
			this.mapUnicodeToBytes = (ushort*)(sharedMemory + 131072);
			this.mapCodePageCached = (int*)(sharedMemory + 262144 + this.iExtraBytes);
			if (*this.mapCodePageCached == 0)
			{
				char* ptr = (char*)(&this.pCodePage->FirstDataWord);
				int i = 0;
				int num = 0;
				while (i < 65536)
				{
					char c = *ptr;
					ptr++;
					if (c == '\u0001')
					{
						i = (int)(*ptr);
						ptr++;
					}
					else if (c < ' ' && c > '\0')
					{
						i += (int)c;
					}
					else
					{
						if (c == '￿')
						{
							num = i;
							c = (char)i;
						}
						else if (c == '￾')
						{
							num = i;
						}
						else
						{
							if (c == '�')
							{
								i++;
								continue;
							}
							num = i;
						}
						if (this.CleanUpBytes(ref num))
						{
							if (c != '￾')
							{
								this.mapUnicodeToBytes[(IntPtr)c] = (ushort)num;
							}
							this.mapBytesToUnicode[num] = c;
						}
						i++;
					}
				}
				this.CleanUpEndBytes(this.mapBytesToUnicode);
				if (this.bFlagDataTable)
				{
					*this.mapCodePageCached = this.dataTableCodePage;
				}
				return;
			}
			if ((*this.mapCodePageCached != this.dataTableCodePage && this.bFlagDataTable) || (*this.mapCodePageCached != this.CodePage && !this.bFlagDataTable))
			{
				throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
			}
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x00162912 File Offset: 0x00160B12
		protected virtual bool CleanUpBytes(ref int bytes)
		{
			return true;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x00162915 File Offset: 0x00160B15
		[SecurityCritical]
		protected unsafe virtual void CleanUpEndBytes(char* chars)
		{
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060068C4 RID: 26820 RVA: 0x00162918 File Offset: 0x00160B18
		private static object InternalSyncObject
		{
			get
			{
				if (DBCSCodePageEncoding.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref DBCSCodePageEncoding.s_InternalSyncObject, value, null);
				}
				return DBCSCodePageEncoding.s_InternalSyncObject;
			}
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x00162944 File Offset: 0x00160B44
		[SecurityCritical]
		protected unsafe override void ReadBestFitTable()
		{
			object internalSyncObject = DBCSCodePageEncoding.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.arrayUnicodeBestFit == null)
				{
					char* ptr = (char*)(&this.pCodePage->FirstDataWord);
					int i = 0;
					while (i < 65536)
					{
						char c = *ptr;
						ptr++;
						if (c == '\u0001')
						{
							i = (int)(*ptr);
							ptr++;
						}
						else if (c < ' ' && c > '\0')
						{
							i += (int)c;
						}
						else
						{
							i++;
						}
					}
					char* ptr2 = ptr;
					int num = 0;
					i = (int)(*ptr);
					ptr++;
					while (i < 65536)
					{
						char c2 = *ptr;
						ptr++;
						if (c2 == '\u0001')
						{
							i = (int)(*ptr);
							ptr++;
						}
						else if (c2 < ' ' && c2 > '\0')
						{
							i += (int)c2;
						}
						else
						{
							if (c2 != '�')
							{
								int num2 = i;
								if (this.CleanUpBytes(ref num2) && this.mapBytesToUnicode[num2] != c2)
								{
									num++;
								}
							}
							i++;
						}
					}
					char[] array = new char[num * 2];
					num = 0;
					ptr = ptr2;
					i = (int)(*ptr);
					ptr++;
					bool flag2 = false;
					while (i < 65536)
					{
						char c3 = *ptr;
						ptr++;
						if (c3 == '\u0001')
						{
							i = (int)(*ptr);
							ptr++;
						}
						else if (c3 < ' ' && c3 > '\0')
						{
							i += (int)c3;
						}
						else
						{
							if (c3 != '�')
							{
								int num3 = i;
								if (this.CleanUpBytes(ref num3) && this.mapBytesToUnicode[num3] != c3)
								{
									if (num3 != i)
									{
										flag2 = true;
									}
									array[num++] = (char)num3;
									array[num++] = c3;
								}
							}
							i++;
						}
					}
					if (flag2)
					{
						for (int j = 0; j < array.Length - 2; j += 2)
						{
							int num4 = j;
							char c4 = array[j];
							for (int k = j + 2; k < array.Length; k += 2)
							{
								if (c4 > array[k])
								{
									c4 = array[k];
									num4 = k;
								}
							}
							if (num4 != j)
							{
								char c5 = array[num4];
								array[num4] = array[j];
								array[j] = c5;
								c5 = array[num4 + 1];
								array[num4 + 1] = array[j + 1];
								array[j + 1] = c5;
							}
						}
					}
					this.arrayBytesBestFit = array;
					char* ptr3 = ptr;
					int l = (int)(*(ptr++));
					num = 0;
					while (l < 65536)
					{
						char c6 = *ptr;
						ptr++;
						if (c6 == '\u0001')
						{
							l = (int)(*ptr);
							ptr++;
						}
						else if (c6 < ' ' && c6 > '\0')
						{
							l += (int)c6;
						}
						else
						{
							if (c6 > '\0')
							{
								num++;
							}
							l++;
						}
					}
					array = new char[num * 2];
					ptr = ptr3;
					l = (int)(*(ptr++));
					num = 0;
					while (l < 65536)
					{
						char c7 = *ptr;
						ptr++;
						if (c7 == '\u0001')
						{
							l = (int)(*ptr);
							ptr++;
						}
						else if (c7 < ' ' && c7 > '\0')
						{
							l += (int)c7;
						}
						else
						{
							if (c7 > '\0')
							{
								int num5 = (int)c7;
								if (this.CleanUpBytes(ref num5))
								{
									array[num++] = (char)l;
									array[num++] = this.mapBytesToUnicode[num5];
								}
							}
							l++;
						}
					}
					this.arrayUnicodeBestFit = array;
				}
			}
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x00162C68 File Offset: 0x00160E68
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			base.CheckMemorySection();
			char c = '\0';
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				if (encoder.InternalHasFallbackBuffer && encoder.FallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
					{
						this.EncodingName,
						encoder.Fallback.GetType()
					}));
				}
			}
			int num = 0;
			char* ptr = chars + count;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (c > '\0')
			{
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, false);
				encoderFallbackBuffer.InternalFallback(c, ref chars);
			}
			char c2;
			while ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c2 == '\0')
				{
					c2 = *chars;
					chars++;
				}
				ushort num2 = this.mapUnicodeToBytes[(IntPtr)c2];
				if (num2 == 0 && c2 != '\0')
				{
					if (encoderFallbackBuffer == null)
					{
						if (encoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = encoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(ptr - count, ptr, encoder, false);
					}
					encoderFallbackBuffer.InternalFallback(c2, ref chars);
				}
				else
				{
					num++;
					if (num2 >= 256)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x00162D80 File Offset: 0x00160F80
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			base.CheckMemorySection();
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars + charCount;
			char* ptr2 = chars;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			if (encoder != null)
			{
				char charLeftOver = encoder.charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, true);
				if (encoder.m_throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
					{
						this.EncodingName,
						encoder.Fallback.GetType()
					}));
				}
				if (charLeftOver > '\0')
				{
					encoderFallbackBuffer.InternalFallback(charLeftOver, ref chars);
				}
			}
			char c;
			while ((c = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c == '\0')
				{
					c = *chars;
					chars++;
				}
				ushort num = this.mapUnicodeToBytes[(IntPtr)c];
				if (num == 0 && c != '\0')
				{
					if (encoderFallbackBuffer == null)
					{
						encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						encoderFallbackBuffer.InternalInitialize(ptr - charCount, ptr, encoder, true);
					}
					encoderFallbackBuffer.InternalFallback(c, ref chars);
				}
				else
				{
					if (num >= 256)
					{
						if (bytes + 1 >= ptr4)
						{
							if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
							{
								chars--;
							}
							else
							{
								encoderFallbackBuffer.MovePrevious();
							}
							base.ThrowBytesOverflow(encoder, chars == ptr2);
							break;
						}
						*bytes = (byte)(num >> 8);
						bytes++;
					}
					else if (bytes >= ptr4)
					{
						if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
						{
							chars--;
						}
						else
						{
							encoderFallbackBuffer.MovePrevious();
						}
						base.ThrowBytesOverflow(encoder, chars == ptr2);
						break;
					}
					*bytes = (byte)(num & 255);
					bytes++;
				}
			}
			if (encoder != null)
			{
				if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
				{
					encoder.charLeftOver = '\0';
				}
				encoder.m_charsUsed = (int)((long)(chars - ptr2));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x00162F40 File Offset: 0x00161140
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			base.CheckMemorySection();
			DBCSCodePageEncoding.DBCSDecoder dbcsdecoder = (DBCSCodePageEncoding.DBCSDecoder)baseDecoder;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			byte* ptr = bytes + count;
			int num = count;
			if (dbcsdecoder != null && dbcsdecoder.bLeftOver > 0)
			{
				if (count == 0)
				{
					if (!dbcsdecoder.MustFlush)
					{
						return 0;
					}
					decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
					decoderFallbackBuffer.InternalInitialize(bytes, null);
					byte[] bytes2 = new byte[]
					{
						dbcsdecoder.bLeftOver
					};
					return decoderFallbackBuffer.InternalFallback(bytes2, bytes);
				}
				else
				{
					int num2 = (int)dbcsdecoder.bLeftOver << 8;
					num2 |= (int)(*bytes);
					bytes++;
					if (this.mapBytesToUnicode[num2] == '\0' && num2 != 0)
					{
						num--;
						decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
						decoderFallbackBuffer.InternalInitialize(ptr - count, null);
						byte[] bytes3 = new byte[]
						{
							(byte)(num2 >> 8),
							(byte)num2
						};
						num += decoderFallbackBuffer.InternalFallback(bytes3, bytes);
					}
				}
			}
			while (bytes < ptr)
			{
				int num3 = (int)(*bytes);
				bytes++;
				char c = this.mapBytesToUnicode[num3];
				if (c == '￾')
				{
					num--;
					if (bytes < ptr)
					{
						num3 <<= 8;
						num3 |= (int)(*bytes);
						bytes++;
						c = this.mapBytesToUnicode[num3];
					}
					else
					{
						if (dbcsdecoder != null && !dbcsdecoder.MustFlush)
						{
							break;
						}
						num++;
						c = '\0';
					}
				}
				if (c == '\0' && num3 != 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (dbcsdecoder == null)
						{
							decoderFallbackBuffer = base.DecoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr - count, null);
					}
					num--;
					byte[] bytes4;
					if (num3 < 256)
					{
						bytes4 = new byte[]
						{
							(byte)num3
						};
					}
					else
					{
						bytes4 = new byte[]
						{
							(byte)(num3 >> 8),
							(byte)num3
						};
					}
					num += decoderFallbackBuffer.InternalFallback(bytes4, bytes);
				}
			}
			return num;
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x001630F4 File Offset: 0x001612F4
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			base.CheckMemorySection();
			DBCSCodePageEncoding.DBCSDecoder dbcsdecoder = (DBCSCodePageEncoding.DBCSDecoder)baseDecoder;
			byte* ptr = bytes;
			byte* ptr2 = bytes + byteCount;
			char* ptr3 = chars;
			char* ptr4 = chars + charCount;
			bool flag = false;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (dbcsdecoder != null && dbcsdecoder.bLeftOver > 0)
			{
				if (byteCount == 0)
				{
					if (!dbcsdecoder.MustFlush)
					{
						return 0;
					}
					decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
					decoderFallbackBuffer.InternalInitialize(bytes, ptr4);
					byte[] bytes2 = new byte[]
					{
						dbcsdecoder.bLeftOver
					};
					if (!decoderFallbackBuffer.InternalFallback(bytes2, bytes, ref chars))
					{
						base.ThrowCharsOverflow(dbcsdecoder, true);
					}
					dbcsdecoder.bLeftOver = 0;
					return (int)((long)(chars - ptr3));
				}
				else
				{
					int num = (int)dbcsdecoder.bLeftOver << 8;
					num |= (int)(*bytes);
					bytes++;
					char c = this.mapBytesToUnicode[num];
					if (c == '\0' && num != 0)
					{
						decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
						decoderFallbackBuffer.InternalInitialize(ptr2 - byteCount, ptr4);
						byte[] bytes3 = new byte[]
						{
							(byte)(num >> 8),
							(byte)num
						};
						if (!decoderFallbackBuffer.InternalFallback(bytes3, bytes, ref chars))
						{
							base.ThrowCharsOverflow(dbcsdecoder, true);
						}
					}
					else
					{
						if (chars >= ptr4)
						{
							base.ThrowCharsOverflow(dbcsdecoder, true);
						}
						*(chars++) = c;
					}
				}
			}
			while (bytes < ptr2)
			{
				int num2 = (int)(*bytes);
				bytes++;
				char c2 = this.mapBytesToUnicode[num2];
				if (c2 == '￾')
				{
					if (bytes < ptr2)
					{
						num2 <<= 8;
						num2 |= (int)(*bytes);
						bytes++;
						c2 = this.mapBytesToUnicode[num2];
					}
					else
					{
						if (dbcsdecoder != null && !dbcsdecoder.MustFlush)
						{
							flag = true;
							dbcsdecoder.bLeftOver = (byte)num2;
							break;
						}
						c2 = '\0';
					}
				}
				if (c2 == '\0' && num2 != 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (dbcsdecoder == null)
						{
							decoderFallbackBuffer = base.DecoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = dbcsdecoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr2 - byteCount, ptr4);
					}
					byte[] array;
					if (num2 < 256)
					{
						array = new byte[]
						{
							(byte)num2
						};
					}
					else
					{
						array = new byte[]
						{
							(byte)(num2 >> 8),
							(byte)num2
						};
					}
					if (!decoderFallbackBuffer.InternalFallback(array, bytes, ref chars))
					{
						bytes -= array.Length;
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(dbcsdecoder, bytes == ptr);
						break;
					}
				}
				else
				{
					if (chars >= ptr4)
					{
						bytes--;
						if (num2 >= 256)
						{
							bytes--;
						}
						base.ThrowCharsOverflow(dbcsdecoder, bytes == ptr);
						break;
					}
					*(chars++) = c2;
				}
			}
			if (dbcsdecoder != null)
			{
				if (!flag)
				{
					dbcsdecoder.bLeftOver = 0;
				}
				dbcsdecoder.m_bytesUsed = (int)((long)(bytes - ptr));
			}
			return (int)((long)(chars - ptr3));
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x00163374 File Offset: 0x00161574
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
			num *= 2L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x001633E4 File Offset: 0x001615E4
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

		// Token: 0x060068CC RID: 26828 RVA: 0x0016344D File Offset: 0x0016164D
		public override Decoder GetDecoder()
		{
			return new DBCSCodePageEncoding.DBCSDecoder(this);
		}

		// Token: 0x04002EFD RID: 12029
		[SecurityCritical]
		[NonSerialized]
		protected unsafe char* mapBytesToUnicode;

		// Token: 0x04002EFE RID: 12030
		[SecurityCritical]
		[NonSerialized]
		protected unsafe ushort* mapUnicodeToBytes;

		// Token: 0x04002EFF RID: 12031
		[SecurityCritical]
		[NonSerialized]
		protected unsafe int* mapCodePageCached;

		// Token: 0x04002F00 RID: 12032
		[NonSerialized]
		protected const char UNKNOWN_CHAR_FLAG = '\0';

		// Token: 0x04002F01 RID: 12033
		[NonSerialized]
		protected const char UNICODE_REPLACEMENT_CHAR = '�';

		// Token: 0x04002F02 RID: 12034
		[NonSerialized]
		protected const char LEAD_BYTE_CHAR = '￾';

		// Token: 0x04002F03 RID: 12035
		[NonSerialized]
		private ushort bytesUnknown;

		// Token: 0x04002F04 RID: 12036
		[NonSerialized]
		private int byteCountUnknown;

		// Token: 0x04002F05 RID: 12037
		[NonSerialized]
		protected char charUnknown;

		// Token: 0x04002F06 RID: 12038
		private static object s_InternalSyncObject;

		// Token: 0x02000CB8 RID: 3256
		[Serializable]
		internal class DBCSDecoder : DecoderNLS
		{
			// Token: 0x06007194 RID: 29076 RVA: 0x00186B24 File Offset: 0x00184D24
			public DBCSDecoder(DBCSCodePageEncoding encoding) : base(encoding)
			{
			}

			// Token: 0x06007195 RID: 29077 RVA: 0x00186B2D File Offset: 0x00184D2D
			public override void Reset()
			{
				this.bLeftOver = 0;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x17001377 RID: 4983
			// (get) Token: 0x06007196 RID: 29078 RVA: 0x00186B49 File Offset: 0x00184D49
			internal override bool HasState
			{
				get
				{
					return this.bLeftOver > 0;
				}
			}

			// Token: 0x040038C9 RID: 14537
			internal byte bLeftOver;
		}
	}
}
