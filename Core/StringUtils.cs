﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SS.Gather.Core
{
    public static class StringUtils
    {
        public static string HtmlDecode(string inputString)
        {
            return HttpUtility.HtmlDecode(inputString);
        }

        public static string HtmlEncode(string inputString)
        {
            return HttpUtility.HtmlEncode(inputString);
        }

        public static string StripTags(string inputString)
        {
            var retval = RegexUtils.Replace("<script[^>]*>.*?<\\/script>", inputString, string.Empty);
            retval = RegexUtils.Replace("<[\\/]?[^>]*>|<[\\S]+", retval, string.Empty);
            return retval;
        }

        public static string StripTags(string inputString, params string[] tagNames)
        {
            var retval = inputString;
            foreach (var tagName in tagNames)
            {
                retval = RegexUtils.Replace($"<[\\/]?{tagName}[^>]*>|<{tagName}", retval, string.Empty);
            }
            return retval;
        }

        public static bool IsMobile(string val)
        {
            return Regex.IsMatch(val, @"^1[3456789]\d{9}$", RegexOptions.IgnoreCase);
        }

        public static bool IsEmail(string val)
        {
            return Regex.IsMatch(val, @"^\w+([-_+.]\w+)*@\w+([-_.]\w+)*\.\w+([-_.]\w+)*$", RegexOptions.IgnoreCase);
        }

        public static bool IsNumber(string val)
        {
            const string formatNumber = "^[0-9]+$";
            return Regex.IsMatch(val, formatNumber);
        }

        public static bool IsDateTime(string val)
        {
            const string formatDate = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            const string formatDateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";

            return Regex.IsMatch(val, formatDate) || Regex.IsMatch(val, formatDateTime);
        }

        public static bool In(string strCollection, int inInt)
        {
            return In(strCollection, inInt.ToString());
        }

        public static bool In(string strCollection, string inStr)
        {
            if (string.IsNullOrEmpty(strCollection)) return false;
            return strCollection == inStr || strCollection.StartsWith(inStr + ",") || strCollection.EndsWith("," + inStr) || strCollection.IndexOf("," + inStr + ",", StringComparison.Ordinal) != -1;
        }

        public static bool Contains(string text, string inner)
        {
            return text?.IndexOf(inner, StringComparison.Ordinal) >= 0;
        }

        public static bool ContainsIgnoreCase(string text, string inner)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(inner)) return false;
            return text.ToLower().IndexOf(inner.ToLower(), StringComparison.Ordinal) >= 0;
        }

        public static bool ContainsIgnoreCase(List<string> list, string target)
        {
            if (list == null || list.Count == 0) return false;

            return list.Any(element => EqualsIgnoreCase(element, target));
        }

        public static string Trim(string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Trim();
        }

        public static string TrimAndToLower(string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.ToLower().Trim();
        }

        public static string Remove(string text, int startIndex)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            if (startIndex >= text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
            return text.Substring(0, startIndex);
        }

        public static string Guid()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static string GetShortGuid()
        {
            long i = 1;
            foreach (var b in System.Guid.NewGuid().ToByteArray())
            {
                i *= b + 1;
            }
            return $"{i - DateTime.Now.Ticks:x}";
        }

        public static string GetShortGuid(bool isUppercase)
        {
            long i = 1;
            foreach (var b in System.Guid.NewGuid().ToByteArray())
            {
                i *= b + 1;
            }
            string retVal = $"{i - DateTime.Now.Ticks:x}";
            return isUppercase ? retVal.ToUpper() : retVal.ToLower();
        }

        public static bool EqualsIgnoreCase(string a, string b)
        {
            if (a == b) return true;
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return false;

            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsIgnoreNull(string a, string b)
        {
            return string.IsNullOrEmpty(a) ? string.IsNullOrEmpty(b) : string.Equals(a, b);
        }

        public static bool StartsWithIgnoreCase(string text, string startString)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(startString)) return false;
            return text.Trim().ToLower().StartsWith(startString.Trim().ToLower()) || string.Equals(text.Trim(), startString.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool EndsWithIgnoreCase(string text, string endString)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(endString)) return false;
            return text.Trim().ToLower().EndsWith(endString.Trim().ToLower());
        }

        public static bool StartsWith(string text, string startString)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(startString)) return false;
            return text.StartsWith(startString);
        }

        public static bool EndsWith(string text, string endString)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(endString)) return false;
            return text.EndsWith(endString);
        }

        public static void InsertBefore(string[] insertBeforeArray, StringBuilder contentBuilder, string insertContent)
        {
            if (contentBuilder == null) return;
            foreach (var insertBefore in insertBeforeArray)
            {
                if (contentBuilder.ToString().IndexOf(insertBefore, StringComparison.Ordinal) != -1)
                {
                    InsertBefore(insertBefore, contentBuilder, insertContent);
                    return;
                }
            }
        }

        private static void InsertBefore(string insertBefore, StringBuilder contentBuilder, string insertContent)
        {
            if (string.IsNullOrEmpty(insertBefore) || contentBuilder == null) return;
            var startIndex = contentBuilder.ToString().IndexOf(insertBefore, StringComparison.Ordinal);
            if (startIndex != -1)
            {
                contentBuilder.Insert(startIndex, insertContent);
            }
        }

        public static void InsertAfter(string[] insertAfterArray, StringBuilder contentBuilder, string insertContent)
        {
            if (contentBuilder != null)
            {
                foreach (var insertAfter in insertAfterArray)
                {
                    if (contentBuilder.ToString().IndexOf(insertAfter, StringComparison.Ordinal) != -1)
                    {
                        InsertAfter(insertAfter, contentBuilder, insertContent);
                        return;
                    }
                }
            }
        }

        private static void InsertAfter(string insertAfter, StringBuilder contentBuilder, string insertContent)
        {
            if (string.IsNullOrEmpty(insertAfter) || contentBuilder == null) return;
            var startIndex = contentBuilder.ToString().IndexOf(insertAfter, StringComparison.Ordinal);
            if (startIndex == -1) return;
            if (startIndex != -1)
            {
                contentBuilder.Insert(startIndex + insertAfter.Length, insertContent);
            }
        }

        public static string ReplaceIgnoreCase(string original, string pattern, string replacement)
        {
            if (original == null) return string.Empty;
            if (replacement == null) replacement = string.Empty;
            var count = 0;
            var position0 = 0;
            int position1;
            var upperString = original.ToUpper();
            var upperPattern = pattern.ToUpper();
            var inc = (original.Length / pattern.Length) * (replacement.Length - pattern.Length);
            var chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern, position0, StringComparison.Ordinal)) != -1)
            {
                for (var i = position0; i < position1; ++i) chars[count++] = original[i];
                foreach (var t in replacement)
                {
                    chars[count++] = t;
                }
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (var i = position0; i < original.Length; ++i) chars[count++] = original[i];
            return new string(chars, 0, count);
        }

        public static string ReplaceFirst(string replace, string input, string to)
        {
            var pos = input.IndexOf(replace, StringComparison.Ordinal);
            if (pos > 0)
            {
                //取位置前部分+替换字符串+位置（加上查找字符长度）后部分
                return input.Substring(0, pos) + to + input.Substring(pos + replace.Length);
            }
            if (pos == 0)
            {
                return to + input.Substring(replace.Length);
            }
            return input;
        }

        public static string ReplaceStartsWith(string input, string replace, string to)
        {
            var retVal = input;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(replace) && input.StartsWith(replace))
            {
                retVal = to + input.Substring(replace.Length);
            }
            return retVal;
        }

        public static string ReplaceStartsWithIgnoreCase(string input, string replace, string to)
        {
            var retVal = input;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(replace) && input.ToLower().StartsWith(replace.ToLower()))
            {
                retVal = to + input.Substring(replace.Length);
            }
            return retVal;
        }

        public static string ReplaceNewlineToBr(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;
            var retVal = new StringBuilder();
            inputString = inputString.Trim();
            foreach (var t in inputString)
            {
                switch (t)
                {
                    case '\n':
                        retVal.Append("<br />");
                        break;
                    case '\r':
                        break;
                    default:
                        retVal.Append(t);
                        break;
                }
            }
            return retVal.ToString();
        }

        public static string ReplaceNewline(string inputString, string replacement)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;
            var retVal = new StringBuilder();
            inputString = inputString.Trim();
            foreach (var t in inputString)
            {
                switch (t)
                {
                    case '\n':
                        retVal.Append(replacement);
                        break;
                    case '\r':
                        break;
                    default:
                        retVal.Append(t);
                        break;
                }
            }
            return retVal.ToString();
        }

        /// <summary>
        /// 得到innerText在content中的数目
        /// </summary>
        /// <param name="innerText"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int GetCount(string innerText, string content)
        {
            if (innerText == null || content == null)
            {
                return 0;
            }
            var count = 0;
            for (var index = content.IndexOf(innerText, StringComparison.Ordinal); index != -1; index = content.IndexOf(innerText, index + innerText.Length, StringComparison.Ordinal))
            {
                count++;
            }
            return count;
        }

        public static int GetStartCount(char startChar, string content)
        {
            if (content == null)
            {
                return 0;
            }
            var count = 0;

            foreach (var theChar in content)
            {
                if (theChar == startChar)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        public static int GetStartCount(string startString, string content)
        {
            if (content == null)
            {
                return 0;
            }
            var count = 0;

            while (true)
            {
                if (content.StartsWith(startString))
                {
                    count++;
                    content = content.Remove(0, startString.Length);
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        public static string GetFirstOfStringCollection(string collection, char separator)
        {
            if (!string.IsNullOrEmpty(collection))
            {
                var index = collection.IndexOf(separator);
                return index == -1 ? collection : collection.Substring(0, index);
            }
            return string.Empty;
        }


        private static int _randomSeq;
        public static int GetRandomInt(int minValue, int maxValue)
        {
            var ro = new Random(unchecked((int)DateTime.Now.Ticks));
            var retVal = ro.Next(minValue, maxValue);
            retVal += _randomSeq++;
            if (retVal >= maxValue)
            {
                _randomSeq = 0;
                retVal = minValue;
            }
            return retVal;
        }

        public static string ValueToUrl(string value)
        {
            var retVal = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                //替换url中的换行符，update by sessionliang at 20151211
                retVal = value.Replace("=", "_equals_").Replace("&", "_and_").Replace("?", "_question_").Replace("'", "_quote_").Replace("+", "_add_").Replace("\r", "").Replace("\n", "");
            }
            return retVal;
        }

        public static string ValueFromUrl(string value)
        {
            var retVal = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                retVal = value.Replace("_equals_", "=").Replace("_and_", "&").Replace("_question_", "?").Replace("_quote_", "'").Replace("_add_", "+");
            }
            return retVal;
        }

        public static string ToJsString(string value)
        {
            var retVal = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                retVal = value.Replace("'", @"\'").Replace("\r", "\\r").Replace("\n", "\\n");
            }
            return retVal;
        }

        public static string LowerFirst(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.First().ToString().ToLower() + input.Substring(1);
        }
    }
}
