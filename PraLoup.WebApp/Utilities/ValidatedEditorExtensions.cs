using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PraLoup.WebApp.Utilities
{
    public static class LabelForExtensions
    {
        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata.IsRequired)
            {
                TagBuilder tb = new TagBuilder("span");
                tb.AddCssClass("fld_req");
                tb.InnerHtml = htmlHelper.LabelFor(expression).ToHtmlString() + "*";
                return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
            }
            else
            {
                return htmlHelper.LabelFor(expression);
            }
        }
    }

    public static class ValidatedEditorExtensions
    {
        public static MvcHtmlString ValidatedEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var html = htmlHelper.EditorFor(expression).ToString() +
                       htmlHelper.DescriptionFor(expression).ToString() +
                       htmlHelper.ValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ValidatedDropDownFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            var html = htmlHelper.DropDownListFor(expression, selectList).ToString() +
                       htmlHelper.ValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(html);
        }
    }

    public static class DescriptionForExtensions
    {
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expresion)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expresion, htmlHelper.ViewData);
            var description = metadata.Description;
            var tag = string.Empty;
            if (!string.IsNullOrEmpty(description))
            {
                TagBuilder builder = new TagBuilder("div");
                builder.AddCssClass("edit_dsc");
                builder.InnerHtml = description;
                tag = builder.ToString(TagRenderMode.Normal);
            }
            return MvcHtmlString.Create(tag);
        }
    }
}
