using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    internal class Objeto1
    {
        Dictionary<string, Parte> vertices = new Dictionary<string, Parte>();

        Shader shader;

        String name;

        public Objeto1(Shader shader, String nombre)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Parte>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public Objeto1(Shader shader, String nombre, Dictionary<string, Parte> listparte)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Parte>();

            foreach (KeyValuePair<string, Parte> k in listparte)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }



        public void add(string key, Parte parte)
        {
            vertices.Add(key, parte);
        }
        public void ArrayverticeTostring()
        {

            String listcaras = "";
            foreach (KeyValuePair<string, Parte> k in vertices)
            {
                listcaras += k.Value.ArrayverticeTostring() + " ";

            }
            Console.WriteLine(listcaras);

        }




        public void dibujar()
        {

            foreach (KeyValuePair<string, Parte> k in vertices)
            {
                k.Value.dibujar();
            }

        }

    }
}