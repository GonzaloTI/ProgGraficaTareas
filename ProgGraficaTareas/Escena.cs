using OpenTK.Mathematics;
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

        public Matrix4 model = Matrix4.Identity;

        public Matrix4 rot = Matrix4.Identity;

        public Dictionary<string, Objeto> vertices { get; set; }
        public Escena(Shader shader, String nombre, Punto origen)
        {
         //   this.shader = shader;
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
                k.Value.dibujar(this.origen, rot * Matrix4.CreateTranslation(this.origen.x, 0.5f, this.origen.z) * this.model);
            }

        }
        public void escalar(float x, float y, float z)
        {
            foreach (KeyValuePair<string, Objeto> k in vertices)
            {
                k.Value.escalar(x, y, z);
            }
        }
        public void trasladar(float x, float y, float z)
        {
            foreach (KeyValuePair<string, Objeto> k in vertices)
            {
                k.Value.trasladar(x, y, z);
            }
        }
        public void rotartodos(float a, float x, float y, float z)
        {
            foreach (KeyValuePair<string, Objeto> k in vertices)
            {
                k.Value.rotar(a, x, y, z);
            }
        }
        public void rotar2(float a, float x, float y, float z)
        {
            this.rot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(60f)) * Matrix4.CreateTranslation(0.0f, 1.0f, 0.0f); 
        //    this.model = rot;
        }  
    }
}