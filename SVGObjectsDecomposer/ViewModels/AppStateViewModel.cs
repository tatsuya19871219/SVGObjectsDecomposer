using CommunityToolkit.Mvvm.ComponentModel;
using SVGObjectsDecomposer.Helpers;

namespace SVGObjectsDecomposer.ViewModels;

// App state
public partial class AppStateViewModel : ObservableObject
{
    [ObservableProperty] bool _isInitialized;
    [ObservableProperty] bool _isSVGLoaded;

    // The accesibility 'readonly public' may be enough for this property
    // if the value does not update during running the app
    [ObservableProperty] bool _canUseInkscape;


    public AppStateViewModel()
    {
        Initialized();

        CanUseInkscape = InkscapeProcessHelper.CheckInkscapeProcess();
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

}