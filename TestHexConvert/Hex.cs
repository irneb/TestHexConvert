using System;
using System.Runtime.CompilerServices;

namespace TestHexConvert
{
	/// <summary>
	/// Convert numbers to and from hex strings
	/// </summary>
	public static class Hex
	{
		const string DIGITS = "0123456789ABCDEF";
		const byte ZERO = (byte)'0';
		const byte ALPHA_CAP = (byte)'A';
		const byte ALPHA_LOW = (byte)'a';
		const byte DIV_SHIFT = 4;
		const byte MOD_AND = 15;

		[MethodImpl (MethodImplOptions.AggressiveInlining)]
		static void HexFromByte (byte value, char[] chars, int offset)
		{
			unchecked {
				chars [offset] = DIGITS [value & MOD_AND];
				chars [offset - 1] = DIGITS [value >> DIV_SHIFT];
			}
		}

		[MethodImpl (MethodImplOptions.AggressiveInlining)]
		static char[] HexFromBytes (byte[] values)
		{
			unchecked {
				var chars = new char[values.Length << 1];
				var j = 1;
				for (int i = 0; i < values.Length; ++i) {
					HexFromByte (values [i], chars, j);
					j += 2;
				}
				return chars;
			}
		}

		[MethodImpl (MethodImplOptions.AggressiveInlining)]
		static byte ByteFromHexDigit (string chars, int offset)
		{
			unchecked {
				byte b = (byte)chars [offset];
				return (byte)((b < ALPHA_LOW) ? ((b < ALPHA_CAP) ? b - ZERO : b - ALPHA_CAP) : b - ALPHA_LOW);
			}
		}

		[MethodImpl (MethodImplOptions.AggressiveInlining)]
		static byte[] BytesFromHex (string chars)
		{
			unchecked {
				var values = new byte[(chars.Length >> 1) + (((chars.Length & 1) > 0) ? 1 : 0)];
				var j = 0;
				for (int i = 1; i < chars.Length; i += 2) {
					values [j] = (byte)(ByteFromHexDigit (chars, i - 1) << DIV_SHIFT);
					values [j] |= ByteFromHexDigit (chars, i);
					++j;
				}
				return values;
			}
		}

		public static string ToHexString (byte[] values)
		{
			return new string (HexFromBytes (values));
		}

		public static byte[] FromHexString (string hex)
		{
			return BytesFromHex (hex);
		}
	}
}
