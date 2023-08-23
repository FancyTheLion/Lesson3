using ReactiveUI;

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
    /// Конструктор, вызывается при создании класса
    /// </summary>
    public MainViewModel()
    {
        Number = "Йифф!";
    }
}
