using LedgerWatch.Core.Entities;

namespace LedgerWatch.Core.Anomalies;

public interface IAnomalyRule
{
    string RuleName { get; }
    bool Applies(Transaction transaction, IReadOnlyList<Transaction> allTransactions);
}