using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PhoneImitator.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Набираемый номер

    /// <summary>
    /// Номер отображаемый на экране (приватное свойство)
    /// </summary>
    private string _number;

    public string Number
    {
        get => _number;

        set => this.RaiseAndSetIfChanged(ref _number, value);
    }

    #endregion

    /// <summary>
    /// Истина если сейчас происходит звонок
    /// </summary>
    private bool _isCallInProgress;

    #region Команды нажатия на определенную кнопку

    /// <summary>
    /// Команда нажатия на единицу
    /// </summary>
    public ReactiveCommand<Unit, Unit> OneCommand { get; set; }
    public ReactiveCommand<Unit, Unit> TwoCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ThreeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> FourCommand { get; set; }
    public ReactiveCommand<Unit, Unit> FiveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> SixCommand { get; set; }
    public ReactiveCommand<Unit, Unit> SevenCommand { get; set; }
    public ReactiveCommand<Unit, Unit> EightCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NineCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ZeroCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }

    public ReactiveCommand<Unit, Unit> CallCommand { get; set; }
    public ReactiveCommand<Unit, Unit> TerminationCommand { get; set; }



    #endregion

    /// <summary>
    /// Конструктор, вызывается при создании класса
    /// </summary>
    public MainViewModel()
    {
        #region Команды связывания

        OneCommand = ReactiveCommand.Create(On1Pressed);
        TwoCommand = ReactiveCommand.Create(On2Pressed);
        ThreeCommand = ReactiveCommand.Create(On3Pressed);
        FourCommand = ReactiveCommand.Create(On4Pressed);
        FiveCommand = ReactiveCommand.Create(On5Pressed);
        SixCommand = ReactiveCommand.Create(On6Pressed);
        SevenCommand = ReactiveCommand.Create(On7Pressed);
        EightCommand = ReactiveCommand.Create(On8Pressed);
        NineCommand = ReactiveCommand.Create(On9Pressed);
        ZeroCommand = ReactiveCommand.Create(On0Pressed);
        DeleteCommand = ReactiveCommand.Create(OnDeleteButtonPressed);
        CallCommand = ReactiveCommand.Create(OnCallButtonPressed);
        TerminationCommand = ReactiveCommand.Create(OnTerminationCallPressed);

        #endregion

        Number = string.Empty;
        _isCallInProgress = false; // Когда телефон только создался - он не звонит
    }

    /// <summary>
    /// Нажатие на кнопку
    /// </summary>
    private void On1Pressed()
    {
        AddDigitToNumber("1");
    }

    private void On2Pressed()
    {
        AddDigitToNumber("2");
    }

    private void On3Pressed()
    {
        AddDigitToNumber("3");
    }

    private void On4Pressed()
    {
        AddDigitToNumber("4");
    }

    private void On5Pressed()
    {
        AddDigitToNumber("5");
    }

    private void On6Pressed()
    {
        AddDigitToNumber("6");
    }

    private void On7Pressed()
    {
        AddDigitToNumber("7");
    }

    private void On8Pressed()
    {
        AddDigitToNumber("8");
    }

    private void On9Pressed()
    {
        AddDigitToNumber("9");
    }

    private void On0Pressed()
    {
        AddDigitToNumber("0");
    }

    private void AddDigitToNumber(string digit)
    {
        Number = Number + digit;
    }

    private void OnDeleteButtonPressed()
    {
        if (Number.Length == 0)
        {
            return;
        }

        Number = Number.Substring(0, Number.Length - 1);
    }


    private void OnCallButtonPressed()
    {
        if (_isCallInProgress) // Если телефон уже звонит
        {
            return; // Выйти из метода, прервав его
        }

        var box = MessageBoxManager
            .GetMessageBoxStandard("Телефон", $"Мы звоним {Number}", ButtonEnum.Ok);

        box.ShowAsync();

        _isCallInProgress = true; // Звонок начался
    }

    private void OnTerminationCallPressed()
    {
        if (!_isCallInProgress)
        {
            // Если телефон сейчас не звонит, то и класть нечего
            return; // Выходим из метода, прервав действия
        }

        var box = MessageBoxManager
            .GetMessageBoxStandard("Телефон", $"Мы кладём трубку", ButtonEnum.Ok);

        box.ShowAsync();

        _isCallInProgress = false; // Звонок закончился
    }

}
