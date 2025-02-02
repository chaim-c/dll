﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x02000002 RID: 2
	public sealed class Helper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void AddCallback(out IntPtr clientDataAddress, object clientData, Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
		{
			Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
			lock (obj)
			{
				clientDataAddress = Helper.AddClientData(clientData);
				Helper.s_Callbacks.Add(clientDataAddress, new Helper.DelegateHolder(publicDelegate, privateDelegate, structDelegates));
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		private static void RemoveCallback(IntPtr clientDataAddress)
		{
			Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
			lock (obj)
			{
				Helper.s_Callbacks.Remove(clientDataAddress);
				Helper.RemoveClientData(clientDataAddress);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		internal static bool TryGetAndRemoveCallback<TCallbackInfoInternal, TCallback, TCallbackInfo>(ref TCallbackInfoInternal callbackInfoInternal, out TCallback callback, out TCallbackInfo callbackInfo) where TCallbackInfoInternal : struct, ICallbackInfoInternal, IGettable<TCallbackInfo> where TCallback : class where TCallbackInfo : struct, ICallbackInfo
		{
			IntPtr intPtr;
			Helper.Get<TCallbackInfoInternal, TCallbackInfo>(ref callbackInfoInternal, out callbackInfo, out intPtr);
			callback = default(TCallback);
			Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
			lock (obj)
			{
				Helper.DelegateHolder delegateHolder;
				bool flag2 = Helper.s_Callbacks.TryGetValue(intPtr, out delegateHolder);
				if (flag2)
				{
					callback = (delegateHolder.Public as TCallback);
					bool flag3 = callback != null;
					if (flag3)
					{
						bool flag4 = delegateHolder.NotificationId != null;
						if (!flag4)
						{
							bool flag5 = callbackInfo.GetResultCode() != null && Common.IsOperationComplete(callbackInfo.GetResultCode().Value);
							if (flag5)
							{
								Helper.RemoveCallback(intPtr);
							}
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021FC File Offset: 0x000003FC
		internal static bool TryGetStructCallback<TCallbackInfoInternal, TCallback, TCallbackInfo>(ref TCallbackInfoInternal callbackInfoInternal, out TCallback callback, out TCallbackInfo callbackInfo) where TCallbackInfoInternal : struct, ICallbackInfoInternal, IGettable<TCallbackInfo> where TCallback : class where TCallbackInfo : struct
		{
			IntPtr key;
			Helper.Get<TCallbackInfoInternal, TCallbackInfo>(ref callbackInfoInternal, out callbackInfo, out key);
			callback = default(TCallback);
			Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
			lock (obj)
			{
				Helper.DelegateHolder delegateHolder;
				bool flag2 = Helper.s_Callbacks.TryGetValue(key, out delegateHolder);
				if (flag2)
				{
					callback = (delegateHolder.StructDelegates.FirstOrDefault((Delegate structDelegate) => structDelegate.GetType() == typeof(TCallback)) as TCallback);
					bool flag3 = callback != null;
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022BC File Offset: 0x000004BC
		internal static void RemoveCallbackByNotificationId(ulong notificationId)
		{
			Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
			lock (obj)
			{
				Helper.RemoveCallback(Helper.s_Callbacks.SingleOrDefault(delegate(KeyValuePair<IntPtr, Helper.DelegateHolder> pair)
				{
					bool result;
					if (pair.Value.NotificationId != null)
					{
						ulong? notificationId2 = pair.Value.NotificationId;
						ulong notificationId3 = notificationId;
						result = (notificationId2.GetValueOrDefault() == notificationId3 & notificationId2 != null);
					}
					else
					{
						result = false;
					}
					return result;
				}).Key);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000232C File Offset: 0x0000052C
		internal static void AddStaticCallback(string key, Delegate publicDelegate, Delegate privateDelegate)
		{
			Dictionary<string, Helper.DelegateHolder> obj = Helper.s_StaticCallbacks;
			lock (obj)
			{
				Helper.s_StaticCallbacks.Remove(key);
				Helper.s_StaticCallbacks.Add(key, new Helper.DelegateHolder(publicDelegate, privateDelegate, Array.Empty<Delegate>()));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002390 File Offset: 0x00000590
		internal static bool TryGetStaticCallback<TCallback>(string key, out TCallback callback) where TCallback : class
		{
			callback = default(TCallback);
			Dictionary<string, Helper.DelegateHolder> obj = Helper.s_StaticCallbacks;
			lock (obj)
			{
				Helper.DelegateHolder delegateHolder;
				bool flag2 = Helper.s_StaticCallbacks.TryGetValue(key, out delegateHolder);
				if (flag2)
				{
					callback = (delegateHolder.Public as TCallback);
					bool flag3 = callback != null;
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002420 File Offset: 0x00000620
		internal static void AssignNotificationIdToCallback(IntPtr clientDataAddress, ulong notificationId)
		{
			bool flag = notificationId == 0UL;
			if (flag)
			{
				Helper.RemoveCallback(clientDataAddress);
			}
			else
			{
				Dictionary<IntPtr, Helper.DelegateHolder> obj = Helper.s_Callbacks;
				lock (obj)
				{
					Helper.DelegateHolder delegateHolder;
					bool flag3 = Helper.s_Callbacks.TryGetValue(clientDataAddress, out delegateHolder);
					if (flag3)
					{
						delegateHolder.NotificationId = new ulong?(notificationId);
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002494 File Offset: 0x00000694
		private static IntPtr AddClientData(object clientData)
		{
			Dictionary<IntPtr, object> obj = Helper.s_ClientDatas;
			IntPtr result;
			lock (obj)
			{
				long value = Helper.s_LastClientDataId += 1L;
				IntPtr intPtr = new IntPtr(value);
				Helper.s_ClientDatas.Add(intPtr, clientData);
				result = intPtr;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024FC File Offset: 0x000006FC
		private static void RemoveClientData(IntPtr clientDataAddress)
		{
			Dictionary<IntPtr, object> obj = Helper.s_ClientDatas;
			lock (obj)
			{
				Helper.s_ClientDatas.Remove(clientDataAddress);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002548 File Offset: 0x00000748
		private static object GetClientData(IntPtr clientDataAddress)
		{
			Dictionary<IntPtr, object> obj = Helper.s_ClientDatas;
			object result;
			lock (obj)
			{
				object obj2;
				Helper.s_ClientDatas.TryGetValue(clientDataAddress, out obj2);
				result = obj2;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002598 File Offset: 0x00000798
		private static void Convert<THandle>(IntPtr from, out THandle to) where THandle : Handle, new()
		{
			to = default(THandle);
			bool flag = from != IntPtr.Zero;
			if (flag)
			{
				to = Activator.CreateInstance<THandle>();
				to.InnerHandle = from;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025D8 File Offset: 0x000007D8
		private static void Convert(Handle from, out IntPtr to)
		{
			to = IntPtr.Zero;
			bool flag = from != null;
			if (flag)
			{
				to = from.InnerHandle;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002604 File Offset: 0x00000804
		private static void Convert(byte[] from, out string to)
		{
			to = null;
			bool flag = from == null;
			if (!flag)
			{
				to = Encoding.ASCII.GetString(from.Take(Helper.GetAnsiStringLength(from)).ToArray<byte>());
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002640 File Offset: 0x00000840
		private static void Convert(string from, out byte[] to, int fromLength)
		{
			bool flag = from == null;
			if (flag)
			{
				from = "";
			}
			to = Encoding.ASCII.GetBytes(new string(from.Take(fromLength).ToArray<char>()).PadRight(fromLength, '\0'));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002684 File Offset: 0x00000884
		private static void Convert<TArray>(TArray[] from, out int to)
		{
			to = 0;
			bool flag = from != null;
			if (flag)
			{
				to = from.Length;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026A4 File Offset: 0x000008A4
		private static void Convert<TArray>(TArray[] from, out uint to)
		{
			to = 0U;
			bool flag = from != null;
			if (flag)
			{
				to = (uint)from.Length;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026C4 File Offset: 0x000008C4
		private static void Convert<TArray>(ArraySegment<TArray> from, out int to)
		{
			to = from.Count;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026D0 File Offset: 0x000008D0
		private static void Convert<T>(ArraySegment<T> from, out uint to)
		{
			to = (uint)from.Count;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026DC File Offset: 0x000008DC
		private static void Convert(int from, out bool to)
		{
			to = (from != 0);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026E5 File Offset: 0x000008E5
		private static void Convert(bool from, out int to)
		{
			to = (from ? 1 : 0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026F4 File Offset: 0x000008F4
		private static void Convert(DateTimeOffset? from, out long to)
		{
			to = -1L;
			bool flag = from != null;
			if (flag)
			{
				DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long ticks = (from.Value.UtcDateTime - d).Ticks;
				long num = ticks / 10000000L;
				to = num;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002754 File Offset: 0x00000954
		private static void Convert(long from, out DateTimeOffset? to)
		{
			to = null;
			bool flag = from >= 0L;
			if (flag)
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				long num = from * 10000000L;
				to = new DateTimeOffset?(new DateTimeOffset(dateTime.Ticks + num, TimeSpan.Zero));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027B1 File Offset: 0x000009B1
		internal static void Get<TArray>(TArray[] from, out int to)
		{
			Helper.Convert<TArray>(from, out to);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027BC File Offset: 0x000009BC
		internal static void Get<TArray>(TArray[] from, out uint to)
		{
			Helper.Convert<TArray>(from, out to);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027C7 File Offset: 0x000009C7
		internal static void Get<TArray>(ArraySegment<TArray> from, out uint to)
		{
			Helper.Convert<TArray>(from, out to);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027D2 File Offset: 0x000009D2
		internal static void Get<TTo>(IntPtr from, out TTo to) where TTo : Handle, new()
		{
			Helper.Convert<TTo>(from, out to);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027DD File Offset: 0x000009DD
		internal static void Get<TFrom, TTo>(ref TFrom from, out TTo to) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			from.Get(out to);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027EE File Offset: 0x000009EE
		internal static void Get(int from, out bool to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027F9 File Offset: 0x000009F9
		internal static void Get(bool from, out int to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002804 File Offset: 0x00000A04
		internal static void Get(long from, out DateTimeOffset? to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000280F File Offset: 0x00000A0F
		internal static void Get<TTo>(IntPtr from, out TTo[] to, int arrayLength, bool isArrayItemAllocated)
		{
			Helper.GetAllocation<TTo>(from, out to, arrayLength, isArrayItemAllocated);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000281C File Offset: 0x00000A1C
		internal static void Get<TTo>(IntPtr from, out TTo[] to, uint arrayLength, bool isArrayItemAllocated)
		{
			Helper.GetAllocation<TTo>(from, out to, (int)arrayLength, isArrayItemAllocated);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002829 File Offset: 0x00000A29
		internal static void Get<TTo>(IntPtr from, out TTo[] to, int arrayLength)
		{
			Helper.GetAllocation<TTo>(from, out to, arrayLength, !typeof(TTo).IsValueType);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002847 File Offset: 0x00000A47
		internal static void Get<TTo>(IntPtr from, out TTo[] to, uint arrayLength)
		{
			Helper.GetAllocation<TTo>(from, out to, (int)arrayLength, !typeof(TTo).IsValueType);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002868 File Offset: 0x00000A68
		internal static void Get(IntPtr from, out ArraySegment<byte> to, uint arrayLength)
		{
			to = default(ArraySegment<byte>);
			bool flag = arrayLength > 0U;
			if (flag)
			{
				byte[] array = new byte[arrayLength];
				Marshal.Copy(from, array, 0, (int)arrayLength);
				to = new ArraySegment<byte>(array);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal static void GetHandle<THandle>(IntPtr from, out THandle[] to, uint arrayLength) where THandle : Handle, new()
		{
			Helper.GetAllocation<THandle>(from, out to, (int)arrayLength);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028B0 File Offset: 0x00000AB0
		internal static void Get<TFrom, TTo>(TFrom[] from, out TTo[] to) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			to = Helper.GetDefault<TTo[]>();
			bool flag = from != null;
			if (flag)
			{
				to = new TTo[from.Length];
				for (int i = 0; i < from.Length; i++)
				{
					from[i].Get(out to[i]);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000290C File Offset: 0x00000B0C
		internal static void Get<TFrom, TTo>(IntPtr from, out TTo[] to, int arrayLength) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			TFrom[] from2;
			Helper.Get<TFrom>(from, out from2, arrayLength);
			Helper.Get<TFrom, TTo>(from2, out to);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000292C File Offset: 0x00000B2C
		internal static void Get<TFrom, TTo>(IntPtr from, out TTo[] to, uint arrayLength) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			Helper.Get<TFrom, TTo>(from, out to, (int)arrayLength);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002938 File Offset: 0x00000B38
		internal static void Get<TTo>(IntPtr from, out TTo? to) where TTo : struct
		{
			Helper.GetAllocation<TTo>(from, out to);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002943 File Offset: 0x00000B43
		internal static void Get(byte[] from, out string to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000294E File Offset: 0x00000B4E
		internal static void Get(IntPtr from, out object to)
		{
			to = Helper.GetClientData(from);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002959 File Offset: 0x00000B59
		internal static void Get(IntPtr from, out Utf8String to)
		{
			Helper.GetAllocation(from, out to);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002964 File Offset: 0x00000B64
		internal static void Get<T, TEnum>(T from, out T to, TEnum currentEnum, TEnum expectedEnum)
		{
			to = Helper.GetDefault<T>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				to = from;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal static void Get<TFrom, TTo, TEnum>(ref TFrom from, out TTo to, TEnum currentEnum, TEnum expectedEnum) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			to = Helper.GetDefault<TTo>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				Helper.Get<TFrom, TTo>(ref from, out to);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal static void Get<TEnum>(int from, out bool? to, TEnum currentEnum, TEnum expectedEnum)
		{
			to = Helper.GetDefault<bool?>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				bool value;
				Helper.Convert(from, out value);
				to = new bool?(value);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A30 File Offset: 0x00000C30
		internal static void Get<TFrom, TEnum>(TFrom from, out TFrom? to, TEnum currentEnum, TEnum expectedEnum) where TFrom : struct
		{
			to = Helper.GetDefault<TFrom?>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				to = new TFrom?(from);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A74 File Offset: 0x00000C74
		internal static void Get<TFrom, TEnum>(IntPtr from, out TFrom to, TEnum currentEnum, TEnum expectedEnum) where TFrom : Handle, new()
		{
			to = Helper.GetDefault<TFrom>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				Helper.Get<TFrom>(from, out to);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AB4 File Offset: 0x00000CB4
		internal static void Get<TEnum>(IntPtr from, out IntPtr? to, TEnum currentEnum, TEnum expectedEnum)
		{
			to = Helper.GetDefault<IntPtr?>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				Helper.Get<IntPtr>(from, out to);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AF4 File Offset: 0x00000CF4
		internal static void Get<TEnum>(IntPtr from, out Utf8String to, TEnum currentEnum, TEnum expectedEnum)
		{
			to = Helper.GetDefault<Utf8String>();
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				Helper.Get(from, out to);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B30 File Offset: 0x00000D30
		internal static void Get<TFrom, TTo>(IntPtr from, out TTo to) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			to = Helper.GetDefault<TTo>();
			TFrom? tfrom;
			Helper.Get<TFrom>(from, out tfrom);
			bool flag = tfrom != null;
			if (flag)
			{
				TFrom value = tfrom.Value;
				value.Get(out to);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B78 File Offset: 0x00000D78
		internal static void Get<TFrom, TTo>(IntPtr from, out TTo? to) where TFrom : struct, IGettable<TTo> where TTo : struct
		{
			to = Helper.GetDefault<TTo?>();
			TFrom? tfrom;
			Helper.Get<TFrom>(from, out tfrom);
			bool flag = tfrom != null;
			if (flag)
			{
				TFrom value = tfrom.Value;
				TTo value2;
				value.Get(out value2);
				to = new TTo?(value2);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BCB File Offset: 0x00000DCB
		internal static void Get<TFrom, TTo>(ref TFrom from, out TTo to, out IntPtr clientDataAddress) where TFrom : struct, ICallbackInfoInternal, IGettable<TTo> where TTo : struct
		{
			from.Get(out to);
			clientDataAddress = from.ClientDataAddress;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static int GetAllocationCount()
		{
			return Helper.s_Allocations.Count + Helper.s_PinnedBuffers.Aggregate(0, (int acc, KeyValuePair<IntPtr, Helper.PinnedBuffer> x) => acc + x.Value.RefCount) + Helper.s_Callbacks.Count + Helper.s_ClientDatas.Count;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal static void Copy(byte[] from, IntPtr to)
		{
			bool flag = from != null && to != IntPtr.Zero;
			if (flag)
			{
				Marshal.Copy(from, 0, to, from.Length);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C80 File Offset: 0x00000E80
		internal static void Copy(ArraySegment<byte> from, IntPtr to)
		{
			bool flag = from.Count != 0 && to != IntPtr.Zero;
			if (flag)
			{
				Marshal.Copy(from.Array, from.Offset, to, from.Count);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CC7 File Offset: 0x00000EC7
		internal static void Dispose(ref IntPtr value)
		{
			Helper.RemoveAllocation(ref value);
			Helper.RemovePinnedBuffer(ref value);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CD8 File Offset: 0x00000ED8
		internal static void Dispose<TDisposable>(ref TDisposable disposable) where TDisposable : IDisposable
		{
			bool flag = disposable != null;
			if (flag)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D08 File Offset: 0x00000F08
		internal static void Dispose<TEnum>(ref IntPtr value, TEnum currentEnum, TEnum expectedEnum)
		{
			bool flag = (int)((object)currentEnum) == (int)((object)expectedEnum);
			if (flag)
			{
				Helper.Dispose(ref value);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D3C File Offset: 0x00000F3C
		private static int GetAnsiStringLength(byte[] bytes)
		{
			int num = 0;
			foreach (byte b in bytes)
			{
				bool flag = b == 0;
				if (flag)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D7C File Offset: 0x00000F7C
		private static int GetAnsiStringLength(IntPtr address)
		{
			int num = 0;
			while (Marshal.ReadByte(address, num) > 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002DA8 File Offset: 0x00000FA8
		internal static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DC4 File Offset: 0x00000FC4
		private static void GetAllocation<T>(IntPtr source, out T target)
		{
			target = Helper.GetDefault<T>();
			bool flag = source == IntPtr.Zero;
			if (!flag)
			{
				object obj;
				bool flag2 = Helper.TryGetAllocationCache(source, out obj);
				if (flag2)
				{
					bool flag3 = obj != null;
					if (flag3)
					{
						bool flag4 = obj.GetType() == typeof(T);
						if (flag4)
						{
							target = (T)((object)obj);
							return;
						}
						throw new CachedTypeAllocationException(source, obj.GetType(), typeof(T));
					}
				}
				target = (T)((object)Marshal.PtrToStructure(source, typeof(T)));
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E64 File Offset: 0x00001064
		private static void GetAllocation<T>(IntPtr source, out T? target) where T : struct
		{
			target = Helper.GetDefault<T?>();
			bool flag = source == IntPtr.Zero;
			if (!flag)
			{
				object obj;
				bool flag2 = Helper.TryGetAllocationCache(source, out obj);
				if (flag2)
				{
					bool flag3 = obj != null;
					if (flag3)
					{
						bool flag4 = obj.GetType() == typeof(T);
						if (flag4)
						{
							target = (T?)obj;
							return;
						}
						throw new CachedTypeAllocationException(source, obj.GetType(), typeof(T));
					}
				}
				target = (T?)Marshal.PtrToStructure(source, typeof(T));
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F04 File Offset: 0x00001104
		private static void GetAllocation<THandle>(IntPtr source, out THandle[] target, int arrayLength) where THandle : Handle, new()
		{
			target = null;
			bool flag = source == IntPtr.Zero;
			if (!flag)
			{
				object obj;
				bool flag2 = Helper.TryGetAllocationCache(source, out obj);
				if (flag2)
				{
					bool flag3 = obj != null;
					if (flag3)
					{
						bool flag4 = obj.GetType() == typeof(THandle[]);
						if (!flag4)
						{
							throw new CachedTypeAllocationException(source, obj.GetType(), typeof(THandle[]));
						}
						Array array = (Array)obj;
						bool flag5 = array.Length == arrayLength;
						if (flag5)
						{
							target = (array as THandle[]);
							return;
						}
						throw new CachedArrayAllocationException(source, array.Length, arrayLength);
					}
				}
				int num = Marshal.SizeOf(typeof(IntPtr));
				List<THandle> list = new List<THandle>();
				for (int i = 0; i < arrayLength; i++)
				{
					IntPtr intPtr = new IntPtr(source.ToInt64() + (long)(i * num));
					intPtr = Marshal.ReadIntPtr(intPtr);
					THandle item;
					Helper.Convert<THandle>(intPtr, out item);
					list.Add(item);
				}
				target = list.ToArray();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003018 File Offset: 0x00001218
		private static void GetAllocation<T>(IntPtr from, out T[] to, int arrayLength, bool isArrayItemAllocated)
		{
			to = null;
			bool flag = from == IntPtr.Zero;
			if (!flag)
			{
				object obj;
				bool flag2 = Helper.TryGetAllocationCache(from, out obj);
				if (flag2)
				{
					bool flag3 = obj != null;
					if (flag3)
					{
						bool flag4 = obj.GetType() == typeof(T[]);
						if (!flag4)
						{
							throw new CachedTypeAllocationException(from, obj.GetType(), typeof(T[]));
						}
						Array array = (Array)obj;
						bool flag5 = array.Length == arrayLength;
						if (flag5)
						{
							to = (array as T[]);
							return;
						}
						throw new CachedArrayAllocationException(from, array.Length, arrayLength);
					}
				}
				int num;
				if (isArrayItemAllocated)
				{
					num = Marshal.SizeOf(typeof(IntPtr));
				}
				else
				{
					num = Marshal.SizeOf(typeof(T));
				}
				List<T> list = new List<T>();
				for (int i = 0; i < arrayLength; i++)
				{
					IntPtr intPtr = new IntPtr(from.ToInt64() + (long)(i * num));
					if (isArrayItemAllocated)
					{
						intPtr = Marshal.ReadIntPtr(intPtr);
					}
					T item;
					Helper.GetAllocation<T>(intPtr, out item);
					list.Add(item);
				}
				to = list.ToArray();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003150 File Offset: 0x00001350
		private static void GetAllocation(IntPtr source, out Utf8String target)
		{
			target = null;
			bool flag = source == IntPtr.Zero;
			if (!flag)
			{
				int ansiStringLength = Helper.GetAnsiStringLength(source);
				byte[] array = new byte[ansiStringLength + 1];
				Marshal.Copy(source, array, 0, ansiStringLength + 1);
				target = new Utf8String(array);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003198 File Offset: 0x00001398
		internal static IntPtr AddAllocation(int size)
		{
			bool flag = size == 0;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				IntPtr intPtr = Marshal.AllocHGlobal(size);
				Marshal.WriteByte(intPtr, 0, 0);
				Dictionary<IntPtr, Helper.Allocation> obj = Helper.s_Allocations;
				lock (obj)
				{
					Helper.s_Allocations.Add(intPtr, new Helper.Allocation(size, null, null));
				}
				result = intPtr;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000321C File Offset: 0x0000141C
		internal static IntPtr AddAllocation(uint size)
		{
			return Helper.AddAllocation((int)size);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003234 File Offset: 0x00001434
		private static IntPtr AddAllocation<T>(int size, T cache)
		{
			bool flag = size == 0 || cache == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				IntPtr intPtr = Marshal.AllocHGlobal(size);
				Marshal.StructureToPtr<T>(cache, intPtr, false);
				Dictionary<IntPtr, Helper.Allocation> obj = Helper.s_Allocations;
				lock (obj)
				{
					Helper.s_Allocations.Add(intPtr, new Helper.Allocation(size, cache, null));
				}
				result = intPtr;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000032C8 File Offset: 0x000014C8
		private static IntPtr AddAllocation<T>(int size, T[] cache, bool? isArrayItemAllocated)
		{
			bool flag = size == 0 || cache == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				IntPtr intPtr = Marshal.AllocHGlobal(size);
				Marshal.WriteByte(intPtr, 0, 0);
				Dictionary<IntPtr, Helper.Allocation> obj = Helper.s_Allocations;
				lock (obj)
				{
					Helper.s_Allocations.Add(intPtr, new Helper.Allocation(size, cache, isArrayItemAllocated));
				}
				result = intPtr;
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003348 File Offset: 0x00001548
		private static IntPtr AddAllocation<T>(T[] array, bool isArrayItemAllocated)
		{
			bool flag = array == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				int num;
				if (isArrayItemAllocated)
				{
					num = Marshal.SizeOf(typeof(IntPtr));
				}
				else
				{
					num = Marshal.SizeOf(typeof(T));
				}
				IntPtr intPtr = Helper.AddAllocation<T>(array.Length * num, array, new bool?(isArrayItemAllocated));
				for (int i = 0; i < array.Length; i++)
				{
					T t = (T)((object)array.GetValue(i));
					if (isArrayItemAllocated)
					{
						bool flag2 = typeof(T) == typeof(Utf8String);
						IntPtr structure;
						if (flag2)
						{
							structure = Helper.AddPinnedBuffer((Utf8String)((object)t));
						}
						else
						{
							bool flag3 = typeof(T).BaseType == typeof(Handle);
							if (flag3)
							{
								Helper.Convert((Handle)((object)t), out structure);
							}
							else
							{
								structure = Helper.AddAllocation<T>(Marshal.SizeOf(typeof(T)), t);
							}
						}
						IntPtr ptr = new IntPtr(intPtr.ToInt64() + (long)(i * num));
						Marshal.StructureToPtr<IntPtr>(structure, ptr, false);
					}
					else
					{
						IntPtr ptr2 = new IntPtr(intPtr.ToInt64() + (long)(i * num));
						Marshal.StructureToPtr<T>(t, ptr2, false);
					}
				}
				result = intPtr;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034B8 File Offset: 0x000016B8
		private static void RemoveAllocation(ref IntPtr address)
		{
			bool flag = address == IntPtr.Zero;
			if (!flag)
			{
				Dictionary<IntPtr, Helper.Allocation> obj = Helper.s_Allocations;
				Helper.Allocation allocation;
				lock (obj)
				{
					bool flag3 = !Helper.s_Allocations.TryGetValue(address, out allocation);
					if (flag3)
					{
						return;
					}
					Helper.s_Allocations.Remove(address);
				}
				bool flag4 = allocation.IsArrayItemAllocated != null;
				if (flag4)
				{
					bool value = allocation.IsArrayItemAllocated.Value;
					int num;
					if (value)
					{
						num = Marshal.SizeOf(typeof(IntPtr));
					}
					else
					{
						num = Marshal.SizeOf(allocation.Cache.GetType().GetElementType());
					}
					Array array = allocation.Cache as Array;
					for (int i = 0; i < array.Length; i++)
					{
						bool value2 = allocation.IsArrayItemAllocated.Value;
						if (value2)
						{
							IntPtr ptr = new IntPtr(address.ToInt64() + (long)(i * num));
							ptr = Marshal.ReadIntPtr(ptr);
							Helper.Dispose(ref ptr);
						}
						else
						{
							object value3 = array.GetValue(i);
							bool flag5 = value3 is IDisposable;
							if (flag5)
							{
								IDisposable disposable = value3 as IDisposable;
								bool flag6 = disposable != null;
								if (flag6)
								{
									disposable.Dispose();
								}
							}
						}
					}
				}
				bool flag7 = allocation.Cache is IDisposable;
				if (flag7)
				{
					IDisposable disposable2 = allocation.Cache as IDisposable;
					bool flag8 = disposable2 != null;
					if (flag8)
					{
						disposable2.Dispose();
					}
				}
				Marshal.FreeHGlobal(address);
				address = IntPtr.Zero;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003690 File Offset: 0x00001890
		private static bool TryGetAllocationCache(IntPtr address, out object cache)
		{
			cache = null;
			Dictionary<IntPtr, Helper.Allocation> obj = Helper.s_Allocations;
			lock (obj)
			{
				Helper.Allocation allocation;
				bool flag2 = Helper.s_Allocations.TryGetValue(address, out allocation);
				if (flag2)
				{
					cache = allocation.Cache;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036F8 File Offset: 0x000018F8
		private static IntPtr AddPinnedBuffer(byte[] buffer, int offset)
		{
			bool flag = buffer == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(buffer, offset);
				Dictionary<IntPtr, Helper.PinnedBuffer> obj = Helper.s_PinnedBuffers;
				lock (obj)
				{
					bool flag3 = Helper.s_PinnedBuffers.ContainsKey(intPtr);
					if (flag3)
					{
						Helper.PinnedBuffer value = Helper.s_PinnedBuffers[intPtr];
						int refCount = value.RefCount;
						value.RefCount = refCount + 1;
						Helper.s_PinnedBuffers[intPtr] = value;
					}
					else
					{
						Helper.s_PinnedBuffers.Add(intPtr, new Helper.PinnedBuffer(handle));
					}
					result = intPtr;
				}
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037BC File Offset: 0x000019BC
		private static IntPtr AddPinnedBuffer(Utf8String str)
		{
			bool flag = str == null || str.Bytes == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				result = Helper.AddPinnedBuffer(str.Bytes, 0);
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037FC File Offset: 0x000019FC
		internal static IntPtr AddPinnedBuffer(ArraySegment<byte> array)
		{
			return Helper.AddPinnedBuffer(array.Array, array.Offset);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003828 File Offset: 0x00001A28
		private static void RemovePinnedBuffer(ref IntPtr address)
		{
			bool flag = address == IntPtr.Zero;
			if (!flag)
			{
				Dictionary<IntPtr, Helper.PinnedBuffer> obj = Helper.s_PinnedBuffers;
				lock (obj)
				{
					Helper.PinnedBuffer value;
					bool flag3 = Helper.s_PinnedBuffers.TryGetValue(address, out value);
					if (flag3)
					{
						value.Handle.Free();
						int refCount = value.RefCount;
						value.RefCount = refCount - 1;
						bool flag4 = value.RefCount == 0;
						if (flag4)
						{
							Helper.s_PinnedBuffers.Remove(address);
						}
						else
						{
							Helper.s_PinnedBuffers[address] = value;
						}
					}
				}
				address = IntPtr.Zero;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038F0 File Offset: 0x00001AF0
		internal static void Set<T>(ref T from, ref T to) where T : struct
		{
			to = from;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038FF File Offset: 0x00001AFF
		internal static void Set(object from, ref IntPtr to)
		{
			Helper.RemoveClientData(to);
			to = Helper.AddClientData(from);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003912 File Offset: 0x00001B12
		internal static void Set(Utf8String from, ref IntPtr to)
		{
			Helper.Dispose(ref to);
			to = Helper.AddPinnedBuffer(from);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003924 File Offset: 0x00001B24
		internal static void Set(Handle from, ref IntPtr to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000392F File Offset: 0x00001B2F
		internal static void Set<T>(T? from, ref IntPtr to) where T : struct
		{
			Helper.Dispose(ref to);
			to = Helper.AddAllocation<T?>(Marshal.SizeOf(typeof(T)), from);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003950 File Offset: 0x00001B50
		internal static void Set<T>(T[] from, ref IntPtr to, bool isArrayItemAllocated)
		{
			Helper.Dispose(ref to);
			to = Helper.AddAllocation<T>(from, isArrayItemAllocated);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003963 File Offset: 0x00001B63
		internal static void Set(ArraySegment<byte> from, ref IntPtr to, out uint arrayLength)
		{
			to = Helper.AddPinnedBuffer(from);
			Helper.Get<byte>(from, out arrayLength);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003976 File Offset: 0x00001B76
		internal static void Set<T>(T[] from, ref IntPtr to)
		{
			Helper.Set<T>(from, ref to, !typeof(T).IsValueType);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003993 File Offset: 0x00001B93
		internal static void Set<T>(T[] from, ref IntPtr to, bool isArrayItemAllocated, out int arrayLength)
		{
			Helper.Set<T>(from, ref to, isArrayItemAllocated);
			Helper.Get<T>(from, out arrayLength);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000039A7 File Offset: 0x00001BA7
		internal static void Set<T>(T[] from, ref IntPtr to, bool isArrayItemAllocated, out uint arrayLength)
		{
			Helper.Set<T>(from, ref to, isArrayItemAllocated);
			Helper.Get<T>(from, out arrayLength);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000039BB File Offset: 0x00001BBB
		internal static void Set<T>(T[] from, ref IntPtr to, out int arrayLength)
		{
			Helper.Set<T>(from, ref to, !typeof(T).IsValueType, out arrayLength);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000039D9 File Offset: 0x00001BD9
		internal static void Set<T>(T[] from, ref IntPtr to, out uint arrayLength)
		{
			Helper.Set<T>(from, ref to, !typeof(T).IsValueType, out arrayLength);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000039F7 File Offset: 0x00001BF7
		internal static void Set(DateTimeOffset? from, ref long to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A02 File Offset: 0x00001C02
		internal static void Set(bool from, ref int to)
		{
			Helper.Convert(from, out to);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003A0D File Offset: 0x00001C0D
		internal static void Set(string from, ref byte[] to, int stringLength)
		{
			Helper.Convert(from, out to, stringLength);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal static void Set<T, TEnum>(T from, ref T to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
		{
			bool flag = from != null;
			if (flag)
			{
				Helper.Dispose<IDisposable>(ref disposable);
				to = from;
				toEnum = fromEnum;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A4F File Offset: 0x00001C4F
		internal static void Set<TFrom, TEnum, TTo>(ref TFrom from, ref TTo to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null) where TFrom : struct where TTo : struct, ISettable<TFrom>
		{
			Helper.Dispose<IDisposable>(ref disposable);
			Helper.Set<TFrom, TTo>(ref from, ref to);
			toEnum = fromEnum;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003A6C File Offset: 0x00001C6C
		internal static void Set<T, TEnum>(T? from, ref T to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null) where T : struct
		{
			bool flag = from != null;
			if (flag)
			{
				Helper.Dispose<IDisposable>(ref disposable);
				T value = from.Value;
				Helper.Set<T>(ref value, ref to);
				toEnum = fromEnum;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003AA8 File Offset: 0x00001CA8
		internal static void Set<TEnum>(Handle from, ref IntPtr to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
		{
			bool flag = from != null;
			if (flag)
			{
				Helper.Dispose(ref to);
				Helper.Dispose<IDisposable>(ref disposable);
				Helper.Set(from, ref to);
				toEnum = fromEnum;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003AE4 File Offset: 0x00001CE4
		internal static void Set<TEnum>(Utf8String from, ref IntPtr to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
		{
			bool flag = from != null;
			if (flag)
			{
				Helper.Dispose(ref to);
				Helper.Dispose<IDisposable>(ref disposable);
				Helper.Set(from, ref to);
				toEnum = fromEnum;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B20 File Offset: 0x00001D20
		internal static void Set<TEnum>(bool? from, ref int to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
		{
			bool flag = from != null;
			if (flag)
			{
				Helper.Dispose<IDisposable>(ref disposable);
				Helper.Set(from.Value, ref to);
				toEnum = fromEnum;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B58 File Offset: 0x00001D58
		internal static void Set<TFrom, TIntermediate>(ref TFrom from, ref IntPtr to) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
		{
			TIntermediate cache = Activator.CreateInstance<TIntermediate>();
			cache.Set(ref from);
			Helper.Dispose(ref to);
			to = Helper.AddAllocation<TIntermediate>(Marshal.SizeOf(typeof(TIntermediate)), cache);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B9C File Offset: 0x00001D9C
		internal static void Set<TFrom, TIntermediate>(ref TFrom? from, ref IntPtr to) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
		{
			Helper.Dispose(ref to);
			bool flag = from == null;
			if (!flag)
			{
				TIntermediate cache = Activator.CreateInstance<TIntermediate>();
				TFrom value = from.Value;
				cache.Set(ref value);
				to = Helper.AddAllocation<TIntermediate>(Marshal.SizeOf(typeof(TIntermediate)), cache);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003BF5 File Offset: 0x00001DF5
		internal static void Set<TFrom, TTo>(ref TFrom from, ref TTo to) where TFrom : struct where TTo : struct, ISettable<TFrom>
		{
			to.Set(ref from);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C08 File Offset: 0x00001E08
		internal static void Set<TFrom, TIntermediate>(ref TFrom[] from, ref IntPtr to, out int arrayLength) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
		{
			arrayLength = 0;
			bool flag = from != null;
			if (flag)
			{
				TIntermediate[] array = new TIntermediate[from.Length];
				for (int i = 0; i < from.Length; i++)
				{
					array[i].Set(ref from[i]);
				}
				Helper.Set<TIntermediate>(array, ref to);
				Helper.Get<TFrom>(from, out arrayLength);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C74 File Offset: 0x00001E74
		internal static void Set<TFrom, TIntermediate>(ref TFrom[] from, ref IntPtr to, out uint arrayLength) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
		{
			int num;
			Helper.Set<TFrom, TIntermediate>(ref from, ref to, out num);
			arrayLength = (uint)num;
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<IntPtr, Helper.Allocation> s_Allocations = new Dictionary<IntPtr, Helper.Allocation>();

		// Token: 0x04000002 RID: 2
		private static Dictionary<IntPtr, Helper.PinnedBuffer> s_PinnedBuffers = new Dictionary<IntPtr, Helper.PinnedBuffer>();

		// Token: 0x04000003 RID: 3
		private static Dictionary<IntPtr, Helper.DelegateHolder> s_Callbacks = new Dictionary<IntPtr, Helper.DelegateHolder>();

		// Token: 0x04000004 RID: 4
		private static Dictionary<string, Helper.DelegateHolder> s_StaticCallbacks = new Dictionary<string, Helper.DelegateHolder>();

		// Token: 0x04000005 RID: 5
		private static long s_LastClientDataId = 0L;

		// Token: 0x04000006 RID: 6
		private static Dictionary<IntPtr, object> s_ClientDatas = new Dictionary<IntPtr, object>();

		// Token: 0x020006A2 RID: 1698
		private struct Allocation
		{
			// Token: 0x17000D3D RID: 3389
			// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000415C8 File Offset: 0x0003F7C8
			// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x000415D0 File Offset: 0x0003F7D0
			public int Size { get; private set; }

			// Token: 0x17000D3E RID: 3390
			// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000415D9 File Offset: 0x0003F7D9
			// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000415E1 File Offset: 0x0003F7E1
			public object Cache { get; private set; }

			// Token: 0x17000D3F RID: 3391
			// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000415EA File Offset: 0x0003F7EA
			// (set) Token: 0x06002BAB RID: 11179 RVA: 0x000415F2 File Offset: 0x0003F7F2
			public bool? IsArrayItemAllocated { get; private set; }

			// Token: 0x06002BAC RID: 11180 RVA: 0x000415FB File Offset: 0x0003F7FB
			public Allocation(int size, object cache, bool? isArrayItemAllocated = null)
			{
				this.Size = size;
				this.Cache = cache;
				this.IsArrayItemAllocated = isArrayItemAllocated;
			}
		}

		// Token: 0x020006A3 RID: 1699
		private struct PinnedBuffer
		{
			// Token: 0x17000D40 RID: 3392
			// (get) Token: 0x06002BAD RID: 11181 RVA: 0x00041616 File Offset: 0x0003F816
			// (set) Token: 0x06002BAE RID: 11182 RVA: 0x0004161E File Offset: 0x0003F81E
			public GCHandle Handle { get; private set; }

			// Token: 0x17000D41 RID: 3393
			// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00041627 File Offset: 0x0003F827
			// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x0004162F File Offset: 0x0003F82F
			public int RefCount { get; set; }

			// Token: 0x06002BB1 RID: 11185 RVA: 0x00041638 File Offset: 0x0003F838
			public PinnedBuffer(GCHandle handle)
			{
				this.Handle = handle;
				this.RefCount = 1;
			}
		}

		// Token: 0x020006A4 RID: 1700
		private class DelegateHolder
		{
			// Token: 0x17000D42 RID: 3394
			// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x0004164B File Offset: 0x0003F84B
			// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x00041653 File Offset: 0x0003F853
			public Delegate Public { get; private set; }

			// Token: 0x17000D43 RID: 3395
			// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x0004165C File Offset: 0x0003F85C
			// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x00041664 File Offset: 0x0003F864
			public Delegate Private { get; private set; }

			// Token: 0x17000D44 RID: 3396
			// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x0004166D File Offset: 0x0003F86D
			// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x00041675 File Offset: 0x0003F875
			public Delegate[] StructDelegates { get; private set; }

			// Token: 0x17000D45 RID: 3397
			// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x0004167E File Offset: 0x0003F87E
			// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x00041686 File Offset: 0x0003F886
			public ulong? NotificationId { get; set; }

			// Token: 0x06002BBA RID: 11194 RVA: 0x0004168F File Offset: 0x0003F88F
			public DelegateHolder(Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
			{
				this.Public = publicDelegate;
				this.Private = privateDelegate;
				this.StructDelegates = structDelegates;
			}
		}
	}
}
