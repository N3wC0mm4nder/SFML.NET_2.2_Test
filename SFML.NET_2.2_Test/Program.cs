using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace SFML.NET_2._2_Test
{
    class Program
    {
        static ContextSettings context = new ContextSettings();
        static RenderWindow window;
        static RectangleShape rec_bg = new RectangleShape(new System.Vector2f(window.Size.X, window.Size.Y));
        static Texture bg = new Texture("../../holz.jpg");
        static Texture mouse_tex = new Texture("../../resources/cursor.png");
        static Texture stein_tex = new Texture("../../resources/stein.png");
        static Sprite mouse_sprite, stein_sprite;
        static Text text = new Text("Das ist ein Text", new Font("../../resources/Bruss___.ttf"));
        static Text fps = new Text("0 FPS", new Font("../../resources/Arial.ttf"));
        static RectangleShape rec = new RectangleShape(new System.Vector2f(100.0f, 100.0f));
        static CircleShape circle = new CircleShape(50.0f);
        static DateTime startTime;

        static void Main(string[] args)
        {
            bg.Repeated = true;
            context.AntialiasingLevel = 16;
            window = new RenderWindow(new VideoMode(1024, 768), "SFML Window", Styles.Default, context);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(60);
            rec_bg.Texture = bg;
            rec_bg.Draw(window, RenderStates.Default);

            rec.Position = new System.Vector2f(50.0f, 50.0f);
            rec.FillColor = Color.Green;
            stein_sprite = new Sprite(stein_tex);
            stein_sprite.Position = new System.Vector2f(500.0f, 500.0f);
            circle.Position = new System.Vector2f(300.0f, 300.0f);

            window.Closed += window_Closed;
            window.KeyPressed += window_KeyPressed;
            window.MouseMoved += window_MouseMoved;
            window.MouseButtonReleased += window_MouseButtonReleased;
            window.SetActive(true);
            window.SetMouseCursorVisible(false);
            mouse_sprite = new Sprite(mouse_tex);
            while(window.IsOpen)
            {
                fps.DisplayedString = (1000 / (DateTime.Now.Millisecond - startTime.Millisecond)) + " FPS";
                startTime = DateTime.Now;
                window.DispatchEvents();
                window.Clear();
                rec.Draw(window, RenderStates.Default);
                circle.Draw(window, RenderStates.Default);
                stein_sprite.Draw(window, RenderStates.Default);
                mouse_sprite.Draw(window, RenderStates.Default);
                text.Draw(window, RenderStates.Default);
                fps.Draw(window, RenderStates.Default);
                window.Display();
            }
        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if(e.Button == Mouse.Button.Left)
            {
                mouse_sprite.Scale = new System.Vector2f(mouse_sprite.Scale.X * 6.5f, mouse_sprite.Scale.Y * 6.5f);
            }
            else if(e.Button == Mouse.Button.Right)
            {
                mouse_sprite.Scale = new System.Vector2f(mouse_sprite.Scale.X * 0.5f, mouse_sprite.Scale.Y * 0.5f);
            }
            text.Position = new System.Vector2f(mouse_sprite.Position.X + mouse_sprite.GetGlobalBounds().Width, mouse_sprite.Position.Y);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            Console.Clear();
            Console.WriteLine(e.X + "x" + e.Y);
            mouse_sprite.Position = new System.Vector2f(e.X, e.Y);
            text.Position = new System.Vector2f(mouse_sprite.Position.X + mouse_sprite.GetGlobalBounds().Width, mouse_sprite.Position.Y);
        }

        static void window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch(e.Code)
            {
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                case Keyboard.Key.F12:
                    window.Capture().SaveToFile("../../screenshots/screenshot_1.png");
                    break;
            }
        }

        static void window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
        //Hallo
    }
}
