using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

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
        private readonly Pen HandPen = new Pen(new SolidColorBrush(Colors.Red), 5);

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
        }

        #endregion

        #region ����������� ��������� ���������

        /// <summary>
        /// ���� ����� ����� ����������, ����� �������� ����������� ��������
        /// </summary>
        private void HandleMinSpeedChanged(AvaloniaObject sender, double minSpeed)
        {
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

        private void DrawHand(DrawingContext context)
        {
            double a = 2 * Math.PI / (MaxSpeed - MinSpeed);
            double b = -1 * a * MinSpeed;
            double q = a * CurrentSpeed + b;

            DrawNotch(context, 0, 200, q, HandPen);
        }

    }
}
