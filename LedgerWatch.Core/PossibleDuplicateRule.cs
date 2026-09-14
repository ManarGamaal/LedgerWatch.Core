using LedgerWatch.Core.Entities;

namespace LedgerWatch.Core.Anomalies;

public class PossibleDuplicateRule : IAnomalyRule
{
    public string RuleName => "PossibleDuplicate";

    public bool Applies(Transaction transaction, IReadOnlyList<Transaction> allTransactions)
    {
        return allTransactions.Count(t =>
            t.ID != transaction.ID &&
            t.Amount == transaction.Amount &&
            t.Source == transaction.Source &&
            (t.TransactionDate - transaction.TransactionDate).Duration() <= TimeSpan.FromHours(1)
        ) > 0;
    }
}