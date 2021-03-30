using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace GSFramework
{
    public static class GeneralTools
    {
        public static Type GetSingleton(this Type type)
        {
            return default;
        }

        public static Dictionary<TK, TV> DictionaryTypeConversion<SK, SV, TK, TV>(this Dictionary<SK, SV> source)
        {
            if (typeof(SK).IsAssignableFrom(typeof(TK)) && typeof(SV).IsAssignableFrom(typeof(TV)))
            {
                Dictionary<TK, TV> tmpDictionary = new Dictionary<TK, TV>();
                foreach (var kv in source)
                {
                    tmpDictionary.Add((TK)(object)kv.Key, (TV)(object)kv.Value);
                }
            }
            return null;
        }

        public static T ConvertToSimpleType<T>(this string value) where T : struct
        {
            object tmp = ConvertToSimpleType(value, typeof(T));
            if (tmp == null)
            {
                return default;
            }
            else
            {
                return (T)tmp;
            }
        }

        public static object ConvertToSimpleType(this string value, Type type)
        {
            if (string.IsNullOrEmpty(value) || type == typeof(string))
            {
                return value;
            }

            object returnValue = null;
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            bool flag = converter.CanConvertFrom(value.GetType());
            if (!flag)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }
            if (!flag && !converter.CanConvertTo(type))
            {
                Debug.LogError("无法转换成类型：" + value.ToString() + "==>" + type);
            }
            try
            {
                returnValue = flag ? converter.ConvertFrom(null, null, value) : converter.ConvertTo(null, null, value, type);
            }
            catch
            {
                Debug.LogError("类型转换出错：" + value.ToString() + "==>" + type);
            }
            return returnValue;
        }

    }
}