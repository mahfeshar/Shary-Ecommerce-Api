
using System.Runtime.Serialization;

namespace Shary.Core.Entities.Order_Aggregate;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Payment Received")]
    PaymentReceived,
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed
}
