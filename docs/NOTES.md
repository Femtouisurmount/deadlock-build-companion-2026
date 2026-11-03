# Developer Notes

## Overview

Deadlock Build Companion is a reference tool for Deadlock, the Valve multiplayer shooter. It presents hero statistics, an item database, and build presets. The tool is strictly read-only and does not read the game memory or interact with the game process.

## Data Source

Hero and item data is loaded from local JSON files in the `data` directory. Build presets may be enhanced with an external reference source such as deadlock-api.com.

## Updating the Data

If the Deadlock data source changes, update the JSON files in the `data` directory and adjust the property mappings in `DataService.cs`. Replace the TODO placeholders in `BuildManager.cs` with real lookups when public build identifiers are published.

## Known Limitations

- Data may lag behind game patches.
- Accuracy depends on the quality of the reference data source.
- The companion does not read game memory and performs no in-game interaction.
