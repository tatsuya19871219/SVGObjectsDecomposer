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
using Windows.ApplicationModel.DataTransfer;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Helper = SVGObjectsDecomposer.ValueConverterHelper;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SVGObjectsDecomposer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public AppStateViewModel AppState {get; set;} = new();

    public MainWindow()
    {
        this.InitializeComponent();
        //AppState.Initialized();
    }

    void EvokeSVGDecomposerTask(Windows.Storage.StorageFile file)
    {

        var svgdoc = OpenSVGFile(file);

        OriginalSVGImage.Source = Helper.ConvertToBitmapImage(svgdoc);

        AppState.SVGLoaded();

        var svgContainer = new SVGContainer(svgdoc);

        // test
        // for (int i = 0; i < 5; i++)
        // {
        //     DecomposedImages.Items.Add(ConvertToBitmapImage(svgdoc));
        // }

        // foreach(var svgObject in svgContainer.SVGObjects)
        // {
        //     //DecomposedImages.Items.Add(ConvertToBitmapImage(svgObject.Image));
        //     DecomposedImages.Items.Add(svgObject.Image);
        // }

        var result = 
            from obj in svgContainer.SVGObjects
            group obj by obj.LayerID into g
            orderby g.Key
            select g;

        //DecomposedImages.ItemsSource = result;
        GroupedSVGObjects.Source = result;
    }

    SvgDocument OpenSVGFile(Windows.Storage.StorageFile file)
    {
        if (file is null) throw new ArgumentNullException("Given file is null");

        //var filename = file.Name;

        var svgdoc = SvgDocument.Open(file.Path);

        //Bitmap bitmap = svgdoc.Draw();

        ////var hbitmap = bitmap.GetHbitmap();

        //BitmapImage bitmapImage = new BitmapImage();

        //// https://stackoverflow.com/questions/72544135/how-to-display-bitmap-object-in-winui-3-application
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    ms.Position = 0;
        //    bitmapImage.SetSource(ms.AsRandomAccessStream());
        //}

        //// Set to the Image View
        //OriginalSVGImage.Source = bitmapImage;

        return svgdoc;
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

        if (file is null) return;

        EvokeSVGDecomposerTask(file);
    }

    private void CloseFileButton_Click(object sender, RoutedEventArgs e)
    {
        OriginalSVGImage.Source = null;

        AppState.Initialized();
    }

    private void App_DragOver(object sender, DragEventArgs e)
    {
        //DragDropMessage.Text += "Here";

        if (e.DataView.Contains(StandardDataFormats.StorageItems))
        {
            e.AcceptedOperation = DataPackageOperation.Copy;    

        }
    }

    private async void App_Drop(object sender, DragEventArgs e)
    {
        var items = await e.DataView.GetStorageItemsAsync();

        if (items.Count != 1)
        {
            ShowWarningAlart("Please give a single SVG file.");
            return;
        }

        if (Path.GetExtension(items[0].Name) != ".svg")
        {
            ShowWarningAlart("Please confirm your file extension is .svg.");
            return;
        }

        //var filepath = Path.GetFileName(items[0].Path);

        var file = items[0] as Windows.Storage.StorageFile;

        EvokeSVGDecomposerTask(file);
    }

    async void ShowWarningAlart(string message)
    {
        var originalMessage = DragDropMessage.Text;
        DragDropMessage.Text = message;

        await Task.Delay(2000);

        DragDropMessage.Text = originalMessage;
    }

    private void DecomposedImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedSVGObject = e.AddedItems[0] as SVGObject;

        SelectedSVGObject.Source = Helper.ConvertToBitmapImage(selectedSVGObject.Image);
    }

    
}
