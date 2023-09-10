using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Speedometer.Controls
{
    public partial class Speedometer : UserControl
    {
        #region ���������

        /// <summary>
        /// ������ ����������: �������� ����������� �� ������ ���������� �� ������ ���������.
        /// </summary>
        private const double SpeedometerRadiusFactor = 0.9;

        /// <summary>
        /// ��� Pen, ��� ������ ������� ����������
        /// </summary>
        private readonly Pen HandPen = new Pen(new SolidColorBrush(Colors.Red), 6);

        /// <summary>
        /// Pen��� �����������������
        /// </summary>
        private readonly Pen DashPen = new Pen(new SolidColorBrush(Colors.Black), 4);

        /// <summary>
        /// ��������� ��� ���������� ������ �������. �����c ���������� ���������� �� ���� � ���������� ����� �������
        /// </summary>
        private const double HandLengthFactor = 0.95;

        /// <summary>
        /// ��������� ��� ���������� ����������� ������� ������� �� �����. ������ ���������� ���������� �� ��� ����� � ���������� ����������
        /// ������ �������
        /// </summary>
        private const double NotchInnerRadiusFactor = 0.7;

        /// <summary>
        /// ��������� ��� ���������� �������� ������� ������� �� �����
        /// </summary>
        private const double NotchOuterRadiusFactor = 0.9;

        /// <summary>
        /// ����� �������� �� ����������
        /// </summary>
        private const int DashCount = 20;

        /// <summary>
        /// ��������� ��� ������� ��������� ������ � ��������
        /// </summary>
        private const double NotchTextRadiusFactor = 0.65;

        #endregion

        #region ������� �������

        #region ����������� ��������

        /// <summary>
        /// �����-�� �����, ������� ����������� ��������, ������� ����� �������(������� ��� ��� ������ ����)
        /// (�������� ���� ������� ���������, ������)
        /// </summary>
        public static readonly AttachedProperty<double> MinSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(MinSpeed));

        /// <summary>
        /// �������� � ����������� ���������, ������� �������� � �������
        /// </summary>
        public double MinSpeed
        {
            get { return GetValue(MinSpeedProperty); }
            set { SetValue(MinSpeedProperty, value); }
        }

        #endregion

        #region ������� ��������

        /// <summary>
        /// �����-�� �����, ������� ����������� ��������, ������� ����� �������(������� ��� ��� ������ ����)
        /// (�������� ���� ������� ���������, ������)
        /// </summary>
        public static readonly AttachedProperty<double> CurrentSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(CurrentSpeed));

        /// <summary>
        /// �������� � ������� ���������, ������� �������� � �������
        /// </summary>
        public double CurrentSpeed
        {
            get { return GetValue(CurrentSpeedProperty); }
            set { SetValue(CurrentSpeedProperty, value); }
        }

        #endregion

        #region ������������ ��������

        /// <summary>
        /// �����-�� �����, ������� ����������� ��������, ������� ����� �������(������� ��� ��� ������ ����)
        /// (�������� ���� ������� ���������, ������)
        /// </summary>
        public static readonly AttachedProperty<double> MaxSpeedProperty
            = AvaloniaProperty.RegisterAttached<Speedometer, Interactive, double>(nameof(MaxSpeed));

        /// <summary>
        /// �������� � ����������� ������������, ������� �������� � �������
        /// </summary>
        public double MaxSpeed
        {
            get { return GetValue(MaxSpeedProperty); }
            set { SetValue(MaxSpeedProperty, value); }
        }

        #endregion

        #endregion

        #region ���������� ������ �����

        /// <summary>
        /// ������ ��������
        /// </summary>
        private double _width;

        /// <summary>
        /// ������ ��������
        /// </summary>
        private double _height;

        /// <summary>
        /// ����� - ����� ����������
        /// </summary>
        private Point _circleCenter = new Point(0, 0);

        /// <summary>
        /// ������� �� ���� ������ (������\������)
        /// </summary>
        private double _minSide;

        /// <summary>
        /// ������ ������ ����������(�������� �����)
        /// </summary>
        private double _speedometerRadius;

        /// <summary>
        /// ����� ������� ���������� (���������� - � ������� ������)
        /// </summary>
        private double _handLength;

        #region �������

        /// <summary>
        /// ������� �� ������ ������ ��������
        /// </summary>
        private double _notchInnerRadius;

        /// <summary>
        /// ������� �� ������ ������ ��������
        /// </summary>
        private double _notchOuterRadius;

        /// <summary>
        /// ���������� �� ������ ���������� �� ������ �������� �� ���������
        /// </summary>
        private double _notchTextRadius;

        #endregion

        /// <summary>
        /// ��������� �������� �� �������� ����� ����� ����������
        /// </summary>
        private double _speedDeltaPerDashInterval;

        #endregion

        /// <summary>
        /// �����������
        /// </summary>
        public Speedometer()
        {
            InitializeComponent();

            // ������������ ���������� ��������� ������� ��������
            PropertyChanged += OnPropertyChangedListener;

            // ����� �������� �������� �������� MinSpeed, ������� ����� HandleMinSpeedChanged()
            MinSpeedProperty.Changed.Subscribe(x => HandleMinSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));

            // ����� �������� �������� �������� CurrentSpeed, ������� ����� HandleCurrentSpeedChanged()
            CurrentSpeedProperty.Changed.Subscribe(x => HandleCurrentSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));

            // ����� �������� �������� �������� MaxSpeed, ������� ����� HandleMaxSpeedChanged()
            MaxSpeedProperty.Changed.Subscribe(x => HandleMaxSpeedChanged(x.Sender, x.NewValue.GetValueOrDefault<double>()));
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // ������ ����� �������� ������ ����

            // ����� ����
            context.DrawEllipse
            (
                new SolidColorBrush(Colors.Transparent),
                new Pen(new SolidColorBrush(Colors.Black)),
                _circleCenter,
                _speedometerRadius,
                _speedometerRadius
            );

            // ������ ��������
            DrawAllDashes(context);

            // ������ �������
            DrawHand(context);
        }

        #region ���������� ��������� ������� �������� � �������� ����

        /// <summary>
        /// ����� ����������, ����� �������� �����-�� �������� ��������
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // ���� �������� �������� � ������ Bounds (������� ��������)
            {
                OnResize((Rect)e.NewValue); // �������� OnResize, ������� ���� ����� ������� ��������
            }
        }

        /// <summary>
        /// ����� ���������� ��� ��������� �������� ��������
        /// </summary>
        private void OnResize(Rect bounds)
        {
            _width = bounds.Width;
            _height = bounds.Height;

            _circleCenter = new Point(_width / 2.0, _height / 2.0);

            _minSide = Math.Min(_width, _height);

            _speedometerRadius = SpeedometerRadiusFactor * _minSide / 2.0;


            _notchInnerRadius = _speedometerRadius * NotchInnerRadiusFactor;
            _notchOuterRadius = _speedometerRadius * NotchOuterRadiusFactor;
            _notchTextRadius = _speedometerRadius * NotchTextRadiusFactor;

            // ����� �������
            _handLength = HandLengthFactor * _speedometerRadius;


        }

        #endregion

        #region ����������� ��������� ���������

        /// <summary>
        /// ���� ����� ����� ����������, ����� �������� ����������� ��������
        /// </summary>
        private void HandleMinSpeedChanged(AvaloniaObject sender, double minSpeed)
        {
            CalculateSpeedDeltaPerDashInterval();

            InvalidateVisual(); // ���� ����� ���������� ������� ������������ ����
        }

        /// <summary>
        /// ���� ����� ����� ����������, ����� �������� ������� ��������
        /// </summary>
        private void HandleCurrentSpeedChanged(AvaloniaObject sender, double currentSpeed)
        {
            InvalidateVisual(); // ���� ����� ���������� ������� ������������ ����
        }

        /// <summary>
        /// ���� ����� ����� ����������, ����� �������� ������������ ��������
        /// </summary>
        private void HandleMaxSpeedChanged(AvaloniaObject sender, double maxSpeed)
        {
            CalculateSpeedDeltaPerDashInterval();

            InvalidateVisual(); // ���� ����� ���������� ������� ������������ ����
        }

        #endregion

        /// <summary>
        /// �����, ��� ������ ����� �� ��������� ���������� �� ������
        /// </summary>
        /// <param name="r">���������� �� ������ �� �����</param>
        /// <param name="angle">���� ����� ����� � ������</param>
        /// <returns>����������� ����� � ������������</returns>
        private Point GetPointCoodinatesByAngleAndRadius(double r, double angle)
        {
            double x = (-1 * r * Math.Sin(angle)) + _circleCenter.X;
            double y = (r * Math.Cos(angle)) + _circleCenter.Y;

            return new Point(x, y);
        }

        /// <summary>
        /// ����� ��� ������ �������� �����
        /// </summary>
        /// <param name="context">��� ���������� ���������</param>
        /// <param name="r1">����� �� ������ �� ����� ������ ��������</param>
        /// <param name="r2">������ �� ������ �� ����� ��������</param>
        /// <param name="angle">���� ����� ����� � ���������</param>
        /// <param name="pen">�����, ��� ����� ��� ���</param>
        private void DrawNotch(DrawingContext context, double r1, double r2, double angle, Pen pen)
        {
            Point a = GetPointCoodinatesByAngleAndRadius(r1, angle);
            Point b = GetPointCoodinatesByAngleAndRadius(r2, angle);

            context.DrawLine(pen, a, b);
        }

        /// <summary>
        /// ��������� ���� ����-������ (���� ������� ��� ��������) � ����������� �� ��������
        /// </summary>
        /// <param name="speed">�������� � ���������� � ���</param>
        /// <returns>���� �� 0 �� 2 ��</returns>
        private double GetAngleBySpeed(double speed)
        {
            double a = 2 * Math.PI / (MaxSpeed - MinSpeed);
            double b = -1 * a * MinSpeed;
            return a * speed + b;
        }

        /// <summary>
        /// ����� ������ ������� ����������
        /// </summary>
        /// <param name="context">�� ��� ��������</param>
        private void DrawHand(DrawingContext context)
        {
            double angle = GetAngleBySpeed(CurrentSpeed);

            DrawNotch(context, 0, _handLength, angle, HandPen);
        }

        /// <summary>
        /// ����� ��� ������ ���� �������� ��� ��������� ��������
        /// </summary>
        /// <param name="context">��� ��������</param>
        /// <param name="speed">��� ����� �������� ��������</param>
        private void DrawDash(DrawingContext context, double speed)
        {
            double angle = GetAngleBySpeed(speed);


            // ������ ���� ��������
            DrawNotch(context, _notchInnerRadius, _notchOuterRadius, angle, DashPen);

            // ������ ������� � ��������
            Point textPoint = GetPointCoodinatesByAngleAndRadius(_notchTextRadius, angle);

            FormattedText formattedText = new FormattedText
                (
                    $"{speed:0}",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface
                    (
                        FontFamily.Default,
                        FontStyle.Italic,
                        FontWeight.Light,
                        FontStretch.Condensed
                     ),
                    30,
                    new SolidColorBrush(Colors.Green)
                );

            Point correctedTextPoin = new Point
            (
                textPoint.X - formattedText.Width / 2.0,
                textPoint.Y - formattedText.Height / 2.0
            );

            context.DrawText
            (
                formattedText,
                correctedTextPoin
            );
        }

        /// <summary>
        /// ������ ��� ��������
        /// </summary>
        /// <param name="context">�� ��� ������</param>
        private void DrawAllDashes(DrawingContext context)
        {
            for (double speed = MinSpeed; speed <= MaxSpeed; speed += _speedDeltaPerDashInterval)
            {
                DrawDash(context, speed);
            }
        }

        private void CalculateSpeedDeltaPerDashInterval()
        {
            _speedDeltaPerDashInterval = (MaxSpeed - MinSpeed) / (double)(DashCount - 1);
        }
    }
}
