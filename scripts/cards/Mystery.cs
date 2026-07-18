using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using mzb.scripts;
using mzb.scripts.compat;

namespace mzb.scripts.cards;

[Pool(typeof(IroncladCardPool))]
public class Mystery : CustomCardModel
{
    private const string StrengthLossKey = "StrengthLoss";

    public override string PortraitPath => ModResources.MysteryPortraitPath;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3m, ValueProp.Move),
        new DynamicVar(StrengthLossKey, 1m)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Innate,
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    public override List<(string, string)>? Localization =>
    [
        ("title", "神秘"),
        ("description", "每当你打出一张牌，本回合费用减少 1。造成 {Damage} 点伤害。给予敌人 -{StrengthLoss} 点力量。")
    ];

    public Mystery() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    /// <summary>
    /// 打出神秘时先造成伤害，再让目标失去力量。
    /// </summary>
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        // 先执行攻击，再衔接减力量效果，保持和卡面描述一致。
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCardCompat(this, cardPlay)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        await PowerCmd.Apply<StrengthPower>(
            choiceContext,
            cardPlay.Target,
            -DynamicVars[StrengthLossKey].BaseValue,
            Owner.Creature,
            this
        );
    }

    /// <summary>
    /// 进入战斗后按本回合已经打出的牌数，预先把神秘的费用降下来。
    /// </summary>
    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || IsClone)
        {
            return Task.CompletedTask;
        }

        // 只统计本角色本回合已经完成结算的出牌记录，避免重复扣费。
        var cardsPlayedThisTurn = CombatManager.Instance.History.CardPlaysFinished.Count(
            entry => entry.CardPlay.Card.Owner == Owner && entry.HappenedThisTurn(CombatState)
        );
        ReduceCostBy(cardsPlayedThisTurn);

        return Task.CompletedTask;
    }

    /// <summary>
    /// 友方牌被打出时，让手里的神秘再便宜 1 点。
    /// </summary>
    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == Owner)
        {
            ReduceCostBy(1);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 升级后移除消耗，让神秘能在回合内重复使用。
    /// </summary>
    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }

    /// <summary>
    /// 按指定数量减少本回合的费用修正。
    /// </summary>
    private void ReduceCostBy(int amount)
    {
        EnergyCost.AddThisTurn(-amount);
    }
}
