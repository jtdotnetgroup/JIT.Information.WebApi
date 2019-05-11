using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace JIT.InfomationSystem.Localization
{
    public static class InfomationSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(InfomationSystemConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(InfomationSystemLocalizationConfigurer).GetAssembly(),
                        "JIT.InfomationSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
