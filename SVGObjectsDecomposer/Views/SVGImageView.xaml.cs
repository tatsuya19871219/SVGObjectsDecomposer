using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Svg;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Drawing;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SVGObjectsDecomposer.Views;

public sealed partial class SVGImageView : UserControl
{
    DependencyProperty DocumentProperty = DependencyProperty.Register(
        nameof(Document),
        typeof(SvgDocument),
        typeof(SVGImageView),
        new PropertyMetadata(default(SvgDocument), new PropertyChangedCallback(OnDocumentChanged)));

    public SvgDocument Document
    {
        get => (SvgDocument)GetValue(DocumentProperty);
        set => SetValue(DocumentProperty, value);
    }

    public SVGImageView()
    {
        this.InitializeComponent();
    }


    private void UpdateImage(SvgDocument document)
    {
        SVGImage.Source = ConvertToBitmapImage(document);
    }

    private BitmapImage ConvertToBitmapImage(SvgDocument svgdoc)
    {
        if (svgdoc is null) return null;

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

    private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var view = d as SVGImageView;
        var document = e.NewValue as SvgDocument;

        //if (document is not null) view.UpdateImage(document);

        view.UpdateImage(document);
    }
}
