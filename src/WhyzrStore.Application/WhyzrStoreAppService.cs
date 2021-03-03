using System;
using System.Collections.Generic;
using System.Text;
using WhyzrStore.Localization;
using Volo.Abp.Application.Services;

namespace WhyzrStore
{
    /* Inherit your application services from this class.
     */
    public abstract class WhyzrStoreAppService : ApplicationService
    {
        protected WhyzrStoreAppService()
        {
            LocalizationResource = typeof(WhyzrStoreResource);
        }
    }
}
