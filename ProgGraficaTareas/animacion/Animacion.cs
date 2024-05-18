using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas.animacion
{
    public class Animacion
    {
        public String nombre { get; set; }
        public float time { get; set; }
        public float speed { get; set; }

        public bool run;

        public bool stop;

        public Escena escenaani;
        public Dictionary<String, Accion> acciones { get; set; }

        public Animacion(String nomb, float time, float speed, Escena escenaani)
        {
            this.nombre = nomb;
            this.time = time;
            this.speed = speed;
            this.run = false;
            this.stop = false;
            this.acciones = new Dictionary<string, Accion>();
            this.escenaani = escenaani;
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

        public void IniciarAnimacion( )
        {
            foreach (KeyValuePair<string, Accion> k in acciones)
            {
               AnimarByInstru2(k.Value.parte, k.Value);
                Thread.Sleep(200);
                Console.WriteLine("hilo corriendo desde acctions");
                if (stop) { break; }
            }
        }

        public void IniciarAnimacionlanzamiento(Parte parte , float vel, float angulo, double h, float e)
        {
            int i = 0;
            float t = 0.0f;
            float x = 0.0f;
            float y = 0.0f;
            float xultimo = 0.0f;
            float yultimo = 0.0f;
            double g = 9.8; // m/s^2
            float anguloRadianes = angulo * (float)(Math.PI / 180.0);
            float Vox;
            float Voy;

            float alfa = 0.03f;
            Vox = vel * (float)Math.Cos(anguloRadianes);
            Vox = Math.Abs(Vox);
            Voy = vel * (float)Math.Sin(anguloRadianes);
            Voy = Math.Abs(Voy);

            //tiempo de toca y = 0 
            float tfin = (Voy + (float)Math.Sqrt(Voy * Voy + 2 * (float)g * h)) / (float)g;

            Console.WriteLine("V0x = " + Vox + " " + "V0y = " + Voy + " tfin=" + tfin.ToString());

            Animacion ani = new Animacion("ani0", 200, 2f, escenaani);

                while (run == true && !stop)
                {

                    // Si lanzas una pelota desde una altura h0 = 10m con una velocidad inicial v0 = 20m / s y un ángulo de 45

                    x = (float)Vox * t;  // x(t) = V0x * t
                    y = (float)h + (float)Voy * t - ((float)0.5 * (float)g * t * t);  //y(t)= h * V0y * t -1/2 * g * (t^2)


                  //  Console.WriteLine("x = " + x.ToString() + " y= " + y.ToString() + "  t=" + t.ToString());
                    x = x * alfa + xultimo;
                    y = y * alfa + yultimo;

                //    Console.WriteLine("hilo corriendo");
                parte.trasladarXYZ(x, y, 0.0f);
                //  parte.rotar(20f,0.0f,0.0f,1.0f);
                // parte.rotar(20f, 0.0f, 1.0f, 0.0f);
                parte.rotar(20f, 1.0f, 0.0f, 0.0f);
                    t += 0.02f;
                    Thread.Sleep(10);

                    if (t >= tfin)
                    {
                        xultimo = x;
                        yultimo = y;

                        //velocidad antes del impacto
                        //Voy'
                        Vox = Vox;

                        //Voy' = Voy - g * tfin

                        Voy = Voy - ((float)g * tfin);

                        //impacto

                        //velocidades despues del impacto

                        Vox = Vox; // se mantiene porque no hay friccion

                        Voy = -e * Voy;  // se multiplica el coeficiente de restitucion

                        //nuevo angulo 
                        t = 0.0f;
                        h = 0.0f;
                        tfin = (Voy + (float)Math.Sqrt(Voy * Voy + 2 * (float)g)) / (float)g;
                    //    Console.WriteLine("Vox = " + Vox.ToString() + " Voy= " + Voy.ToString() + "  tfin=" + tfin.ToString());
                      
                    }
                }
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
                //    Console.WriteLine("acction rotar");
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
                 //   Console.WriteLine("acction rotar");
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
