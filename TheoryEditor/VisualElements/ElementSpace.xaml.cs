using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.Core;
using Protsenko.TheoryEditor.Core.Interfaces;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.VisualElements
{
    /// <summary>
    /// Логика взаимодействия для CanvasMy.xaml
    /// </summary>
    public partial class ElementSpace : Canvas
    {
        private bool isDragging = false;
        private bool isMoving = false;
        private bool changed; 
        private double lastx = 0, lasty = 0;
        private UIElement currentDraggingObject = null;

        private Session session;
        private TheoryTree tree;
        private TheoryTreeController treeController;
        private ElementAllocator allocator;
        private Arrows arrows;

        private List<TheoryVisualDecorator> focusedObjects;

        delegate void DragStart(TheoryVisualDecorator element, MouseButtonEventArgs e);
        delegate void DragEnd(TheoryVisualDecorator element, MouseButtonEventArgs e);
        delegate void Focused(TheoryVisualDecorator decorator);

        public event Core.Events.Focused ElementsFocused;

        //********************************Theory Visual Decorator************************************************************************************************************************************************************

        /// <summary>
        /// Object that is used to display various information on Canvas. It supports
        /// </summary>
        class TheoryVisualDecorator : Border, Core.Interfaces.ITheoryContainer, Core.Interfaces.IEVisualInfoable, IMultiLanguage
        {
            StackPanel mainpanel;
            Button bclose;
            Button bedit;
            DockPanel toppanel;
            TextBlock titleblock;
            private EVisualInfo visualinfo;

            private bool isInFocus = false; 
            /// <summary>
            /// Событие возникающее при нажатии левой кнопкой мыши на шапке элемента.
            /// </summary>
            public event DragStart DragStart;
            /// <summary>
            /// Событие возникающее при отжатии левой кнопки мыши от шапки элемента.
            /// </summary>
            public event DragEnd DragEnd;

            public event Focused Focused;

            public event Core.Events.Updated WantsToBeEdited;

            /// <summary>
            /// Свойство устанавливающее является ли данный элемент активным. Также изменяет вид самого элемента.
            /// </summary>
            public bool IsInFocus
            {
                get
                {
                    return isInFocus;
                }
                set
                {
                    isInFocus = value;
                    if (isInFocus)
                    {
                        BorderBrush = Brushes.PaleVioletRed;
                        Focused(this);
                    }
                    else
                        BorderBrush = Brushes.GhostWhite;
                }
            }

     
            public static TheoryVisualDecorator СreateInstance()
            {
                return new TheoryVisualDecorator(new Point(0, 0), "default", null);
            }

            public static TheoryVisualDecorator СreateInstance(UIElement content, string title)
            {
                return new TheoryVisualDecorator(new Point(0, 0), title, content);
            }

            public static TheoryVisualDecorator СreateInstance(Point p, string title, UIElement content)
            {
                return new TheoryVisualDecorator(p, title, content);
            }


            protected TheoryVisualDecorator(Point p, string title, UIElement element)
            {
                SomeTextCodeInit(p, title, element);
            }

            private void SomeTextCodeInit(Point p, string title, UIElement element)
            {
                this.Background = Brushes.WhiteSmoke;
                this.BorderThickness = new Thickness(2);
                this.BorderBrush = Brushes.GhostWhite;
                this.CornerRadius = new CornerRadius(2);

                mainpanel = new StackPanel();
                bclose = new Button();
                bedit = new Button();
                toppanel = new DockPanel();
                titleblock = new TextBlock();


                mainpanel.Orientation = Orientation.Vertical;
                toppanel.Background = Brushes.AliceBlue;
                toppanel.Name = "DragnDropPanel";


                StackPanel buttonPanel = new StackPanel();
                buttonPanel.HorizontalAlignment = HorizontalAlignment.Right;
                buttonPanel.Orientation = Orientation.Horizontal;

                bclose.Content = "x";
                bclose.Click += new RoutedEventHandler(bclose_Click);
                bclose.Margin = new Thickness(2);

                bedit.Click += new RoutedEventHandler(bedit_Click);
                bedit.Margin = new Thickness(2);

                buttonPanel.Children.Add(bedit);
                buttonPanel.Children.Add(bclose);

                titleblock.Text = title;
                titleblock.HorizontalAlignment = HorizontalAlignment.Left;

                toppanel.Children.Add(titleblock);
                toppanel.Children.Add(buttonPanel);
                

                mainpanel.Children.Add(toppanel);
                mainpanel.Children.Add(element);
                this.Child = mainpanel; 
                Canvas.SetLeft(this, p.X); Canvas.SetTop(this, p.Y);

                ITheoryElement theoryElement = GetTheoryElement();
                visualinfo = new EVisualInfo();
                visualinfo.Updated += new Protsenko.TheoryEditor.Core.Events.Updated(visualinfo_Updated);

                toppanel.MouseLeftButtonDown += new MouseButtonEventHandler(toppanel_MouseLeftButtonDown);
                toppanel.MouseLeftButtonUp += new MouseButtonEventHandler(toppanel_MouseLeftButtonUp);

                DragStart += new DragStart(TheoryVisualDecorator_DragStart);
                DragEnd += new DragEnd(TheoryVisualDecorator_DragEnd);
                WantsToBeEdited += new Protsenko.TheoryEditor.Core.Events.Updated(TheoryVisualDecorator_WantsToBeEdited);
                LanguageResource.Updated +=new Protsenko.TheoryEditor.Core.Events.Updated(LanguageResource_Updated);
                LanguageResource_Updated(null);
            }

            void TheoryVisualDecorator_WantsToBeEdited(object sender)
            {
            }

            void bedit_Click(object sender, RoutedEventArgs e)
            {
                WantsToBeEdited(this);
            }

            void TheoryVisualDecorator_DragEnd(TheoryVisualDecorator element, MouseButtonEventArgs e)
            {
            }

            protected void TheoryVisualDecorator_DragStart(TheoryVisualDecorator element, MouseButtonEventArgs e)
            { 
            }

            void toppanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                DragEnd(this, e);
            }

            void toppanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                DragStart(this, e);
            }

            void visualinfo_Updated(object sender)
            {
                EVisualInfo info = (EVisualInfo)sender;
                Canvas.SetLeft(this, info.Position.X);
                Canvas.SetTop(this, info.Position.Y);
            } 

            void bclose_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    ((ElementSpace)this.Parent).treeController.Delete(this.GetTheoryElement());
                }
                catch (Exception exc) 
                { 
                    MessageBox.Show(exc.ToString()); 
                }
            }

            #region Члены ITheoryContainer

            public ITheoryElement GetTheoryElement()
            {
                return ((Core.Interfaces.ITheoryContainer)((StackPanel)this.Child).Children[1]).GetTheoryElement();
            }

            public List<ITheoryElement> GetTheoryElements()
            {
                return ((Core.Interfaces.ITheoryContainer)((StackPanel)this.Child).Children[1]).GetTheoryElements();
            }

            #endregion

            #region Члены IEVisualInfoable

            public Protsenko.TheoryEditor.Core.EVisualInfo GetVisualInformation()
            {
                ITheoryElement theoryElement = GetTheoryElement();
                visualinfo.SetInfo(Canvas.GetLeft(this),
                                   Canvas.GetTop(this),
                                   this.RenderSize.Width,
                                   this.RenderSize.Height,
                                   theoryElement.PositionX,
                                   theoryElement.PositionY);
                return visualinfo;
            }

            #endregion           
        
            #region Члены IMultiLanguage

            public void LanguageResource_Updated(object language)
            {
                bedit.Content = LanguageResource.currentDictionary["edit"];
            }

            #endregion
        }

        //*****************************Element Allocator*********************************************************************************************************************************************************************

        /// <summary>
        /// Класс специлизирующийся на задании местоположений элементов на элементе Canvas.
        /// </summary>
        class ElementAllocator : Core.Interfaces.IEAllocator
        {
            UIElementCollection collection;
            Canvas canvas;
            double dx;
            double dy;


            public ElementAllocator(UIElementCollection collection, Canvas canvas)
            {
                this.collection = collection;
                this.canvas = canvas;
            }

            /// <summary>
            /// Задаёт новые позиции для элементов и обновляет их на панели Canvas
            /// Для каждого элемента обновляет EVisualInfo и его вершины.
            /// </summary>
            /// <param name="sender"></param>
            public void Update(Canvas canvas)
            {              
            }

            /// <summary>
            /// Задаёт новые местоположения каждого элемента в Canvas в соответствии
            /// с распределением после последнего вызова метода allocate();
            /// </summary>
            //public void Update()
            //{
            //    foreach(TheoryVisualDecorator element in collection)
            //    {
            //        EVisualInfo info = element.getVisualInformation();
            //        Canvas.SetLeft(element, info.Position.X);
            //        Canvas.SetTop(element, info.Position.Y);
            //    }
            //}


            public void TranslateElement(UIElement element, double dx, double dy)
            {
                EVisualInfo info = ((TheoryVisualDecorator)element).GetVisualInformation();
                info.SetPosition(info.Position.X + dx, info.Position.Y + dy);
            }

            public void TranslateElements(UIElementCollection elements, double dx, double dy)
            {
                foreach(TheoryVisualDecorator element in elements)
                {
                    EVisualInfo info = element.GetVisualInformation();
                    info.SetPosition(info.Position.X + dx, info.Position.Y + dy);
                }
                this.dx += dx;
                this.dy += dy;
            }

            public void Zoom(double x, double y, double dz, UIElementCollection elements)
            {
                foreach (TheoryVisualDecorator element in elements)
                {
                    EVisualInfo info = element.GetVisualInformation();
                    info.SetPosition((info.Position.X  - x) * Math.Pow(1.08, dz) + x, (info.Position.Y  - y) * Math.Pow(1.08, dz) + y);
                }
            }

            public void Rotate(double x, double y, double angle, List<Point> points)
            {
                throw new NotImplementedException();
            }

            public EVisualInfo GetVisualInfo(ITheoryElement element)
            {
                TheoryVisualDecorator dec;
                for (int i = 0; i < collection.Count; i++)
                {
                    dec = (TheoryVisualDecorator)collection[i];
                    if (dec.GetTheoryElement() == element)
                    {
                        return dec.GetVisualInformation(); 
                    }
                }
                return null;
            }

            public EVisualInfo GetVisualInfo(UIElement element, UIElementCollection collection)
            {
                TheoryVisualDecorator decorator = (TheoryVisualDecorator)element;
                return decorator.GetVisualInformation();
            }

            #region Члены IEAllocator

            public void Allocate(List<Protsenko.TheoryEditor.Core.EVisualInfo> elements)// something strange happening here
            {
                
            }

            public void Allocate()
            {
                EVisualInfo info;
                foreach (TheoryVisualDecorator element in collection)
                {
                    info = element.GetVisualInformation();
                    info.SetPosition(20 + info.PositionInGraphX * (info.Width + 5)  + this.dx, 
                                     20 + info.PositionInGraphY * (info.Height + 5) +  + this.dy);
                }
            }

            #endregion
        }

        //***************************Arrows**********************************************************************************************************************************************************************************
        /// <summary>
        /// Класс обеспечивающий прорисовку связей между элементами.
        /// </summary>
        class Arrows
        {
            ElementSpace space;
            Path path;

            public Arrows(ElementSpace space)
            {
                this.space = space;
                path = new Path();
            }

            public void ArrangeArrows()
            {
                //foreach (TheoryVisualDecorator element in space.Children)
                //{
                //    EVisualInfo elementInfo = element.getVisualInformation();
                //    ITheoryElement theoryElement = element.getTheoryElement();
                    
                //}

                

                PathFigure figure = new PathFigure();
                figure.StartPoint = new Point(10,10);

                LineSegment segment = new LineSegment(new Point(10,100), false);
                figure.Segments.Add(segment);

                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);


                path.Stroke = Brushes.Black;
                path.StrokeThickness = 12;
                path.Data = geometry; 
            }
        }

        //***************************Element Space***************************************************************************************************************************************************************************
        

        public ElementSpace(TheoryTree tree, TheoryTreeController treeController, Session session)
        {
            InitializeComponent();
            allocator = new ElementAllocator(Children,this);
            //arrows = new Arrows(this);
            this.session = session;
            this.tree = tree;
            this.treeController = treeController;
            this.tree.ContentUpdated += new Protsenko.TheoryEditor.Core.Events.ContentUpdated(tree_ContentUpdated);
            focusedObjects = new List<TheoryVisualDecorator>();

            this.MouseRightButtonUp += new MouseButtonEventHandler(CanvasMy_MouseRightButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(dragpanel_MouseLeftButtonDown);
            this.MouseMove += new MouseEventHandler(dragpanel_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(dragpanel_MouseLeftButtonUp);
            this.MouseLeave += new MouseEventHandler(CanvasMy_MouseLeave);
            this.MouseWheel += new MouseWheelEventHandler(CanvasMy_MouseWheel);
            this.LayoutUpdated += new EventHandler(ElementSpace_LayoutUpdated);     // not mvc //костыль
            this.ElementsFocused += new Protsenko.TheoryEditor.Core.Events.Focused(ElementSpace_ElementFocused);

            if (tree.Count != 0)
                tree.RunUpdateForeachElement();
        }

        protected void ElementSpace_ElementFocused(List<ITheoryElement> elements) {}
       
        protected void OnContentUpdated(object sender) { }

        public void SetMC(TheoryTree tree, TheoryTreeController treeController)
        {
            this.tree = tree;
            this.treeController = treeController;
            this.tree.ContentUpdated += new Protsenko.TheoryEditor.Core.Events.ContentUpdated(tree_ContentUpdated);

            this.Children.Clear();

            if (tree.Count != 0)
                tree.RunUpdateForeachElement();
        }

        public void Zoom(double inc, Point center)
        {
            allocator.Zoom(center.X, center.Y, inc, Children);
        }

        public void TranslateElements(UIElementCollection elements, double dx, double dy)
        {
            allocator.TranslateElements(elements, dx, dy);
            foreach (TheoryVisualDecorator dec in Children)
            {
                EVisualInfo info = dec.GetVisualInformation();
            }
        }

        public void TranslateElement(UIElement element, double dx, double dy)
        {
            allocator.TranslateElement(element, dx, dy);
        }

        private void tree_ContentUpdated(object added, object deleted)
        {
            if (added != null)
            {
                tree_ContentAdded((ITheoryElement)added);
            }
            else if (deleted != null)
            {
                tree_ContentDeleted((ITheoryElement)deleted);
            }
            else
            {
                changed = true;
            }
        }

        private void tree_ContentAdded(ITheoryElement element)
        {
            TheoryVisualDecorator decorator = TheoryVisualDecorator.СreateInstance(InfromationPresenter.MakeVisual(element), element.Theoryindex.ToString() + " " + (Children.Count + 1));
            decorator.DragStart += new DragStart(obj_DragStart);
            decorator.DragEnd += new DragEnd(obj_DragEnd);
            decorator.Focused += new Focused(obj_Focused);
            decorator.MouseLeftButtonDown += new MouseButtonEventHandler(dragpanel_MouseLeftButtonDown);
            decorator.WantsToBeEdited += new Protsenko.TheoryEditor.Core.Events.Updated(decorator_WantsToBeEdited);
            this.Children.Add(decorator);
            changed = true;
        }

        private void decorator_WantsToBeEdited(object sender)
        {
            TheoryVisualDecorator dec = (TheoryVisualDecorator)sender;
            ITheoryElement element = dec.GetTheoryElement();
            dec.IsInFocus = true;
            new ModalWindows.ElementWindow(element, tree.GetBasis(element), session, tree);
            dec.IsInFocus = false;
        }

        private void tree_ContentDeleted(ITheoryElement element)
        {
            try
            {
                int ind = -1;
                TheoryVisualDecorator dec; 
                for (int i = 0; i < Children.Count; i++)
                {
                    dec = (TheoryVisualDecorator)Children[i];
                    if (dec.GetTheoryElement() == element)
                    {
                        ind = i; break;
                    }
                }
                this.Children.RemoveAt(ind);
                changed = true;
            }
            finally { }
        }

        private void obj_Focused(ElementSpace.TheoryVisualDecorator decorator)
        {  
        }

        private void ElementSpace_LayoutUpdated(object sender, EventArgs e) // not mvc
        {
            if (changed)
            {
                allocator.Allocate();
                //arrows.ArrangeArrows();
                changed = false;
            }
        }

        private void CanvasMy_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((sender is Canvas) && ((Canvas)sender) == this) 
            {
                Zoom(e.Delta/Math.Abs(e.Delta), e.GetPosition(this));

            }
        }

        private void CanvasMy_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
            isMoving = false;
        }

        private void dragpanel_MouseMove(object sender, MouseEventArgs e)
        {
            double dx = e.GetPosition(this).X - lastx;
            double dy = e.GetPosition(this).Y - lasty;
            if (isDragging) { TranslateElement(currentDraggingObject,dx,dy); }
            if (isMoving) { TranslateElements(Children, dx, dy); }
            lastx = e.GetPosition(this).X;
            lasty = e.GetPosition(this).Y;
        }

        private void dragpanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TheoryVisualDecorator)
            {
                foreach (TheoryVisualDecorator element in focusedObjects)
                {
                    element.IsInFocus = false;
                }
                focusedObjects.Clear();
                
                TheoryVisualDecorator decorator = (TheoryVisualDecorator)sender;
                decorator.IsInFocus = true;
                focusedObjects.Add(decorator);
                List<ITheoryElement> basis = tree.GetBasis(decorator.GetTheoryElement());
                foreach (ITheoryElement element in basis)
                {
                    TheoryVisualDecorator dec = Search(element);
                    dec.IsInFocus = true;
                    focusedObjects.Add(dec);
                }
                
                basis.Add(decorator.GetTheoryElement()); // is not true basis now
                ElementsFocused(basis);
            }

            if (!isDragging && ((e.OriginalSource is ElementSpace) && ((ElementSpace)sender) == this))
            {
                isMoving = true;
                lastx = e.GetPosition(this).X;
                lasty = e.GetPosition(this).Y;
                e.Handled = true;
                Cursor = Cursors.SizeAll;
                foreach (TheoryVisualDecorator element in focusedObjects)
                {
                    element.IsInFocus = false;
                }
                focusedObjects.Clear();
                ElementsFocused(null);
            }
        }

        private void dragpanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
            Cursor = Cursors.Arrow;
        }

        private void obj_DragEnd(ElementSpace.TheoryVisualDecorator element, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void obj_DragStart(ElementSpace.TheoryVisualDecorator element, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastx = e.GetPosition(this).X;
            lasty = e.GetPosition(this).Y;
            currentDraggingObject = element;
            e.Handled = true;
        }

        private void CanvasMy_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Corollary corollary = new Corollary(1, "A = B, B = C -> A = C", "");
            //UIElement b = core.InfromationPresenter.makeVisual(corollary);
            //Add(b, e.GetPosition(this), "Title!");
        }
        
        private TheoryVisualDecorator Search(ITheoryElement element)
        {
            foreach(TheoryVisualDecorator dec in Children)
            {
                if (dec.GetTheoryElement() == element)
                    return dec;
            }
            return null;
        }
    }
}
