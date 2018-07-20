﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media;

namespace Kino.Toolkit.Wpf
{
    [StyleTypedProperty(Property = nameof(RibbonStyle), StyleTargetType = typeof(Ribbon))]
    public class KinoRibbonWindow : RibbonWindow
    {
        public KinoRibbonWindow()
        {
            DefaultStyleKey = typeof(KinoRibbonWindow);
            var length = WindowService.PaddedBorder;
            var dpi = VisualTreeHelper.GetDpi(this);
            var lengthWithScale = length / dpi.DpiScaleX;
            ExtraBorderPadding = new Thickness(lengthWithScale);
        }

        public Thickness ExtraBorderPadding { get; }


        /// <summary>
        /// 获取或设置RibbonStyle的值
        /// </summary>  
        public Style RibbonStyle
        {
            get => (Style)GetValue(RibbonStyleProperty);
            set => SetValue(RibbonStyleProperty, value);
        }

        /// <summary>
        /// 标识 RibbonStyle 依赖属性。
        /// </summary>
        public static readonly DependencyProperty RibbonStyleProperty =
            DependencyProperty.Register(nameof(RibbonStyle), typeof(Style), typeof(KinoRibbonWindow), new PropertyMetadata(default(Style), OnRibbonStyleChanged));

        private static void OnRibbonStyleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {

            var oldValue = (Style)args.OldValue;
            var newValue = (Style)args.NewValue;
            if (oldValue == newValue)
                return;

            var target = obj as KinoRibbonWindow;
            target?.OnRibbonStyleChanged(oldValue, newValue);
        }

        /// <summary>
        /// RibbonStyle 属性更改时调用此方法。
        /// </summary>
        /// <param name="oldValue">RibbonStyle 属性的旧值。</param>
        /// <param name="newValue">RibbonStyle 属性的新值。</param>
        protected virtual void OnRibbonStyleChanged(Style oldValue, Style newValue)
        {
            Resources.Remove(typeof(Ribbon));
            if (newValue != null)
                Resources.Add(typeof(Ribbon), newValue);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (SizeToContent == SizeToContent.WidthAndHeight)
                InvalidateMeasure();
        }

       
    }
}
