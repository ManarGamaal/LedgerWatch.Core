namespace LedgerWatch.Core.Entities;

public class Anomaly
{
    public int ID { get; private set; }
    public int TransactionId { get; private set; }
    public string RuleName { get; private set; } = string.Empty; // 'PossibleDuplicate', 'Unmatched7Days'
    public string Notes { get; private set; } = string.Empty;
    public DateTime DetectedAtUtc { get; private set; } = DateTime.UtcNow;
    public bool IsResolved { get; private set; } = false;

    private Anomaly() { } // For EF Core

    public Anomaly(int transactionId, string ruleName, string notes = "")
    {
        TransactionId = transactionId;
        RuleName = ruleName;
        Notes = notes;
    }

    public void Resolve()
    {
        IsResolved = true;
    }
}