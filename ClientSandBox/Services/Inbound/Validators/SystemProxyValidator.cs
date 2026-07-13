using System;
using System.Collections.Generic;
using System.Text;
using ClientSandBox.Models;
using System.Net.NetworkInformation;

namespace ClientSandBox.Services.Inbound.Validators;

/// <summary>
/// Анализатор Inbound, предназначенных для использования в качестве системного прокси.
/// </summary>
public sealed class SystemProxyValidator : IInboundValidator
{
    /// <inheritdoc/>
    public void Analyze(InboundInfo inbound)
    {
        string? type = inbound.GetString("type");

        if (type is null)
        {
            inbound.Status = InboundStatuses.AnalysisError;
            return;
        }

        if (type != "mixed" &&
            type != "http" &&
            type != "socks")
        {
            return;
        }

        string? listen = inbound.GetString("listen");
        int? listenPort = inbound.GetInt("listen_port");

        if (string.IsNullOrWhiteSpace(listen) || listenPort is null)
        {
            inbound.Status = InboundStatuses.InsufficientData;
            return;
        }

        if (IsPortBusy(listenPort.Value))
        {
            inbound.Status = InboundStatuses.PortInUse;
            return;
        }

        inbound.Status = InboundStatuses.Ready;
    }

    /// <summary>
    /// Проверяет, используется ли TCP-порт.
    /// </summary>
    /// <param name="port">Порт.</param>
    /// <returns>true, если порт уже используется.</returns>
    private static bool IsPortBusy(int port)
    {
        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

        return properties
            .GetActiveTcpListeners()
            .Any(endpoint => endpoint.Port == port);
    }
}
