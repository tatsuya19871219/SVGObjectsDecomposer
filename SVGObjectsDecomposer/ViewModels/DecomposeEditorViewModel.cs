using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Svg;
using SVGObjectsDecomposer.Models;
using SVGObjectsDecomposer.OutputWriters;
using System.Diagnostics;
using System.Windows.Input;

namespace SVGObjectsDecomposer.ViewModels;

public partial class DecomposeEditorViewModel : ObservableObject
{
    [ObservableProperty] SvgDocument _currentDocument;
    [ObservableProperty] EditableSVGContainer _editingSVGContainer;
    [ObservableProperty] EditableSVGObject _selectedSVGObject;
    [ObservableProperty] OutputPurpose _outputPurposeType = OutputPurpose.Generic;
    [ObservableProperty] string _outputBaseDirname;

    public ICommand SetOutputPurposeCommand { get; }

    OutputWriterFactory _outputWriterFactory;

    public DecomposeEditorViewModel() 
    {
        SetOutputPurposeCommand = new RelayCommand<OutputPurpose>(SetOutputPurpose);
    }

    internal void SetNewDocument(SvgDocument document)
    {
        CurrentDocument = document;

        var svgContainer = new SVGContainer(document);

        EditingSVGContainer = new EditableSVGContainer(svgContainer);

        _outputWriterFactory = new OutputWriterFactory(EditingSVGContainer);

        OutputBaseDirname = _outputWriterFactory.GetDefaultOutputBaseDirname();
        
    }

    private void SetOutputPurpose(OutputPurpose purpose) => OutputPurposeType = purpose;

    //internal void DiscardChanges() => SetNewDocument(_currentDocument);

    internal void ReleaseDocument()
    {
        SelectedSVGObject.Dispose();
        SelectedSVGObject = null;
        CurrentDocument = null;
        EditingSVGContainer.Dispose();
        EditingSVGContainer = null;
        OutputBaseDirname = null;
        _outputWriterFactory = null;
    }


    internal void Save() //=> EditingSVGContainer.SaveAll();
    {
        IOutputWriter writer = _outputWriterFactory.Create(OutputBaseDirname, OutputPurposeType);

        // Todo: show message dialog to notify the output directory is already exist

        // open output folder
        var outputPath = @$"{ writer.OutputBaseDirname}";
        Process.Start("explorer.exe", $"{outputPath}");

        writer.Execute();
    }
}
