// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Svg;
using SVGObjectsDecomposer.Models;
using SVGObjectsDecomposer.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SVGObjectsDecomposer;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    // DataContext
    public AppStateViewModel AppState {get; set;} = new();
    public DecomposeEditorViewModel DecomposeEditor { get; set; } = new();

    public MainWindow()
    {
        this.InitializeComponent();
    }

    void EvokeSVGDecomposerTask(Windows.Storage.StorageFile file)
    {
        var svgdoc = OpenSVGFile(file);

        DecomposeEditor.SetNewDocument(svgdoc);

        AppState.SVGLoaded();
    }

    SvgDocument OpenSVGFile(Windows.Storage.StorageFile file)
    {
        if (file is null) throw new ArgumentNullException("Given file is null");

        return SvgDocument.Open(file.Path);
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
        DecomposeEditor.ReleaseDocument();

        AppState.Initialized();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //
        DecomposeEditor.Save();
    }

    private void App_DragOver(object sender, DragEventArgs e)
    {
        // When the user drags item over the view, change cursor appearance.
        if (e.DataView.Contains(StandardDataFormats.StorageItems))
        {
            e.AcceptedOperation = DataPackageOperation.Copy;    
        }
    }

    private async void App_Drop(object sender, DragEventArgs e)
    {
        var items = await e.DataView.GetStorageItemsAsync();

        // Single file is only acceptable.
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
        if (e.AddedItems.Count == 0)
        {
            DecomposeEditor.SelectedSVGObject = null;
            return;
        }

        var selectedSVGObject = e.AddedItems[0] as EditableSVGObject;

        DecomposeEditor.SelectedSVGObject = selectedSVGObject;
    }

}
