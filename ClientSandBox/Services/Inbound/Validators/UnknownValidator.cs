using ClientSandBox.Models;

namespace ClientSandBox.Services.Inbound.Validators;

/// <summary>
/// Анализатор неподдерживаемых типов Inbound.
/// </summary>
public sealed class UnknownValidator : IInboundValidator
{
    /// <inheritdoc/>
    public void Analyze(InboundInfo inbound)
    {
        if (!string.IsNullOrWhiteSpace(inbound.Status))
        {
            return;
        }

        inbound.Status = InboundStatuses.UnknownType;
    }
}