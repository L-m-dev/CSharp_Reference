using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technology.Classes {
    public class Smartphone : Computer {
        public readonly double _size;
        public bool IsOnBattery { get; set; }

        public Smartphone(string brand, double wattage, double power, double size) : base(brand, wattage, power) {
            this._size = size;
            IsOnBattery = false;
        }

        public bool IsCompact(double pocketSize) {
            if (this._size > pocketSize) {
                return false;
            }
            return true;
        }
    }
}
