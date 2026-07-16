# STS2 mzb Mod

<p align="center">
  <a href="#中文">中文</a>
  &nbsp;|&nbsp;
  <a href="#english">English</a>
</p>

## 中文

一个《杀戮尖塔 2》轻量玩法 Mod。

### 功能

- 替换铁甲战士初始「打击」牌画像。
- 为静默猎手初始牌组额外加入 1 张原版「中和」牌。
- 所有角色开局获得，并在角色卡池中新增一张稀有攻击牌「神秘」：
  - 固有，费用 3。
  - 每当你打出一张牌，本回合费用减少 1。
  - 造成 3 点伤害，并使敌人失去 1 点力量。
  - 未升级时消耗，升级后不再消耗。

### 版本与适配

- 当前版本：`0.1.0`
- 游戏最低版本：`0.107.1`
- 适配平台：Windows x86_64
- 依赖：BaseLib

> 当前 Release 包仅针对 Windows 版 Slay the Spire 2 测试，其他系统暂未适配。

### 预览

| 铁甲战士打击画像 | 神秘 |
| :---: | :---: |
| ![铁甲战士打击自定义画像](mzb/images/cards/ironclad_strike.png) | ![神秘卡牌画像](mzb/images/cards/mystery.png) |

### 安装运行

1. 安装 Slay the Spire 2 Windows 版。
2. 安装 [BaseLib](https://thunderstore.io/c/slay-the-spire-2/p/Alchyr/BaseLib/)。
3. 下载 [mzb-v0.1.0-windows-x64.zip](https://github.com/Muzibing/sts2mod/releases/download/v0.1.0/mzb-v0.1.0-windows-x64.zip)。
4. 解压后将 `mzb/` 文件夹放入：

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/
   ```

5. 最终目录应包含：

   ```text
   mods/mzb/mzb.dll
   mods/mzb/mzb.json
   mods/mzb/mzb/images/cards/ironclad_strike.png
   mods/mzb/mzb/images/cards/mystery.png
   ```

6. 启动游戏并启用 Mod。

### 本地开发

1. 安装依赖：
   - .NET SDK 9.0+
   - Godot 4.5.1 Mono
   - Slay the Spire 2
   - BaseLib

2. 复制本机配置：

   ```powershell
   Copy-Item .\Local.props.example .\Local.props
   ```

3. 修改 `Local.props`：

   ```xml
   <Project>
     <PropertyGroup>
       <Sts2Dir>D:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
       <GodotExe>C:\Path\To\Godot_v4.5.1-stable_mono_win64.exe</GodotExe>
     </PropertyGroup>
   </Project>
   ```

4. 构建：

   ```powershell
   dotnet build .\mzb.sln
   ```

   构建会复制 `mzb.dll`、`mzb.json` 和卡图 PNG 到本机游戏 `mods/mzb` 目录。

5. 发布配置构建：

   ```powershell
   dotnet build .\mzb.sln -c ExportRelease
   ```

AI 协作入口见 [AGENTS.md](AGENTS.md)。

## English

A lightweight gameplay mod for Slay the Spire 2.

### Features

- Replaces the Ironclad starting Strike card portrait.
- Adds one extra vanilla Neutralize card to the Silent starting deck.
- Adds a rare Attack card named Mystery to every character starting deck and card pool:
  - Innate, costs 3.
  - Whenever you play a card, its cost is reduced by 1 this turn.
  - Deals 3 damage and gives the enemy -1 Strength.
  - Exhausts before upgrade; no longer Exhausts after upgrade.

### Version And Compatibility

- Current version: `0.1.0`
- Minimum game version: `0.107.1`
- Supported platform: Windows x86_64
- Dependency: BaseLib

> The current Release package is tested only for the Windows version of Slay the Spire 2. Other operating systems are not supported yet.

### Preview

| Ironclad Strike Portrait | Mystery |
| :---: | :---: |
| ![Ironclad Strike custom portrait](mzb/images/cards/ironclad_strike.png) | ![Mystery card portrait](mzb/images/cards/mystery.png) |

### Installation

1. Install the Windows version of Slay the Spire 2.
2. Install [BaseLib](https://thunderstore.io/c/slay-the-spire-2/p/Alchyr/BaseLib/).
3. Download [mzb-v0.1.0-windows-x64.zip](https://github.com/Muzibing/sts2mod/releases/download/v0.1.0/mzb-v0.1.0-windows-x64.zip).
4. Extract it and place the `mzb/` folder into:

   ```text
   <Steam>/steamapps/common/Slay the Spire 2/mods/
   ```

5. The final folder should contain:

   ```text
   mods/mzb/mzb.dll
   mods/mzb/mzb.json
   mods/mzb/mzb/images/cards/ironclad_strike.png
   mods/mzb/mzb/images/cards/mystery.png
   ```

6. Start the game and enable the mod.

### Local Development

1. Install dependencies:
   - .NET SDK 9.0+
   - Godot 4.5.1 Mono
   - Slay the Spire 2
   - BaseLib

2. Copy local config:

   ```powershell
   Copy-Item .\Local.props.example .\Local.props
   ```

3. Edit `Local.props`:

   ```xml
   <Project>
     <PropertyGroup>
       <Sts2Dir>D:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
       <GodotExe>C:\Path\To\Godot_v4.5.1-stable_mono_win64.exe</GodotExe>
     </PropertyGroup>
   </Project>
   ```

4. Build:

   ```powershell
   dotnet build .\mzb.sln
   ```

   The build copies `mzb.dll`, `mzb.json`, and the card PNG into the local game `mods/mzb` folder.

5. Build release configuration:

   ```powershell
   dotnet build .\mzb.sln -c ExportRelease
   ```

For AI collaboration notes, see [AGENTS.md](AGENTS.md).
