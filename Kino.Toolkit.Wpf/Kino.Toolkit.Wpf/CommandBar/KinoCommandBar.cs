﻿using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Kino.Toolkit.Wpf
{
    public class KinoCommandBar : ItemsControl
    {
        public KinoCommandBar()
        {
        }

        public ObservableCollection<object> Options { get; } = new ObservableCollection<object>();
    }
}
