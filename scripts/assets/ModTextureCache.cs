using Godot;
using MegaCrit.Sts2.Core.Logging;

namespace mzb.scripts.assets;

public static class ModTextureCache
{
    private static Texture2D? ironcladStrikePortrait;

    public static Texture2D? IroncladStrikePortrait =>
        ironcladStrikePortrait ??= LoadTexture(ModResources.IroncladStrikePortraitRelativePath);

    private static Texture2D? LoadTexture(string relativePath)
    {
        var assemblyPath = typeof(ModTextureCache).Assembly.Location;
        var assemblyDir = Path.GetDirectoryName(assemblyPath);
        if (string.IsNullOrEmpty(assemblyDir))
        {
            Log.Warn($"[mzb] Could not resolve mod assembly directory for {relativePath}.");
            return null;
        }

        var imagePath = Path.Combine(assemblyDir, relativePath);
        if (!File.Exists(imagePath))
        {
            Log.Warn($"[mzb] Missing texture file: {imagePath}");
            return null;
        }

        var image = Image.LoadFromFile(imagePath);
        return ImageTexture.CreateFromImage(image);
    }
}
