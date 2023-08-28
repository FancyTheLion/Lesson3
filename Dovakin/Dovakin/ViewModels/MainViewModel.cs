using Dovakin.Model;
using ReactiveUI;
using System.Reactive;

namespace Dovakin.ViewModels;

public class MainViewModel : ViewModelBase // три приватных свойства  с текущим и макс здор 
{
    #region Модели

    /// <summary>
    /// Модель игрока
    /// </summary>
    private Player _player;

    #endregion

    #region Имя

    public string Name
    {
        get => _player.Name;

        set => this.RaiseAndSetIfChanged(ref _player.Name, value);
    }

    #endregion

    #region Максимальное здоровье

    public int MaxHealth
    {
        get => _player.MaxHealth;

        set => this.RaiseAndSetIfChanged(ref _player.MaxHealth, value);
    }

    #endregion

    #region Текущее здоровье

    public int CurrentHealth
    {
        get => _player.CurrentHealth;

        set => this.RaiseAndSetIfChanged(ref _player.CurrentHealth, value);
    }

    #endregion

    #region Жив персонаж или мертв

    public bool IsAlive
    {
        get => _player.IsAlive;

        set => this.RaiseAndSetIfChanged(ref _player.IsAlive, value);
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
        _player = new Player("Довакин", 200);

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
