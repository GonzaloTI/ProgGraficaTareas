using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Audio.OpenAL;

using System.Text.Json;

namespace ProgGraficaTareas
{
   
    public class Cara
    {
       public Shader shader { get; set; }

       public String name { get; set; }

        public Vector3 origen;
        public Dictionary<string, Punto> vertices { get; set; }

        public Cara(Shader shader, String nombre ,Vector3 origenne) { 
            this.shader = shader;
            this.name = nombre;
            this.origen = origenne;
            this.vertices = new Dictionary<string, Punto>();// cuando se crea el objeto solo con el chaser y el nombre
        }

       /* public Cara(Shader shader, String nombre , Dictionary<string, Punto> listpunto)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Punto>();

            foreach (KeyValuePair<string, Punto> k in listpunto)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }*/

        public String getName()
        {
           return this.name;
        }

        public void add(string key, Punto punto)
        {
            vertices.Add(key, punto);
        }
        public float[] Arrayvertice()
        {
            float[] result = new float[vertices.Count * 3];
            int pos = 0;
            foreach (KeyValuePair<string, Punto> k in vertices)
            {
                result[pos] = k.Value.x+this.origen.X;
                pos++;
                result[pos] = k.Value.y+this.origen.Y;
                pos++;
                result[pos] = k.Value.z+this.origen.Z;
                pos++;
            }
            return result;

        }




        public void dibujar() { 
            float[] array = Arrayvertice();
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }

    }
}
