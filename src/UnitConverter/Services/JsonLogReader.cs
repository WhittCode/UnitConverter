using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using UnitConverter.Models;

namespace UnitConverter.Services;

public class JsonLogReader : ILogReader
{
    private readonly string _logDirectory;

    public JsonLogReader(IWebHostEnvironment environment)
    {
        // Guard against hardcoded paths by utilizing content root environment markers
        _logDirectory = Path.Combine(environment.ContentRootPath, "Logs");
    }

    public IEnumerable<LogEntry> Read()
    {
        var entries = new List<LogEntry>();

        if (!Directory.Exists(_logDirectory))
        {
            return entries;
        }

        // Gather all generated json files
        string[] logFiles = Directory.GetFiles(_logDirectory, "*.json");

        foreach (string logFile in logFiles)
        {
            try
            {
                string[] lines = File.ReadAllLines(logFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Keep try block narrow solely around individual line deserialization
                    try
                    {
                        LogEntry? entry = JsonSerializer.Deserialize<LogEntry>(line);
                        if (entry != null)
                        {
                            // Serilog omits level tags for basic Information markers
                            if (string.IsNullOrEmpty(entry.Level))
                            {
                                entry.Level = "Information";
                            }
                            entries.Add(entry);
                        }
                    }
                    catch (JsonException)
                    {
                        // Skip malformed runtime logs line-by-line without throwing an application fault
                        continue;
                    }
                }
            }
            catch (IOException)
            {
                // Fallback catch if log files are temporarily locked during write streams
                continue;
            }
        }

        return entries;
    }
}
