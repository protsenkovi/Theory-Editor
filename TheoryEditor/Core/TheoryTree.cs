using System;
using System.Collections.Generic;

using Protsenko.TheoryEditor.Core.Types;
using Protsenko.TheoryEditor.Core.Debug;
using Protsenko.TheoryEditor.Core.Languages;

namespace Protsenko.TheoryEditor.Core
{
    /// <summary>
    /// Theory Tree. Contains Theorems, Postulates, Lemmas, Definitions, Corollarys.
    /// </summary>
    [Serializable()]
    public class TheoryTree : IList<ITheoryElement>
    {
        private List<Node> nodes;
        private List<ITheoryElement> theorylist;
        private Node root;
        private int maxdepth = 0;

        [field: NonSerialized()]
        public event Events.ContentUpdated ContentUpdated;

        public int Count { get; private set; }

        [Serializable()]
        class Node 
        {
            public List<Node> basis;
            public ITheoryElement element;

            public Node()
            {
                basis = new List<Node>();
                element = null;
            }

            public Node(ITheoryElement element)
            {
                basis = new List<Node>();
                this.element = element;
            }

            public Node(ITheoryElement element, List<Node> links)
            {
                this.element = element;
                basis = links;
            }
        }

        public TheoryTree()
        {
            root = new Node();
            theorylist = new List<ITheoryElement>();
            nodes = new List<Node>();
            this.ContentUpdated += new Protsenko.TheoryEditor.Core.Events.ContentUpdated(OnContentUpdated);
        }

        public void RunUpdateForeachElement()
        {
            foreach (ITheoryElement element in GetAllElements())
            {
                ContentUpdated(element, null);
            }
        }

        protected void OnContentUpdated(object added, object deleted)
        {
        }

        /// <summary>
        /// Returns list of links on other elements of desired element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public List<ITheoryElement> GetBasis(ITheoryElement element)
        {
            Node node = SearchNode(element);
            List<ITheoryElement> basis = new List<ITheoryElement>();
            foreach (Node basisNode in node.basis)
            {
                basis.Add(basisNode.element);
            }
            return basis;
        }

        /// <summary>
        /// Returns list containing all elements.
        /// </summary>
        /// <returns></returns>
        public List<ITheoryElement> GetAllElements()
        {
            return CopyList(theorylist);
        }

        public ITheoryElement Search(ITheoryElement element)
        {
            return SearchNode(element).element;
        }

        private Node SearchNode(ITheoryElement element)
        {
            foreach (Node node in nodes)
            {
                if (node.element.Equals(element))
                    return node;
            }
            return null;
        }


        #region New indexation

        //public void newreindexate()
        //{
        //    foreach (Node node in nodes)
        //    {
        //        addreindexateY(node);
        //    }
        //    newreindexateX();
        //}

        private void newreindexate(Node inode)
        {
            newreindexateY(inode); 
            newreindexateX();
        }

        private void newreindexateY(Node inode) //checks yposition of nodes that links to nodeToDelete.
        {
            int maxY = 0;
            if (inode.basis.Count != 0)
            {
                foreach (Node node in inode.basis)
                {
                    if (maxY < node.element.PositionY) maxY = node.element.PositionY;
                }
                maxY = maxY + 1;
            }
            inode.element.PositionY = maxY;
            if (inode.element.PositionY > maxdepth) maxdepth = inode.element.PositionY; 
        }

        private void newreindexateX()
        {
            int[] xpositions = new int[maxdepth+1]; //xpositions
            ITheoryElement element = null;
            List<ITheoryElement> elements = GetAllElements();
            for (int i = 0; i < elements.Count; i++)
            {
                element = elements[i];
                element.PositionX = xpositions[element.PositionY]++;
            }
        }

        /// <summary>
        /// Adds desired element to tree.
        /// </summary>
        /// <param name="elementToAdd"></param>
        /// <param name="basis"></param>
        public void Add(ITheoryElement elementToAdd, List<ITheoryElement> basis)
        {
            if (elementToAdd != null)
            {
                if (!this.Contains(elementToAdd))
                {
                    Node nodeToAdd = new Node(elementToAdd);
                    if (basis != null)
                        foreach (ITheoryElement element in basis)
                        {
                            nodeToAdd.basis.Add(SearchNode(element));
                        }
                    nodes.Add(nodeToAdd);
                    theorylist.Add(elementToAdd);
                    root.basis.Add(nodeToAdd);

                    foreach (Node node in nodeToAdd.basis)
                    {
                        root.basis.Remove(node);
                    }
                    
                    newreindexate(nodeToAdd);

                    Count++;
                    ContentUpdated(elementToAdd, null);
                }
                else throw new Exception(LanguageResource.currentDictionary["errorAddingElementToTreeAdded"]);
            }
            else throw new Exception(LanguageResource.currentDictionary["errorAddingElementToTreeNotExists"]);
        }

