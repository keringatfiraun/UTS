using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Pertemuan1
{
    static class Constants
    {
        public const string path = "../../../Shaders/";
    }
    public class Window : GameWindow
    {
        Asset3d[] _object3d = new Asset3d[16];
        double _time;
        float degr = 0;
        Camera _camera;
        List<Asset3d> _objectGerobak = new List<Asset3d>();
        bool _firstMove = true;
        Vector2 _lastPost;
        List<Asset3d> _objectMobil2 = new List<Asset3d>();



        // A simple constructor to let us set properties like window size, title, FPS, etc. on the window.
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            //ganti background warna
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            //GEROBAK
            //badan gerobak
            _object3d[0] = new Asset3d(new Vector3(1f, 0.9f, 0));
            _object3d[0].createBoxVertices(0.0f, 0.0f, 0.0f, 0.5f);
            _objectGerobak.Add(_object3d[0]);

            _object3d[1] = new Asset3d(new Vector3(1f, 0.9f, 0));
            _object3d[1].createBoxVertices(-0.5f, 0.0f, 0.0f, 0.5f);
            _objectGerobak.Add(_object3d[1]);
            
            //objek dalam gerobak 1
            _object3d[2] = new Asset3d(new Vector3(0f, 0.8f, 0.9f));
            _object3d[2].createEllipsoid2(0.0f, 0.5f, 0.0f, 0.3f, 0.3f, 0.3f, 72, 24);
            _objectGerobak.Add(_object3d[2]);
            
            //objek dalam gerobak 2
            _object3d[3] = new Asset3d(new Vector3(0.3f, 1f, 0.9f));
            _object3d[3].createTorus(-0.5f, 0.3f, 0.0f, 0.2f, 0.08f, 72, 24);
            _objectGerobak.Add(_object3d[3]);
            
            //tongkat
            _object3d[4] = new Asset3d(new Vector3(1f, 0.9f, 0f));
            _object3d[4].createEllipsoid2(0.5f, 0.1f, 0.15f, 0.5f, 0.01f, 0.02f, 72, 24);
            _objectGerobak.Add(_object3d[4]);

            _object3d[5] = new Asset3d(new Vector3(1f, 0.9f, 0));
            _object3d[5].createEllipsoid2(0.5f, 0.1f, -0.15f, 0.5f, 0.01f, 0.02f, 72, 24);
            _objectGerobak.Add(_object3d[5]);

            //roda
            _object3d[6] = new Asset3d();
            _object3d[6].createEllipsoid2(0.0f, -0.2f, 0.27f, 0.1f, 0.1f, 0.05f, 72, 24);
            _objectGerobak.Add(_object3d[6]);
          
            _object3d[7] = new Asset3d();
            _object3d[7].createEllipsoid2(-0.5f, -0.2f, 0.27f, 0.1f, 0.1f, 0.05f, 72, 24);
            _objectGerobak.Add(_object3d[7]);

            _object3d[8] = new Asset3d(new Vector3());
            _object3d[8].createEllipsoid2(0.0f, -0.2f, -0.27f, 0.1f, 0.1f, 0.05f, 72, 24);
            _objectGerobak.Add(_object3d[8]);

            _object3d[9] = new Asset3d();
            _object3d[9].createEllipsoid2(-0.5f, -0.2f, -0.27f, 0.1f, 0.1f, 0.05f, 72, 24);
            _objectGerobak.Add(_object3d[9]);

            // mobil-2
            // trapesium
            _object3d[10] = new Asset3d();
            _object3d[10].createBoxVertices2(1.5f, 0.0f, 0, 0.5f);
            _objectMobil2.Add(_object3d[10]);

            _object3d[11] = new Asset3d();
            _object3d[11].createBoxVertices(1.5f, 0.0f, 0.5f, 0.5f);
            _objectMobil2.Add(_object3d[11]);

            // roda
            _object3d[12] = new Asset3d(new Vector3(1f, 0.6f, 1f));
            _object3d[12].createTorus2(-0.2f, 1.2f, 0.0f, 0.15f, 0.065f, 76, 28);
            _objectMobil2.Add(_object3d[12]);

            _object3d[13] = new Asset3d(new Vector3(1f, 0.6f, 1f));
            _object3d[13].createTorus2(-0.2f, 1.8f, 0.0f, 0.15f, 0.065f, 76, 28);
            _objectMobil2.Add(_object3d[13]);

            _object3d[14] = new Asset3d(new Vector3(1f, 0.6f, 1f));
            _object3d[14].createTorus2(-0.2f, 1.2f, 0.6f, 0.15f, 0.065f, 76, 28);
            _objectMobil2.Add(_object3d[14]);

            _object3d[15] = new Asset3d(new Vector3(1f, 0.6f, 1f));
            _object3d[15].createTorus2(-0.2f, 1.8f, 0.6f, 0.15f, 0.065f, 76, 28);
            _objectMobil2.Add(_object3d[15]);


            _camera = new Camera(new Vector3(0, 0, 2), Size.X / Size.Y);
            CursorGrabbed = true;

            foreach (Asset3d i in _objectGerobak)
            {
                i.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }

            foreach (Asset3d i in _objectMobil2)
            {
                i.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Matrix4 temp = Matrix4.Identity;
            
            _object3d[0].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[1].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[2].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[3].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[4].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[5].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[6].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[7].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[8].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[9].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[10].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[11].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[12].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[13].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[14].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());
            _object3d[15].render(0, temp, _camera.GetViewMatrix(), _camera.GetProjectionMatrix());


            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            Console.WriteLine("Ini Resize");
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        // This function runs on every update frame.
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;
            var mouse_input = MouseState;
            var mouse = MouseState;
            var sensitivity = 0.1f;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            float cameraSpeed = 0.5f;
            if(KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                foreach (Asset3d i in _objectGerobak)
                {
                    i.translate(0.005f, 0.0f, 0.0f);
                }
                _object3d[6].rotate(_object3d[6].objectCenter, Vector3.UnitZ, 15);
                _object3d[7].rotate(_object3d[7].objectCenter, Vector3.UnitZ, 15);
                _object3d[8].rotate(_object3d[8].objectCenter, Vector3.UnitZ, 15);
                _object3d[9].rotate(_object3d[9].objectCenter, Vector3.UnitZ, 15);
            }
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                foreach (Asset3d i in _objectGerobak)
                {
                    i.translate(-0.005f, 0.0f, 0.0f);
                }
                _object3d[6].rotate(_object3d[6].objectCenter, -Vector3.UnitZ, 15);
                _object3d[7].rotate(_object3d[7].objectCenter, -Vector3.UnitZ, 15);
                _object3d[8].rotate(_object3d[8].objectCenter, -Vector3.UnitZ, 15);
                _object3d[9].rotate(_object3d[9].objectCenter, -Vector3.UnitZ, 15);
            }

            if(KeyboardState.IsKeyDown(Keys.Up))
            {
                foreach (Asset3d i in _objectMobil2)
                {
                    i.translate(0.0f, 0.0f, -0.005f);
                }

            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                foreach (Asset3d i in _objectMobil2)
                {
                    i.translate(0.0f, 0.0f, 0.005f);
                }

            }

            if (_firstMove)
            {
                _lastPost = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;

            }
            else
            {
                var deltaX = mouse.X - _lastPost.X;
                var deltaY = mouse.Y - _lastPost.Y;
                _lastPost = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X/2)/(Size.X/2);
                float _y = -(MousePosition.Y - Size.Y/2)/(Size.Y/2);

                Console.WriteLine("x = " + _x + " y = " + _y);
                //_object[3].updateMousePosisition(_x, _y);
            }
        }
    }
}
