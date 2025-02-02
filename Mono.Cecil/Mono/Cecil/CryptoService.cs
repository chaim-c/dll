using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Mono.Cecil.PE;

namespace Mono.Cecil
{
	// Token: 0x020000F5 RID: 245
	internal static class CryptoService
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0001E514 File Offset: 0x0001C714
		public static void StrongName(Stream stream, ImageWriter writer, StrongNameKeyPair key_pair)
		{
			int strong_name_pointer;
			byte[] strong_name = CryptoService.CreateStrongName(key_pair, CryptoService.HashStream(stream, writer, out strong_name_pointer));
			CryptoService.PatchStrongName(stream, strong_name_pointer, strong_name);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001E539 File Offset: 0x0001C739
		private static void PatchStrongName(Stream stream, int strong_name_pointer, byte[] strong_name)
		{
			stream.Seek((long)strong_name_pointer, SeekOrigin.Begin);
			stream.Write(strong_name, 0, strong_name.Length);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001E550 File Offset: 0x0001C750
		private static byte[] CreateStrongName(StrongNameKeyPair key_pair, byte[] hash)
		{
			byte[] result;
			using (RSA rsa = key_pair.CreateRSA())
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm("SHA1");
				byte[] array = rsapkcs1SignatureFormatter.CreateSignature(hash);
				Array.Reverse(array);
				result = array;
			}
			return result;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		private static byte[] HashStream(Stream stream, ImageWriter writer, out int strong_name_pointer)
		{
			Section text = writer.text;
			int headerSize = (int)writer.GetHeaderSize();
			int pointerToRawData = (int)text.PointerToRawData;
			DataDirectory strongNameSignatureDirectory = writer.GetStrongNameSignatureDirectory();
			if (strongNameSignatureDirectory.Size == 0U)
			{
				throw new InvalidOperationException();
			}
			strong_name_pointer = (int)((long)pointerToRawData + (long)((ulong)(strongNameSignatureDirectory.VirtualAddress - text.VirtualAddress)));
			int size = (int)strongNameSignatureDirectory.Size;
			SHA1Managed sha1Managed = new SHA1Managed();
			byte[] buffer = new byte[8192];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, CryptoStreamMode.Write))
			{
				stream.Seek(0L, SeekOrigin.Begin);
				CryptoService.CopyStreamChunk(stream, cryptoStream, buffer, headerSize);
				stream.Seek((long)pointerToRawData, SeekOrigin.Begin);
				CryptoService.CopyStreamChunk(stream, cryptoStream, buffer, strong_name_pointer - pointerToRawData);
				stream.Seek((long)size, SeekOrigin.Current);
				CryptoService.CopyStreamChunk(stream, cryptoStream, buffer, (int)(stream.Length - (long)(strong_name_pointer + size)));
			}
			return sha1Managed.Hash;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001E694 File Offset: 0x0001C894
		private static void CopyStreamChunk(Stream stream, Stream dest_stream, byte[] buffer, int length)
		{
			while (length > 0)
			{
				int num = stream.Read(buffer, 0, Math.Min(buffer.Length, length));
				dest_stream.Write(buffer, 0, num);
				length -= num;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public static byte[] ComputeHash(string file)
		{
			if (!File.Exists(file))
			{
				return Empty<byte>.Array;
			}
			SHA1Managed sha1Managed = new SHA1Managed();
			using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				byte[] buffer = new byte[8192];
				using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, CryptoStreamMode.Write))
				{
					CryptoService.CopyStreamChunk(fileStream, cryptoStream, buffer, (int)fileStream.Length);
				}
			}
			return sha1Managed.Hash;
		}
	}
}
