using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound.Validators;

/// <summary>
/// Анализатор Inbound типа TUN.
/// </summary>
public sealed class TunValidator : IInboundValidator
{
    /// <inheritdoc/>
    public void Analyze(InboundInfo inbound)
    {
        if (!inbound.IsType("tun"))
        {
            return;
        }

        inbound.Status = InboundStatuses.Ready;
    }
}