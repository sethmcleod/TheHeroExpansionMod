using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Pael), "get_OptionPool1")]
public static class PaelPatch
{
    public static void Postfix(Pael __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.PaelsBlight)
            return;
        
        var relic = ModelDb.Relic<PaelsBlight>().ToMutable();
        
        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}