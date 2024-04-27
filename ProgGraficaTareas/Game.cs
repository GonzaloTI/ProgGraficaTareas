using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework; // Asegúrate de agregar esta referencia
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgGraficaTareas
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        Shader shader;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

        Televisor televisor;

        Escena escena1;

        Escena escenaDeserializada;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private double _time;
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

         
           projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(20.0f), Size.X / (float)Size.Y, 0.1f, 20.0f);
         
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
          // projection = Matrix4.CreateOrthographic(2.0f, 2.0f, 0.1f, 100.0f); // Tamaño de la proyección ortogonal en el plano XY
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(30.0f));


            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            shader.Use();
            var move = Matrix4.Identity;
            move = move * Matrix4.CreateTranslation(0f, 0f, 0f);
           shader.SetMatrix4("origen", move);
            
            //#################

            televisor = new Televisor(shader, new Vector3(0.0f, 0.0f, 0.0f));

            Punto punto1 = new Punto(0.15f, 0.10f, -0.10f);

            Punto origen = new Punto(0.0f, 0.0f, 0.0f);

            Cara baseFlorero = new Cara(shader, "Base Florero",origen);
            baseFlorero.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseFlorero.add("2", new Punto(0.17f, 0.20f, -0.10f));
            baseFlorero.add("3", new Punto(0.17f, 0.25f, -0.10f));
            baseFlorero.add("4", new Punto(0.10f, 0.25f, -0.10f));
            baseFlorero.add("5", new Punto(0.10f, 0.20f, -0.10f));
            baseFlorero.add("6", new Punto(0.13f, 0.10f, -0.10f));

           
            Cara baseFlorero2 = new Cara(shader, "Base Florero 2", origen);
            baseFlorero2.add("1", new Punto(0.15f, 0.10f, -0.13f));
            baseFlorero2.add("2", new Punto(0.17f, 0.20f, -0.13f));
            baseFlorero2.add("3", new Punto(0.17f, 0.25f, -0.13f));
            baseFlorero2.add("4", new Punto(0.10f, 0.25f, -0.13f));
            baseFlorero2.add("5", new Punto(0.10f, 0.20f, -0.13f));
            baseFlorero2.add("6", new Punto(0.13f, 0.10f, -0.13f));

            Cara basesup = new Cara(shader, "Base sup Florero", origen);
            basesup.add("1", new Punto(0.17f, 0.25f, -0.13f));
            basesup.add("2", new Punto(0.10f, 0.25f, -0.13f));
            basesup.add("3", new Punto(0.17f, 0.25f, -0.10f));
            basesup.add("4", new Punto(0.10f, 0.25f, -0.10f));

            Cara baseinf = new Cara(shader, "Base inf Florero", origen);
            baseinf.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseinf.add("2", new Punto(0.13f, 0.10f, -0.10f));
            baseinf.add("3", new Punto(0.15f, 0.10f, -0.13f));
            baseinf.add("4", new Punto(0.13f, 0.10f, -0.13f));

      

            Cara caraeq = new Cara(shader, "Base equipo", origen); 
            caraeq.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            caraeq.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            caraeq.add("3", new Punto(-0.45f, 0.01f, -0.10f));
            caraeq.add("4", new Punto(-0.55f, 0.01f, -0.10f));


            Cara caraeq2 = new Cara(shader, "Base equipo 2", origen);
            caraeq2.add("1", new Punto(-0.55f, -0.20f, -0.0f));
            caraeq2.add("2", new Punto(-0.45f, -0.20f, -0.0f));
            caraeq2.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            caraeq2.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara carasupp = new Cara(shader, "superior", origen);
            carasupp.add("1", new Punto(-0.55f,  0.01f, -0.10f));
            carasupp.add("2", new Punto(-0.45f, 0.01f, -0.10f));
            carasupp.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            carasupp.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara carainf = new Cara(shader, "inferior", origen);
            carainf.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            carainf.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            carainf.add("4", new Punto(-0.45f, -0.20f, -0.0f));
             carainf.add("3", new Punto(-0.55f, -0.20f, -0.0f));


            Cara caraeqdelante2 = new Cara(shader, "Base equipo", origen);
            caraeqdelante2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            caraeqdelante2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            caraeqdelante2.add("3", new Punto(0.45f, 0.01f, -0.10f));
            caraeqdelante2.add("4", new Punto(0.55f, 0.01f, -0.10f));


            Cara caraeqatras2 = new Cara(shader, "Base equipo 2", origen)  ;
            caraeqatras2.add("1", new Punto(0.55f, -0.20f, -0.0f));
            caraeqatras2.add("2", new Punto(0.45f, -0.20f, -0.0f));
            caraeqatras2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            caraeqatras2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carasupp2 = new Cara(shader, "superior", origen);
            carasupp2.add("1", new Punto(0.55f, 0.01f, -0.10f));
            carasupp2.add("2", new Punto(0.45f, 0.01f, -0.10f));
            carasupp2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            carasupp2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carainf2 = new Cara(shader, "inferior", origen);
            carainf2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            carainf2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            carainf2.add("4", new Punto(0.45f, -0.20f, -0.0f));
            carainf2.add("3", new Punto(0.55f, -0.20f, -0.0f));

            Parte parte1 = new Parte(shader,"parte1", origen);  //parlante 1
            parte1.add("1",carainf2);
            parte1.add("2", carasupp2);
            parte1.add("3", caraeqatras2);
            parte1.add("4", caraeqdelante2);


            Parte parte2 = new Parte(shader, "parte2", origen);  //parlante 2
            parte2.add("1", carasupp);
            parte2.add("2", carainf);
            parte2.add("3", caraeq2);
            parte2.add("4", caraeq);

          
            Parte parte3 = new Parte(shader, "parte3", origen);  //florero
            parte3.add("1", basesup);
            parte3.add("2", baseFlorero2);
            parte3.add("3", baseFlorero);
            parte3.add("4", baseinf);


            Objeto objetonew = new Objeto(shader, "obj1", origen);

            objetonew.add("part1", parte1);
            objetonew.add("part2", parte2);
            objetonew.add("part3", parte3);

            Punto origen2 = new Punto(0.0f, 0.0f, -0.1f);


            this.escena1 = new Escena(shader, "escena1", origen2);

            escena1.add("1", objetonew);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = null,
                WriteIndented = true // Opcional: para una salida con formato legible
            };
            string json = JsonSerializer.Serialize(escena1, options);

            // Imprimir JSON resultante
            Console.WriteLine(json);
            string rutaArchivo = "../../../escena1.json"; 
            // Escribir JSON en el archivo
            File.WriteAllText(rutaArchivo, json);

            //Deserializar objeto
            string rutaArchivoDes = "../../../escena1.json"; // Ruta del archivo JSON

            // Leer el contenido del archivo JSON
            string jsonDes = File.ReadAllText(rutaArchivoDes);

            // Deserializar el JSON a un objeto de tipo Escena
            this.escenaDeserializada = JsonSerializer.Deserialize<Escena>(jsonDes);

            // Ahora puedes trabajar con el objeto deserializado
            Console.WriteLine("Objeto deserializado:"+ escenaDeserializada.getName() + " cara");
            Console.WriteLine(escenaDeserializada);

            this.escenaDeserializada.setShader(shader);

            _camera = new Camera(Vector3.UnitZ, Size.X / (float)Size.Y);
          //

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            _time += 0.0 * args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();


           var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
           shader.SetMatrix4("model", model);
           shader.SetMatrix4("view", _camera.GetViewMatrix());
           shader.SetMatrix4("projection", _camera.GetProjectionMatrix());



            televisor.dibujar();
            // this.objeto1.Dibujar();
            //   this.objeto2.Dibujar();
            // this.objeto3.Dibujar();

             this.escena1.dibujar(); 
          //  this.escenaDeserializada.dibujar();
            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
           

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0); 
            base.OnUnload();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
           
            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.0f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
              //  _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }
    }
}
