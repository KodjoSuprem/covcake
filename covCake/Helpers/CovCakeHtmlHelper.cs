using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using covCake.DataAccess;

namespace covCake.Helpers
{

    [DebuggerStepThrough]
    public static class CovCakeHtmlHelper
    {
        public enum FlagSize
        {
            s48,
            s24,
            s16
        }


        public static string FacebookPublishLink(this HtmlHelper htmlHelper,string url, string text, string JSFunctionName)
        {
    //     <a href="http://www.facebook.com/share.php?u=<%= this.Request.Url.ToString()%>" class="fb_share_button" onclick="return fbs_click()" target="_blank" style="text-decoration:none;">Partager sur facebook</a>
            string fbUri = "http://www.facebook.com/share.php?u="+url;
            return htmlHelper.Link(text,url, new { @class = "fb_share_button", onclick = "return "+JSFunctionName, target = "_blank", style="text-decoration:none;" });

        }


        /// <summary>
        /// Ajoute la class .text-input
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string cTextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);
            if(!attributes.ContainsKey("class"))
                attributes.Add("class", "text-input");
            return htmlHelper.TextBox(name, value, attributes);
        }


        public static string cSubmit(this HtmlHelper htmlHelper, string value)
        {
            return htmlHelper.Submit(value, new { @class = "button" });
        }

        public static string cPassword(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.cPassword(name, null);
        }
         public static string cPassword(this HtmlHelper htmlHelper, string name, string value)
        {
            RouteValueDictionary attributes = new RouteValueDictionary();
            if(!attributes.ContainsKey("class"))
                attributes.Add("class", "text-input");
            return htmlHelper.Password(name, value, attributes);
        }
        public static string cTextBox(this HtmlHelper htmlHelper, string name, object value)
        {
            return htmlHelper.cTextBox(name, value,null);
        }

        public static string cTextBox(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.cTextBox(name,null,null);
        }

        public static string LoginModal(this HtmlHelper htmlHelper,string linkTest,string ReturnUrl)
        {
            return htmlHelper.ActionLink(linkTest, "Login", "Account", new { fb = "true", returnUrl = ReturnUrl}, new { rel = "facebox" });
        }

        public static string LoginModal(this HtmlHelper htmlHelper, string linkTest)
        {
            return htmlHelper.ActionLink(linkTest, "Login", "Account", new { fb = "true" }, new { rel = "facebox" });
        }

        /// <summary>
        /// Utilise le CSS .validation-summary-infos et .validation-summary-infos li
        /// </summary>
        /// <param name="hetmlHelper"></param>
        /// <returns></returns>
        public static string ViewInfosSummary(this HtmlHelper htmlHelper)
          {
              string validUL = "";
              TagBuilder ul = new TagBuilder("ul");
              ul.AddCssClass("validation-summary-infos");
              covCake.Controllers.BaseController currController = htmlHelper.ViewContext.Controller as covCake.Controllers.BaseController;
              if(currController != null)
              {
                  //Dictionary<string, string> validMessages = currController.ValidStateMessages;
                  List< string> validMessages = currController.ValidStateMessages;

                  if(validMessages.Count > 0)
                  {
                      string lis = "";
                      foreach(var item in validMessages)
                      {
                         // lis += "<li>" + item.Value + "</li>\n";
                          lis += "<li>" + item + "</li>\n";

                      }
                      ul.InnerHtml = lis;
                      validUL = ul.ToString();
                  }
              }
            return validUL;
          }
        public static string HelpBullet(this HtmlHelper htmlHelper, string toolTipMessage)
        {
            return "<img src=\"/Content/help.png\" class=\"help\" title=\"" + toolTipMessage + "\"/>";
            
        }


        public static string  ValidationSummaryWrapped(this HtmlHelper htmlHelper)
        {
            if(htmlHelper.ValidationSummary().IsNullOrEmpty())
                return "";
            return "<div class='validationsummary'>" + htmlHelper.ValidationSummary() + "</div>";
        }

        /// <summary>
        /// retourne l'url de licone spécifiée
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="iconfilename"></param>
        /// <returns></returns>
        public static string IconImageUrl(this HtmlHelper htmlHelper, string iconfilename)
        {
            string iconFolder = "/Content";
            return iconFolder + "/" + iconfilename;
        }

        /// <summary>
        /// retourne la balise img avec l'icone spécifiée
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="iconfilename"></param>
        /// <returns></returns>
        public static string IconImage(this HtmlHelper htmlHelper, string iconfilename)
        {

            return htmlHelper.Image(htmlHelper.IconImageUrl(iconfilename), "");
        }

        public static string IconImage(this HtmlHelper htmlHelper, string iconfilename, string alt)
        {

            return htmlHelper.Image(htmlHelper.IconImageUrl(iconfilename), alt);
        }

        public static string UserSexeIcon(this HtmlHelper htmlHelper, IUserProfile user)
        {
            return htmlHelper.UserSexeIcon(user,null);
        }

        public static string UserSexeIcon(this HtmlHelper htmlHelper, IUserProfile user, object htmlAttributes)
        {
            string genre = (user.Sexe) ? "male" : "female";
            string imgsrc = "/Content/" + genre + ".png";
            if(htmlAttributes!=null)
                return htmlHelper.Image(imgsrc, genre,htmlAttributes);
            return htmlHelper.Image(imgsrc, genre);
        }

        public static string ProjetIndexLink(this HtmlHelper htmlHelper, string text, IProjet proj)
        {
            return htmlHelper.ProjetIndexLink(text, proj, null);

        }




        public static string ProjetIndexLink(this HtmlHelper htmlHelper, string text, IProjet proj, object htmlAttributes)
        {
            return htmlHelper.RouteLink(text, CovCake.Routes.PROJETINDEX, new { ProjetId = proj.IdProjet }, htmlAttributes);
        }


        public static string MonCompteUserLink(this HtmlHelper htmlHelper, string linkText)
        {

            return htmlHelper.MonCompteUserLink(linkText, null);
        }
        public static string MonCompteUserLink(this HtmlHelper htmlHelper, string linkText, object htmlAttributes)
        {

            return htmlHelper.RouteLink(linkText, CovCake.Routes.MONCOMPTE, null, htmlAttributes);
        }


        /// <summary>
        /// Lien vers la page de l'utilisateur. Attention l'UserId est shrinké!!
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string UserProfileLink(this HtmlHelper htmlHelper, IUserProfile user)
        {
            return htmlHelper.UserProfileLink(user, null);
        }

        /// <summary>
        /// Lien vers la page de l'utilisateur. Attention l'UserId est shrinké!!
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="user"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string UserProfileLink(this HtmlHelper htmlHelper, IUserProfile user, object htmlAttributes)
        {
            return htmlHelper.RouteLink(user.DisplayName,CovCake.Routes.USERINDEX, new { userId = user.UserId.Shrink() }, htmlAttributes);
        }

        public static string CountryFlagImgUrl(this HtmlHelper htmlHelper, IPays pays, FlagSize size)
        {
            string codeIso = pays.CodePays2.ToLower().Trim();
            string sz = Enum.GetName(typeof(FlagSize), size).Replace("s","");
 
            return ("/Content/flag_iso_"+sz+"/" + codeIso + ".png");
        }

        public static string CountryFlagImg(this  HtmlHelper htmlHelper, IPays pays, FlagSize size)
        {
           return htmlHelper.Image(htmlHelper.CountryFlagImgUrl(pays,size),"");
        }
        public static string CountryFlagImg(this  HtmlHelper htmlHelper, IPays pays)
        {
            return htmlHelper.Image(htmlHelper.CountryFlagImgUrl(pays), "");
        }
        public static string CountryFlagImgUrl(this HtmlHelper htmlHelper, IPays pays)
        {
            return CountryFlagImgUrl(htmlHelper, pays, FlagSize.s48);
           // string codeIso = pays.CodePays2.ToLower();
           
           //return  htmlHelper.ViewContext.HttpContext.Server.MapPath("~Content/flag_iso_48/"+codeIso+".png");
        }

        public static string LightBoxLink(this HtmlHelper htmlHelper, string linktext, string action, string controller)
        {
            return htmlHelper.ActionLink(linktext, action, controller, new { fb = "true" }, new { rel = "facebox" });

        }
      
        public static string Paginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            string actionName = (string)htmlHelper.ViewContext.Controller.ValueProvider["action"].RawValue;
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, null,null);
        }

        public static string Paginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, string actionName)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, null,null);
        }
        public static string Paginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, string actionName, object routeValues)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, routeValues,null);
        }

        public static string AjaxPaginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, AjaxOptions ajaxOptions)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            string actionName = (string)htmlHelper.ViewContext.Controller.ValueProvider["action"].RawValue;
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, null, ajaxOptions);
        }

        public static string AjaxPaginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, string actionName, AjaxOptions ajaxOptions)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, null, ajaxOptions);
        }
        public static string AjaxPaginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            string controllerName = (string)htmlHelper.ViewContext.Controller.ValueProvider["controller"].RawValue; //htmlHelper.ViewContext.Controller.ToString();
            return Paginator(htmlHelper, totalPagesCount, currentPage, actionName, controllerName, routeValues, ajaxOptions);
        }

        delegate string PaginatorLink(string linkText);
        public static string Paginator(this HtmlHelper htmlHelper, int totalPagesCount, int currentPage, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
        {

            string pagiStr = "";

            //TEST: totalPagesCount = 56;

            // currentPage++;

            int adjacents = 3;

            int maxDisp = 7;
            //string prev, next;
            string currentPageSpan = "<span class=\"current\">{0}</span>";
            string prevSimbol = "◄";//"&#9668";
            string nextSimbol = "►"; //&#9658"; //Suivant »
            if(totalPagesCount <= 1)
                return pagiStr;

            AjaxHelper ajaxHelper = new AjaxHelper(htmlHelper.ViewContext, htmlHelper.ViewDataContainer);
            bool IsAjax = (ajaxOptions != null);


            RouteValueDictionary actionValues = new RouteValueDictionary();
            if(routeValues != null)
                actionValues = new RouteValueDictionary(routeValues);
            if(!actionValues.ContainsKey("page"))
                actionValues["page"] = currentPage;
            //actionValues.Add("page", 0);

            PaginatorLink PageActionLink;
            if(IsAjax)
            {
                PageActionLink = delegate(string linkText)
                {
                    return ajaxHelper.ActionLink(linkText, actionName, controllerName, actionValues, ajaxOptions, null);
                };
               
            }
            else
            {
                PageActionLink = delegate(string linkText)
                {
                    return htmlHelper.ActionLink(linkText, actionName, controllerName, actionValues, null);
                };
            }

            if(currentPage != 0)
            {
                actionValues["page"] = currentPage - 1;
                pagiStr += PageActionLink(prevSimbol); //htmlHelper.ActionLink(prevSimbol, actionName, controllerName, actionValues, null);
            }
            else
                pagiStr += "<span class=\"disabled\">" + prevSimbol + "</span>";

            int dispPage;// index de la page à afficher

            if(totalPagesCount < maxDisp + (adjacents * 2))
            {
                for(int i = 1; i <= totalPagesCount; i++)
                {

                    actionValues["page"] = i - 1;
                    if(i == currentPage + 1)
                        pagiStr += String.Format(currentPageSpan, i);
                    else
                        pagiStr += PageActionLink(i.ToString());
                }
            }
            else if(totalPagesCount >= maxDisp + (adjacents * 2))//enough pages to hide some
            {
                //close to beginning; only hide later pages
                if(currentPage < 1 + (adjacents * 3))
                {
                    for(int i = 1; i < 4 + (adjacents * 2); i++)
                    {

                        actionValues["page"] = i;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, i);
                        else
                            pagiStr += PageActionLink(i.ToString());


                    }
                    pagiStr += "...";
                    dispPage = totalPagesCount - 1;
                    actionValues["page"] = dispPage;
                    pagiStr += PageActionLink(dispPage.ToString());//htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = totalPagesCount;
                    pagiStr += PageActionLink(dispPage.ToString());//htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                }

            //in middle; hide some front and some back
                else if(totalPagesCount - (adjacents * 2) > currentPage && currentPage > (adjacents * 2))
                {
                    actionValues["page"] = 1;
                    pagiStr += PageActionLink("1");//htmlHelper.ActionLink((1).ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = 2;
                    pagiStr += PageActionLink("2");//htmlHelper.ActionLink((2).ToString(), actionName, controllerName, actionValues, null);
                    pagiStr += "...";

                    for(int i = currentPage - adjacents; i <= currentPage + adjacents; i++)
                    {
                        actionValues["page"] = i;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, i);
                        else
                            pagiStr = PageActionLink(i.ToString());
                          
                    }

                    pagiStr += "...";
                    dispPage = totalPagesCount - 1;
                    actionValues["page"] = dispPage;
                    pagiStr += PageActionLink(dispPage.ToString());//htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = totalPagesCount;
                    pagiStr += PageActionLink(dispPage.ToString());//htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                }
                //close to end; only hide early pages
                else
                {
                    actionValues["page"] = 1;
                    pagiStr += PageActionLink("1");//htmlHelper.ActionLink((1).ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = 2;
                    pagiStr += PageActionLink("2");//htmlHelper.ActionLink((2).ToString(), actionName, controllerName, actionValues, null);
                    pagiStr += "...";

                    for(int i = totalPagesCount - (1 + (adjacents * 3)); i <= totalPagesCount; i++)
                    {
                        dispPage = i + 1;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, dispPage);
                        else
                            pagiStr += PageActionLink(i.ToString());//htmlHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, null);
                        
                    }

                }
            }
            //next button
            if(currentPage < totalPagesCount - 1)
            {
                // object htmlAttributes = new { @class = "next" };
                dispPage = currentPage + 1;
                actionValues["page"] = dispPage;
                pagiStr += PageActionLink(nextSimbol);//htmlHelper.ActionLink(nextSimbol, actionName, controllerName, actionValues, null);
            }
            else
                pagiStr += "<span class=\"disabled\">" + nextSimbol + "</span>";

            return pagiStr + "\n";
        }



        [Obsolete]
        public static string PaginatorObs(this HtmlHelper htmlHelper,  int totalPagesCount, int currentPage, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
        {

            string pagiStr = "";

           //TEST: totalPagesCount = 56;

           // currentPage++;

            int adjacents = 3;

            int maxDisp = 7;
            //string prev, next;
            string currentPageSpan = "<span class=\"current\">{0}</span>";
            string prevSimbol = "◄";//"&#9668";
            string nextSimbol = "►"; //&#9658"; //Suivant »
            if(totalPagesCount <= 1)
                return pagiStr;

            AjaxHelper ajaxHelper = new AjaxHelper(htmlHelper.ViewContext, htmlHelper.ViewDataContainer);
            bool IsAjax = (ajaxOptions != null);


            RouteValueDictionary actionValues = new RouteValueDictionary(); 
            if(routeValues != null)
                actionValues = new RouteValueDictionary(routeValues);
            if(!actionValues.ContainsKey("page"))
                actionValues["page"] = currentPage;
                //actionValues.Add("page", 0);

            PaginatorLink ActionLink;
            if(IsAjax)
            {
                ActionLink = delegate(string linkText)
                {
                    return htmlHelper.ActionLink(prevSimbol, actionName, controllerName, actionValues, null);
                };
            }
            else
            {
                ActionLink = delegate(string linkText)
                {
                    return ajaxHelper.ActionLink(prevSimbol, actionName, controllerName, actionValues, ajaxOptions, null);
                };
            }

            if(currentPage != 0)
            {
                actionValues["page"] = currentPage - 1;
                pagiStr += htmlHelper.ActionLink(prevSimbol, actionName, controllerName, actionValues, null);
            }
            else
                pagiStr += "<span class=\"disabled\">" + prevSimbol + "</span>";

            int dispPage;// index de la page à afficher

            if(totalPagesCount < maxDisp + (adjacents * 2))
            {
                for(int i = 1; i <= totalPagesCount; i++)
                {
          
                    actionValues["page"] = i - 1;
                    if(i == currentPage + 1)
                        pagiStr += String.Format(currentPageSpan, i);
                    else
                    {
                        if(IsAjax)
                            pagiStr += ajaxHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues,ajaxOptions, null);
                        else
                            pagiStr += htmlHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, null);
                    }
                }
            }
            else if(totalPagesCount >= maxDisp + (adjacents * 2))//enough pages to hide some
            {
                //close to beginning; only hide later pages
                if(currentPage < 1 + (adjacents * 3))
                {
                    for(int i = 1; i < 4 + (adjacents * 2); i++)
                    {

                        actionValues["page"] = i;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, i);
                        else
                        {
                            if(IsAjax)
                                pagiStr += ajaxHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, ajaxOptions, null);
                            else
                                pagiStr += htmlHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, null);
                        }

                    }
                    pagiStr += "...";
                    dispPage = totalPagesCount - 1;
                    actionValues["page"] = dispPage;
                    pagiStr += htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = totalPagesCount;
                    pagiStr += htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                }

            //in middle; hide some front and some back
                else if(totalPagesCount - (adjacents * 2) > currentPage && currentPage > (adjacents * 2))
                {
                    actionValues["page"] = 1;
                    pagiStr += htmlHelper.ActionLink((1).ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = 2;
                    pagiStr += htmlHelper.ActionLink((2).ToString(), actionName, controllerName, actionValues, null);
                    pagiStr += "...";

                    for(int i = currentPage - adjacents; i <= currentPage + adjacents; i++)
                    {
                        
                        actionValues["page"] = i;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, i);
                        else
                        {
                            if(IsAjax)
                                pagiStr += ajaxHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, ajaxOptions, null);
                            else
                                pagiStr += htmlHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, null);
                        }
                    }

                    pagiStr += "...";
                    dispPage = totalPagesCount - 1;
                    actionValues["page"] = dispPage;
                    pagiStr += htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = totalPagesCount;
                    pagiStr += htmlHelper.ActionLink(dispPage.ToString(), actionName, controllerName, actionValues, null);
                }
                //close to end; only hide early pages
                else
                {
                    actionValues["page"] = 1;
                    pagiStr += htmlHelper.ActionLink((1).ToString(), actionName, controllerName, actionValues, null);
                    actionValues["page"] = 2;
                    pagiStr += htmlHelper.ActionLink((2).ToString(), actionName, controllerName, actionValues, null);
                    pagiStr += "...";

                    for(int i = totalPagesCount - (1 + (adjacents * 3)); i <= totalPagesCount; i++)
                    {
                        dispPage = i + 1;
                        if(i == currentPage)
                            pagiStr += String.Format(currentPageSpan, dispPage);
                        else
                        {
                            if(IsAjax)
                                pagiStr += ajaxHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, ajaxOptions, null);
                            else
                                pagiStr += htmlHelper.ActionLink(i.ToString(), actionName, controllerName, actionValues, null);
                        }
                    }

                }
            }
            //next button
            if(currentPage < totalPagesCount -1)
            {
               // object htmlAttributes = new { @class = "next" };
                dispPage = currentPage + 1;
                actionValues["page"] = dispPage;
                pagiStr += htmlHelper.ActionLink(nextSimbol, actionName, controllerName, actionValues, null);
            }
            else
                pagiStr += "<span class=\"disabled\">" + nextSimbol + "</span>";

            return pagiStr + "\n";
        }



      

        public static string VoyageDestination(this HtmlHelper htmlHelper, IProjet proj)
        {

            return "";
        }
    }
}
