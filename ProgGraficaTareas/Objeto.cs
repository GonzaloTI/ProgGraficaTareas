using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    public class Objeto
    {


        public Shader shader;

        public String name { get; set; }

        public Punto origen { get; set; }

        public Dictionary<string, Parte> vertices { get; set; }
        public Objeto(Shader shader, String nombre, Punto origen)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Parte>();// cuando se crea el objeto solo con el chaser y el nombre
            this.origen = origen;
        }

        public Objeto(Shader shader, String nombre, Dictionary<string, Parte> listparte)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Parte>();

            foreach (KeyValuePair<string, Parte> k in listparte)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }

        public Objeto()
        {
            this.name = "";
            this.vertices = new Dictionary<string, Parte>();// cuando se crea el objeto solo con el chaser y el nombre
        }
        public String getName()
        {
            return this.name;
        }
        public void setShader(Shader shader)
        {
            this.shader = shader;
            foreach (KeyValuePair<string, Parte> k in this.vertices)
            {
                k.Value.setShader(shader);
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




        public void dibujar(Punto origen)
        {
            Punto origennew = origen.sum(origen ,this.origen);

            foreach (KeyValuePair<string, Parte> k in vertices)
            {
                k.Value.dibujar(origennew);
            }

        }

    }
}