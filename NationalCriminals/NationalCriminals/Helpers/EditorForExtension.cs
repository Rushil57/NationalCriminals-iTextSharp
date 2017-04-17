using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NationalCriminals.Helpers
{
    /// <summary>
    /// Represents the class for editor for extension.
    /// </summary>
    public static class EditorForExtension
    {
        /// <summary>
        /// Creates an HTML-encoded string using the specified text value.
        /// </summary>
        /// <param name="html">Html of control.</param>
        /// <param name="expression">Lambda expression.</param>
        /// <param name="isExtended">Flag to identify whether it is extended version or not.</param>
        /// <returns>HTML-encoded string.</returns>
        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool isExtended = true)
        {
            return EditorFor(html, expression, null, isExtended);
        }

        /// <summary>
        /// Creates an HTML-encoded string using the specified text value.
        /// </summary>
        /// <param name="html">Html of control.</param>
        /// <param name="expression">Lambda expression.</param>
        /// <param name="htmlAttributes">Html attributes of control.</param>
        /// <param name="isExtended">Flag to identify whether it is extended version or not.</param>
        /// <returns>HTML-encoded string.</returns>
        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes, bool isExtended = true)
        {
            const string maxlengthAttribute = "maxlength";
            string value = EditorExtensions.EditorFor(html, expression, htmlAttributes).ToString();

            // Change type="number" with type="tel". so that RegEx works on this field. Remove value="0".
            const string typeNumber = "type=\"number\"";
            if (value.IndexOf(typeNumber, StringComparison.OrdinalIgnoreCase) != -1)
            {
                const string typeTel = "type=\"tel\"", valueZero = "value=\"0\"";
                value = value.Replace(typeNumber, typeTel).Replace(valueZero, string.Empty);
            }

            // If maxlength attribute is already added to control, than don't change it.
            if (value.IndexOf(maxlengthAttribute, StringComparison.OrdinalIgnoreCase) == -1)
            {
                MemberExpression memberExpression = expression.Body as MemberExpression;
                if (memberExpression != null)
                {
                    var htmlPropertiesAttribute = memberExpression.Member.GetCustomAttributes(typeof(HtmlPropertiesAttribute), false).FirstOrDefault() as HtmlPropertiesAttribute;
                    int insertMaxLengthAttributeAtindex = -1;
                    if (htmlPropertiesAttribute != null)
                    {
                        const string endTag = "/>", textareaEndTag = "</textarea>";
                        string maxlengthAttributeWithValue = string.Format(" {0}=\"{1}\"", maxlengthAttribute, htmlPropertiesAttribute.MaxLength);
                        if (value.IndexOf(endTag, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            insertMaxLengthAttributeAtindex = value.Length - endTag.Length;
                        }
                        else if (value.IndexOf(textareaEndTag, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            insertMaxLengthAttributeAtindex = value.Replace(textareaEndTag, string.Empty).LastIndexOf('>');
                        }

                        if (insertMaxLengthAttributeAtindex != -1)
                        {
                            value = value.Insert(insertMaxLengthAttributeAtindex, maxlengthAttributeWithValue);
                        }
                    }
                }
            }
            return MvcHtmlString.Create(value);
        }
    }

    /// <summary>
    /// Represents the class for html attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HtmlPropertiesAttribute : Attribute
    {
        public int MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// Adds html attributes Maxlength.
        /// </summary>
        /// <returns>Dictionary.</returns>
        public IDictionary<string, object> HtmlAttributes()
        {
            var htmlatts = new Dictionary<string, object>();

            if (MaxLength != 0)
            {
                htmlatts.Add("MaxLength", MaxLength);
            }

            return htmlatts;
        }
    }

}