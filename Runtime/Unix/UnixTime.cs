using System;

namespace CommonUtility.Unix
{
    public static class UnixTime
    {
        private static long _deltaMs = 0;

        public static long Now => (long)GetNow(_deltaMs).TotalSeconds;

        public static long NowMs => (long)GetNow(_deltaMs).TotalMilliseconds;

        public static long NowMsNoDelta => (long)GetNow().TotalMilliseconds;

        public static void SetDelta(long deltaMs) => _deltaMs = deltaMs;

        private static TimeSpan GetNow(long deltaMs = 0)
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            return DateTime.Now.ToUniversalTime() - baseDate + TimeSpan.FromMilliseconds(deltaMs);
        }

        public static string GetDottedTimespan(long timeMs)
        {
            TimeSpan value = new TimeSpan(0, 0, (int)(timeMs / 1000));
            return (value.Hours + (value.Days * 24)) + ":" +
                value.Minutes.ToString("00") + ":" +
                value.Seconds.ToString("00");
        }

        public static long TotalSeconds(this DateTime dateTime) => (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
}