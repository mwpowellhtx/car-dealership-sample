using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Controllers
{
    using static Guid;

    [Authorize]
    public abstract class AuthorizedController : DisposableController
    {
        protected Guid UserId => Parse(User.Identity.GetUserId());
    }
}
