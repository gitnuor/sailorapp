using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;

public class TimezoneAwareDateTimeModelBinder : IModelBinderProvider
{

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(DateTime))
        {
            var tsss = context;
            //var valueProviderResult = bindingContext.ValueProvider.GetValue(context.ModelName);
            //if (valueProviderResult != ValueProviderResult.None)
            //{
            //    var value = valueProviderResult.FirstValue;
            //    if (DateTime.TryParse(value, out DateTime dateTime))
            //    {
            //        // Adjust timezone here
            //        dateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Utc);

            //        bindingContext.Result = ModelBindingResult.Success(dateTime);
            //        //return Task.CompletedTask;
            //        return new BinderTypeModelBinder(typeof(DateTime));
            //    }
            //}
            throw new ArgumentNullException(nameof(context));
        }


        return null;
    }


}
//public Task BindModelAsync(ModelBindingContext bindingContext)
//{
//    var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
//    if (valueProviderResult != ValueProviderResult.None)
//    {
//        var value = valueProviderResult.FirstValue;
//        if (DateTime.TryParse(value, out DateTime dateTime))
//        {
//            // Adjust timezone here
//            dateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Utc);

//            bindingContext.Result = ModelBindingResult.Success(dateTime);
//            return Task.CompletedTask;
//        }
//    }

//    // Fallback to default model binding behavior
//    bindingContext.Result = ModelBindingResult.Failed();
//    return Task.CompletedTask;
//}


