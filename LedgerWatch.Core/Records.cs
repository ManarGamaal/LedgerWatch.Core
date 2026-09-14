using LedgerWatch.Core.Entities;

namespace LedgerWatch.Core.Models;

public record MonthlySummary
{
    public string Source { get; init; } = string.Empty;
    public DateTime Month { get; init; }
    public decimal TotalAmount { get; init; }
    public int MatchedCount { get; init; }
    public int UnmatchedCount { get; init; }
}

public record ReconciliationRow
{
    public Transaction BankEntry { get; init; } = default!;
    public Transaction? ManualEntry { get; init; }
}