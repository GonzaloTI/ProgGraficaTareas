using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas.animacion
{
    internal class Animacion
    {

        public float time { get; set; }
        public float speed { get; set; }
        public Dictionary<String, Acciones> acciones { get; set; }

        public Animacion(float t ,float s) { 
            this.time = t;
            this.speed = s;
        }

        public void Animar(Escena escena) {
            escena.trasladar(0.001f, 0.001f, 0.001f);
        }

    }
}
