using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace BatteryWatcher.Icon;

public static class BatteryIconRenderer
{
    private const int IconSize = 32;
    private const string FontName = "Segoe UI";
    private const float PercentFontSize = 10f;

    public static System.Drawing.Icon Render(string text, Color textColor)
    {
        using var bitmap = new Bitmap(IconSize, IconSize, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            using var textPath = BuildTextPath(text);
            CenterPath(textPath);

            using var textBrush = new SolidBrush(textColor);
            g.FillPath(textBrush, textPath);
        }

        return ToIcon(bitmap);
    }

    private static GraphicsPath BuildTextPath(string text)
    {
        // "43%"のような数字+%は、%だけ小さいフォントで描画して数字側の見切れを防ぐ(BW-010)。
        if (text.Length > 1 && text[^1] == '%')
        {
            return BuildNumberWithPercentPath(text[..^1]);
        }

        var fontSize = text.Length switch
        {
            <= 2 => 27f,
            3 => 21f,
            _ => 14f,
        };
        return CreateTextPath(text, fontSize);
    }

    private static GraphicsPath BuildNumberWithPercentPath(string numberPart)
    {
        var numberFontSize = numberPart.Length switch
        {
            1 => 27f,
            2 => 24f,
            _ => 16f,
        };

        using var numberFont = new Font(FontName, numberFontSize, FontStyle.Bold, GraphicsUnit.Pixel);
        using var percentFont = new Font(FontName, PercentFontSize, FontStyle.Bold, GraphicsUnit.Pixel);

        var numberPath = new GraphicsPath();
        numberPath.AddString(numberPart, numberFont.FontFamily, (int)FontStyle.Bold, numberFont.Size, PointF.Empty, StringFormat.GenericTypographic);

        using var percentPath = new GraphicsPath();
        percentPath.AddString("%", percentFont.FontFamily, (int)FontStyle.Bold, percentFont.Size, PointF.Empty, StringFormat.GenericTypographic);

        var numberBounds = numberPath.GetBounds();
        var baselineShift = GetBaselineOffset(numberFont) - GetBaselineOffset(percentFont);
        using (var matrix = new Matrix())
        {
            matrix.Translate(numberBounds.Right, baselineShift);
            percentPath.Transform(matrix);
        }

        numberPath.AddPath(percentPath, false);
        return numberPath;
    }

    private static GraphicsPath CreateTextPath(string text, float fontSize)
    {
        using var font = new Font(FontName, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
        var path = new GraphicsPath();
        path.AddString(text, font.FontFamily, (int)FontStyle.Bold, font.Size, PointF.Empty, StringFormat.GenericTypographic);
        return path;
    }

    private static float GetBaselineOffset(Font font)
    {
        var family = font.FontFamily;
        var style = font.Style;
        return font.Size * family.GetCellAscent(style) / family.GetEmHeight(style);
    }

    public static System.Drawing.Icon RenderPowerPlug(Color plugColor)
    {
        using var bitmap = new Bitmap(IconSize, IconSize, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using var boltPath = CreateBoltPath();
            DrawOutlinedPath(g, boltPath, plugColor);
        }

        return ToIcon(bitmap);
    }

    private static void CenterPath(GraphicsPath path)
    {
        var bounds = path.GetBounds();

        // フォントの実測幅がアイコンサイズを超える場合は、見切れを防ぐため縮小してから中央に配置する。
        var scale = Math.Min(1f, Math.Min(IconSize / bounds.Width, IconSize / bounds.Height));
        if (scale < 1f)
        {
            using var scaleMatrix = new Matrix();
            scaleMatrix.Scale(scale, scale);
            path.Transform(scaleMatrix);
            bounds = path.GetBounds();
        }

        var offsetX = (IconSize - bounds.Width) / 2f - bounds.X;
        var offsetY = (IconSize - bounds.Height) / 2f - bounds.Y;
        using var matrix = new Matrix();
        matrix.Translate(offsetX, offsetY);
        path.Transform(matrix);
    }

    private static void DrawOutlinedPath(Graphics g, GraphicsPath path, Color fillColor)
    {
        using var outlinePen = new Pen(Color.Black, 1f) { LineJoin = LineJoin.Round };
        g.DrawPath(outlinePen, path);
        using var fillBrush = new SolidBrush(fillColor);
        g.FillPath(fillBrush, path);
    }

    private static GraphicsPath CreateBoltPath()
    {
        // 稲妻（電源接続中）のジグザグ形状。正規化座標(0..1)をアイコンサイズにスケールする。
        ReadOnlySpan<(float X, float Y)> points = stackalloc (float X, float Y)[]
        {
            (0.60f, 0.05f),
            (0.30f, 0.55f),
            (0.47f, 0.55f),
            (0.40f, 0.95f),
            (0.72f, 0.45f),
            (0.53f, 0.45f),
        };

        var path = new GraphicsPath();
        var scaled = new PointF[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            scaled[i] = new PointF(points[i].X * IconSize, points[i].Y * IconSize);
        }
        path.AddPolygon(scaled);
        return path;
    }

    private static System.Drawing.Icon ToIcon(Bitmap bitmap)
    {
        var hIcon = bitmap.GetHicon();
        try
        {
            using var handleIcon = System.Drawing.Icon.FromHandle(hIcon);
            return (System.Drawing.Icon)handleIcon.Clone();
        }
        finally
        {
            NativeMethods.DestroyIcon(hIcon);
        }
    }

}
