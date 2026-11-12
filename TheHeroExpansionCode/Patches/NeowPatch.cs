using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Neow), "get_PositiveOptions")]
public static class NeowPatch
{
    public static void Postfix(Neow __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.BladeHandle)
            return;

        var relic = ModelDb.Relic<BladeHandle>().ToMutable();

        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}