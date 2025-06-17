using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace GitHubMauiApp.Converters
{
    public class EditModeToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEdit && isEdit)
                return "Edit Issue";
            return "Add Issue";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 