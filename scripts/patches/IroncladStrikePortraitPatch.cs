using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using mzb.scripts;
using mzb.scripts.assets;
using mzb.scripts.cards;

namespace mzb.scripts.patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
public static class IroncladStrikePortraitPatch
{
    /// <summary>
    /// 把铁甲战士的 Strike 头像路径替换成模组自定义图片。
    /// </summary>
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
    /// <summary>
    /// 在卡牌请求头像纹理时，优先返回模组缓存的本地 PNG。
    /// </summary>
    static bool Prefix(CardModel __instance, ref Texture2D __result)
    {
        var texture = __instance switch
        {
            StrikeIronclad => ModTextureCache.IroncladStrikePortrait,
            Mystery => ModTextureCache.MysteryPortrait,
            _ => null
        };

        if (texture == null)
        {
            return true;
        }

        __result = texture;
        return false;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
public static class MysteryPortraitPathPatch
{
    /// <summary>
    /// 让 Mystery 的头像路径指向模组自定义图片。
    /// </summary>
    static void Postfix(CardModel __instance, ref string __result)
    {
        if (__instance is Mystery)
        {
            __result = ModResources.MysteryPortraitPath;
        }
    }
}
