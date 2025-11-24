using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace PIM4App.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                return id switch
                {
                    1 => "Aberto",
                    2 => "Em Andamento",
                    3 => "Finalizado",
                    _ => "Desconhecido"
                };
            }
            return "---";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                return id switch
                {
                    1 => Colors.OrangeRed,
                    2 => Color.FromArgb("#764dfc"),
                    3 => Colors.Green,
                    _ => Colors.Gray
                };
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}