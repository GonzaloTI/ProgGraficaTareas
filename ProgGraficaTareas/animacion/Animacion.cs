using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas.animacion
{
    internal class Animacion
    {
        public String nombre { get; set; }
        public float time { get; set; }
        public float speed { get; set; }
        public Dictionary<String, Accion> acciones { get; set; }

        public Animacion(String nomb, float time, float speed)
        {
            this.nombre = nomb;
            this.time = time;
            this.speed = speed;
            this.acciones = new Dictionary<string, Accion>();
        }

        public void add(String key, Accion accion)
        {
            this.acciones.Add(key, accion);
        }

        public void Animar(Escena escena)
        {
            //escena.trasladar(0.001f, 0.001f, 0.001f);
            escena.rotar(5, 1.0f, 0.0f, 0.0f);
        }
        public void AnimarByInstru(Escena escena, Accion action)
        {
            String trans = action.transformacion.ToString();
            switch (trans)
            {
                case "trasladar":
                    escena.trasladar(action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction trasladar");
                    break;
                case "rotar":
                    escena.rotar(action.angulo, action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction rotar");
                    break;
                case "escalar":
                    escena.escalar(action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction escalar");
                    break;
                default:
                    Console.WriteLine("default");
                    break;

            }


        }
        public void AnimarByInstru2(Parte escena, Accion action)
        {
            String trans = action.transformacion.ToString();
            switch (trans)
            {
                case "trasladar":
                    escena.trasladar(action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction trasladar");
                    break;
                case "rotar":
                    escena.rotar(action.angulo, action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction rotar");
                    break;
                case "escalar":
                    escena.escalar(action.pos.x, action.pos.y, action.pos.z);
                    Console.WriteLine("acction escalar");
                    break;
                default:
                    Console.WriteLine("default");
                    break;

            }


        }
    }
}
