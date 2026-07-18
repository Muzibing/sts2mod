using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using mzb.scripts.cards;
using mzb.scripts;

namespace mzb.scripts.patches;

[HarmonyPatch(typeof(Player), "PopulateStartingDeck")]
public static class StartingDeckPatch
{
    /// <summary>
    /// 在角色初始牌组生成后补上 Mystery，并给静默猎手额外塞一张 Neutralize。
    /// </summary>
    static void Postfix(Player __instance)
    {
        AddMysteryToStartingDeck(__instance);

        if (__instance.Character.Id.Entry != ModResources.SilentCharacterId)
        {
            return;
        }

        var card = ModelDb.Card<Neutralize>().ToMutable();
        card.FloorAddedToDeck = 1;
        __instance.Deck.AddInternal(card, -1, true);

        Log.Info($"[mzb] Added an extra {ModResources.NeutralizeId} to Silent starting deck.");
    }

    /// <summary>
    /// 确保每个角色的初始牌组里都只会额外出现一张 Mystery。
    /// </summary>
    private static void AddMysteryToStartingDeck(Player player)
    {
        if (player.Deck.Cards.Any(card => card is Mystery))
        {
            return;
        }

        var card = ModelDb.Card<Mystery>().ToMutable();
        card.FloorAddedToDeck = 1;
        player.Deck.AddInternal(card, -1, true);

        Log.Info($"[mzb] Added Mystery to {player.Character.Id.Entry} starting deck.");
    }
}
