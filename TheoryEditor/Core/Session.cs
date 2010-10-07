using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.Core.Events;
using Protsenko.TheoryEditor.VisualElements;
using Protsenko.TheoryEditor.Core.Debug;

namespace Protsenko.TheoryEditor.Core
{
    public class Session
    {
        public string Name { get; set; }
        TheoryTree tree;
        TheoryTreeController treeController;
        ExceptionHandler exceptionHandler = ExceptionHandler.exceptionHandler;
        SessionVisual visualRepresentation;
        ElementSpace elementspace;

        public enum ConstructorMods
        {
            New,
            Load
        }

        public enum Mods
        {
            Basic,
            QuickRedactor
        }

        public Session(UIElement parent, string name, ConstructorMods mod)
        {
            Name = name;
            if (mod == ConstructorMods.New)
            {
                tree = new TheoryTree();
            }
            else if (mod == ConstructorMods.Load)
            {
                try
                {
                    Microsoft.Win32.OpenFileDialog filedialog = new Microsoft.Win32.OpenFileDialog();
                    filedialog.InitialDirectory = SaveLoad.currentFoldierPath;
                    filedialog.DefaultExt = SaveLoad.fileExtension;
                    filedialog.ShowDialog();

                    object loadedObject = null;
                    SaveLoad.LoadData(filedialog.SafeFileName, out loadedObject);
                    tree = (TheoryTree)loadedObject;
                }
                catch (Exception e) 
                {
                    exceptionHandler.Add(e);
                    tree = new TheoryTree(); 
                }
            }

            treeController = new TheoryTreeController(tree);
            elementspace = new ElementSpace(tree, treeController, this);
            visualRepresentation = new SessionVisual(elementspace,tree,this);

            if (parent is DockPanel)
            {
                DockPanel panel = (DockPanel)parent;
                panel.Children.Add(visualRepresentation);
            }
            elementspace = visualRepresentation.GetElementSpace();
        }

        public bool AddTheoryElement(TheoryTypes type, int theoryindex, string formulation, string template, string proof, List<ITheoryElement> newbasis)
        {
            try 
            {
                treeController.Add(type, theoryindex, formulation, template, proof, newbasis);
                return true;
            }
            catch (Exception e) 
            {
                exceptionHandler.Add(e);
                return false;
            }
        }

        public bool AddTheoryElement(TheoryTypes type, int theoryindex, string formulation, string template, string proof)
        {
            try
            {
                treeController.Add(type, theoryindex, formulation, template, proof);
                return true;
            }
            catch (Exception e) 
            {
                exceptionHandler.Add(e);
                return false;
            }
        }

        public void EditTheoryElement(ITheoryElement element, int theoryindex, string formulation, string template, string proof)
        {
            try
            {
                treeController.Edit(element, theoryindex, formulation, template, proof);
            }
            catch (Exception e) 
            {
                exceptionHandler.Add(e);
            }
        }

        public void DeleteTheoryElement(TheoryTypes type, int theoryindex, string formulation, string template, string proof)
        {
            try
            {
                treeController.Delete(type, theoryindex, formulation, template, proof);
            }
            catch (Exception e) 
            { 
                exceptionHandler.Add(e); 
            }
        }

        public List<ITheoryElement> GetAllTheoryElements()
        {
            return tree.GetAllElements();
        }

        private void ArrangeAndShow()
        {
        }

        public void SaveData()
        {
            try
            {
                SaveLoad.SaveData(Name, tree);
            }
            catch (Exception e) 
            {
                exceptionHandler.Add(e);
            }
        }

        public void LoadData()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog filedialog = new Microsoft.Win32.OpenFileDialog();
                filedialog.InitialDirectory = SaveLoad.currentFoldierPath; filedialog.RestoreDirectory = true;
                filedialog.DefaultExt = SaveLoad.fileExtension;
                filedialog.ShowDialog();
                

                object loadedObject = null;
                SaveLoad.LoadData(filedialog.SafeFileName, out loadedObject);
                tree = (TheoryTree)loadedObject;

                treeController.SetTree(tree);
                elementspace.SetMC(tree, treeController);
            }
            catch (Exception e) 
            {
                exceptionHandler.Add(e);
            }
        }

        public void Close()
        {
            ((Panel)visualRepresentation.Parent).Children.Remove(visualRepresentation);
        }

        public void ChangeMode(Mods mod)
        {
            if (mod == Mods.Basic)
            {
                visualRepresentation.ChangeMode(mod);
            }
            else if (mod == Mods.QuickRedactor)
            {
                visualRepresentation.ChangeMode(mod);
            }
        }
    }
}
