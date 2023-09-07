using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speedometer.Models
{
    /// <summary>
    /// Класс, описывающий автомобильный спидометр
    /// </summary>
    public class CarSpeedometer
    {
        /// <summary>
        /// Минимальная скорость
        /// </summary>
        public double MinSpeed;

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public double MaxSpeed;

        /// <summary>
        /// Текущая скорость
        /// </summary>
        public double CurrentSpeed;

        public CarSpeedometer(int minSpeed, int currentSpeed, int maxSpeed)
        {
            MinSpeed = minSpeed;
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;
        }
    }
}
