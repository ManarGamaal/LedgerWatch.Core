using LedgerWatch.Core;
using LedgerWatch.Core.Entities;

namespace LedgerWatch.Core.Entities;

public class Transaction
{
    public int ID { get; private set; }
    public string Source { get; private set; } = string.Empty; // 'BankImport' or 'Manual'
    public decimal Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public TransactionStatus Status { get; private set; } = TransactionStatus.UnMatched;
    public int? MatchedTransactionId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    private Transaction() { } 

    public static Transaction Create(string source, decimal amount, DateTime date, string description)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("Source is required.", nameof(source));

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        return new Transaction
        {
            Source = source,
            Amount = amount,
            TransactionDate = date,
            Description = description
        };
    }

    public void MarkMatched(int matchedId)
    {
        if (Status == TransactionStatus.Matched)
            throw new InvalidOperationException("Transaction is already Matched.");

        Status = TransactionStatus.Matched;
        MatchedTransactionId = matchedId;
    }

    public void Flag()
    {
        Status = TransactionStatus.Flagged;
    }
}