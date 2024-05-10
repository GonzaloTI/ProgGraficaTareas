using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas.animacion
{
    public class Acciones
    {
        public String nombre;
        public Punto pos;
        public float angulo;

        public Acciones( String nombre) {
            this.nombre = nombre;
            this.pos = new Punto(0,0,0);
            this.angulo = 0.0f;
        }
        public Acciones() { }
        public Acciones(String nombre,Punto pos , float a)
        {
            this.nombre = nombre;
            this.pos = new Punto(pos.x, pos.y, pos.z);
            this.angulo = a;
        }

    }
}
