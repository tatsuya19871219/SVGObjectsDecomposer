using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
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

    public DecomposeEditorViewModel() { }

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
    }

    //internal void DiscardChanges() => SetNewDocument(_currentDocument);

    internal void ReleaseDocument()
    {
        CurrentDocument = null;
        EditingSVGContainer.Dispose();
        EditingSVGContainer = null;
        LayeredObjects = null;
    }


    internal void Save() //=> EditingSVGContainer.SaveAll();
    {
        IOutputWriter writer = OutputWriterFactory.Create(EditingSVGContainer, OutputPurposeType);

        writer.Execute();
    }
}
