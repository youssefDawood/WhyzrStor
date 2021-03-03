using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace WhyzrStore.Web
{
    [Dependency(ReplaceServices = true)]
    public class WhyzrStoreBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WhyzrStore";
    }
}
