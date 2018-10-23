using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LedgerEntry
{
    public DateTime Date { get; }
    public string Desc { get; }
    public float Chg { get; }

    public LedgerEntry(DateTime date, string desc, float chg) => 
        (Date, Desc, Chg) = (date, desc, chg);
}

public static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string desc, int chng) =>
        new LedgerEntry(DateTime.Parse(date, CultureInfo.InvariantCulture), desc, chng / 100.0f);

    private static CultureInfo CreateCulture(string cur, string loc)
    {
        var culture = new CultureInfo(loc);
        culture.NumberFormat.CurrencySymbol = cur == "USD" ? "$" : "€";
        culture.NumberFormat.CurrencyNegativePattern = loc == "en-US" ? 0 : 12;
        culture.DateTimeFormat.ShortDatePattern = loc == "en-US" ? "MM/dd/yyyy" : "dd/MM/yyyy";
        return culture;
    }

    private static string PrintHead(string loc) =>
        loc == "en-US"
            ? "Date       | Description               | Change       "
            : "Datum      | Omschrijving              | Verandering  ";

    private static string PrintEntry(LedgerEntry entry)
    {
        var description = entry.Desc.Length > 25 ? entry.Desc.Substring(0, 22) + "..." : entry.Desc;
        var space = entry.Chg < 0.0 ? "" : " ";
        return $"{entry.Date:d} | {description,-25} | {entry.Chg,13:C}{space}";
    }

    private static IEnumerable<LedgerEntry> Sort(LedgerEntry[] entries) =>
        entries.Where(e => e.Chg < 0).OrderBy(x => $"{x.Date}@{x.Desc}@{x.Chg}")
        .Concat(entries.Where(e => e.Chg >= 0).OrderBy(x => $"{x.Date}@{x.Desc}@{x.Chg}"));

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        CultureInfo.DefaultThreadCurrentCulture = CreateCulture(currency, locale);
        return string.Join('\n', new[] { PrintHead(locale) }.Concat(Sort(entries).Select(PrintEntry)));
    }
}
