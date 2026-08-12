using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace BatteryWatcher.Icon;

public static class BatteryIconRenderer
{
    private const int IconSize = 32;

    public static System.Drawing.Icon Render(string text, Color textColor)
    {
        using var bitmap = new Bitmap(IconSize, IconSize, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var fontSize = text.Length switch
            {
                <= 2 => 25f,
                3 => 20f,
                _ => 14f,
            };
            using var font = new Font("Segoe UI", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            using var textPath = new GraphicsPath();
            textPath.AddString(text, font.FontFamily, (int)FontStyle.Bold, font.Size, PointF.Empty, StringFormat.GenericTypographic);
            CenterPath(textPath);

            DrawOutlinedPath(g, textPath, textColor);
        }

        return ToIcon(bitmap);
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
