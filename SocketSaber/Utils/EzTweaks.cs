using System;
using System.Collections.Generic;

namespace SocketSaber.Utils {
    /// <summary>
    /// alias because i'm so lazy
    /// </summary>
    public class DictStrO : Dictionary<string, object> {
    }
    /// <summary>
    /// Advanced (and better) keyword-based format
    /// </summary>
    public static class AdvFormat {
        /// <summary>
        /// Format string from dict with keywords
        /// </summary>
        /// <param name="srcStr">Source string (string to format)</param>
        /// <param name="format">Dictionary, where keys is keyword for format, and values is values!</param>
        /// <param name="strictize">Need to throw exception if formattion failed?</param>
        /// <returns>Formatted string</returns>
        /// <exception cref="FormatException">When formattion fails</exception>
        public static string Format(string srcStr, Dictionary<string, object> format, bool strictize = false) {
            if (srcStr != null || srcStr != "") {
                foreach (var k in format.Keys) {
                    try {
                        srcStr = srcStr.Replace($@"{{{k}}}", format[k].ToString());
                    } catch (ArgumentNullException e) {
                        if (strictize) throw new FormatException($@"unable to format string with null key", e);
                    } catch (ArgumentException e) {
                        if (strictize) throw new FormatException($@"unable to format string with {{{k}}} key", e);
                    }
                }
                return srcStr;
            }
            throw new FormatException("unable to format empty string");
        }
    }
}
