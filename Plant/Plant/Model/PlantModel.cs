namespace Plant.Model
{
    internal class PlantModel
    {
        /// <summary>
        /// Название растения
        /// </summary>
        public string Title;

        /// <summary>
        /// Семейство растения
        /// </summary>
        /// <returns></returns>
        public string Family;

        /// <summary>
        /// Высота растения
        /// </summary>
        public double HeightThreshold;

        /// <summary>
        /// Цветет ли растение
        /// </summary>
        public bool DoesThePlantBloom;

        /// <summary>
        /// Текущая высота растения
        /// </summary>
        public double CurrentHeight;

        public PlantModel(string title, string family, double heightThreshold)
        {
            DoesThePlantBloom = false;
            Title = title;
            Family = family;
            HeightThreshold = heightThreshold;
            CurrentHeight = 0;
        }
    }
}
