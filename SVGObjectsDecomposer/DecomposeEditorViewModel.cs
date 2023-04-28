using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Svg;
using SVGObjectsDecomposer.Models;
using SVGObjectsDecomposer.OutputWriters;
using System.Linq;
using System.Collections.Generic;

namespace SVGObjectsDecomposer;

public partial class DecomposeEditorViewModel : ObservableObject
{
    [ObservableProperty] SvgDocument _currentDocument;
    [ObservableProperty] EditableSVGContainer _editingSVGContainer;
    [ObservableProperty] EditableSVGObject _selectedSVGObject;
    // [ObservableProperty] object _layeredObjects;
    // [ObservableProperty] Dictionary<string, EditableSVGLayer> _layerDict;
    [ObservableProperty] OutputPurpose _outputPurposeType = OutputPurpose.Generic;
    [ObservableProperty] string _outputBaseDirname;
    // [ObservableProperty] string _message;

    public ICommand SetOutputPurposeCommand { get; }

    OutputWriterFactory _outputWriterFactory;

    public DecomposeEditorViewModel() 
    {
        SetOutputPurposeCommand = new RelayCommand<OutputPurpose>(SetOutputPurpose);

        // Message = "Hoge";
    }

    internal void SetNewDocument(SvgDocument document)
    {
        CurrentDocument = document;

        var svgContainer = new SVGContainer(document);

        EditingSVGContainer = new EditableSVGContainer(svgContainer);

        // Grouped = 
        //     from obj in svgContainer.SVGObjects
        //     group obj by obj.LayerID into g
        //     orderby g.Key
        //     select g;

        // LayeredObjects = 
        //     from layer in EditingSVGContainer.Layers
        //     from obj in layer.Objects
        //     group obj by layer.LayerName into g
        //     orderby g.Key
        //     select g;

        // // Todo : add the references to each editable layer 
        // _layerDict = new();

        // foreach(var layer in EditingSVGContainer.Layers)
        //     LayerDict.Add(layer.LayerName, layer);

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
        // LayeredObjects = null;
        OutputBaseDirname = null;
        _outputWriterFactory = null;
    }


    internal void Save() //=> EditingSVGContainer.SaveAll();
    {
        IOutputWriter writer = _outputWriterFactory.Create(OutputBaseDirname, OutputPurposeType);

        // show message dialog to notify the output directory is already exist

        writer.Execute();
    }
}
