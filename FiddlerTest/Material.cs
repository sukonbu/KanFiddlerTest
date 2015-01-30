using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
	class Material:ICloneable
	{
		//private double fuel;

		//public double Fuel
		//{
		//	get { return fuel; }
		//	set { fuel = value; }
		//}

		public double Fuel { get; set; }
		public double Ammunition { get; set; }
		public double Steel { get; set; }
		public double Bauxite { get; set; }


		public object Clone()
		{
			Material cloneMaterial = new Material();
			cloneMaterial.Fuel = this.Fuel;
			cloneMaterial.Ammunition = this.Ammunition;
			cloneMaterial.Steel = this.Steel;
			cloneMaterial.Bauxite = this.Bauxite;
			return cloneMaterial;
		}
        
        public override string ToString()
        {
            return string.Format("燃料:{0}/弾薬:{1}/鋼材:{2}/ボーキサイト:{3}", Fuel, Ammunition, Steel, Bauxite);
        }

	}
}
