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
    //public string Message = "Hey";

    SvgDocument _currentDocument;

    //public EditableSVGContainer EditingSVGContainer {get; set;}
    [ObservableProperty] EditableSVGContainer editingSVGContainer;
    [ObservableProperty] object grouped;
    //[ObservableProperty] EditableSVGObject selectedSVGObject;
    [ObservableProperty] SVGObject selectedSVGObject;

    public DecomposeEditorViewModel() { }

    internal void SetNewDocument(SvgDocument document)
    {
        _currentDocument = document;

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
        _currentDocument = null;
        EditingSVGContainer.Dispose();
        //EditingSVGContainer = null;
        Grouped = null;
    }

    internal void SelectSVGObject(SVGObject svgObject)
    {
        SelectedSVGObject = svgObject;
    }

    internal void Save() => EditingSVGContainer.SaveAll();
}
