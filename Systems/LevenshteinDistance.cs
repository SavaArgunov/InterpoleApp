using System;
using System.Collections.Generic;
using System.Linq;

namespace InterpoleApp.Systems;

public class Levenshtein
{
    public static int LevenshteinDistance(string s, string t)
    {
        if (string.IsNullOrEmpty(s)) return t.Length;
        if (string.IsNullOrEmpty(t)) return s.Length;

        var dp = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= t.Length; j++) dp[0, j] = j;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(
                        dp[i - 1, j] + 1,
                        dp[i, j - 1] + 1),
                    dp[i - 1, j - 1] + cost);
            }
        }

        return dp[s.Length, t.Length];
    }
    public static List<T> ApplySearch<T>(IEnumerable<T> items, Func<T, string> selector, string query, int maxDistance = 2)
    {
        query = query?.ToLowerInvariant() ?? "";

        if (string.IsNullOrWhiteSpace(query))
            return items.ToList();

        return items
            .Where(item =>
            {
                var value = selector(item)?.ToLowerInvariant() ?? "";
                return LevenshteinDistance(value, query) <= maxDistance;
            })
            .ToList();
    }
}