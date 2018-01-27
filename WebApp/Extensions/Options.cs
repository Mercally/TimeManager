using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TimeManager.Entities.Extensions;

namespace TimeManager.WebApp.Extensions
{
    public static class Options
    {
        /// <summary>
        /// Return options represnting the True and False titles of a 
        /// boolean field.
        /// </summary>
        /// <returns>A list with the false title at position 0, 
        /// and true title at position 1.</returns>
        public static IList<SelectListItem> OptionsForBoolean<TModel,
                                                              TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression,
                                                          htmlHelper.ViewData);
            object trueTitle;
            metaData.AdditionalValues.TryGetValue(
                BooleanDisplayValuesAttribute.TrueTitleAdditionalValueName,
                out trueTitle);
            trueTitle = trueTitle ?? "Si";

            object falseTitle;
            metaData.AdditionalValues.TryGetValue(
                BooleanDisplayValuesAttribute.FalseTitleAdditionalValueName,
                out falseTitle);
            falseTitle = falseTitle ?? "No";

            var options = new[]
                                {
                            new SelectListItem {Text = (string) falseTitle,
                                                Value = Boolean.FalseString},
                            new SelectListItem {Text = (string) trueTitle,
                                                Value = Boolean.TrueString},
                        };
            return options;
        }
    }
}