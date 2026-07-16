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
