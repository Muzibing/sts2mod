# STS2 mzb Mod

<p align="center">
  <a href="#中文">中文</a>
  &nbsp;|&nbsp;
  <a href="#english">English</a>
</p>

## 中文

> 一个面向《杀戮尖塔 2》的轻量玩法 Mod，当前主要修改铁甲战士和静默猎手。

本 Mod 目前包含两个功能：

- 将铁甲战士初始「打击」牌的画像替换为自定义图片。
- 为静默猎手初始牌组额外加入 1 张原版「中和」牌。

Release 打包功能后续再处理。目前这个仓库以源码和本地构建测试为主。

### 效果预览

| 铁甲战士打击画像 |
| :---: |
| ![铁甲战士打击自定义画像](mzb/images/cards/ironclad_strike.png) |

### 功能说明

| 功能 | 目标 | 效果 |
| --- | --- | --- |
| 自定义卡牌画像 | 铁甲战士 `StrikeIronclad` | 使用 `mzb/images/cards/ironclad_strike.png` 作为卡牌画像。 |
| 初始牌组补丁 | 静默猎手 | 在初始牌组中额外加入 1 张原版 `Neutralize` /「中和」。 |

### 项目结构

| 路径 | 作用 |
| --- | --- |
| `scripts/patches/StartingDeckPatch.cs` | 使用 Harmony 修改静默猎手初始牌组。 |
| `scripts/patches/IroncladStrikePortraitPatch.cs` | 使用 Harmony 修改铁甲战士打击的画像路径和纹理。 |
| `scripts/assets/ModTextureCache.cs` | 从 Mod DLL 旁边加载 loose PNG 纹理。 |
| `scripts/ModResources.cs` | 统一保存角色 ID、卡牌 ID 和资源路径。 |
| `mzb/images/cards/ironclad_strike.png` | 自定义卡牌画像资源。 |

### 依赖

- Slay the Spire 2
- .NET SDK 9.0+
- Godot 4.5.1 Mono
- BaseLib，需安装在游戏的 `mods/BaseLib` 文件夹中

### 本地构建

1. 将 `Local.props.example` 复制为 `Local.props`。
2. 按你的本机环境修改 `Local.props`：

   ```xml
   <Project>
     <PropertyGroup>
       <Sts2Dir>D:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
       <GodotExe>C:\Path\To\Godot_v4.5.1-stable_mono_win64.exe</GodotExe>
     </PropertyGroup>
   </Project>
   ```

3. 构建 C# Mod：

   ```powershell
   dotnet build .\mzb.sln
   ```

   构建会把 `mzb.dll`、`mzb.json` 和 loose 卡图 PNG 复制到本机游戏目录的 `mods/mzb` 文件夹。

4. 如果已配置 `GodotExe`，可以导出 Godot 资源包：

   ```powershell
   dotnet publish .\mzb.sln -c ExportRelease
   ```

### 说明

- `Local.props` 包含本机路径，已加入 `.gitignore`，不会提交。
- `.godot/`、`bin/`、`obj/`、DLL、PDB 等中间产物已加入 `.gitignore`，不应提交到仓库。
- Release 打包和发布流程后续再处理。

## English

> A small Slay the Spire 2 gameplay mod for Ironclad and Silent.

This mod currently does two things:

- Replaces the Ironclad starting Strike card portrait with a custom image.
- Adds one extra vanilla Neutralize card to the Silent starting deck.

Release packaging is not handled yet. For now, this repository is source-first and intended for local building/testing.

### Preview

| Ironclad Strike Portrait |
| :---: |
| ![Ironclad Strike custom portrait](mzb/images/cards/ironclad_strike.png) |

### Features

| Feature | Target | Behavior |
| --- | --- | --- |
| Custom card portrait | Ironclad `StrikeIronclad` | Uses `mzb/images/cards/ironclad_strike.png` for the card portrait. |
| Starting deck patch | Silent | Adds one extra vanilla `Neutralize` to the starting deck. |

### Project Structure

| Path | Purpose |
| --- | --- |
| `scripts/patches/StartingDeckPatch.cs` | Harmony patch for Silent's starting deck. |
| `scripts/patches/IroncladStrikePortraitPatch.cs` | Harmony patches for Ironclad Strike portrait path/texture. |
| `scripts/assets/ModTextureCache.cs` | Loads the loose PNG texture copied beside the mod DLL. |
| `scripts/ModResources.cs` | Shared IDs and asset paths. |
| `mzb/images/cards/ironclad_strike.png` | Custom card portrait asset. |

### Dependencies

- Slay the Spire 2
- .NET SDK 9.0+
- Godot 4.5.1 Mono
- BaseLib installed in the game `mods/BaseLib` folder

### Local Build

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

### Notes

- `Local.props` is ignored by git because it contains machine-specific paths.
- `.godot/`, `bin/`, `obj/`, DLLs, PDBs, and other generated files are ignored and should not be committed.
- Release packaging will be handled later.
