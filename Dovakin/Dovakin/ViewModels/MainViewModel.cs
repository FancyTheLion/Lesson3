using ReactiveUI;
using System.Reactive;

namespace Dovakin.ViewModels;

public class MainViewModel : ViewModelBase // три приватных свойства  с текущим и макс здор 
{
    #region Имя

    /// <summary>
    /// Имя героя
    /// </summary>
    private string _name;

    public string Name
    {
        get => _name;

        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    #endregion

    #region Максимальное здоровье

    /// <summary>
    /// Мах здоровье
    /// </summary>
    private int _maxHealth;

    public int MaxHealth
    {
        get => _maxHealth;

        set => this.RaiseAndSetIfChanged(ref _maxHealth, value);
    }

    #endregion

    #region Текущее здоровье

    /// <summary>
    /// Текущее здоровье
    /// </summary>
    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;

        set => this.RaiseAndSetIfChanged(ref _currentHealth, value);
    }

    #endregion

    #region Жив персонаж или мертв

    /// <summary>
    /// Жив ли
    /// </summary>
    private bool _isAlive;

    public bool IsAlive
    {
        get => _isAlive;

        set => this.RaiseAndSetIfChanged(ref _isAlive, value);
    }

    #endregion

    #region Команды

    /// <summary>
    /// Команда нанесения урона
    /// </summary>
    public ReactiveCommand<Unit, Unit> DoDamageCommand { get; set; }

    /// <summary>
    /// Команда лечения
    /// </summary>
    public ReactiveCommand<Unit, Unit> HealCommand { get; set; }


    #endregion

    public MainViewModel()
    {
        Name = "Dovakin";
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        IsAlive = true;

        #region Связывание команд и методов

        DoDamageCommand = ReactiveCommand.Create(DoDamage);

        HealCommand = ReactiveCommand.Create(Heal);

        #endregion
    }

    /// <summary>
    /// Метод для нанесения урона
    /// </summary>
    private void DoDamage()
    {
        if (!IsAlive)
        {
            return;
        }

        CurrentHealth = CurrentHealth - 33;

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            CurrentHealth = 0;
        }
    }

    /// <summary>
    /// Восполнение здоровья
    /// </summary>
    private void Heal()
    {
        if (!IsAlive)
        {
            return;
        }
        
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth = CurrentHealth + 1;
        }
    }




}
