using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Svg;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer;

public partial class DecomposeEditorViewModel : ObservableObject
{
    
    [ObservableProperty] SvgDocument currentDocument;
    [ObservableProperty] EditableSVGContainer editingSVGContainer;
    [ObservableProperty] SVGObject selectedSVGObject;
    [ObservableProperty] object grouped;

    public DecomposeEditorViewModel() { }

    internal void SetNewDocument(SvgDocument document)
    {
        CurrentDocument = document;

        var svgContainer = new SVGContainer(document);

        EditingSVGContainer = new EditableSVGContainer(svgContainer);

        Grouped = 
            from obj in svgContainer.SVGObjects
            group obj by obj.LayerID into g
            orderby g.Key
            select g;
    }

    //internal void DiscardChanges() => SetNewDocument(_currentDocument);

    internal void ReleaseDocument()
    {
        CurrentDocument = null;
        EditingSVGContainer.Dispose();
        //EditingSVGContainer = null;
        Grouped = null;
    }


    internal void Save() => EditingSVGContainer.SaveAll();
}
