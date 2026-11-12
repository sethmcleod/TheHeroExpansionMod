using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheHeroExpansion.TheHeroExpansionCode.Helpers;
using TheHeroExpansion.TheHeroExpansionCode.Relics;

namespace TheHeroExpansion.TheHeroExpansionCode;

[HarmonyPatch(typeof(Orobas), "get_OptionPool2")]
public static class OrobasPatch
{
    public static void Postfix(Orobas __instance, ref IEnumerable<EventOption> __result)
    {
        if (!TheHeroExpansionConfig.TomeOfOrigin)
            return;
        
        var relic = ModelDb.Relic<TomeOfOrigin>().ToMutable(); //change this to whatever relic I want
        
        var option = AncientRelicOption.Create(__instance, relic);
        if (option == null)
            return;

        var list = __result.ToList();
        list.Add(option);
        __result = list;
    }
}