﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FoE.Farmer.Library
{
    public static class Helper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static DateTime GenerateNextInterval(int intervalType, BuildType type)
        {
            var randSalt = new Random();

            if (type == BuildType.Goods)
            {
                switch (intervalType)
                {
                    case (int)TimeIntervalGoods.FourHours:
                        return DateTime.Now + TimeSpan.FromHours(4) + TimeSpan.FromMinutes(randSalt.Next(10, 59));
                    case (int)TimeIntervalGoods.EightHours:
                        return DateTime.Now + TimeSpan.FromHours(8) + TimeSpan.FromMinutes(randSalt.Next(10, 59));
                    case (int)TimeIntervalGoods.OneDay:
                        return DateTime.Now + TimeSpan.FromHours(24) + TimeSpan.FromMinutes(randSalt.Next(10, 59));
                    case (int)TimeIntervalGoods.TwoDay:
                        return DateTime.Now + TimeSpan.FromHours(48) + TimeSpan.FromMinutes(randSalt.Next(10, 59));
                }
            }
            else if (type == BuildType.Supplies || type == BuildType.Residential)
            {
                switch (intervalType)
                {
                    case (int)TimeIntervalSupplies.FiveMinutes:
                        return DateTime.Now + TimeSpan.FromMinutes(5) + TimeSpan.FromMinutes(randSalt.Next(1, 4));
                    case (int)TimeIntervalSupplies.FiftenMinuts:
                        return DateTime.Now + TimeSpan.FromMinutes(15) + TimeSpan.FromMinutes(randSalt.Next(1, 5));
                    case (int)TimeIntervalSupplies.OneHour:
                        return DateTime.Now + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(randSalt.Next(3, 10));
                    case (int)TimeIntervalSupplies.FourHour:
                        return DateTime.Now + TimeSpan.FromHours(4) + TimeSpan.FromMinutes(randSalt.Next(3, 15));
                    case (int)TimeIntervalSupplies.EightHour:
                        return DateTime.Now + TimeSpan.FromHours(8) + TimeSpan.FromMinutes(randSalt.Next(3, 25));
                    case (int)TimeIntervalSupplies.OneDay:
                        return DateTime.Now + TimeSpan.FromHours(24) + TimeSpan.FromMinutes(randSalt.Next(3, 30));
                }
            }

            throw new ArgumentException();
        }

        public static DateTime GenerateNextInterval(int productionTime, DateTime? fromDate = null)
        {
            if (fromDate == null) fromDate = DateTime.Now;
            var randSalt = new Random();
            var maxSalt = (int) (productionTime / 10f);

            return fromDate.Value + TimeSpan.FromSeconds(productionTime) + TimeSpan.FromSeconds(randSalt.Next(20, maxSalt));
        }
    }
}