        /// <summary>
        /// Sets list of links on other elements of desired element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="newbasis"></param>
        public void SetBasis(ITheoryElement element, List<ITheoryElement> newbasis)
        {
            Node node = SearchNode(element);
            node.basis.Clear();
            if (newbasis != null)
                foreach (ITheoryElement elt in newbasis)
                {
                    node.basis.Add(SearchNode(elt));
                }
            newreindexate(node);
            ContentUpdated(null, null);
        }

        #endregion

        #region Члены IList<ITheoryElement>

        public int IndexOf(ITheoryElement item)
        {
            return theorylist.IndexOf(item);
        }

        public void Insert(int index, ITheoryElement item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ITheoryElement this[int index]
        {
            get
            {
                return theorylist[index];
            }
            set
            {
                throw new Exception(LanguageResource.currentDictionary["errorReadOnly"]);
            }
        }

        #endregion

        #region Члены ICollection<ITheoryElement>

        public void Add(ITheoryElement item)
        {
            Add(item, null);
        }

        public void Clear()
        {
            nodes.Clear();
            theorylist.Clear();
            root.basis.Clear();
        }

        public bool Contains(ITheoryElement item)
        {
            return theorylist.Contains(item);
        }

        public void CopyTo(ITheoryElement[] array, int arrayIndex)
        {
            theorylist.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ITheoryElement item)
        {
            Node nodeToDelete = SearchNode(item);
            if (!(nodes.Remove(nodeToDelete) && (theorylist.Remove(item))))
                return false;

            List<Node> passednodes = new List<Node>();
            List<Node> nodesWithNodeToDelete = new List<Node>();

            passednodes.Add(nodeToDelete);

            foreach(Node node in root.basis)
                RecurseSearch(nodeToDelete, node, passednodes, nodesWithNodeToDelete);

            foreach (Node node in nodesWithNodeToDelete)
            {
                newreindexateY(node);
            }
            
            root.basis.Remove(nodeToDelete);

            Count--;

            newreindexateX();
            ContentUpdated(null, item);
            return true;
        }

        private Node RecurseSearch(Node nodeToDelete, Node node, List<Node> passednodes, List<Node> nodesWithNodeToDelete)
        {
            if (passednodes.Contains(node)) return null;


            foreach (Node basisNode in node.basis)
            {
                RecurseSearch(nodeToDelete, basisNode, passednodes, nodesWithNodeToDelete);
            }


            if (node.basis.Contains(nodeToDelete))
            {
                foreach (Node basisNode in nodeToDelete.basis)
                {
                    if (!node.basis.Contains(basisNode))
                    {
                        node.basis.Add(basisNode);
                    }
                }
                node.basis.Remove(nodeToDelete);
                nodesWithNodeToDelete.Add(node);
            }

            foreach (Node n in nodesWithNodeToDelete)
            {
                if (node.basis.Contains(n))
                {
                    nodesWithNodeToDelete.Add(node);
                    passednodes.Add(node);
                    return null;
                }
            }

            passednodes.Add(node);
            return null;
        }

        #endregion

        #region Члены IEnumerable<ITheoryElement>


        public IEnumerator<ITheoryElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Члены IEnumerable

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        private List<T> CopyList<T>(List<T> listToCopy)
        {
            List<T> list = new List<T>();
            foreach (T something in listToCopy)
                list.Add(something);
            return list;
        }

        private List<ITheoryElement> transformList(List<Node> listSource)
        {
            List<ITheoryElement> list = new List<ITheoryElement>();
            foreach (Node something in listSource)
                list.Add(something.element);
            return list;
        }
    }

