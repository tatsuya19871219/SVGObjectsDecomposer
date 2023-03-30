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

    //private bool IsOutputPurposeOf(OutputPurpose purpose) => OutputPurposeType.Equals(purpose);

    //public Visibility AsVisibleWhen(OutputPurpose purpose) => IsOutputPurposeOf(purpose) ? Visibility.Visible : Visibility.Collapsed;

    //public Visibility AsVisibleIf(OutputPurpose purpose, OutputPurpose targetPurpose) => AsVisibleIf( purpose.Equals(targetPurpose) );

    //public Visibility AsVisibleIf(bool flag) => flag ? Visibility.Visible : Visibility.Collapsed;


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

        writer.Execute();
    }
}
