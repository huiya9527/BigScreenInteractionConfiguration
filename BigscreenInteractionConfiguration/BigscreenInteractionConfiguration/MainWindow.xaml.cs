using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using System.Linq;


namespace BigscreenInteractionConfiguration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const float MOUSE_SENSITIVITY = 3.5f;
        private const float CURSOR_SMOOTHING = 0.2f;

        public MainWindow()
        {
            InitializeComponent();
            initial_value();
        }

        private void MouseSensitivity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MouseSensitivity.IsLoaded)
            {
                txtMouseSensitivity.Text = ((float)MouseSensitivity.Value).ToString("f2");

            }
        }

        private void txtMouseSensitivity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float v;
                if (float.TryParse(txtMouseSensitivity.Text, out v))
                {
                    MouseSensitivity.Value = v;
                }
            }
        }

        private void CursorSmoothing_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CursorSmoothing.IsLoaded)
            {
                txtCursorSmoothing.Text = ((float)CursorSmoothing.Value).ToString("f2");
            }
        }

        private void txtCursorSmoothing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float v;
                if (float.TryParse(txtMouseSensitivity.Text, out v))
                {
                    CursorSmoothing.Value = v;
                }
            }
        }

        private void initial_value()
        {
            XElement xe = XElement.Load("BigScreenInteraction.exe.config");
            var text = xe.Descendants("BigScreenInteraction.Properties.Settings");
            foreach (var a in text)
            {
                var b = a.Descendants("setting");
                foreach (var c in b)
                {
                    if (c.Attribute("name").Value.Equals("CursorSmoothing"))
                    {
                        String value = ""; 
                        foreach (var s in c.Descendants("value"))
                        {
                            value = s.Value;
                        }
                        Console.WriteLine(value);
                        CursorSmoothing.Value = Convert.ToSingle(value); 
                        txtCursorSmoothing.Text = ((float)CursorSmoothing.Value).ToString("f2");
                    }
                    else if (c.Attribute("name").Value.Equals("MouseSensitivity"))
                    {
                        String value = "";
                        foreach (var s in c.Descendants("value"))
                        {
                            value = s.Value;
                        }
                        Console.WriteLine(value);
                        MouseSensitivity.Value = Convert.ToSingle(value);
                        txtCursorSmoothing.Text = ((float)CursorSmoothing.Value).ToString("f2");
                    }
                    else if (c.Attribute("name").Value.Equals("PrimeHand"))
                    {
                        String value = "";
                        foreach (var s in c.Descendants("value"))
                        {
                            value = s.Value;
                        }
                        Console.WriteLine(value);
                        if (value.Equals("True"))
                        {
                            rightHand.IsChecked = true;
                        }
                        else
                        {
                            leftHand.IsChecked = true;
                        }
                        
                    }
                    else if (c.Attribute("name").Value.Equals("MouseClickRegion"))
                    {
                        String value = "";
                        foreach (var s in c.Descendants("value"))
                        {
                            value = s.Value;
                        }
                        Console.WriteLine(value);
                        if (value.Equals("True"))
                        {
                            left_hand_closer_to_body.IsChecked = true;
                        }
                        else
                        {
                            right_hand_closer_to_body.IsChecked = true;
                        }
                    }
                    else if (c.Attribute("name").Value.Equals("MiddleButtonAndWheel"))
                    {
                        String value = "";
                        foreach (var s in c.Descendants("value"))
                        {
                            value = s.Value;
                        }
                        Console.WriteLine(value);
                        if (value.Equals("True"))
                        {
                            two_hand_for_wheel.IsChecked = true;
                        }
                        else
                        {
                            right_hand_for_wheel.IsChecked = true;
                        }
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
        }

        private void write_file(XElement xe, String name, String value)
        {
            var text = xe.Descendants("BigScreenInteraction.Properties.Settings");
            foreach (var a in text)
            {
                var b = a.Descendants("setting");
                foreach (var c in b)
                {
                    if (c.Attribute("name").Value.Equals(name))
                    {
                        var d = c.Descendants("value");
                        foreach (var s in d)
                        {
                            s.SetValue(value);
                        }
                    }
                }
            }
        }

        private void save_button(object sender, RoutedEventArgs e)
        {
            //write file
            XElement xe = XElement.Load("BigScreenInteraction.exe.config");
            write_file(xe, "CursorSmoothing", txtCursorSmoothing.Text);
            write_file(xe, "MouseSensitivity", txtMouseSensitivity.Text);
            write_file(xe, "PrimeHand", rightHand.IsChecked.ToString());
            write_file(xe, "MouseClickRegion", left_hand_closer_to_body.IsChecked.ToString());
            write_file(xe, "MiddleButtonAndWheel", two_hand_for_wheel.IsChecked.ToString());
            xe.Save("BigScreenInteraction.exe.config");
        }

        private void default_button(object sender, RoutedEventArgs e)
        {
            //set default value
            MouseSensitivity.Value = MOUSE_SENSITIVITY;
            CursorSmoothing.Value = CURSOR_SMOOTHING;

            txtMouseSensitivity.Text = ((float)MouseSensitivity.Value).ToString("f2");
            txtCursorSmoothing.Text = ((float)CursorSmoothing.Value).ToString("f2");

            rightHand.IsChecked = true;
            left_hand_closer_to_body.IsChecked = true;
            two_hand_for_wheel.IsChecked = true;
        }
    }
}
