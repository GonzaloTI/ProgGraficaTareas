using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    internal class Punto
    {

        private float _x;
        private float _y;
        private float _z;

        public Punto(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public float x
        {
            get { return _x; }
            set { _x = value; }
        }

        public float y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float z
        {
            get { return _z; }
            set { _z = value; }
        }


        public Punto(Punto p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
        }


    }
}
