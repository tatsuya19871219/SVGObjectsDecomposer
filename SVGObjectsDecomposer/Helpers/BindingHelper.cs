using Microsoft.UI.Xaml;

namespace SVGObjectsDecomposer.Helpers;

public static class BindingHelper
{
    // public static Visibility AsVisibleIfSame(OutputPurpose purpose, OutputPurpose targetPurpose)
    //     => AsVisibleIf( purpose.Equals(targetPurpose) );

    public static Visibility AsVisibleIf(bool flag) => flag ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility AsVisibleIfNot(bool flag) => AsVisibleIf(!flag);

    // These functions can not use in x:Bind ...
    //public static bool IfAny(params bool[] conditions) => conditions.Any(c => c);
    //public static bool IfAll(params bool[] conditions) => conditions.All(c => c);

    public static bool IfAll(bool c1, bool c2) => c1 & c2;
}
