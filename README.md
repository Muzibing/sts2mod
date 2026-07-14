# STS2 mzb Mod

<p align="center">
  <a href="#中文">中文</a>
  &nbsp;|&nbsp;
  <a href="#english">English</a>
</p>

## 中文

> 一个面向《杀戮尖塔 2》的轻量玩法 Mod，当前主要修改铁甲战士和静默猎手。

### 当前版本

- Mod 版本：`0.1.0`
- 游戏最低版本：`0.107.1`
- 适配平台：Windows x86_64
- 依赖：BaseLib

> 当前 Release 包只针对 Windows 版 Slay the Spire 2 测试。其他系统暂未适配。

### 功能

- 将铁甲战士初始「打击」牌的画像替换为自定义图片。
- 为静默猎手初始牌组额外加入 1 张原版「中和」牌。

### 效果预览

| 铁甲战士打击画像 |
| :---: |
| ![铁甲战士打击自定义画像](mzb/images/cards/ironclad_strike.png) |

### 下载

从 GitHub Release 下载：

- [mzb-v0.1.0-windows-x64.zip](https://github.com/Muzibing/sts2mod/releases/download/v0.1.0/mzb-v0.1.0-windows-x64.zip)

### 安装

1. 确认已经安装 Slay the Spire 2 Windows 版。
2. 安装 [BaseLib](https://thunderstore.io/c/slay-the-spire-2/p/Alchyr/BaseLib/)，并确认游戏目录中存在：

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/BaseLib/BaseLib.dll
   ```

3. 下载 Release 包 `mzb-v0.1.0-windows-x64.zip`。
4. 解压后，将 `mzb/` 文件夹放入游戏的 `mods/` 目录：

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/mzb/
   ```

5. 最终目录应至少包含：

   ```text
   mods/mzb/mzb.dll
   mods/mzb/mzb.json
   mods/mzb/mzb/images/cards/ironclad_strike.png
   ```

6. 启动游戏并启用 Mod。

### 项目结构

| 路径 | 作用 |
| --- | --- |
| `scripts/patches/StartingDeckPatch.cs` | 使用 Harmony 修改静默猎手初始牌组。 |
| `scripts/patches/IroncladStrikePortraitPatch.cs` | 使用 Harmony 修改铁甲战士打击的画像路径和纹理。 |
| `scripts/assets/ModTextureCache.cs` | 从 Mod DLL 旁边加载 loose PNG 纹理。 |
| `scripts/ModResources.cs` | 统一保存角色 ID、卡牌 ID 和资源路径。 |
| `mzb/images/cards/ironclad_strike.png` | 自定义卡牌画像资源。 |

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
- `.godot/`、`bin/`、`obj/`、DLL、PDB、`dist/` 等中间产物已加入 `.gitignore`，不应提交到仓库。
- 当前 Release 包不包含 PCK，卡图由 DLL 从 loose PNG 文件加载。

## English

> A small Slay the Spire 2 gameplay mod for Ironclad and Silent.

### Current Version

- Mod version: `0.1.0`
- Minimum game version: `0.107.1`
- Supported platform: Windows x86_64
- Dependency: BaseLib

> The current Release package is tested for the Windows version of Slay the Spire 2 only. Other operating systems are not supported yet.

### Features

- Replaces the Ironclad starting Strike card portrait with a custom image.
- Adds one extra vanilla Neutralize card to the Silent starting deck.

### Preview

| Ironclad Strike Portrait |
| :---: |
| ![Ironclad Strike custom portrait](mzb/images/cards/ironclad_strike.png) |

### Download

Download from GitHub Releases:

- [mzb-v0.1.0-windows-x64.zip](https://github.com/Muzibing/sts2mod/releases/download/v0.1.0/mzb-v0.1.0-windows-x64.zip)

### Installation

1. Make sure the Windows version of Slay the Spire 2 is installed.
2. Install [BaseLib](https://thunderstore.io/c/slay-the-spire-2/p/Alchyr/BaseLib/) and confirm this file exists:

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/BaseLib/BaseLib.dll
   ```

3. Download `mzb-v0.1.0-windows-x64.zip` from the Release page.
4. Extract it and place the `mzb/` folder into the game's `mods/` directory:

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/mzb/
   ```

5. The final folder should contain at least:

   ```text
   mods/mzb/mzb.dll
   mods/mzb/mzb.json
   mods/mzb/mzb/images/cards/ironclad_strike.png
   ```

6. Start the game and enable the mod.

### Project Structure

| Path | Purpose |
| --- | --- |
| `scripts/patches/StartingDeckPatch.cs` | Harmony patch for Silent's starting deck. |
| `scripts/patches/IroncladStrikePortraitPatch.cs` | Harmony patches for Ironclad Strike portrait path/texture. |
| `scripts/assets/ModTextureCache.cs` | Loads the loose PNG texture copied beside the mod DLL. |
| `scripts/ModResources.cs` | Shared IDs and asset paths. |
| `mzb/images/cards/ironclad_strike.png` | Custom card portrait asset. |

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
- `.godot/`, `bin/`, `obj/`, DLLs, PDBs, `dist/`, and other generated files are ignored and should not be committed.
- The current Release package does not include a PCK. The card portrait is loaded by the DLL from a loose PNG file.
