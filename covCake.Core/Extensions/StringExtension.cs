using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Security.Cryptography;

namespace covCake
{
   // [DebuggerStepThrough]
    public static class StringExtension
    {
        public static string Replace(this string str, IDictionary<string, string> tokensVaues)
        {
            foreach (var item in tokensVaues)
                str = str.Replace(item.Key, item.Value);

            return str;
        }
        public static T EnumParse<T>(this int? value)
        {
            return EnumParse<T>(value.ToString());
        }

        public static T EnumParse<T>(this int value)
        {
            return EnumParse<T>(value.ToString());
        }


        public static T EnumParse<T>(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            value = value.Trim();

            if (value.Length == 0)
                throw new ArgumentException("Must specify valid information for parsing in the string.", "value");

            Type t = typeof(T);

            if (!t.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", "T");

            T enumType = (T)Enum.Parse(t, value, true);
            return enumType;
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        
        public static string AddApostrophe(this string str, char lettreApostrophe)
        {
            return AddApostrophe(str, "", lettreApostrophe);
        }

        //public static bool
        
        public static string AddApostrophe(this string str, string replace, char lettreApostrophe)
        {

            char voyelle = str.ToLowerInvariant().RemoveAccents()[0];
            if (!replace.IsNullOrEmpty())
                replace += " ";
            switch (voyelle)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'y':
                    break;
                default:
                    return replace + " " + str;
            }

            return lettreApostrophe + "'" + str;
        }



        
        public static string EmptyIfNull(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        
        public static string SwapIfEmpty(this string str, string replacestring)
        {
            return (!string.IsNullOrEmpty(str)) ? str : replacestring;
        }
        /// <summary>
        /// Test si la chaine est null ou vide (FAIT UN TRIM!!)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        
        public static bool IsNullOrEmpty(this string str)
        {
            return (str == null || str.Trim() == string.Empty);
           // pas utiliser string.IsNullOrEmpty(str) car on veu le Trim()
        }

        public static string RemoveSpecialChars(this string target)
        {
            return System.Text.RegularExpressions.Regex.Replace(target, "[^a-zA-Z0-9_]", "");
        }

        public static string FormatWith(this string target, params object[] args)
        {

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        /// <summary>
        /// Return a Base 64 computed hash
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        
        public static string Hash(this string target)
        {


            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(target);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        // 
        public static string Shrink(this string target)
        {
            target = target.EmptyIfNull();

            string base64 = Convert.ToBase64String(Encoding.Unicode.GetBytes(target));

            string encoded = base64.Replace("/", "_").Replace("+", "-");

            return encoded;//.Replace("=","");// .LastIndexOf("==").; //TODO: remove les == de fin
        }

        public static string ShrinkForUrl(this string str)
        {
            return str.Shrink().UrlEncode();
        }

        public static string UnShrinkForUrl(this string str)
        {
            return str.UrlDecode().UnShrink();
        }

        public static string UnShrink(this string target)
        {
            string rez = string.Empty;

            if ((!string.IsNullOrEmpty(target)))
            {
                string encoded = target.Trim().Replace("-", "+").Replace("_", "/");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);
                    rez = Encoding.Unicode.GetString(base64);
                }
                catch (FormatException)
                {
                }
            }

            return rez;
        }


        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }
        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }


        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        
        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                string encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }

            return result;
        }


        
        public static string RemoveAccents(this string str)
        {

            String normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();

        }
        
        public static string ToTitleCase(this string str)
        {
            str = str.ToLower();
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
    }
}
