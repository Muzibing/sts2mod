using System.Reflection;
using MegaCrit.Sts2.Core.Models;

namespace mzb.scripts.compat;

public static class AttackCommandCompatibility
{
    /// <summary>
    /// 兼容不同版本的 FromCard 签名，确保攻击命令还能正确绑定卡牌上下文。
    /// </summary>
    public static TCommand FromCardCompat<TCommand>(this TCommand command, CardModel card, object cardPlay)
        where TCommand : class
    {
        var commandType = command.GetType();
        var method = FindFromCard(commandType, typeof(CardModel), cardPlay.GetType())
            ?? FindFromCardWithAssignableCardPlay(commandType, cardPlay.GetType())
            ?? FindFromCard(commandType, typeof(CardModel));

        if (method == null)
        {
            throw new MissingMethodException(commandType.FullName, "FromCard");
        }

        var arguments = method.GetParameters().Length == 2
            ? new object?[] { card, cardPlay }
            : [card];

        return (TCommand)method.Invoke(command, arguments)!;
    }

    /// <summary>
    /// 按指定参数类型直接查找 FromCard 重载。
    /// </summary>
    private static MethodInfo? FindFromCard(Type commandType, params Type[] parameterTypes) =>
        commandType.GetMethod("FromCard", parameterTypes);

    /// <summary>
    /// 查找第二个参数类型可兼容当前 cardPlay 的 FromCard 重载。
    /// </summary>
    private static MethodInfo? FindFromCardWithAssignableCardPlay(Type commandType, Type cardPlayType) =>
        commandType.GetMethods()
            .FirstOrDefault(method =>
            {
                if (method.Name != "FromCard")
                {
                    return false;
                }

                var parameters = method.GetParameters();
                return parameters.Length == 2
                    && parameters[0].ParameterType == typeof(CardModel)
                    && parameters[1].ParameterType.IsAssignableFrom(cardPlayType);
            });
}
