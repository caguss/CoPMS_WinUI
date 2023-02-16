
using Microsoft.UI.Xaml.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace winui
{
        public class TimeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                return new DateTimeOffset(((DateTime)value).ToUniversalTime());

            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                return ((DateTimeOffset)value).DateTime;
            }
        }
}
