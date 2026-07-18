using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using mzb.scripts.cards;

namespace mzb.scripts.patches;

public static class MysteryCardPoolPatch
{
    /// <summary>
    /// 把 Mystery 追加到原始卡池数组里，供具体卡池生成结果复用。
    /// </summary>
    public static void AddMystery(ref CardModel[] cards)
    {
        cards = WithMystery(cards).ToArray();
    }

    /// <summary>
    /// 在枚举卡池中补上 Mystery，但不会重复添加已有实例或同 ID 卡牌。
    /// </summary>
    public static IEnumerable<CardModel> WithMystery(IEnumerable<CardModel> cards)
    {
        var cardList = cards.ToList();
        var mystery = ModelDb.Card<Mystery>();
        if (cardList.Any(card => card is Mystery || card.Id == mystery.Id))
        {
            return cardList;
        }

        cardList.Add(mystery);
        return cardList;
    }

    /// <summary>
    /// 判断当前卡池是不是需要注入 Mystery 的角色池。
    /// </summary>
    public static bool IsSupportedCharacterPool(CardPoolModel pool) => pool is
        IroncladCardPool or
        SilentCardPool or
        DefectCardPool or
        RegentCardPool or
        NecrobinderCardPool;
}

[HarmonyPatch(typeof(CardPoolModel), nameof(CardPoolModel.GetUnlockedCards))]
public static class MysteryUnlockedCardsPatch
{
    /// <summary>
    /// 在已解锁卡牌列表返回前补进 Mystery。
    /// </summary>
    static void Postfix(CardPoolModel __instance, ref IEnumerable<CardModel> __result)
    {
        if (!MysteryCardPoolPatch.IsSupportedCharacterPool(__instance))
        {
            return;
        }

        __result = MysteryCardPoolPatch.WithMystery(__result);
    }
}

[HarmonyPatch(typeof(CardPoolModel), "get_AllCards")]
public static class MysteryAllCardsPatch
{
    /// <summary>
    /// 在完整卡池列表返回前补进 Mystery。
    /// </summary>
    static void Postfix(CardPoolModel __instance, ref IEnumerable<CardModel> __result)
    {
        if (!MysteryCardPoolPatch.IsSupportedCharacterPool(__instance))
        {
            return;
        }

        __result = MysteryCardPoolPatch.WithMystery(__result);
    }
}

[HarmonyPatch(typeof(SilentCardPool), "GenerateAllCards")]
public static class SilentMysteryCardPoolPatch
{
    /// <summary>
    /// 给静默猎手的原始卡池生成结果追加 Mystery。
    /// </summary>
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(DefectCardPool), "GenerateAllCards")]
public static class DefectMysteryCardPoolPatch
{
    /// <summary>
    /// 给故障体的原始卡池生成结果追加 Mystery。
    /// </summary>
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(RegentCardPool), "GenerateAllCards")]
public static class RegentMysteryCardPoolPatch
{
    /// <summary>
    /// 给摄政的原始卡池生成结果追加 Mystery。
    /// </summary>
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(NecrobinderCardPool), "GenerateAllCards")]
public static class NecrobinderMysteryCardPoolPatch
{
    /// <summary>
    /// 给死灵缚者的原始卡池生成结果追加 Mystery。
    /// </summary>
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}
