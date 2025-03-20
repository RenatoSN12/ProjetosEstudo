using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document(string number, EDocumentType type) : ValueObject
{
    public string Number { get; private set; } = number;
    public EDocumentType Type { get; private set; } = type;
}