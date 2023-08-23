using ReactiveUI;
using System.Reactive;

namespace SimpleCalculator.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Первое число

    /// <summary>
    /// Первое число (приватное свойство)
    /// </summary>
    private string _firstNumber;

    public string FirstNumber
    {
        get => _firstNumber;

        set => this.RaiseAndSetIfChanged(ref _firstNumber, value);
    }

    #endregion

    #region Второе число

    /// <summary>
    /// Второе число (приватное свойство)
    /// </summary>
    private string _secondNumber;

    public string SecondNumber
    {
        get => _secondNumber;

        set => this.RaiseAndSetIfChanged(ref _secondNumber, value);
    }

    #endregion

    #region Результат вычислений

    /// <summary>
    /// Результат вычислений
    /// </summary>
    private double _result;

    /// <summary>
    /// Геттеры и сеттеры для результатов вычисления
    /// </summary>
    public double Result
    {
        get => _result;

        set => this.RaiseAndSetIfChanged(ref _result, value);
    }

    #endregion

    /// <summary>
    /// Команда складывания чисел
    /// </summary>
    public ReactiveCommand<Unit, Unit> PlusCommand { get; set; }

    /// <summary>
    /// Команда вычитания чисел
    /// </summary>
    public ReactiveCommand<Unit, Unit> MinusCommand { get; set; }

    /// <summary>
    /// Команда умножения чисел
    /// </summary>
    public ReactiveCommand<Unit, Unit> MultiplyCommand { get; set; }

    /// <summary>
    /// Команда деления чисел
    /// </summary>
    public ReactiveCommand<Unit, Unit> DivideCommand { get; set; }

    /// <summary>
    /// Команда сбрасывания значений результата
    /// </summary>
    public ReactiveCommand<Unit, Unit> ResetCommand { get; set; }


    public MainViewModel()
    {
        // Мы сказали "когда дёргается команда PlusCommand надо вызывать метод OnPlusPressed)"
        PlusCommand = ReactiveCommand.Create(OnPlusPressed);

        // Мы сказали "когда дёргается команда MinusCommand надо вызываеть метод OnMinusPressed)"
        MinusCommand = ReactiveCommand.Create(OnMinusPressed);

        // Мы сказали "когда дёргается команда MultiplyCommand надо вызываеть метод OnMultiplyPressed)"
        MultiplyCommand = ReactiveCommand.Create(OnMultiplyPressed);

        // Мы сказали "когда дёргается команда DivideCommand надо вызываеть метод OnDividePressed)"
        DivideCommand = ReactiveCommand.Create(OnDividePressed);

        // Мы сказали "когда дёргается команда ResetCommand надо вызываеть метод OnResetPressed)"
        ResetCommand = ReactiveCommand.Create(OnResetPressed);
    }

    /// <summary>
    /// Метод, вызываемый когда нажата кнопка сложения
    /// </summary>
    private void OnPlusPressed()
    {
        double firstNumber = double.Parse(FirstNumber);
        double secondNumber = double.Parse(SecondNumber);

        Result = firstNumber + secondNumber;
    }

    /// <summary>
    /// Метод, вызываемый когда нажата кнопка вычитания
    /// </summary>
    private void OnMinusPressed()
    {
        double firstNumber = double.Parse(FirstNumber);
        double secondNumber = double.Parse(SecondNumber);

        Result = firstNumber - secondNumber;
    }

    /// <summary>
    /// Метод, вызываемый когда нажата кнопка умножения
    /// </summary>
    private void OnMultiplyPressed()
    {
        double firstNumber = double.Parse(FirstNumber);
        double secondNumber = double.Parse(SecondNumber);

        Result = firstNumber * secondNumber;
    }

    /// <summary>
    /// Метод, вызываемый когда нажата кнопка деления
    /// </summary>
    private void OnDividePressed()
    {
        double firstNumber = double.Parse(FirstNumber);
        double secondNumber = double.Parse(SecondNumber);

        Result = firstNumber / secondNumber;
    }

    /// <summary>
    /// Метод, вызываемый когда нажата кнопка сбрасывания
    /// </summary>
    private void OnResetPressed()
    {
        FirstNumber = "";
        SecondNumber = "";
        Result = 0;
    }

}
