using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Svg;
using SVGObjectsDecomposer.Models;
using SVGObjectsDecomposer.OutputWriters;

namespace SVGObjectsDecomposer;

public partial class DecomposeEditorViewModel : ObservableObject
{
    
    [ObservableProperty] SvgDocument currentDocument;
    [ObservableProperty] EditableSVGContainer editingSVGContainer;
    [ObservableProperty] EditableSVGObject selectedSVGObject;
    [ObservableProperty] object layeredObjects;
    [ObservableProperty] OutputPurpose outputPurposeType = OutputPurpose.Generic;
    [ObservableProperty] string outputBaseDirname;

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

        // Grouped = 
        //     from obj in svgContainer.SVGObjects
        //     group obj by obj.LayerID into g
        //     orderby g.Key
        //     select g;

        LayeredObjects = 
            from layer in EditingSVGContainer.Layers
            from obj in layer.Objects
            group obj by layer.LayerName into g
            orderby g.Key
            select g;

        _outputWriterFactory = new OutputWriterFactory(EditingSVGContainer);

        OutputBaseDirname = _outputWriterFactory.GetDefaultOutputBaseDirname();
        
    }

    private void SetOutputPurpose(OutputPurpose purpose) => OutputPurposeType = purpose;

    //internal void DiscardChanges() => SetNewDocument(_currentDocument);

    internal void ReleaseDocument()
    {
        CurrentDocument = null;
        EditingSVGContainer.Dispose();
        EditingSVGContainer = null;
        LayeredObjects = null;
        outputBaseDirname = null;
        _outputWriterFactory = null;
    }


    internal void Save() //=> EditingSVGContainer.SaveAll();
    {
        IOutputWriter writer = _outputWriterFactory.Create(OutputBaseDirname, OutputPurposeType);

        // show message dialog to notify the output directory is already exist

        writer.Execute();
    }
}
