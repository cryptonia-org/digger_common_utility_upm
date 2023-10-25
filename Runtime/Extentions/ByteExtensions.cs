using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonUtility.Extensions
{
    public static class ByteExtensions
    {
        public static int SetBit(this int source, int bitShift, bool value) => source | (value ? 1 : 0) << bitShift;

        public static bool GetBit(this int value, int bitShift) => (value & 1 << bitShift) != 0;

        public static byte[] ToByteArray<T>(this T obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}