using System;
using UnityEngine;

namespace UPatterns
{
    public static class TimeHelper
    {
        public static string GetTimeAgoFromUtcString(string utcTimestamp)
        {
            DateTime utcDateTime = DateTime.Parse(
                utcTimestamp,
                null,
                System.Globalization.DateTimeStyles.AdjustToUniversal
            );

            DateTime localDateTime = utcDateTime.ToLocalTime();

            TimeSpan diff = DateTime.Now - localDateTime;

            return FormatTimeAgo(diff);
        }

        private static string FormatTimeAgo(TimeSpan diff)
        {
            if (diff.TotalSeconds < 60)
                return $"{Mathf.FloorToInt((float)diff.TotalSeconds)} seconds ago";
            if (diff.TotalMinutes < 60)
                return $"{Mathf.FloorToInt((float)diff.TotalMinutes)} minutes ago";
            if (diff.TotalHours < 24)
                return $"{Mathf.FloorToInt((float)diff.TotalHours)} hours ago";
            if (diff.TotalDays < 30)
                return $"{Mathf.FloorToInt((float)diff.TotalDays)} days ago";
            if (diff.TotalDays < 365)
                return $"{Mathf.FloorToInt((float)(diff.TotalDays / 30))} months ago";

            return $"{Mathf.FloorToInt((float)(diff.TotalDays / 365))} years ago";
        }
    }
}