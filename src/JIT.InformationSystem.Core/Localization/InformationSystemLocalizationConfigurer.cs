using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace JIT.InformationSystem.Localization
{
    public static class InformationSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(InformationSystemConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(InformationSystemLocalizationConfigurer).GetAssembly(),
                        "JIT.InformationSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
