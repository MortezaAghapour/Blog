﻿using System;
using System.Collections.Generic;
using System.IO;
using Blog.Shared.Enums.DateTime;

namespace Blog.Shared.Helpers
{
    public class CommonHelper
    {
        #region Properties
        public static string BaseDirectory { get; set; }
        #endregion
        #region Get TimeSpan By Period

        public static TimeSpan GetTimeSpanByPeriod(Period period, int val)
        {
            switch (period)
            {
                case Period.Millisecond:
                {
                    return TimeSpan.FromMilliseconds(val);
                }

                case Period.Second:
                {
                    return TimeSpan.FromSeconds(val);
                }

                case Period.Minute:
                {
                    return TimeSpan.FromMinutes(val);
                }

                case Period.Hour:
                {
                    return TimeSpan.FromHours(val);
                }

                case Period.Day:
                {
                    return TimeSpan.FromDays(val);
                }

                case Period.Month:
                {
                    return TimeSpan.FromDays(30 * val);
                }

                case Period.Year:
                {
                    return TimeSpan.FromDays(365 * val);
                }

                case Period.Week:
                {
                    return TimeSpan.FromDays(7 * val);
                }

                default:
                {
                    return TimeSpan.FromHours(1);
                }
            }
        }
        #endregion

        public static string MapPath(string path)
        {
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(BaseDirectory ?? string.Empty, path);
        }

        public static string ImageName(string path)
        {
            var splitPic = path.Split('\\');
            return !string.IsNullOrEmpty(path) ? splitPic[splitPic.Length - 1] : path;
        }
    }
}
