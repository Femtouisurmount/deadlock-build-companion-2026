using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DeadlockCompanion
{
    /// <summary>
    /// Manages custom build presets for Deadlock Build Companion.
    /// </summary>
    public class BuildManager
    {
        private readonly string _storagePath;

        public BuildManager(string? storagePath = null)
        {
            _storagePath = storagePath ?? Path.Combine(Directory.GetCurrentDirectory(), "data", "custom_builds.json");
        }

        /// <summary>
        /// Returns a stub build preset for the given build identifier.
        /// TODO: replace with lookup from the Deadlock reference database.
        /// </summary>
        public BuildPreset ImportBuild(string buildId)
        {
            return new BuildPreset
            {
                HeroName = "Unknown",
                Name = "Imported Build",
                BuildId = buildId,
                Items = new List<string>()
            };
        }

        /// <summary>
        /// Saves a custom build preset to local storage.
        /// </summary>
        public void SaveCustomBuild(BuildPreset preset)
        {
            var builds = LoadAll();
            builds.Add(preset);
            SaveBuilds(builds);
        }

        /// <summary>
        /// Exports build presets to the given JSON file.
        /// </summary>
        public List<BuildPreset> ExportBuilds(string path)
        {
            var builds = LoadAll();
            var json = JsonSerializer.Serialize(builds, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            return builds;
        }

        private List<BuildPreset> LoadAll()
        {
            if (!File.Exists(_storagePath))
                return new List<BuildPreset>();

            var json = File.ReadAllText(_storagePath);
            return JsonSerializer.Deserialize<List<BuildPreset>>(json) ?? new List<BuildPreset>();
        }

        private void SaveBuilds(List<BuildPreset> builds)
        {
            var json = JsonSerializer.Serialize(builds, new JsonSerializerOptions { WriteIndented = true });
            var dir = Path.GetDirectoryName(_storagePath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(_storagePath, json);
        }
    }
}