    /// <summary>
    /// Theory Tree. Contains Theorems, Postulates, Lemmas, Definitions, Corollarys.
    /// </summary>
    //public class oldTheoryTree 
    //{
    //    public event Events.ContentUpdated ContentUpdated;
    //    Node root = null;
    //    public int Count { get; private set; }
    //    public List<ITheoryElement> theorylist { get; private set; }
    //    private int maxdepth = 0;

    //    class Node
    //    {
    //        public List<Node> basis;
    //        public ITheoryElement element;

    //        public Node()
    //        {
    //            basis = new List<Node>();
    //        }

    //        public Node(ITheoryElement element)
    //        {
    //            basis = new List<Node>();
    //            this.element = element;
    //        }

    //        public Node(ITheoryElement element, List<Node> links)
    //        {
    //            this.element = element;
    //            basis = links;
    //        }

    //    }

    //    public oldTheoryTree()
    //    {
    //        theorylist = new List<ITheoryElement>();
    //        //Updated += new CanvasTest.core.Events.Updated(OnUpdated);// depricated
    //        this.ContentUpdated += new CanvasTest.core.Events.ContentUpdated(OnContentUpdated);
    //        root = new Node(new Definition(0, "BASE", ""));
    //        theorylist.Add(root.element);
    //        Count++;
    //        reindexate();
    //    }

    //    public void RunUpdateForeachElement()
    //    {
    //        foreach(ITheoryElement element in GetAllElements())
    //        {
    //            ContentUpdated(element, null);
    //        }
    //    }

    //    protected void OnContentUpdated(object added, object deleted)
    //    {

    //    }

    //    protected void OnUpdated(object sender)
    //    {
    //        reindexate();
    //    }

    //    /// <summary>
    //    /// Returns link on desired element if tree contains element.
    //    /// </summary>
    //    /// <param name="element"></param>
    //    /// <returns></returns>
    //    public ITheoryElement Search(ITheoryElement element)
    //    {
    //        List<Node> passednodes = new List<Node>();
    //        Node node = recurseSearch(element, root, passednodes);
    //        if (node != null)
    //            return node.element;
    //        else
    //            throw new Exception("Search failed. Element doesn't exists.");
    //    }


    //    /// <summary>
    //    /// Adds desired element to tree.
    //    /// </summary>
    //    /// <param name="elementToAdd"></param>
    //    /// <param name="basis"></param>
    //    public void Add(ITheoryElement elementToAdd)
    //    {
    //        if (elementToAdd != null)
    //        {
    //            Node node = new Node(elementToAdd);
    //            theorylist.Add(elementToAdd);
    //            root.basis.Add(node); 
    //            Count++;
    //            //Updated(this);  //depricated
    //            reindexate();
    //            ContentUpdated(elementToAdd, null);
    //        }
    //        else throw new Exception("Unable to add element. It doesn't exists.");
    //    }

    //    /// <summary>
    //    /// Adds desired element to tree.
    //    /// </summary>
    //    /// <param name="elementToAdd"></param>
    //    /// <param name="basis"></param>
    //    public void Add(ITheoryElement elementToAdd, List<ITheoryElement> basis)
    //    {
    //        if (elementToAdd != null)
    //        {
    //            Node node = new Node(elementToAdd);
    //            if (basis != null)
    //                foreach (ITheoryElement element in basis)
    //                {
    //                    node.basis.Add(searchNode(element));
    //                }

    //            theorylist.Add(elementToAdd);
    //            root.basis.Add(node);
    //            reindexate();
    //            Count++;
    //            ContentUpdated(elementToAdd,null);
    //        }
    //        else throw new Exception("Unable to add element. It doesn't exists.");
    //    }

    //    /// <summary>
    //    /// Delets desired element.
    //    /// </summary>
    //    /// <param name="element"></param>
    //    public void Remove(ITheoryElement element)
    //    {
    //        Node nodeToDelete = searchNode(element);
    //        if (!SearchAndDelete(nodeToDelete))
    //        {
    //            throw new Exception("Can't delete\n" + element);
    //        }
    //        theorylist.Remove(element);
    //        Count--;
    //        //Updated(this);
    //        reindexate();
    //        ContentUpdated(null, element);
    //    }

