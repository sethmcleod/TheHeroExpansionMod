using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Vakuu), "get_Pool1")]
public static class VakuuPatch
{
    public static void Postfix(Vakuu __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.TheRing)
            return;
        
        var relic = ModelDb.Relic<TheRing>().ToMutable();

        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}