using System;
using Microsoft.AspNet.Identity;

namespace Powell.Vehicles.Mvc.Controllers
{
    using static Guid;

    public abstract class UnauthorizedController : DisposableController
    {
        protected Guid UserId => Parse(User.Identity.GetUserId());
    }
}
