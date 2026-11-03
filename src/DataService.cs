using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeadlockCompanion
{
    /// <summary>
    /// Represents a hero with combat statistics from Deadlock.
    /// </summary>
    public class Hero
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public int Health { get; set; }

        [JsonPropertyName("weapon_dps")]
        public int WeaponDps { get; set; }

        public int Range { get; set; }

        public int Ammo { get; set; }

        public double Reload { get; set; }

        public int Difficulty { get; set; }

        public string Role { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents an item from the Deadlock item database.
    /// </summary>
    public class Item
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Tier { get; set; }

        public int Souls { get; set; }

        public string Activation { get; set; } = string.Empty;

        public string Stats { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a hero build preset.
    /// </summary>
    public class BuildPreset
    {
        [JsonPropertyName("hero_name")]
        public string HeroName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("build_id")]
        public string BuildId { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<string> Items { get; set; } = new List<string>();
    }

    /// <summary>
    /// Loads read-only Deadlock reference data from JSON files.
    /// </summary>
    public class DataService
    {
        private readonly string _dataDirectory;

        public DataService(string? dataDirectory = null)
        {
            _dataDirectory = dataDirectory ?? Path.Combine(Directory.GetCurrentDirectory(), "data");
        }

        /// <summary>
        /// Loads the hero statistics list from data/heroes.json.
        /// </summary>
        public List<Hero> LoadHeroes()
        {
            var path = Path.Combine(_dataDirectory, "heroes.json");
            if (!File.Exists(path))
                return new List<Hero>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Hero>>(json) ?? new List<Hero>();
        }

        /// <summary>
        /// Loads the item database from data/items.json.
        /// </summary>
        public List<Item> LoadItems()
        {
            var path = Path.Combine(_dataDirectory, "items.json");
            if (!File.Exists(path))
                return new List<Item>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        }

        /// <summary>
        /// Loads build presets from data/builds.json.
        /// </summary>
        public List<BuildPreset> LoadBuilds()
        {
            var path = Path.Combine(_dataDirectory, "builds.json");
            if (!File.Exists(path))
                return new List<BuildPreset>();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<BuildPreset>>(json) ?? new List<BuildPreset>();
        }
    }
}
