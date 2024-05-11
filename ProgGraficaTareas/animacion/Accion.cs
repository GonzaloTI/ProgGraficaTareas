using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas.animacion
{
    public class Accion
    {
        public String nombre { get; set; }
        public String transformacion { get; set; }
        public Punto pos { get; set; }
        public float angulo { get; set; }

        public Accion(String nombre)
        {
            this.nombre = nombre;
            this.pos = new Punto(0, 0, 0);
            this.angulo = 0.0f;
        }
        public Accion() { }
        public Accion(String nombre, String trans, float a, Punto pos)
        {
            this.nombre = nombre;
            this.transformacion = trans;
            this.pos = new Punto(pos.x, pos.y, pos.z);
            this.angulo = a;
        }
    }
}
