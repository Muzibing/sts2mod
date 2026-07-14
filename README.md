# STS2 mzb Mod

> A small Slay the Spire 2 gameplay mod for Ironclad and Silent.

This mod currently does two things:

- Replaces the Ironclad starting Strike card portrait with a custom image.
- Adds one extra Neutralize card to the Silent starting deck.

Release packaging is not handled yet. For now, this repository is source-first and intended for local building/testing.

## Preview

| Ironclad Strike Portrait |
| :---: |
| ![Ironclad Strike custom portrait](mzb/images/cards/ironclad_strike.png) |

## Features

| Feature | Target | Behavior |
| --- | --- | --- |
| Custom card portrait | Ironclad `StrikeIronclad` | Uses `mzb/images/cards/ironclad_strike.png` for the card portrait. |
| Starting deck patch | Silent | Adds one extra vanilla `Neutralize` to the starting deck. |

## Project Structure

| Path | Purpose |
| --- | --- |
| `scripts/patches/StartingDeckPatch.cs` | Harmony patch for Silent's starting deck. |
| `scripts/patches/IroncladStrikePortraitPatch.cs` | Harmony patches for Ironclad Strike portrait path/texture. |
| `scripts/assets/ModTextureCache.cs` | Loads the loose PNG texture copied beside the mod DLL. |
| `scripts/ModResources.cs` | Shared IDs and asset paths. |
| `mzb/images/cards/ironclad_strike.png` | Custom card portrait asset. |

## Dependencies

- Slay the Spire 2
- .NET SDK 9.0+
- Godot 4.5.1 Mono
- BaseLib installed in the game `mods/BaseLib` folder

## Local Build

1. Copy `Local.props.example` to `Local.props`.
2. Edit `Local.props` for your local machine:

   ```xml
   <Project>
     <PropertyGroup>
       <Sts2Dir>D:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
       <GodotExe>C:\Path\To\Godot_v4.5.1-stable_mono_win64.exe</GodotExe>
     </PropertyGroup>
   </Project>
   ```

3. Build the C# mod:

   ```powershell
   dotnet build .\mzb.sln
   ```

   This copies `mzb.dll`, `mzb.json`, and the loose card portrait PNG into the local Slay the Spire 2 `mods/mzb` folder.

4. Export the Godot resource pack when `GodotExe` is configured:

   ```powershell
   dotnet publish .\mzb.sln -c ExportRelease
   ```

## Notes

- `Local.props` is ignored by git because it contains machine-specific paths.
- `.godot/`, `bin/`, `obj/`, DLLs, PDBs, and other generated files are ignored and should not be committed.
- Release packaging will be handled later.
