// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Svg;
using Microsoft.UI.Xaml.Media.Imaging;

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
        SVGImage.Source = ValueConverterHelper.ConvertToBitmapImage(document);
    }

    private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var view = d as SVGImageView;
        var document = e.NewValue as SvgDocument;

        if (document is null)
        {
            // nop
            //view.SVGBitmapImage = null;
        }
        else
        {
            //view.SVGBitmapImage = ValueConverterHelper.ConvertToBitmapImage(document);

            view.UpdateImage(document);
        }
    }
}
