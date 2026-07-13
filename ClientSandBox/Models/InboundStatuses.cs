using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSandBox.Models;

public static class InboundStatuses
{
    public const string Ready = "Готов";

    public const string InsufficientData = "Недостаточно данных";

    public const string PortInUse = "Порт занят";

    public const string UnknownType = "Неизвестный тип";

    public const string AnalysisError = "Ошибка анализа";
}
