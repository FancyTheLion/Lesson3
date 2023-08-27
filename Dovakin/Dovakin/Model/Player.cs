namespace Dovakin.Model
{
    /// <summary>
    /// Класс, описывающий игрока
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Имя героя
        /// </summary>
        public string Name;

        /// <summary>
        /// Жив ли
        /// </summary>
        public bool IsAlive;

        /// <summary>
        /// Мах здоровье
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// Текущее здоровье
        /// </summary>
        public int CurrentHealth;

        public Player(string name, int maxHealth)
        {
            Name = name;
            IsAlive = true;
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }
    }
}
