using Volo.Abp.Settings;

namespace WhyzrStore.Settings
{
    public class WhyzrStoreSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(WhyzrStoreSettings.MySetting1));
        }
    }
}
