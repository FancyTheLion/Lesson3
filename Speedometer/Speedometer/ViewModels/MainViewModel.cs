using ReactiveUI;
using Speedometer.Models;
using System.Numerics;
using System.Reactive;

namespace Speedometer.ViewModels;

public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Модель спидометра
    /// </summary>
    private CarSpeedometer _carSpeedometer;

    #region Обращение к свойствам через ширму

    #region Минимальная скорость

    public double MinSpeed
    {
        get => _carSpeedometer.MinSpeed;

        set => this.RaiseAndSetIfChanged(ref _carSpeedometer.MinSpeed, value);
    }

    #endregion

    #region Максимальная скорость

    public double MaxSpeed
    {
        get => _carSpeedometer.MaxSpeed;

        set => this.RaiseAndSetIfChanged(ref _carSpeedometer.MaxSpeed, value);
    }

    #endregion

    #region Текущая скорость

    public double CurrentSpeed
    {
        get => _carSpeedometer.CurrentSpeed;

        set => this.RaiseAndSetIfChanged(ref _carSpeedometer.CurrentSpeed, value);
    }

    #endregion

    #endregion

    #region Команды

    /// <summary>
    /// Команда для увеличения Скорости
    /// </summary>
    public ReactiveCommand<Unit, Unit> IncreaseSpeedCommand { get; set; }

    /// <summary>
    /// Команда для уменьшения Скорости
    /// </summary>
    public ReactiveCommand<Unit, Unit> DecreaseSpeedCommand { get; set; }

    #endregion

    public MainViewModel()
    {
        _carSpeedometer = new CarSpeedometer(0, 100, 300);




        #region Связывание команд и методов

        IncreaseSpeedCommand = ReactiveCommand.Create(IncreaseSpeed);
        DecreaseSpeedCommand = ReactiveCommand.Create(DecreaseSpeed);


        #endregion
    }

    /// <summary>
    /// Уменьшение скорости
    /// </summary>
    private void IncreaseSpeed()
    {
        if (CurrentSpeed >= MaxSpeed)
        {
            return;
        }

        CurrentSpeed = CurrentSpeed + 10;
    }

    /// <summary>
    /// Увеличение скорости
    /// </summary>
    private void DecreaseSpeed()
    {
        if (CurrentSpeed <= MinSpeed)
        {
            return;
        }

        CurrentSpeed = CurrentSpeed - 10;
    }
}
