using Game.Models;
using Game.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Game.Helpers
{
    /// <summary>
    /// This converter is used by the Pickers, to change from the picker value to the Name value 
    /// from id or vice versa. This allows the convert in the picker to be data bound back and forth the model
    /// </summary>
    public class ItemLocationPickerConverter : IValueConverter
    {
        /// <summary>
        /// Parses the id to name.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("0"))
                return "None";
            return ItemIndexViewModel.Instance.Dataset.Where(a =>
                        a.Id == value.ToString())
                        .FirstOrDefault().Name;
        }


        /// <summary>
        /// Converts the name to id.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
