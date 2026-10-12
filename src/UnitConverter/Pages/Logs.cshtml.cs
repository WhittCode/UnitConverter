using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class LogsModel : PageModel
{
    private readonly ILogReader _logReader;

    public LogsModel(ILogReader logReader)
    {
        _logReader = logReader;
    }

    public List<LogEntry> Entries { get; set; } = new();

    // Support GET model binding so filters stay active in the URL
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Level { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? ConversionType { get; set; }

    public void OnGet()
    {
        // 1. Read all logs from the daily JSON rolling files
        var allEntries = _logReader.Read();

        // 2. Filter records manually using loops and statements
        foreach (var entry in allEntries)
        {
            // Filter by Severity Level
            if (!string.IsNullOrEmpty(Level) && !string.Equals(entry.Level, Level, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Filter by Structured Conversion Type
            if (!string.IsNullOrEmpty(ConversionType) && !string.Equals(entry.ConversionType, ConversionType, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            // Filter by Free Text Search phrase inside the message body
            if (!string.IsNullOrEmpty(Search) && (entry.Message == null || !entry.Message.Contains(Search, StringComparison.OrdinalIgnoreCase)))
            {
                continue;
            }

            Entries.Add(entry);
        }
    }
}

