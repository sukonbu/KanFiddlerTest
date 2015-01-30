using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
    class FleetMaterial
    {
		//private double fuel;

		//public double Fuel
		//{
		//	get { return fuel; }
		//	set { fuel = value; }
		//}

		public double Fuel { get; private set; }

        public double Ammunition { get; private set; }
		public double Steel	{ get; private set;	}
		public double Bauxite { get; private set; }

        public FleetMaterial(double fuel, double ammunition, double steel, double bauxite)
        {
            this.Fuel = fuel;
            this.Ammunition = ammunition;
            this.Steel = steel;
            this.Bauxite = bauxite;
        }

        public void setFleetMaterial(double fuel, double ammunition, double steel, double bauxite)
        {
            this.Fuel = fuel;
            this.Ammunition = ammunition;
            this.Steel = steel;
            this.Bauxite = bauxite;
        }
        
        public override string ToString()
        {
            return string.Format("資材  燃料:{0}/弾薬:{1}/鋼材:{2}/ボーキサイト:{3}", Fuel, Ammunition, Steel, Bauxite);
        }
    }
}
