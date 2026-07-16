# AGENTS.md

AI 入口速查。详细给真人看的说明放在 `README.md`。

## 项目基础

- 项目：Slay the Spire 2 Mod，Mod ID / 程序集名为 `mzb`。
- 功能：所有角色开局获得并在角色卡池中加入 `Mystery`；替换铁甲战士 `StrikeIronclad` 卡图；静默猎手开局额外获得 1 张原版 `Neutralize`。
- 关键文件：
  - `scripts/patches/`：Harmony 补丁。
  - `scripts/cards/Mystery.cs`：自定义卡牌「神秘」。
  - `scripts/assets/ModTextureCache.cs`：加载 loose PNG。
  - `scripts/ModResources.cs`：共享 ID 和资源路径。
  - `mzb/images/cards/`：卡图资源。

## 常用命令

- 构建验证：`dotnet build .\mzb.sln`
- 发布配置构建：`dotnet build .\mzb.sln -c ExportRelease`
- 查看状态：`git -c safe.directory=F:/mzb status --short --branch`
- 查看差异：`git -c safe.directory=F:/mzb diff`

## 代码规范

- 新逻辑按职责拆文件，避免把补丁、资源加载、常量写在同一个文件。
- 命名空间跟随目录，例如 patches 使用 `mzb.scripts.patches`。
- 游戏 ID、资源路径统一放在 `ModResources`。
- Harmony patch 保持小而明确，类名表达目标行为。
- 日志统一使用 `[mzb]` 前缀。
- 修改 C# 后必须运行 `dotnet build .\mzb.sln`。

## 操作边界

- 不提交 `.godot/`、`bin/`、`obj/`、`dist/`、`Local.props`、DLL/PDB 等生成物或本机配置。
- 不把本机 Steam/Godot 绝对路径写进 `mzb.csproj` 或 README。
- 不改 `master/main`，除非用户明确要求；默认在 `test` 或新分支工作。
- 不改版本号、tag、GitHub Release 附件，除非用户明确要求发布。
- 不删除用户未指定删除的资源、分支、tag 或远程配置。
- 仅改文档时可不构建，但必须检查 diff，确认没有误改代码或资源。
