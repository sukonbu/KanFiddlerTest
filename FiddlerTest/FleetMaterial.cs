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

        private double ammunition;
        private double steel;
        private double bauxite;

        public FleetMaterial(double fuel, double ammunition, double steel, double bauxite)
        {
            this.Fuel = fuel;
            this.ammunition = ammunition;
            this.steel = steel;
            this.bauxite = bauxite;
        }

        public void setFleetMaterial(double fuel, double ammunition, double steel, double bauxite)
        {
            this.Fuel = fuel;
            this.ammunition = ammunition;
            this.steel = steel;
            this.bauxite = bauxite;
        }


        public void setAmmunition(double Ammunition)
        {
            ammunition = Ammunition;
        }

        public void setSteel(double Steel)
        {
            steel = Steel;
        }

        public void setBauxite(double Bauxite)
        {
            bauxite = Bauxite;
        }


        public double getAmmunition()
        {
            return ammunition;
        }

        public double getSteel()
        {
            return steel;
        }

        public double getBauxite()
        {
            return bauxite;
        }
        
        public override string ToString()
        {
            return string.Format("資材  燃料:{0}/弾薬:{1}/鋼材:{2}/ボーキサイト:{3}", Fuel, ammunition, steel, bauxite);
        }
    }
}