    //    /// <summary>
    //    /// Sets list of links on other elements of desired element.
    //    /// </summary>
    //    /// <param name="element"></param>
    //    /// <param name="newbasis"></param>
    //    public void SetBasis(ITheoryElement element, List<ITheoryElement> newbasis)
    //    {
    //        Node node = searchNode(element);
    //        node.basis.Clear();
    //        if (newbasis != null)
    //            foreach (ITheoryElement elt in newbasis)
    //            {
    //                node.basis.Add(searchNode(elt));
    //            }
    //        reindexate();
    //        ContentUpdated(null, null);
    //    }

    //    /// <summary>
    //    /// Returns list of links on other elements of desired element.
    //    /// </summary>
    //    /// <param name="element"></param>
    //    /// <returns></returns>
    //    public List<ITheoryElement> getBasis(ITheoryElement element)
    //    {
    //        Node node = searchNode(element);
    //        List<ITheoryElement> basis = new List<ITheoryElement>();
    //        foreach(Node n in node.basis)
    //        {
    //            basis.Add(n.element);
    //        }
    //        return basis;
    //    }

    //    /// <summary>
    //    /// Returns list containing all elements.
    //    /// </summary>
    //    /// <returns></returns>
    //    public List<ITheoryElement> GetAllElements()
    //    {
    //        if (root != null)
    //        {
    //            List<ITheoryElement> elements = new List<ITheoryElement>(Count);
    //            List<Node> passednodes = new List<Node>();
    //            recurseCompletePass(root, passednodes, elements);
    //            return elements;
    //        }
    //        else
    //            return null;       
    //    }

    //    private Node searchNode(ITheoryElement element)
    //    {
    //        List<Node> passednodes = new List<Node>();
    //        return recurseSearch(element, root, passednodes);
    //    }

    //    private Node recurseCompletePass(Node node, List<Node> passednodes, List<ITheoryElement> theoryElementsList)
    //    {
    //        if (passednodes.Contains(node)) return null;
    //        passednodes.Add(node);
    //        theoryElementsList.Add(node.element);
    //        Node result;
    //        foreach (Node tnode in node.basis)
    //        {
    //            result = recurseCompletePass(tnode, passednodes, theoryElementsList);
    //            if (result != null)
    //                return result;
    //        }
    //        return null;
    //    }


    //    /// <summary>
    //    /// Method that reindexates Theory elements. 
    //    /// Updates xposition and yposition in TheoryElement.
    //    /// </summary>
    //    public void reindexate()
    //    {
    //        List<Node> passednodes = new List<Node>();
    //        recurseCompletePassIndexate(root, passednodes, 0, 0);
    //        ContentUpdated(null, null);
    //    }

    //    private Node recurseCompletePassIndexate(Node node, List<Node> passednodes, int xposition, int yposition)
    //    {
    //        if (passednodes.Contains(node)) return null;
    //        passednodes.Add(node);
    //        node.element.xposition = xposition;
    //        node.element.yposition = yposition;
    //        Node result;
    //        Node tnode;
    //        for(int i = 0; i < node.basis.Count; i++)
    //        {
    //            tnode = node.basis[i];
    //            result = recurseCompletePassIndexate(tnode, passednodes, i, yposition + 1);
    //            if (result != null)
    //                return result;
    //        }
    //        return null;
    //    }

    //    private Node recurseSearch(ITheoryElement elementToSearch,Node node, List<Node> passednodes)
    //    {
    //        if (passednodes.Contains(node)) return null;
    //        if (elementToSearch.Equals(node.element))
    //            return node;
    //        passednodes.Add(node);
    //        Node result;
    //        foreach (Node tnode in node.basis)
    //        {
    //            result = recurseSearch(elementToSearch, tnode, passednodes);
    //            if (result != null)
    //                return result;
    //        }
    //        return null;
    //    }

    //    private bool SearchAndDelete(Node nodeToDelete)
    //    {
    //        if (root == nodeToDelete)
    //        {
    //            try
    //            {
    //                root = root.basis[0];
    //            }
    //            catch (Exception e)
    //            {
    //                throw new Exception("Cann't delete root.");
    //            }
    //            return true;
    //        }
    //        List<Node> passednodes = new List<Node>();
    //        return (recurseSearchAndDelete(nodeToDelete, root, passednodes) == null);
    //    }


