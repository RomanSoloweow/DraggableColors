using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.ColorGenerator;
using DraggableColors.Views;
using DynamicData;
using ReactiveUI;

namespace DraggableColors.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        SourceList<RectangleColorViewModel> Rectangles = new();
        public ReadOnlyObservableCollection<RectangleColorViewModel> RectanglesForView;
        public MainWindowViewModel()
        {
          
            Rectangles.Connect().ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out RectanglesForView).Subscribe();

            if (MainWindow.Current is null)
                return;
            
            var pixelDensity = MainWindow.Current.Screens.Primary.PixelDensity;
            var screenSize = new Size(MainWindow.Current.Screens.Primary.WorkingArea.Size.Width/pixelDensity, 
                MainWindow.Current.Screens.Primary.WorkingArea.Size.Height/pixelDensity);
            
            var countColumns = 10;
            var countRows = 14;
            var rectangleSize = new Size(screenSize.Width / countColumns, (screenSize.Height-25) / countRows);
        
            ColorGenerator generator = new();
            for (int j = 0; j < countRows; j++)
            for (int i = 0; i < countColumns; i++)
            {
                var color =  generator.NextUnique();
                Rectangles.Add(new RectangleColorViewModel(color,
                    rectangleSize,
                    new Point(rectangleSize.Width*i, rectangleSize.Height*j))); 
            }
        }
    }
}
