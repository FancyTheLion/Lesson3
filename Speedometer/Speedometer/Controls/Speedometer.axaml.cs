using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Speedometer.Controls
{
    public partial class Speedometer : UserControl
    {
        #region Константы

        private const double Radius = 200;

        #endregion

        #region Биндинг свойств

        #region Минимальная Скорость

        /// <summary>
        /// Какая-то хрень, кажется регистрация свойства, которое можно биндить(заучить что так должно быть)
        /// (навсегда этот коммент останется, бугага)
        /// </summary>
        public static readonly AttachedProperty<double> MinSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(MinSpeed));

        /// <summary>
        /// Свойство с минимальной скоростью, которое биндится в контрол
        /// </summary>
        public double MinSpeed
        {
            get { return GetValue(MinSpeedProperty); }
            set { SetValue(MinSpeedProperty, value); }
        }

        #endregion

        #region Текущая скорость

        /// <summary>
        /// Какая-то хрень, кажется регистрация свойства, которое можно биндить(заучить что так должно быть)
        /// (навсегда этот коммент останется, бугага)
        /// </summary>
        public static readonly AttachedProperty<double> CurrentSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(CurrentSpeed));

        /// <summary>
        /// Свойство с текущей скоростью, которое биндится в контрол
        /// </summary>
        public double CurrentSpeed
        {
            get { return GetValue(CurrentSpeedProperty); }
            set { SetValue(CurrentSpeedProperty, value); }
        }

        #endregion

        #region Максимальная скорость

        /// <summary>
        /// Какая-то хрень, кажется регистрация свойства, которое можно биндить(заучить что так должно быть)
        /// (навсегда этот коммент останется, бугага)
        /// </summary>
        public static readonly AttachedProperty<double> MaxSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(MaxSpeed));

        /// <summary>
        /// Свойство с минимальной температурой, которое биндится в контрол
        /// </summary>
        public double MaxSpeed
        {
            get { return GetValue(MaxSpeedProperty); }
            set { SetValue(MaxSpeedProperty, value); }
        }

        #endregion

        #endregion

        #region Размер "холста"

        /// <summary>
        /// Ширина контрола
        /// </summary>
        private int _width;

        /// <summary>
        /// Высота контрола
        /// </summary>
        private int _height;

        #endregion

        /// <summary>
        /// Точка - центр окружности
        /// </summary>
        private Point _circleCenter = new Point(200, 200);

        /// <summary>
        /// Конструктор
        /// </summary>
        public Speedometer()
        {
            InitializeComponent();

            // Регистрируем обработчик изменения свойств контрола
            PropertyChanged += OnPropertyChangedListener;

            // Когда меняется значение свойства MinSpeed, вызвать метод HandleMinSpeedChanged()
            MinSpeedProperty.Changed.Subscribe(x => HandleMinSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));

            // Когда меняется значение свойства CurrentSpeed, вызвать метод HandleCurrentSpeedChanged()
            CurrentSpeedProperty.Changed.Subscribe(x => HandleCurrentSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));

            // Когда меняется значение свойства MaxSpeed, вызвать метод HandleMaxSpeedChanged()
            MaxSpeedProperty.Changed.Subscribe(x => HandleMaxSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // Теперь можно рисовать всякие вещи

            // Рисую круг
            context.DrawEllipse
            (
                new SolidColorBrush(Colors.Transparent),
                new Pen(new SolidColorBrush(Colors.Black)),
                _circleCenter,
                Radius,
                Radius
            );

        }

        #region Обработчик изменения свойств контрола и размеров окна

        /// <summary>
        /// Метод вызывается, когда меняются какие-то свойства контрола
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // Если меняется свойство с именем Bounds (границы контрола)
            {
                OnResize((Rect)e.NewValue); // Вызываем OnResize, передав туда новые размеры контрола
            }
        }

        /// <summary>
        /// Метод вызывается при изменении размеров контрола
        /// </summary>
        private void OnResize(Rect bounds)
        {
            _width = (int)bounds.Width;
            _height = (int)bounds.Height;
        }

        #endregion

        #region Обработчики изменения скоростей

        /// <summary>
        /// Этот метод будет вызываться, когда меняется минимальная скорость
        /// </summary>
        private void HandleMinSpeedChanged(AvaloniaObject sender, double minSpeed)
        {
            InvalidateVisual(); // Этот метод заставляет контрол перерисовать себя
        }

        /// <summary>
        /// Этот метод будет вызываться, когда меняется текущая скорость
        /// </summary>
        private void HandleCurrentSpeedChanged(AvaloniaObject sender, double currentSpeed)
        {
            InvalidateVisual(); // Этот метод заставляет контрол перерисовать себя
        }

        /// <summary>
        /// Этот метод будет вызываться, когда меняется максимальная скорость
        /// </summary>
        private void HandleMaxSpeedChanged(AvaloniaObject sender, double maxSpeed)
        {
            InvalidateVisual(); // Этот метод заставляет контрол перерисовать себя
        }

        #endregion
    }
}
