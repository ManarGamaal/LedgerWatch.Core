using LedgerWatch.Core.Entities;

namespace LedgerWatch.Core.Anomalies;

public class AnomalyDetector
{
    private readonly List<IAnomalyRule> _rules;

    public AnomalyDetector(IEnumerable<IAnomalyRule> rules)
    {
        _rules = rules.ToList();
    }

    public IEnumerable<Anomaly> Detect(Transaction transaction, IReadOnlyList<Transaction> allTransactions)
    {
        return _rules
            .Where(rule => rule.Applies(transaction, allTransactions))
            .Select(rule => new Anomaly(
                transactionId: transaction.ID,
                ruleName: rule.RuleName,
                notes: $"Flagged by rule: {rule.RuleName} at {DateTime.UtcNow:yyyy-MM-dd HH:mm}"
            ));
    }
}