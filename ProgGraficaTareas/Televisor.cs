using OpenTK.Mathematics;
using System;

using OpenTK.Graphics.OpenGL4;

namespace ProgGraficaTareas
{
 
    public class Televisor
    {  
        
        Shader shader;
       
        private Vector3 origen;

        private float[] vertices;

      
        public Televisor(Shader shader, Vector3 origen)
        {
            this.shader = shader;
            this.origen = origen;

            // Definir los vértices en función del origen
            vertices = new float[] {
                // Coordenadas X, Y, Z relativas al origen
                -0.15f + origen.X, -0.20f + origen.Y, 0f + origen.Z,  // Primer punto (base)
                0.15f + origen.X, -0.20f + origen.Y, 0f + origen.Z,
                -0.15f + origen.X, -0.20f + origen.Y, -0.3f + origen.Z,
                0.15f + origen.X, -0.20f + origen.Y, -0.3f + origen.Z,

                -0.17f + origen.X, 0.10f + origen.Y, -0.10f + origen.Z,  // Primer punto (cuerpo)
                0.17f + origen.X, 0.10f + origen.Y, -0.10f + origen.Z,
                -0.17f + origen.X, -0.10f + origen.Y, -0.10f + origen.Z,
                0.17f + origen.X, -0.10f + origen.Y, -0.10f + origen.Z,

                -0.17f + origen.X, 0.10f + origen.Y, -0.25f + origen.Z,  // Primer punto (cuerpo detras)
                0.17f + origen.X, 0.10f + origen.Y, -0.25f + origen.Z,
                -0.17f + origen.X, -0.10f + origen.Y, -0.25f + origen.Z,
                0.17f + origen.X, -0.10f + origen.Y, -0.25f + origen.Z,

                -0.02f + origen.X, -0.20f + origen.Y, -0.10f + origen.Z,  // Primer punto (tronco)
                0.02f + origen.X, -0.20f + origen.Y, -0.10f + origen.Z,
                -0.02f + origen.X, -0.20f + origen.Y, -0.25f + origen.Z,
                0.02f + origen.X, -0.20f + origen.Y, -0.25f + origen.Z,

                -0.02f + origen.X, -0.10f + origen.Y, -0.10f + origen.Z,  // Primer punto (tronco arriba)
                0.02f + origen.X, -0.10f + origen.Y, -0.10f + origen.Z,
                -0.02f + origen.X, -0.10f + origen.Y, -0.25f + origen.Z,
                0.02f + origen.X, -0.10f + origen.Y, -0.25f + origen.Z
            };
        }

        void dibujarBase()
        {
            uint[] index = { 0, 1, 2, 3 , 0, 2, 1, 3};
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            //GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
            // Dibuja las líneas utilizando el modo GL_LINES
            GL.DrawElements(PrimitiveType.Lines, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarBaseTV()
        {                   //delante                  //detras                    //union
            uint[] index = { 4, 5, 6, 7 , 4, 6, 5, 7 , 8, 9, 10, 11, 8, 10, 9, 11 , 4, 8 , 5, 9, 6, 10, 7, 11 };
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            //GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
            // Dibuja las líneas utilizando el modo GL_LINES
            GL.DrawElements(PrimitiveType.Lines, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarTronco()
        {                   //abajo                           // arriba                         // union 
            uint[] index = { 12, 13 , 14, 15, 12, 14, 13, 15,  16, 17, 18, 19, 16, 18, 17, 19, 12, 16, 13, 17, 14, 18 , 15, 19  };
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            //GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
            // Dibuja las líneas utilizando el modo GL_LINES
            GL.DrawElements(PrimitiveType.Lines, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void dibujar()
        {
            shader.Use();

           

         
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            dibujarBase();
            dibujarBaseTV();
            dibujarTronco();
        }



    }
}
