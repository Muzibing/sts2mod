using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using mzb.scripts;
using mzb.scripts.assets;

namespace mzb.scripts.patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
public static class IroncladStrikePortraitPatch
{
    static void Postfix(CardModel __instance, ref string __result)
    {
        if (__instance is StrikeIronclad)
        {
            __result = ModResources.IroncladStrikePortraitPath;
        }
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.Portrait), MethodType.Getter)]
public static class IroncladStrikeTexturePatch
{
    static bool Prefix(CardModel __instance, ref Texture2D __result)
    {
        if (__instance is not StrikeIronclad)
        {
            return true;
        }

        var texture = ModTextureCache.IroncladStrikePortrait;
        if (texture == null)
        {
            return true;
        }

        __result = texture;
        return false;
    }
}
