using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
using JIT.InformationSystem;
using JIT.InformationSystem.Localization;

namespace JIT.DIME2Barcode.Localization
{
    public class ProductionPlanLocalizationConfigurer
    {
        

        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource("ProductionPlan",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ProductionPlanLocalizationConfigurer).GetAssembly(),
                        "JIT.DIME2Barcode.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}