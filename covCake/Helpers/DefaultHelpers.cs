using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Text;
using System.Web.Mvc.Ajax;
namespace covCake.Helpers
{

    public static class DefaultHelperExtensions
    {

        public static string MailTo(this HtmlHelper htmlHelper, string email)
        {
            return htmlHelper.MailTo(email, "", "");
        }

        public static string MailTo(this HtmlHelper htmlHelper, string email, string linkText)
        {
            return htmlHelper.MailTo(email, linkText, "");
        }
        public static string MailTo(this HtmlHelper htmlHelper, string email, string linkText, string subject)
        {
            //<a href="mailto:chose@bidule.fr?subject=Zobi">ici</a>
            subject = (!subject.IsNullOrEmpty()) ? "?subject="+subject : "";
          //  linkText = (!linkText.IsNullOrEmpty()) ? linkText : email;
            linkText = linkText.SwapIfEmpty(email);
            string mailto = "mailto:"+email+subject;
            return htmlHelper.Link(linkText, mailto);

        }
   

        public static string ImageLink(this HtmlHelper htmlHelper,  string imgSrc, string alt, string actionName)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            return htmlHelper.ImageLink(imgSrc, alt, actionName, controllerName, null, null,null);
        }


        public static string ImageLink(this HtmlHelper htmlHelper,  string imgSrc, string alt, string actionName, string controllerName)
        {
            return htmlHelper.ImageLink( imgSrc, alt, actionName, controllerName, null, null,null);
        }


        public static string ImageLink(this HtmlHelper htmlHelper,  string imgSrc, string alt, string actionName, string controllerName, object routeValues)
        {
           // string img = htmlHelper.Image(imgSrc, alt);
            return htmlHelper.ImageLink(imgSrc, alt, actionName, controllerName, routeValues, null,null);
            //return htmlHelper.ActionLink(img, actionName, controllerName, routeValues, htmlAttributes);
        }
        public static string ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues,object htmlAttributes)
        {

            // string img = htmlHelper.Image(imgSrc, alt);
            return htmlHelper.ImageLink( imgSrc, alt, actionName, controllerName, routeValues, htmlAttributes, null);
            //return htmlHelper.ActionLink(img, actionName, controllerName, routeValues, htmlAttributes);
        }
        public static string ImageLink(this AjaxHelper ajaxHelper, string imgSrc, string alt, string actionName, string controllerName, AjaxOptions ajaxOptions, object routeValues, object htmlAttributes)
        {

            // string img = htmlHelper.Image(imgSrc, alt);
            return ajaxHelper.ImageLink( imgSrc, alt, actionName, controllerName,routeValues,ajaxOptions,htmlAttributes, null);
            //return htmlHelper.ActionLink(img, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static string ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            UrlHelper urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            string imgtag = htmlHelper.Image(imgSrc, alt,imgHtmlAttributes);
            string url = urlHelper.Action(actionName, controllerName, routeValues);

  

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml =imgtag;
            imglink.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return imglink.ToString();
    
        }
