using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls;

namespace SnakeEllipse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            snake[0] = new Ellipse();
            snake[0].Width = 30;
            snake[0].Height = 30;
            Canvas.SetTop(snake[0], 90);
            Canvas.SetLeft(snake[0], 90);
            snake[0].Fill = Brushes.DodgerBlue;
            CanvasMap.Children.Add(snake[0]);
            this.KeyDown += new KeyEventHandler(OKP);
            timer.Interval = new TimeSpan(0, 0, 0, 0, milsec);
            timer.Tick += new EventHandler(Direction);
            timer.Tick += new EventHandler(moveSnake);
            timer.Start();
            snake[count] = new Ellipse();
        }

        Random random = new Random();
        Ellipse[] snake = new Ellipse[400];
        Ellipse fruit = new Ellipse();
        DispatcherTimer timer = new DispatcherTimer();
        

        int randomXFood;
        int randomYFood;
        int dirX = 1;
        int dirY = 0;
        int count = 1;
        int milsec = 500;


        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Right":
                    if (dirX == -1 & count > 1)
                    {
                        dirX = -1;
                        dirY = 0;
                    }
                    else
                    {
                        dirX = 1;
                        dirY = 0;
                    }
                    
                    break;
                case "Left":
                    if (dirX == 1 & count > 1)
                    {
                        dirX = 1;
                        dirY = 0;
                    }
                    else
                    {
                        dirX = -1;
                        dirY = 0;
                    }
                    
                    
                    break;
                case "Up":
                    if (dirY == 1 & count > 1)
                    {
                        dirX = 0;
                        dirY = 1;
                    }
                    else
                    {
                        dirX = 0;
                        dirY = -1;
                    }
                    
                    break;
                case "Down":
                    if (dirY == -1 & count > 1)
                    {
                        dirX = 0;
                        dirY = -1;
                    }
                    else
                    {
                        dirX = 0;
                        dirY = 1;
                    }
                    
                    break;
            }
        }


        private void Direction(object Sender, EventArgs e)
        {


            for (int i = count; i >= 1; i--)
            {
                double ysnake = Canvas.GetTop(snake[i - 1]);
                double xsnake = Canvas.GetLeft(snake[i - 1]);

                Canvas.SetTop(snake[i], ysnake);
                Canvas.SetLeft(snake[i], xsnake);

            }

            double x = Canvas.GetLeft(snake[0]);
            double y = Canvas.GetTop(snake[0]);

            Canvas.SetTop(snake[0], y + 30 * dirY);
            Canvas.SetLeft(snake[0], x + 30 * dirX);
        }

        private void foodAppearance()
        {
            CanvasMap.Children.Remove(fruit);
            fruit = new Ellipse();
            fruit.Width = 30;
            fruit.Height = 30;
            fruit.Fill = Brushes.Red;
            bool frt = false;
            while (!frt)
            {
                frt = true;
                randomXFood = random.Next(20);

                randomXFood *= 30;
                randomYFood = random.Next(13);

                randomYFood *= 30;

                for (int i = count; i >= 0; i--)
                {
                    double ysnake = Canvas.GetTop(snake[i]);
                    double xsnake = Canvas.GetLeft(snake[i]);
                    if (ysnake == randomYFood & xsnake == randomXFood)
                    {
                        frt = false;
                        break;
                    }
                }
            }

            Canvas.SetTop(fruit, randomYFood);
            Canvas.SetLeft(fruit, randomXFood);

            CanvasMap.Children.Add(fruit);

        }

        private void CanvasMap_Loaded(object sender, RoutedEventArgs e)
        {
            foodAppearance();
        }

        private void AddSnakes()
        {
            double x = Canvas.GetLeft(snake[count - 1]);
            Canvas.SetLeft(snake[count], x - 30 * dirX);
            double y = Canvas.GetTop(snake[count - 1]);
            Canvas.SetTop(snake[count], y - 30 * dirY);
            snake[count].Width = 30;
            snake[count].Height = 30;

            snake[count].Fill = Brushes.Blue;
            CanvasMap.Children.Add(snake[count]);
            count++;
            snake[count] = new Ellipse();
        }

        private void moveSnake(object Sender, EventArgs e)
        {

            double xsnake = Canvas.GetLeft(snake[0]);
            double ysnake = Canvas.GetTop(snake[0]);
            double xfruit = Canvas.GetLeft(fruit);
            double yfruit = Canvas.GetTop(fruit);

            if (xsnake == 600)
            {
                Canvas.SetLeft(snake[0], 0);
            }

            if (xsnake == -30)
            {
                Canvas.SetLeft(snake[0], 570);
            }

            if (ysnake == 390)
            {
                Canvas.SetTop(snake[0], 0);
            }

            if (ysnake == -30)
            {
                Canvas.SetTop(snake[0], 360);
            }

            if (xsnake == xfruit & ysnake == yfruit)
            {
                foodAppearance();
                AddSnakes();
                milsec -= 100;
                return;
            }

            for (int i = count; i >= 1; i--)
            {
                double ysnakei = Canvas.GetTop(snake[i]);
                double xsnakei = Canvas.GetLeft(snake[i]);
                double ysnake0 = Canvas.GetTop(snake[0]);
                double xsnake0 = Canvas.GetLeft(snake[0]);
                {
                    if (ysnakei == ysnake0 & xsnakei == xsnake0)
                    {
                        
                        for (int j = count; j >= 1; j--)
                        {
                            CanvasMap.Children.Remove(snake[j]);
                            milsec = 500;
                        }
                        count = 1;

                    }
                }

            }


            return;
        }

        

        
    }
}
