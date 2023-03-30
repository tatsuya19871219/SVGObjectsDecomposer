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
    {
        return purpose.Equals(targetPurpose) ? Visibility.Visible : Visibility.Collapsed;
    }
}