//   
        public static string ImageLink(this AjaxHelper ajaxHelper, string imgSrc, string alt, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, object imgHtmlAttributes)
        {

            string imgtag = new HtmlHelper(ajaxHelper.ViewContext,ajaxHelper.ViewDataContainer).Image(imgSrc, alt, imgHtmlAttributes);
            string url = new UrlHelper(ajaxHelper.ViewContext.RequestContext).Action(actionName, controllerName, routeValues);
            // string urlImg = "<a href='" + url + "'>" + imgtag + "</a>";

            TagBuilder imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgtag;
            imglink.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            return imglink.ToString();

        }
        public static string Label(this HtmlHelper htmlhelper, string labelText, string id) //J'ai oublier mon mot de passe","Forgot","Account",null)
        {
            return "<label for=\""+id+"\">"+labelText+"</label>";
        }

        public static string TimerRedirect(this HtmlHelper htmlhelper, string url, int seconds)
        {
            string jscode = "<script type='text/javascript'> function Redirect(){{ location.href = '{0}'; }} setTimeout('Redirect()',{1});</script>";
           // jscode = "{0}{1}";
            jscode = jscode.FormatWith(url, seconds * 1000);
            return jscode;
        }

        #region File

        public static string File(this HtmlHelper htmlHelper,string name)
        {
            return "<input type='file' name='"+name+"'>";
        }
       // <INPUT TYPE=file NAME="html_file" ACCEPT="text/html"></P>
        //<input type='file' name='pix'><br>


        #endregion

        #region Gravatar

        //public static string Gravatar(this HtmlHelper htmlHelper, IUser user, string size, string defaultImage)
        //{
        //    return htmlHelper.Gravatar(user.HashedEmail, user.DisplayName, size, defaultImage);
        //}

        //public static string Gravatar(this HtmlHelper htmlHelper, string id, string name, string size,
        //                              string defaultImage)
        //{
        //    return htmlHelper.Image(
        //        string.Format("http://www.gravatar.com/avatar/{0}?s={1}&default={2}", id, size, defaultImage),
        //        string.Format("{0} (gravatar)", name),
        //        new RouteValueDictionary(new {width = size, height = size, @class = "gravatar"})
        //        );
        //}

        #endregion

        #region HeadLink

        //public static string HeadLink(this HtmlHelper htmlHelper, HeadLink headLink)
        //{
        //    return htmlHelper.HeadLink(headLink.Rel, headLink.Href, headLink.Type, headLink.Title,
        //                               headLink.HtmlAttributes);
        //}

        //public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title)
        //{
        //    return htmlHelper.HeadLink(rel, href, type, title, null);
        //}

        //public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title,
        //                              object htmlAttributes)
        //{
        //    TagBuilder tagBuilder = new TagBuilder("link");

        //    tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        //    if (!string.IsNullOrEmpty(rel))
        //    {
        //        tagBuilder.MergeAttribute("rel", rel);
        //    }
        //    if (!string.IsNullOrEmpty(href))
        //    {
        //        tagBuilder.MergeAttribute("href", href);
        //    }
        //    if (!string.IsNullOrEmpty(type))
        //    {
        //        tagBuilder.MergeAttribute("type", type);
        //    }
        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        tagBuilder.MergeAttribute("title", title);
        //    }

        //    return tagBuilder.ToString(TagRenderMode.SelfClosing);
        //}

        #endregion

        #region Image

        public static string Image(this HtmlHelper helper, string src, string alt)
        {
            src = src.Replace("~", "");
            return  string.Format("<img src=\"{0}\" alt=\"{1}\"/>", src,alt);
        }

        public static string Image(this HtmlHelper helper, string imageSrc, string alt, object htmlAttributes)
        {
            imageSrc = imageSrc.Replace("~", "");
            return Image(helper, imageSrc, alt, new RouteValueDictionary(htmlAttributes));
        }

        public static string Image(this HtmlHelper helper, string imageSrc, string alt, IDictionary<string, object> htmlAttributes)
        {

            if (string.IsNullOrEmpty(imageSrc)) imageSrc = "/";
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            string imageUrl = url.Content(imageSrc);

            TagBuilder imageTag = new TagBuilder("img");

            if (!string.IsNullOrEmpty(imageUrl))
            {
                imageTag.MergeAttribute("src", imageUrl);
            }

            if (!string.IsNullOrEmpty(alt))
            {
                imageTag.MergeAttribute("alt", alt);
            }

            imageTag.MergeAttributes(htmlAttributes, true);

            if (imageTag.Attributes.ContainsKey("alt") && !imageTag.Attributes.ContainsKey("title"))
            {
                imageTag.MergeAttribute("title", imageTag.Attributes["alt"] ?? "");
            }

            return imageTag.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region Input
        public static string Submit(this HtmlHelper htmlHelper, string value)
        {
            //Applique le style par defaut
            //Not Good
            //return htmlHelper.Submit(value, null);
            return htmlHelper.Submit(value, new { @class = "button" });

        }
        public static string Submit(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return htmlHelper.Submit(value, new RouteValueDictionary(htmlAttributes));

        }

         public static string Submit(this HtmlHelper htmlHelper, string value, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "submit");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("value", value);
            return tagBuilder.ToString(TagRenderMode.SelfClosing); 

        }
        
        public static string DropDownList(this HtmlHelper htmlHelper, string name, SelectList selectList,
                                          object htmlAttributes, bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();
                inputItemBuilder.AppendLine(htmlHelper.DropDownList(string.Format("{0}_view", name), selectList,
                                                                    htmlAttributeDictionary));
                inputItemBuilder.AppendLine(htmlHelper.Hidden(name, selectList.SelectedValue));
                return inputItemBuilder.ToString();
            }

            return htmlHelper.DropDownList(name, selectList, htmlAttributeDictionary);
        }

        public static string RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked,
                                         object htmlAttributes, bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();
                inputItemBuilder.AppendLine(htmlHelper.RadioButton(string.Format("{0}_view", name), value, isChecked,
                                                                   htmlAttributeDictionary));
                if (isChecked)
                {
                    inputItemBuilder.AppendLine(htmlHelper.Hidden(name, value));
                }
                return inputItemBuilder.ToString();
            }

            return htmlHelper.RadioButton(name, value, isChecked, htmlAttributeDictionary);
        }

        public static string TextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes,
                                     bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();
                inputItemBuilder.Append(htmlHelper.TextBox(string.Format("{0}_view", name), value,
                                                           htmlAttributeDictionary));
                inputItemBuilder.Append(htmlHelper.Hidden(name, value));
                return inputItemBuilder.ToString();
            }

            return htmlHelper.TextBox(name, value, htmlAttributeDictionary);
        }

        public static string Button(this HtmlHelper htmlHelper, string name, string buttonContent, object htmlAttributes)
        {
            return htmlHelper.Button(name, buttonContent, new RouteValueDictionary(htmlAttributes));
        }

        public static string Button(this HtmlHelper htmlHelper, string name, string buttonContent,
                                    IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("button")
                                    {
                                        InnerHtml = buttonContent
                                    };
            tagBuilder.MergeAttributes(htmlAttributes);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region Link

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href)
        {
            return Link(htmlHelper, linkText, href, null);
        }

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href, object htmlAttributes)
        {
            return htmlHelper.Link(linkText, href, new RouteValueDictionary(htmlAttributes));
        }

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href,
                                  IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("a")
                                    {
                                        InnerHtml = linkText
                                    };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", href);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region Pager

        //public static string SimplePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList, BaseViewPage view,
        //                                    string routeName, object values)
        //{
        //    return htmlHelper.SimplePager(pageOfAList, s => view.Localize(s), routeName, values);
        //}

        //public static string SimplePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                    BaseViewUserControl control, string routeName, object values)
        //{
        //    return htmlHelper.SimplePager(pageOfAList, s => control.Localize(s), routeName, values);
        //}

        //public static string SimplePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                    Func<string, string> localize, string routeName, object values)
        //{
        //    return SimplePager<T>(htmlHelper, pageOfAList, routeName, values, localize("« Newer"), localize("Older »"),
        //                          false);
        //}

        //public static string SimplePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList, string routeName,
        //                                    object values, string previousText, string nextText,
        //                                    bool alwaysShowPreviousAndNext)
        //{
        //    StringBuilder sb = new StringBuilder(50);
        //    ViewContext viewContext = htmlHelper.ViewContext;
        //    RouteValueDictionary rvd = new RouteValueDictionary();

        //    foreach (KeyValuePair<string, object> item in viewContext.RouteData.Values)
        //    {
        //        rvd.Add(item.Key, item.Value);
        //    }

        //    UrlHelper urlHelper = new UrlHelper(viewContext);

        //    rvd.Remove("controller");
        //    rvd.Remove("action");
        //    rvd.Remove("id");

        //    if (values != null)
        //    {
        //        RouteValueDictionary rvd2 = new RouteValueDictionary(values);

        //        foreach (KeyValuePair<string, object> item in rvd2)
        //        {
        //            rvd[item.Key] = item.Value;
        //        }
        //    }

        //    if (pageOfAList.PageIndex < pageOfAList.TotalPageCount - 1 || alwaysShowPreviousAndNext)
        //    {
        //        rvd["page"] = pageOfAList.PageIndex + 2;

        //        sb.AppendFormat("<a href=\"{1}{2}\" class=\"next\">{0}</a>", nextText,
        //                        urlHelper.RouteUrl(routeName, rvd),
        //                        viewContext.HttpContext.Request.QueryString.ToQueryString());
        //    }

        //    if (pageOfAList.PageIndex > 0 || alwaysShowPreviousAndNext)
        //    {
        //        rvd["page"] = pageOfAList.PageIndex;

        //        sb.AppendFormat("<a href=\"{1}{2}\" class=\"previous\">{0}</a>", previousText,
        //                        urlHelper.RouteUrl(routeName, rvd),
        //                        viewContext.HttpContext.Request.QueryString.ToQueryString());
        //    }

        //    return sb.ToString();
        //}

        //public static string SimpleArchivePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                           BaseViewPage view, string routeName, object values)
        //{
        //    return htmlHelper.SimpleArchivePager(pageOfAList, s => view.Localize(s), routeName, values);
        //}

        //public static string SimpleArchivePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                           BaseViewUserControl control, string routeName, object values)
        //{
        //    return htmlHelper.SimpleArchivePager(pageOfAList, s => control.Localize(s), routeName, values);
        //}

        //public static string SimpleArchivePager<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                           Func<string, string> localize, string routeName, object values)
        //{
        //    return SimpleArchivePager<T>(htmlHelper, pageOfAList, routeName, values, localize("« Newer"),
        //                                 localize("Older »"), false);
        //}

       

        #endregion

        #region PageStatus

        //public static string PageStatus<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList, BaseViewPage view)
        //{
        //    return pageOfAList.Count > 0
        //               ?
        //                   htmlHelper.PageStatus<T>(
        //                       pageOfAList,
        //                       p => string.Format(
        //                                view.Localize("Displaying {0}-{1} of {2}"),
        //                                p.PageIndex * p.PageSize + 1,
        //                                p.PageIndex * p.PageSize + p.Count,
        //                                p.TotalItemCount
        //                                )
        //                       )
        //               :
        //                   view.Localize("None found");
        //}

        //public static string PageStatus<T>(this HtmlHelper htmlHelper, IPageOfAList<T> pageOfAList,
        //                                   Func<IPageOfAList<T>, string> generateContent)
        //{
        //    return generateContent(pageOfAList);
        //}

        #endregion

        #region ScriptBlock

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src)
        {
            return htmlHelper.ScriptBlock(type, src, null);
        }

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("script");

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (!string.IsNullOrEmpty(type))
            {
                tagBuilder.MergeAttribute("type", type);
            }
            if (!string.IsNullOrEmpty(src))
            {
                tagBuilder.MergeAttribute("src", src);
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region UnorderedList
        public static string UnorderedList(this HtmlHelper htmlHelper, IEnumerable<string> items, object htmlAttributes)                   
        {
            if(items == null || items.Count() == 0)
                return "";

            TagBuilder ul = new TagBuilder("ul");
            ul.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            StringBuilder listItems = new StringBuilder();
         
            foreach(string item in items)
            {
                listItems.Append("<li>");
                listItems.Append(item);
                listItems.Append("</li>");
            }

            ul.InnerHtml = listItems.ToString();
            return ul.ToString();
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items,
                                              Func<T, string> generateContent)
        {
            return UnorderedList<T>(htmlHelper, items, (t, i) => generateContent(t));
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items,
                                              Func<T, string> generateContent, string cssClass)
        {
            return UnorderedList<T>(htmlHelper, items, (t, i) => generateContent(t), cssClass, null, null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items,
                                              Func<T, int, string> generateContent)
        {
            return UnorderedList<T>(htmlHelper, items, generateContent, null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items,
                                              Func<T, int, string> generateContent, string cssClass)
        {
            return UnorderedList<T>(htmlHelper, items, generateContent, cssClass, null, null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items,
                                              Func<T, int, string> generateContent, string cssClass, string itemCssClass,
                                              string alternatingItemCssClass)
        {
            if (items == null || items.Count() == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder(100);
            int counter = 0;

            sb.Append("<ul");
            if(!string.IsNullOrEmpty(cssClass))
            {
                sb.AppendFormat(" class=\"{0}\"", cssClass);
            }
            sb.Append(">");
            foreach(T item in items)
            {
                StringBuilder sbClass = new StringBuilder(40);

                if(counter == 0)
                {
                    sbClass.Append("first ");
                }
                if(item.Equals(items.Last()))
                {
                    sbClass.Append("last ");
                }

                if(counter % 2 == 0 && !string.IsNullOrEmpty(itemCssClass))
                {
                    sbClass.AppendFormat("{0} ", itemCssClass);
                }
                else if(counter % 2 != 0 && !string.IsNullOrEmpty(alternatingItemCssClass))
                {
                    sbClass.AppendFormat("{0} ", alternatingItemCssClass);
                }

                sb.Append("<li");
                if(sbClass.Length > 0)
                {
                    sb.AppendFormat(" class=\"{0}\"", sbClass.Remove(sbClass.Length - 1, 1));
                }
                sb.AppendFormat(">{0}</li>", generateContent(item, counter));

                counter++;
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        #endregion
    
        /*
        public static string ImageClass(this HtmlHelper helper, string src, string cssClass)
        {
            string imageTag = string.Format("<img class=\"{0}\" src=\"{1}\"/>", cssClass, src);
            return imageTag;
        }
        public static string Image(this HtmlHelper helper, string src, string id)
        {
            string imageTag = string.Format("<img id=\"{0}\" src=\"{1}\"/>", id, src);
            return imageTag;
        }
        public static string Image(this HtmlHelper helper, string src, string id, string alt)
        {
            string imageTag = string.Format("<img id=\"{0}\" src=\"{1}\" alt=\"{2}\"/>", id, src,alt);
            return imageTag;
        }
        */
    }
}
