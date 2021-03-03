using WhyzrStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WhyzrStore.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class WhyzrStoreController : AbpController
    {
        protected WhyzrStoreController()
        {
            LocalizationResource = typeof(WhyzrStoreResource);
        }
    }
}