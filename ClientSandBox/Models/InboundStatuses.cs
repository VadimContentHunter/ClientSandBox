using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSandBox.Models;

public static class InboundStatuses
{
    public const string Ready = "✔ Готов";

    public const string InsufficientData = "⚠ Недостаточно данных";

    public const string PortInUse = "⚠ Порт занят";

    public const string UnknownType = "✖ Неизвестный тип";

    public const string AnalysisError = "✖ Ошибка анализа";

    /// <summary>
    /// Определяет, можно ли выбрать Inbound.
    /// </summary>
    /// <param name="inbound">Inbound.</param>
    /// <returns>True, если Inbound доступен для выбора.</returns>
    private static bool CanSelectInbound(InboundInfo inbound)
    {
        return inbound.Status == InboundStatuses.Ready;
    }
}
