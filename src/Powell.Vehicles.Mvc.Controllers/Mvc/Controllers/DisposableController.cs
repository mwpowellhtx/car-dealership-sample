using System.Web.Mvc;

namespace Powell.Vehicles.Mvc.Controllers
{
    public abstract class DisposableController : Controller
    {
        protected bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && !IsDisposed) IsDisposed = true;
        }
    }
}
