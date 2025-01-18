

using EPYSLSAILORAPP.Application.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;


namespace Web.Core.Frame.Helpers
{


    public static class clsUtil
    {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        public static string CleanJsonString(string cleanedJsonString)
        {
            cleanedJsonString = cleanedJsonString.Replace("\\r\\n", "").Replace("\\n", "").Replace("\\", "")
                .Replace("\"{", "{").Replace("}\"", "}").Replace("\"[", "[").Replace("]\"", "]").Replace("\\\"", "\"");

            return cleanedJsonString;

        }


        public static string GetBase64DecodedString(string EncodedData)
        {
            try
            {
                var decodedBytes = Convert.FromBase64String(EncodedData);
                var decodedData = Encoding.UTF8.GetString(decodedBytes);

                return decodedData;

            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
        public static string GetUnScapeString(string str)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Unescape(str);

            }
            catch (Exception ex)
            {
                return str;
            }
        }
        public static string GenStyleCode(string product_name)
        {
            try
            {
                string ShortName = "";
                product_name.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(i => ShortName += (char.IsLetter(i[0]) ? i[0].ToString():""));

                return ShortName.ToUpper(); 
            }
            catch(Exception ex) {
                return product_name;
            }

        }


        private static bool IsDefaultValue(object value, Type type)
        {
            return value == null || value.Equals(Activator.CreateInstance(type));
        }

        public static T CombineObjects<T>(params T[] objects)
       where T : new()
        {
            // Create a new instance of the target type
            var combinedObject = new T();

            // Get the properties of the target type
            var properties = typeof(T).GetProperties();

            // Iterate over the source objects
            foreach (var sourceObject in objects)
            {
                // Iterate over the properties
                foreach (var property in properties)
                {
                    // Get the value of the property from the source object
                    var value = property.GetValue(sourceObject);

                    // If the value is not the default value for the property type, set it in the combined object
                    if (!IsDefaultValue(value, property.PropertyType))
                    {
                        property.SetValue(combinedObject, value);
                    }
                }
            }

            return combinedObject;
        }
        public static string CamelCase(this string str)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            str = cultInfo.ToTitleCase(str);
            str = str.Replace(" ", "");
            return str;
        }
        public static List<KAFParamsListGeneric> ToSelectList<T>(Type type)
        {
            List<KAFParamsListGeneric> obj = new List<KAFParamsListGeneric>();
            foreach (var enumName in Enum.GetNames(type))
            {
                var idValue = ((int)Enum.Parse(type, enumName, true)).ToString();
                var displayValue = enumName;

                // get the corresponding enum field using reflection
                var field = type.GetField(enumName);
                var display = ((System.ComponentModel.DataAnnotations.DisplayAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)).FirstOrDefault();
                if (display != null)
                {
                    displayValue = display.Name;
                }
                obj.Add(new KAFParamsListGeneric()
                {
                    paramname = displayValue,
                    paramvalue = idValue
                });

            }

            return obj;
        }
        public static string GetAbbreviation(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            string[] words = input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            string abbreviation = string.Join("_", words.Select(word => word[0]));

            return abbreviation.ToLower();
        }
        public static string GetDisplayName(long? value, Type type)
        {
            string displayValue = "";
            try
            {
                foreach (var enumName in Enum.GetNames(type))
                {
                    if (((int)Enum.Parse(type, enumName, true)) == value)
                    {
                        var field = type.GetField(enumName);
                        var display = ((System.ComponentModel.DataAnnotations.DisplayAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)).FirstOrDefault();
                        if (display != null)
                        {
                            displayValue = display.Name;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return displayValue;
        }

        public static string EncodeString(string toEncode)
        {
           
            try
            {
                byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
                string toReturn = System.Convert.ToBase64String(bytes);
                return toReturn;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string DecodeString(string toDecode)
        {
            try
            {
                return Encoding.GetEncoding(28591).GetString(Convert.FromBase64String(toDecode));
            }
            catch (Exception ex) 
            {
                return "0";
            }
            
        }
    }
}