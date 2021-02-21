using Avalonia;
using Avalonia.ColorGenerator;
using ReactiveUI.Fody.Helpers;

namespace DraggableColors.ViewModels
{
    public class RectangleColorViewModel:ViewModelBase
    {
        [Reactive] public ColorWithValue Color { get; set; }
        [Reactive] public Point Position { get; set; }
        [Reactive] public Size Size { get; set; }
        public RectangleColorViewModel(ColorWithValue color, Size size, Point point = default)
        {
            Color = color;
            Position = point;
            Size = size;
        }
    }
}