// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SVGObjectsDecomposer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        var svgPicker = new Windows.Storage.Pickers.FileOpenPicker();
        svgPicker.FileTypeFilter.Add(".svg");

        // Ref : https://github.com/microsoft/WindowsAppSDK/issues/1188

        // Get the current window's HWND by passing in the Window object
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        // Associate the HWND with the file picker
        WinRT.Interop.InitializeWithWindow.Initialize(svgPicker, hwnd);

        Windows.Storage.StorageFile file = await svgPicker.PickSingleFileAsync();

        if (file is not null ) 
        {
            var filename = file.Name;

            var svgdoc = SvgDocument.Open(file.Path);

            Bitmap bitmap = svgdoc.Draw();

            //var hbitmap = bitmap.GetHbitmap();

            BitmapImage bitmapImage = new BitmapImage();

            // https://stackoverflow.com/questions/72544135/how-to-display-bitmap-object-in-winui-3-application
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                bitmapImage.SetSource(ms.AsRandomAccessStream());
            }

            //
            OriginalSVGImage.Source = bitmapImage;
        }

    }
}
