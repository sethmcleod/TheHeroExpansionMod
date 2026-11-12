using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Tanx), "get_BaseOptionPool")]
public static class TanxPatch
{
    public static void Postfix(Tanx __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.SteelBall)
            return;
        
        var relic = ModelDb.Relic<SteelBall>().ToMutable();
        
        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}