    //    private Node recurseSearchAndDelete(Node nodeToDelete, Node node, List<Node> passednodes)
    //    {
    //        if (passednodes.Contains(node)) return null;
    //        if (node.basis.Contains(nodeToDelete))
    //            node.basis.Remove(nodeToDelete);
    //        passednodes.Add(node);
    //        Node result;
    //        foreach (Node tnode in node.basis)
    //        {
    //            result = recurseSearchAndDelete(nodeToDelete, tnode, passednodes);
    //            if (result != null)
    //                return result;
    //        }
    //        return null;
    //    }

    //    #region New indexation

    //    //private void newreindexate()
    //    //{
    //    //    //foreach(Node node in GetAllNodes())
    //    //    //{
    //    //    //    newreindexateY(node);   
    //    //    //}
    //    //    newreindexateX();
    //    //}

    //    //private void newreindexate(Node inode)
    //    //{
    //    //    newreindexateY(inode); // what a shit??
    //    //    newreindexateX();
    //    //}

    //    //private void newreindexateY(Node inode)
    //    //{
    //    //    foreach (Node node in inode.basis) //ypositions
    //    //    {
    //    //        if (inode.element.yposition < node.element.yposition + 1) // inode.element.yposition = Math.Max(inode.element.yposition, node.element.yposition + 1);
    //    //        {
    //    //            inode.element.yposition = node.element.yposition + 1;
    //    //        }
    //    //    }
    //    //}

    //    //private void newreindexateX()
    //    //{
    //    //    int[] xpositions = new int[maxdepth]; //xpositions
    //    //    ITheoryElement element = null;
    //    //    List<ITheoryElement> elements = GetAllElements();
    //    //    for (int i = 0; i < elements.Count; i++)
    //    //    {
    //    //        element = elements[i];
    //    //        element.xposition = xpositions[element.yposition]++;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// Adds desired element to tree.
    //    ///// </summary>
    //    ///// <param name="elementToAdd"></param>
    //    ///// <param name="basis"></param>
    //    //public void Add(ITheoryElement elementToAdd, List<ITheoryElement> basis)
    //    //{
    //    //    if (elementToAdd != null)
    //    //    {
    //    //        Node nodeToAdd = new Node(elementToAdd);
    //    //        if (basis != null)
    //    //            foreach (ITheoryElement element in basis)
    //    //            {
    //    //                nodeToAdd.basis.Add(searchNode(element));
    //    //            }

    //    //        theorylist.Add(elementToAdd);
    //    //        root.basis.Add(nodeToAdd);

    //    //        foreach (Node node in nodeToAdd.basis)
    //    //        {
    //    //            root.basis.Remove(node);
    //    //        }

    //    //        newreindexate(nodeToAdd);

    //    //        Count++;
    //    //        ContentUpdated(elementToAdd, null);
    //    //    }
    //    //    else throw new Exception("Unable to add element. It doesn't exists.");
    //    //}

    //    ///// <summary>
    //    ///// Delets desired element.
    //    ///// </summary>
    //    ///// <param name="element"></param>
    //    //public void Delete(ITheoryElement element)
    //    //{
    //    //    Node nodeToDelete = searchNode(element);
    //    //    if (!SearchAndDelete(nodeToDelete))
    //    //    {
    //    //        throw new Exception("Can't delete\n" + element);
    //    //    }
    //    //    theorylist.Remove(element);

    //    //    //foreach (Node node in GetAllNodes())
    //    //    //{
    //    //    //    foreach (Node basisNode in node.basis)
    //    //    //    {
    //    //    //        if (!node.basis.Contains(basisNode))
    //    //    //        {
    //    //    //            node.basis.Add(basisNode);
    //    //    //        }
    //    //    //    }
    //    //    //}

    //    //    Count--;
    //    //    //Updated(this);
    //    //    reindexate();
    //    //    ContentUpdated(null, element);
    //    //}

    //    ///// <summary>
    //    ///// Sets list of links on other elements of desired element.
    //    ///// </summary>
    //    ///// <param name="element"></param>
    //    ///// <param name="newbasis"></param>
    //    //public void SetBasis(ITheoryElement element, List<ITheoryElement> newbasis)
    //    //{
    //    //    Node node = searchNode(element);
    //    //    node.basis.Clear();
    //    //    if (newbasis != null)
    //    //        foreach (ITheoryElement elt in newbasis)
    //    //        {
    //    //            node.basis.Add(searchNode(elt));
    //    //        }
    //    //    reindexate();
    //    //    ContentUpdated(null, null);
    //    //}

    //    #endregion
    //}
}
