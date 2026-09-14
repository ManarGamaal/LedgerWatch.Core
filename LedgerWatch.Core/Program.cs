using LedgerWatch.Core.Entities;
using LedgerWatch.Core.Anomalies;

namespace LedgerWatch.Core;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== LedgerWatch Engine Started ===");

        var bankTx = Transaction.Create("BankImport", 500.00m, DateTime.UtcNow, "Client Payment");
        var manualTx = Transaction.Create("Manual", 500.00m, DateTime.UtcNow, "Client Payment");

        var transactions = new List<Transaction> { bankTx, manualTx };

        bankTx.MarkMatched(manualTx.ID);
        manualTx.MarkMatched(bankTx.ID);
        Console.WriteLine($"[Success] Matched Transaction #{bankTx.ID} with Transaction #{manualTx.ID}");

        var rules = new List<IAnomalyRule> { new PossibleDuplicateRule() };
        var detector = new AnomalyDetector(rules);

        var anomalies = detector.Detect(bankTx, transactions);
        foreach (var anomaly in anomalies)
        {
            Console.WriteLine($"[Anomaly Flagged] Rule: {anomaly.RuleName} - {anomaly.Notes}");
        }

        Console.WriteLine("=== LedgerWatch Execution Completed ===");
    }
}