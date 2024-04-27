using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    public class Escena
    {


        public Shader shader;

        public String name { get; set; }

        public Punto origen { get; set; }

        public Dictionary<string, Objeto> vertices { get; set; }
        public Escena(Shader shader, String nombre, Punto origen)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Objeto>();// cuando se crea el objeto solo con el chaser y el nombre
            this.origen = origen;
        }

        public Escena()
        {
            this.name = "";
            this.vertices = new Dictionary<string, Objeto>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public void setShader(Shader shader) { 
        this.shader= shader;
            foreach (KeyValuePair<string, Objeto> k in this.vertices)
            {
                k.Value.setShader(shader);
            }
        }

        public Escena(Shader shader, String nombre, Dictionary<string, Objeto> listparte)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Objeto>();

            foreach (KeyValuePair<string, Objeto> k in listparte)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }
        public String getName() { 
        return this.name;
        }


        public void add(string key, Objeto parte)
        {
            vertices.Add(key, parte);
        }
        public void ArrayverticeTostring()
        {
            foreach (KeyValuePair<string, Objeto> k in vertices)
            {
                k.Value.ArrayverticeTostring();

            }
            Console.WriteLine("Lista de objetos-partes-caras");

        }




        public void dibujar()
        {

            foreach (KeyValuePair<string, Objeto> k in vertices)
            {
                k.Value.dibujar(this.origen);
            }

        }

    }
}