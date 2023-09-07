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

        /// <summary>
        /// Радиус спидометра: половина минимальной из сторон умноженная на данную константу.
        /// </summary>
        private const double SpeedometerRadiusFactor = 0.9;

        /// <summary>
        /// Это Pen, что рисует стрелку спидометра
        /// </summary>
        private readonly Pen HandPen = new Pen(new SolidColorBrush(Colors.Red), 5);

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

        #region Координаты особых точек

        /// <summary>
        /// Ширина контрола
        /// </summary>
        private double _width;

        /// <summary>
        /// Высота контрола
        /// </summary>
        private double _height;

        /// <summary>
        /// Точка - центр окружности
        /// </summary>
        private Point _circleCenter = new Point(0, 0);

        /// <summary>
        /// Меньшая из двух сторон (ширины\высоты)
        /// </summary>
        private double _minSide;

        /// <summary>
        /// Радиус самого спидометра(внешнего круга)
        /// </summary>
        private double _speedometerRadius;

        #endregion

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
                _speedometerRadius,
                _speedometerRadius
            );

            // Рисуем стрелку
            DrawHand(context);


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
            _width = bounds.Width;
            _height = bounds.Height;

            _circleCenter = new Point(_width / 2.0, _height / 2.0);

            _minSide = Math.Min(_width, _height);

            _speedometerRadius = SpeedometerRadiusFactor * _minSide / 2.0;
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

        /// <summary>
        /// Метод, что рисует точку на некотором расстоянии от центра
        /// </summary>
        /// <param name="r">Расстояние от центра до точки</param>
        /// <param name="angle">Угол между нулем и точкой</param>
        /// <returns>Возвращение точки с координатами</returns>
        private Point GetPointCoodinatesByAngleAndRadius(double r, double angle)
        {
            double x = (-1 * r * Math.Sin(angle)) + _circleCenter.X;
            double y = (r * Math.Cos(angle)) + _circleCenter.Y;

            return new Point(x, y);
        }

        /// <summary>
        /// Метод что рисует условную линию
        /// </summary>
        /// <param name="context">Где происходит рисование</param>
        /// <param name="r1">Радис от центра до точки начала черточки</param>
        /// <param name="r2">Радиус от центра до конца черточки</param>
        /// <param name="angle">Угол между нулем и черточкой</param>
        /// <param name="pen">Кисть, что рисет это все</param>
        private void DrawNotch(DrawingContext context, double r1, double r2, double angle, Pen pen)
        {
            Point a = GetPointCoodinatesByAngleAndRadius(r1, angle);
            Point b = GetPointCoodinatesByAngleAndRadius(r2, angle);

            context.DrawLine(pen, a, b);
        }

        private void DrawHand(DrawingContext context)
        {
            double a = 2 * Math.PI / (MaxSpeed - MinSpeed);
            double b = -1 * a * MinSpeed;
            double q = a * CurrentSpeed + b;

            DrawNotch(context, 0, 200, q, HandPen);
        }

    }
}
