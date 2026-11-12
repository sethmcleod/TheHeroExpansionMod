using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Nonupeipe), "get_OptionPool")]
public static class NonuPatch
{
    public static void Postfix(Nonupeipe __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.DaintyDandelion)
            return;
        
        var relic = ModelDb.Relic<DaintyDandelion>().ToMutable();

        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}