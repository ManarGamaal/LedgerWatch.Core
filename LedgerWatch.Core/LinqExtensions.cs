using LedgerWatch.Core.Entities;
using LedgerWatch.Core.Models;

namespace LedgerWatch.Core.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<MonthlySummary> BuildMonthlySummary(this IEnumerable<Transaction> transactions)
    {
        return transactions
            .GroupBy(t => new { t.Source, Month = new DateTime(t.TransactionDate.Year, t.TransactionDate.Month, 1) })
            .Select(g => new MonthlySummary
            {
                Source = g.Key.Source,
                Month = g.Key.Month,
                TotalAmount = g.Sum(t => t.Amount),
                MatchedCount = g.Count(t => t.Status == TransactionStatus.Matched),
                UnmatchedCount = g.Count(t => t.Status == TransactionStatus.UnMatched)
            })
            .OrderByDescending(s => s.Month);
    }

    public static IEnumerable<Transaction> StaleUnmatched(this IEnumerable<Transaction> transactions, int days)
    {
        return transactions.Where(t => t.Status == TransactionStatus.UnMatched &&
                                       (DateTime.UtcNow - t.TransactionDate).TotalDays > days);
    }

    public static IEnumerable<ReconciliationRow> PairBySource(
        this IEnumerable<Transaction> bankEntries, IEnumerable<Transaction> manualEntries)
    {
        return bankEntries.GroupJoin(
            manualEntries,
            b => b.MatchedTransactionId,
            m => (int?)m.ID,
            (bank, matches) => new ReconciliationRow
            {
                BankEntry = bank,
                ManualEntry = matches.FirstOrDefault()
            });
    }
}