using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    internal class Escena
    {
        Dictionary<string, Objeto1> vertices = new Dictionary<string, Objeto1>();

        Shader shader;

        String name;

        public Escena(Shader shader, String nombre)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Objeto1>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public Escena(Shader shader, String nombre, Dictionary<string, Objeto1> listparte)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Objeto1>();

            foreach (KeyValuePair<string, Objeto1> k in listparte)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }



        public void add(string key, Objeto1 parte)
        {
            vertices.Add(key, parte);
        }
        public void ArrayverticeTostring()
        {
            foreach (KeyValuePair<string, Objeto1> k in vertices)
            {
                k.Value.ArrayverticeTostring();

            }
            Console.WriteLine("Lista de objetos-partes-caras");

        }




        public void dibujar()
        {

            foreach (KeyValuePair<string, Objeto1> k in vertices)
            {
                k.Value.dibujar();
            }

        }

    }
}