using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ2
{
    public class LineEntity
    {
        private string id;
        private string name;
        private bool isUnderground;
        private double r;
        private string conductorMaterial;
        private string lineType;
        private double thermalConstantHeat;
        private ulong firstEnd;
        private ulong secondEnd;
        private List<MyPoint> points;

        public double Distance { get; set; }

        public List<MyPoint> Points
        {
            get { return points; }
            set { points = value; }
        }


        public ulong SecondEnd
        {
            get { return secondEnd; }
            set { secondEnd = value; }
        }


        public ulong FirstEnd
        {
            get { return firstEnd; }
            set { firstEnd = value; }
        }


        public double ThermalConstantHeat
        {
            get { return thermalConstantHeat; }
            set { thermalConstantHeat = value; }
        }


        public string LineType
        {
            get { return lineType; }
            set { lineType = value; }
        }


        public string ConductorMaterial
        {
            get { return conductorMaterial; }
            set { conductorMaterial = value; }
        }


        public double R
        {
            get { return r; }
            set { r = value; }
        }


        public bool IsUnderground
        {
            get { return isUnderground; }
            set { isUnderground = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string ID
        {
            get { return id; }
            set { id = value; }
        }

    }
}
