using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using ReactiveUI;
using DraggableColors.ViewModels;
using Avalonia.Input;
using System;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using AvaloniaAppTemplate.Extensions;

namespace DraggableColors.Views
{
    public partial class RectangleColor : ReactiveUserControl<RectangleColorViewModel>
    {
        private Point oldPosition;
        public RectangleColor()
        {
            InitializeComponent();
        }
        public TranslateTransform? TranslateTransform;

        private void InitializeComponent()
        {
            this.WhenActivated(disposables =>
            {
                TranslateTransform = this.RenderTransform as TranslateTransform;
                this.OneWayBind(ViewModel, 
                        x => x.Color.Color, 
                        x => x.Panel.Background,
                        x=> new SolidColorBrush(x))
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                    x=>x.Position.X,
                    x=>x.TranslateTransform!.X)
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                    x => x.Position.Y, 
                    x => x.TranslateTransform!.Y)
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                        x => x.Size.Width, 
                        x => x.Panel.Width)
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                        x => x.Size.Height, 
                        x => x.Panel.Height)
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                        x => x.Color.Name, 
                        x => x.ColorName.Text)
                    .DisposeWith(disposables);
                
                this.OneWayBind(this.ViewModel, 
                        x => x.Color.Hex, 
                        x => x.ColorHex.Text)
                    .DisposeWith(disposables);
                
                this.Panel.Events().PointerPressed.Subscribe(OnEventBorderPointerPressed).DisposeWith(disposables);
                this.Panel.Events().PointerReleased.Subscribe(OnEventBorderPointerReleased).DisposeWith(disposables);
                this.Panel.Events().DoubleTapped.Subscribe(OnEventBorderDoubleTapped).DisposeWith(disposables);
                this.OneWayBind(this.ViewModel, 
                        x => x.Position.X, 
                        x => x.Parent.ZIndex)
                    .DisposeWith(disposables);
                
            });

            AvaloniaXamlLoader.Load(this);

        }
        
        void OnEventBorderPointerPressed(PointerPressedEventArgs e)
        {
            this.PointerMoved += OnEventPointerMoved;
            oldPosition = e.GetPosition(MainWindow.Current.ItemsControlRectangles);
        }
        void OnEventBorderDoubleTapped(RoutedEventArgs e)
        {
            Application.Current.Clipboard.SetTextAsync(ViewModel.Color.ToFormatString());
        }
        void OnEventBorderPointerReleased(PointerReleasedEventArgs e)
        {
            this.PointerMoved -= this.OnEventPointerMoved;
        }

        void OnEventPointerMoved(object subject, PointerEventArgs e)
        {
            var currentPosition = e.GetPosition(MainWindow.Current.ItemsControlRectangles);
            this.ViewModel.Position  = this.ViewModel.Position + currentPosition - oldPosition;
            oldPosition = currentPosition;
        }
    }
}