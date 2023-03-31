using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SVGObjectsDecomposer;

// App state
public partial class AppStateViewModel : ObservableObject
{
    [ObservableProperty] bool _isInitialized;
    [ObservableProperty] bool _isSVGLoaded;

    [ObservableProperty] bool _canUseInkscape;

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

        CheckInkscape();
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

    void CheckInkscape()
    {
        try
        {
            using(Process proc = new Process())
            {
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = "inkscape.exe";
                proc.StartInfo.Arguments = "--version";
                proc.Start();
                proc.WaitForExit();
            }

            CanUseInkscape = true;
        }
        catch
        {
            CanUseInkscape = false;
        }
    }
    
    // // https://learn.microsoft.com/ja-jp/windows/uwp/data-binding/data-binding-in-depth
    // public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    // {
    //     // Raise the PropertyChanged event, passing the name of the property whose value has changed.
    //     this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    // }
}