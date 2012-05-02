using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Gamification.Core;
using Gamification.Web.Utils.CommonViewModels;
using Microsoft.Web.Mvc;

namespace Gamification.Web.Utils.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString AddClassIfTrue(this HtmlHelper html, bool condition, string className)
        {
            Guard.StringArgumentIsNullOrBlank(className, "className");
            if (condition)
                return MvcHtmlString.Create(string.Format("class=\"{0}\"", className));

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString ActiveClassIf(this HtmlHelper html, bool condition)
        {
            return html.AddClassIfTrue(condition, "active");
        }

        public static MvcHtmlString BuildSelectForEnum<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> accessor)
            where TEnum : struct where TModel : class
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("only enum supported for building select list", "accessor");
            
            var selectBuilder = new StringBuilder();
            var selectTag = new TagBuilder("select");
            var selectId = html.IdFor(accessor).ToString();
            var name = html.NameFor(accessor).ToString();
            selectTag.MergeAttribute("id", selectId);
            selectTag.MergeAttribute("name", name);
            selectBuilder.Append(selectTag.ToString(TagRenderMode.StartTag));
            
            var enumValues = typeof(TEnum).GetEnumValues();
            object currentValue;
            if (html.ViewData.Model == null)
            {
                var enumerator = enumValues.GetEnumerator();
                enumerator.MoveNext();
                currentValue = enumerator.Current;
            }
            else
            {
                currentValue = accessor.Compile().Invoke(html.ViewData.Model);
            }
            
            foreach (var value in enumValues)
            {
                var optionTab = new TagBuilder("option");
                if (value == currentValue)
                {
                    optionTab.MergeAttribute("selected", "selected");
                }

                optionTab.MergeAttribute("value", ((byte)value).ToString());
                optionTab.SetInnerText(value.ToString());
                selectBuilder.Append(optionTab.ToString());
            }

            selectBuilder.Append(selectTag.ToString(TagRenderMode.EndTag));

            return MvcHtmlString.Create(selectBuilder.ToString());
        }

        public static MvcHtmlString SelectListFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, DataSource>> propertyAccessor) where TModel : class
        {
            if (html.ViewData.Model == null)
            {
                return html.DropDownListFor(propertyAccessor, Enumerable.Empty<SelectListItem>());
            }

            var dataSource = propertyAccessor.Compile().Invoke(html.ViewData.Model);
            if (dataSource == null)
            {
                return html.DropDownListFor(propertyAccessor, Enumerable.Empty<SelectListItem>());
            }

            var selectItems = dataSource.Select(x => new SelectListItem
                                                         {
                                                             Selected = x.Selected,
                                                             Value = x.Value.ToString(),
                                                             Text = x.Text
                                                         }).ToList();
            return html.DropDownListFor(propertyAccessor, selectItems);
        }
    }
}
