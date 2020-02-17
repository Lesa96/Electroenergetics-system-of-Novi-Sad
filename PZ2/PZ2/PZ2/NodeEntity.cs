using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ2
{
    public class NodeEntity
    {
        private string id;
        private string name;
        private double x;
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }


        public double X
        {
            get { return x; }
            set { x = value; }
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
