using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technology.Classes {
    public class Laptop : Computer {

        public double Weight { get; set; }
        public bool IsOnBattery { get; set; }

        public Laptop(string brand, double power, double wattage, double weight) : base(brand, wattage, power) {

            Weight = weight;
            IsOnBattery = false;
        }

        public void ConnectToPowerSocket() {
            if (!IsOnBattery) {
                IsOnBattery = true;
            }
        }

        public bool IsClunky() {
            return this.Weight > 6 ? true : false;
        }
    }
}
