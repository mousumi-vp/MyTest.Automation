using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database.Models;
using Database.OModels;
using Winnovative.PdfToText;

namespace Repository
{
    public class Utility
    {
        public static string GetTextFromPDF(string path, string key)
        {
            PdfToTextConverter pdfToTextConverter = new PdfToTextConverter();
            pdfToTextConverter.LicenseKey = key;
            string extractedText = pdfToTextConverter.ConvertToText(path);
            return extractedText.ToString();

        }
        public static string ExtractEmails(string textToScrape)
        {
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match match;
            List<string> results = new List<string>();
            for (match = reg.Match(textToScrape); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }
            if (results.Count > 0)
                return results[0];
            else
                return "To Be Updated";
        }
        public static string ExtractPhones(string textToScrape)
        {
            Regex reg = new Regex(@"\d+", RegexOptions.IgnoreCase);
            Match match;
            List<string> results = new List<string>();
            for (match = reg.Match(textToScrape); match.Success; match = match.NextMatch())
            {
                if (!(results.Contains(match.Value)))
                    results.Add(match.Value);
            }
            results = results.Where(x => x.Length >= 10).ToList();
            if (results.Count > 0)
                return results[0];
            else
                return "To Be Updated";
        }
        public static string EncodeValue(string text)
        {
            byte[] encData_byte = new byte[text.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(text);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        public static string base64Encode(string sData) // Encode
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string base64Decode(string sData) //Decode
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
        public static DateTime ConvertDateSourceToDestTimeZone(string sourceTimeZone, DateTime dateValue, string destTimeZone)
        {
            TimeZoneInfo FROM_ZONE = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZone);
            TimeZoneInfo TO_ZONE = TimeZoneInfo.FindSystemTimeZoneById(destTimeZone);
            DateTime time;
            DateTime utctime = TimeZoneInfo.ConvertTimeToUtc(dateValue, FROM_ZONE);
            time = TimeZoneInfo.ConvertTimeFromUtc(utctime, TO_ZONE);
            return time;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string ShortTimeZone(string timezone)
        {
            if (timezone == null)
                return "";
            string[] lst = timezone.Split(' ');
            string shortcode = "";
            lst.ToList().ForEach(x =>
            {
                if (x.Length > 0)
                    shortcode += x[0];
            });
            return shortcode;
        }
        public static string GenerateUniqueCode(int length)
        {
            const string alphanumericChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(alphanumericChars.Length);
                char randomChar = alphanumericChars[randomIndex];
                sb.Append(randomChar);

                // Add "-" as a separator after every 3 characters (except the last group)
                if (i < length - 1 && (i + 1) % 3 == 0)
                    sb.Append("-");
            }

            return sb.ToString();
        }
    }
}
