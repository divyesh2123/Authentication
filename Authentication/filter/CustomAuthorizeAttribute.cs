﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Authentication.filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
               
                var userIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                
                var claims = userIdentity.Claims;
               
                var roles = claims.Where(c => c.Type == "Role").ToList();


                if (this.allowedroles.Contains(roles[0].Value) )
                {
                    return true;
                }
                else
                {
                    return false;
                }





            }
            else
            {
                return false;
            }

        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                             new RouteValueDictionary
                             {
                    { "controller", "Account" },
                    { "action", "Login" }
                             });

            }

                filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary
              {
                    { "controller", "Home" },
                    { "action", "UnAuthorized" }
              });

        }
        }
    }