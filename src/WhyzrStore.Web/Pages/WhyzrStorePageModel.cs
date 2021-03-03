using WhyzrStore.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WhyzrStore.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class WhyzrStorePageModel : AbpPageModel
    {
        protected WhyzrStorePageModel()
        {
            LocalizationResourceType = typeof(WhyzrStoreResource);
        }
    }
}