using Avalonia.Controls;
using Plant.Model;
using ReactiveUI;
using System.Reactive;

namespace Plant.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Модель

    /// <summary>
    /// Модель растения
    /// </summary>
    private PlantModel _plant;

    #endregion

    #region Название

    public string Title
    {
        get => _plant.Title;

        set => this.RaiseAndSetIfChanged(ref _plant.Title, value);
    }

    #endregion

    #region Семейство растений

    public string Family
    {
        get => _plant.Family;

        set => this.RaiseAndSetIfChanged(ref _plant.Family, value);
    }

    #endregion

    #region Высота растения

    public double HeightThreshold
    {
        get => _plant.HeightThreshold;

        set => this.RaiseAndSetIfChanged(ref _plant.HeightThreshold, value);
    }

    #endregion

    #region Цветет ли растение

    public bool DoesThePlantBloom
    {
        get => _plant.DoesThePlantBloom;

        set => this.RaiseAndSetIfChanged(ref _plant.DoesThePlantBloom, value);
    }

    #endregion

    #region Текущее значение высоты растения

    public double CurrentHeight
    {
        get => _plant.CurrentHeight;

        set => this.RaiseAndSetIfChanged(ref _plant.CurrentHeight, value);
    }

    #endregion

    #region Команды

    /// <summary>
    /// Команда
    /// </summary>
    public ReactiveCommand<Unit, Unit> GrowthCommand { get; set; }

    #endregion

    public MainViewModel()
    {
        //        _plant = new PlantModel("Лилия", "Лилейные", 1);
        _plant = new PlantModel("Дуб", "Березовое", 1.5);

        GrowthCommand = ReactiveCommand.Create(Growth);
    }

    private void Growth()
    {
        CurrentHeight = CurrentHeight + 0.1;
        
        if (CurrentHeight > HeightThreshold)
        {
            StartToBloom();
        }
    }

    private void StartToBloom()
    {
        if (DoesThePlantBloom)
        {
            return;
        }

        DoesThePlantBloom = true;
    }
}
