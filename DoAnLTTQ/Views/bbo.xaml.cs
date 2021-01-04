using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace DoAnLTTQ
{
    /// <summary>
    /// Interaction logic for bbo.xaml
    /// </summary>
    public partial class bbo : UserControl
    {
        public static readonly DependencyProperty blurRadiusProperty;
        public static readonly DependencyProperty blurOnProperty;
        public static readonly DependencyProperty blurObjProperty;
        public static readonly DependencyProperty contentProperty;
        public bbo()
        {
            InitializeComponent();
        }

        static bbo()
        {
            blurRadiusProperty = DependencyProperty.Register("BlurRadius", typeof(double), typeof(bbo), new PropertyMetadata((double)20, new PropertyChangedCallback(blurRadiusPropertyChange)));
            blurOnProperty = DependencyProperty.Register("BlurOn", typeof(bool), typeof(bbo), new PropertyMetadata(false, new PropertyChangedCallback(blurOnPropertyChange)));
            blurObjProperty = DependencyProperty.Register("BlurObj", typeof(Visual), typeof(bbo), new PropertyMetadata(null, new PropertyChangedCallback(blurObjPropertyChange)));
            contentProperty = DependencyProperty.Register("Content", typeof(ContentControl), typeof(bbo), new PropertyMetadata(null, new PropertyChangedCallback(contentPropertyChange)));
        }

        [Category("Blur settings"), Description("Blur radius"), Bindable(true)]
        public double BlurRadius
        {
            get { return (double)GetValue(blurRadiusProperty); }
            set { SetValue(blurRadiusProperty, value); }
        }

        [Category("Blur settings"), Description("Blur on/off"), Bindable(true)]
        public bool BlurOn
        {
            get { return (bool)GetValue(blurOnProperty); }
            set { SetValue(blurOnProperty, value); }
        }

        [Category("Blur settings"), Description("Blur obj"), Bindable(true)]
        public Visual BlurObj
        {
            get { return (Visual)GetValue(blurObjProperty); }
            set { SetValue(blurObjProperty, value); }
        }

        new public ContentControl Content
        {
            get { return (ContentControl)GetValue(contentProperty); }
            set { SetValue(contentProperty, value); }
        }

        

        private Visual _parentVisual;
        private bool _blurOn = false;

        private static void blurRadiusPropertyChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            bbo c = obj as bbo;
            double nv = (double)e.NewValue;
            c.BlurRadius = c.blurEffect.Radius = nv;
        }

        private static void blurOnPropertyChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            bbo c = obj as bbo;
            bool nv = (bool)e.NewValue;
            if (nv) c.OnBlur(null);
            else c.OffBlur();
            c.BlurOn = nv;
        }

        private static void blurObjPropertyChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            bbo c = obj as bbo;
            Visual nv = (Visual)e.NewValue;
            c.BlurObj = nv;
        }

        private static void contentPropertyChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            bbo c = obj as bbo;
            ContentControl nv = (ContentControl)e.NewValue;
            c.MyContentControl.Content = nv;
        }

        public void OnBlur(double? radius = 20)
        {
            double newRadius = (radius == null) ? BlurRadius : (double)radius;
            BlurOn = true;
            BlurRadius = newRadius;

            blurR.Fill = new VisualBrush(BlurObj) //создаём новую кисть для блюра
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                ViewboxUnits = BrushMappingMode.Absolute,
                Stretch = Stretch.None
            };
        }

        public void OffBlur()
        {
            BlurOn = false;
            blurR.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); //убираем кисть для блюра
        }

        public void UpdateBlur()
        {
            if (BlurOn == false || BlurObj == null) //проверяем включен ли блюр
            {
                return;
            }

            try
            {
                if (_parentVisual == null) _parentVisual = VisualTreeHelper.GetParent(BlurObj) as Visual;



                VisualBrush b = blurR.Fill as VisualBrush;
                if (b == null)
                {
                    OnBlur(null);
                    return;
                }

                Point offset = this.TransformToVisual(_parentVisual).Transform(new Point()); //получаем отступ. Можно променять this на blurR и тогда нам не надо считать Margin
                b.Viewbox = new Rect(offset.X + gridBlur.Margin.Left + 2, offset.Y + gridBlur.Margin.Top + 2, 1, 1); //число 2 это размер окантовки 
            }
            catch (InvalidOperationException) { } //уберает ошибку в редакторе форм


        }

        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {
            if (BlurOn) UpdateBlur();

            if (BlurOn != _blurOn) //нужно чтобы если функция по изминеннию BlurOn не сработала, то блюр всёравно приминился
            {
                if (BlurOn)
                {
                    OnBlur(null);
                }
                else
                {
                    OffBlur();
                }
                _blurOn = BlurOn;
            }
        }
    }
}
