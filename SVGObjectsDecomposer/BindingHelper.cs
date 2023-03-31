using Microsoft.UI.Xaml;
using SVGObjectsDecomposer.OutputWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer;

public static class BindingHelper
{
    public static Visibility AsVisibleIfSame(OutputPurpose purpose, OutputPurpose targetPurpose)
        => AsVisibleIf( purpose.Equals(targetPurpose) );


    public static Visibility AsVisibleIf(bool flag) => flag ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility AsVisibleIfNot(bool flag) => AsVisibleIf(!flag);
}
