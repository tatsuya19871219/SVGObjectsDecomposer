using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;
using Svg;

namespace SVGObjectsDecomposer;

static class ValueConverterHelper
{

    internal static bool Not(bool value) => !value;

    // converter helper
    internal static BitmapImage ConvertToBitmapImage(SvgDocument svgdoc)
    {
        Bitmap bitmap = svgdoc.Draw();

        BitmapImage bitmapImage = new BitmapImage();

        // https://stackoverflow.com/questions/72544135/how-to-display-bitmap-object-in-winui-3-application
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            bitmapImage.SetSource(ms.AsRandomAccessStream());
        }

        return bitmapImage;
    }
}
