using System;
using System.Collections.Generic;
using System.Text;
using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound;

/// <summary>
/// Проверяет корректность Inbound.
/// </summary>
public interface IInboundValidator
{
    /// <summary>
    /// Анализирует Inbound.
    /// </summary>
    /// <param name="inbound">Inbound для проверки.</param>
    void Analyze(InboundInfo inbound);
}
