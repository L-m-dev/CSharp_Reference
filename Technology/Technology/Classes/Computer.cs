using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Technology.Classes {
    public class Computer : AbstractEntity {
        public string Brand { get; set; }
        public double Wattage { get; set; }
        public double Power { get; set; }
        public bool IsOn { get; set; } = false;

        public readonly double MAXIMUM_POWER_RATIO;
        public double CurrentPowerRatio { get; }

        public Computer(string brand, double wattage, double power) {
            Brand = brand;
            Wattage = wattage;
            Power = power;
            double cpr = power / wattage;
            CurrentPowerRatio = cpr;

        }
        public void TurnOn() {
            if (!IsOn) {
                this.IsOn = true;
            }
        }
        public void TurnOff() {
            if (IsOn) {
                this.IsOn = false;
            }
        }

        public double AddPower(double power) {
            double cpr = (this.Power + power) / this.Wattage;
            if (cpr > MAXIMUM_POWER_RATIO) {
                throw new ArgumentException($"Should not exceed maximum Power Ratio. Provided: {cpr}, Maximum allowed: {MAXIMUM_POWER_RATIO} ");
            }
            this.Power += power;
            return this.Power;
        }
    }
}
