using BookStore.Utilties;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCartQuantity(int quantity)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(SessionHelper.SessionCart, quantity);
        }

        public int GetCartQuantity()
        {
            // Attempt to retrieve the value from the session, return 0 if not found.
            return _httpContextAccessor.HttpContext?.Session.GetInt32(SessionHelper.SessionCart) ?? 0;
        }
    }
}
