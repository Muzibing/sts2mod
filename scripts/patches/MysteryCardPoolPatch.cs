using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using mzb.scripts.cards;

namespace mzb.scripts.patches;

public static class MysteryCardPoolPatch
{
    public static void AddMystery(ref CardModel[] cards)
    {
        cards = WithMystery(cards).ToArray();
    }

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
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(DefectCardPool), "GenerateAllCards")]
public static class DefectMysteryCardPoolPatch
{
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(RegentCardPool), "GenerateAllCards")]
public static class RegentMysteryCardPoolPatch
{
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}

[HarmonyPatch(typeof(NecrobinderCardPool), "GenerateAllCards")]
public static class NecrobinderMysteryCardPoolPatch
{
    static void Postfix(ref CardModel[] __result) => MysteryCardPoolPatch.AddMystery(ref __result);
}
