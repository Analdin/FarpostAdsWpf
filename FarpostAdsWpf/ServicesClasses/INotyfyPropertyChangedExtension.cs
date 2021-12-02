using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;

namespace FarpostAdsWpf.ServicesClasses
{
    public static class INotyfyPropertyChangedExtension
    {
        public static void Mutateverbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs>
            raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}
