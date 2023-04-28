using Microsoft.UI.Xaml;
using SVGObjectsDecomposer.OutputWriters;

namespace SVGObjectsDecomposer.Helpers;

public static class BindingHelper
{
    public static Visibility AsVisibleIfSame(OutputPurpose purpose, OutputPurpose targetPurpose)
        => AsVisibleIf( purpose.Equals(targetPurpose) );

    public static Visibility AsVisibleIf(bool flag) => flag ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility AsVisibleIfNot(bool flag) => AsVisibleIf(!flag);
}
