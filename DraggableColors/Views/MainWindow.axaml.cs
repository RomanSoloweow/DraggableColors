using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using DraggableColors.ViewModels;

namespace DraggableColors.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public static MainWindow? Current { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Current = this;
            // ItemControl = this.ItemsControlRectangles;
//-:cnd:noEmit
#if DEBUG
            this.AttachDevTools();
#endif
//+:cnd:noEmit
        }
        

        private void InitializeComponent()
        {
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(this.ViewModel, 
                    x => x.RectanglesForView, 
                    x => x.ItemsControlRectangles.Items)
                    .DisposeWith(disposables);
            });

            AvaloniaXamlLoader.Load(this);

        }
    }
}