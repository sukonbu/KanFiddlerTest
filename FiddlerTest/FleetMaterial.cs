using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
    class FleetMaterial
    {
		public Material NowMaterial = new Material();
		public Material BeforeMaterial = new Material();

		public override string ToString()
		{
			return string.Format("NowMaterial  燃料:{0}/弾薬:{1}/鋼材:{2}/ボーキサイト:{3}", NowMaterial.Fuel, NowMaterial.Ammunition, NowMaterial.Steel, NowMaterial.Bauxite);
		}
    }
}
