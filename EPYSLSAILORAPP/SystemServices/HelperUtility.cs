using BDO.Core.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;



namespace EPYSLSAILORAPP.HelperUtility
{
    public static class HelperUtility
    {
      
        public static async Task<byte[]> GeneratePdf(string html)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            var htmlparser = new HTMLWorker(pdfDoc);

            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                pdfDoc.Open();

                htmlparser.Parse(new StringReader(html));

                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();

                memoryStream.Close();

                return bytes;
            }

        }

        
        public static class NumberToWordsConverter
        {
            public static string ConvertNumberToWords(long n)
            {
                string result = "";
                long crore = n / 10000000;
                long remainder = n % 10000000;

                if (remainder == 0)
                {
                    result = $"{SpellingNumber(crore)} Crore Taka".ToLower();
                }
                else
                {
                    long lakh = remainder / 100000;
                    long lakhRemainder = remainder % 100000;

                    if (lakh == 0)
                    {
                        result = $"{(crore != 0 ? $"{SpellingNumber(crore)} Crore " : "")}{SpellingNumber(remainder)} Taka".ToLower();
                    }
                    else
                    {
                        if (lakhRemainder == 0)
                        {
                            result = $"{(crore != 0 ? $"{SpellingNumber(crore)} Crore " : "")}{(lakh != 0 ? $"{SpellingNumber(lakh)} Lakh " : "")}".ToLower();
                        }
                        else
                        {
                            result = $"{(crore != 0 ? $"{SpellingNumber(crore)} Crore " : "")}{(lakh != 0 ? $"{SpellingNumber(lakh)} Lakh " : "")}{SpellingNumber(lakhRemainder)} Taka".ToLower();
                        }
                    }
                }

                return ToCamelCase(result);
            }

            // Placeholder for spellingNumber equivalent in C#
            private static string SpellingNumber(long number)
            {
                // Convert number to words (implement this method according to your needs)
                return NumberToWords(number); // Example method, replace with actual implementation
            }

            // Example method for converting a number to words
            private static string NumberToWords(long number)
            {
                if (number == 0) return "zero";

                if (number < 0) return "minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 10000000) > 0)
                {
                    words += NumberToWords(number / 10000000) + " crore ";
                    number %= 10000000;
                }

                if ((number / 100000) > 0)
                {
                    words += NumberToWords(number / 100000) + " lakh ";
                    number %= 100000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "and ";

                    var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += "-" + unitsMap[number % 10];
                    }
                }

                return words;
            }

            // Convert the text to camel case
            private static string ToCamelCase(string str)
            {
                if (string.IsNullOrEmpty(str))
                    return str;

                return char.ToUpper(str[0]) + str.Substring(1).ToLower();
            }
        }

    }
}
