﻿using System;
using System.Security.Claims;
using System.Security.Principal;
using AuthDomain.Entities.Auth;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class AuditService : IAuditService
    {
        private readonly IHttpContextAccessor _httpContext;
        public AuditService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public T CreateEntity<T>(T entity)
        {
            if (entity is IAudit model)
            {
                model.CreatedDate = DateTime.UtcNow;
                model.CreatedBy = _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
                return (T)model;
            }

            return entity;
        }

        public T UpdateEntity<T>(T entity)
        {
            if (entity is IAudit model)
            {
                model.UpdatedDate = DateTime.UtcNow;
                model.UpdatedBy = _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
                return (T)model;
            }


            return entity;
        }

        public T DeleteEntity<T>(T entity)
        {
            if (entity is IDeleteEntity model)
            {
                model.DeletedDate = DateTime.UtcNow;
                model.DeletedBy = _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
                model.IsDeleted = true;
                return (T)model;
            }

            return entity;
        }

        bool IsInRole(string role)
        {
            return _httpContext.HttpContext.User.IsInRole(role);
        }

        public string UserName => _httpContext.HttpContext.User?.Identity?.Name ?? "Anonymous";
        public string UserId => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        public string RequesterIp => _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();

        //   public string UserLanguage { get { return GetUserLanguageAsync().Result; }  } 
        //  public string UserLanguage => _httpContext.HttpContext.User.Claims
        public string UserLanguage => _httpContext.HttpContext.Request.Headers["Lang"].ToString()??"ar";
        public string WebToken => _httpContext.HttpContext.Request.Headers["WebToken"].ToString() ?? "Anonymous";

        //private async System.Threading.Tasks.Task<string> GetUserLanguageAsync()
        //{
        //    var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    return user.UserLang;
        //}

        //private async System.Threading.Tasks.Task<string> GetUserTokenAsync()
        //{
        //    var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    return user.WebToken;
        //}

    }
}