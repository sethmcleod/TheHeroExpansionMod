using System.Reflection;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;

namespace TheHeroExpansion.TheHeroExpansionCode.Helpers;

public static class AncientRelicOption
{
    private static readonly MethodInfo? RelicOptionMethod = typeof(AncientEventModel)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        .FirstOrDefault(m =>
        {
            if (m.Name != "RelicOption" || m.IsGenericMethodDefinition)
                return false;

            var parameters = m.GetParameters();
            return parameters.Length >= 2
                   && parameters[0].ParameterType == typeof(RelicModel)
                   && parameters[1].ParameterType == typeof(string);
        });

    public static EventOption? Create(AncientEventModel ancient, RelicModel relic)
    {
        if (RelicOptionMethod == null)
        {
            MainFile.Logger.Warn("AncientEventModel.RelicOption not found, skipping relic option for " + relic.Id);
            return null;
        }

        var parameters = RelicOptionMethod.GetParameters();
        var args = new object?[parameters.Length];
        args[0] = relic;
        args[1] = "INITIAL";
        for (var i = 2; i < args.Length; i++)
            args[i] = parameters[i].HasDefaultValue ? parameters[i].DefaultValue : null;

        return (EventOption?)RelicOptionMethod.Invoke(ancient, args);
    }
}
