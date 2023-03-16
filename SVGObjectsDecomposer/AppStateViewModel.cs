using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SVGObjectsDecomposer;

// App state
public partial class AppStateViewModel : ObservableObject
{
    [ObservableProperty] private bool _isInitialized;
    [ObservableProperty] private bool _isSVGLoaded;

    // public bool IsInitialized
    // {
    //     get { return _isInitialized; }
    //     set
    //     {
    //         _isInitialized = value;
    //         this.OnPropertyChanged(nameof(IsInitialized));
    //     }
    // }
    // public bool IsSVGLoaded
    // {
    //     get { return _isSVGLoaded; }
    //     set
    //     {
    //         _isSVGLoaded = value;
    //         this.OnPropertyChanged(nameof(IsSVGLoaded));
    //     }
    // }

    //public event PropertyChangedEventHandler PropertyChanged = delegate { };

    public AppStateViewModel()
    {
        Initialized();
    }

    public void SVGLoaded()
    {
        IsInitialized = false;
        IsSVGLoaded = true;
    }

    public void Initialized()
    {
        IsInitialized = true;
        IsSVGLoaded = false;
    }
    
    // // https://learn.microsoft.com/ja-jp/windows/uwp/data-binding/data-binding-in-depth
    // public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    // {
    //     // Raise the PropertyChanged event, passing the name of the property whose value has changed.
    //     this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    // }
}