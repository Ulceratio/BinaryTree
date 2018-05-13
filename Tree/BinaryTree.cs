using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    internal class BinaryTree<MainType> where MainType : IComparable
    {
        #region Fields
        private IEnumerable<MainType> sourceSetOfElements;
        private Element _Root { get; set; }
        public Element Root
        {
            get
            {
                return _Root;
            }
        }
        #endregion

        #region Constructors
        public BinaryTree(IEnumerable<MainType> sourceSetOfElements,string typeOfBinaryTree)
        {
            if(typeOfBinaryTree == "Simple")
            {
                this.sourceSetOfElements = sourceSetOfElements;
                _Root = new Element(sourceSetOfElements.ElementAt(0));
                CreateSimpleTree();
            }            
            if(typeOfBinaryTree == "AVL")
            {
                this.sourceSetOfElements = sourceSetOfElements;
                _Root = null;
                CreateAVLTree();
            }
        }

        public BinaryTree() { }

        private void CreateSimpleTree()
        {
            foreach (var element in sourceSetOfElements)
            {
                SearchWithInsert(element);
            }
        }

        private void CreateAVLTree()
        {
            foreach (var element in sourceSetOfElements)
            {
                _Root = BalancedSearchWithInsertRecursion(_Root,new Element(element)).Item1;
            }
        }
        #endregion    

        #region Operations with Tree
        public MainType SearchWithInsert(MainType Key)
        {
            Element tempElement = null;
            Element searchElement = new Element(Key);
            while (true)
            {
                if (tempElement == null)
                {
                    if (_Root > searchElement)
                    {
                        if (_Root.LLink != null)
                        {
                            tempElement = _Root.LLink;
                            continue;
                        }
                        if (_Root.LLink == null)
                        {
                            _Root.LLink = searchElement;
                            return _Root.LLink;
                        }
                    }
                    if (_Root < searchElement)
                    {
                        if (_Root.RLink != null)
                        {
                            tempElement = _Root.RLink;
                            continue;
                        }
                        if (_Root.RLink == null)
                        {
                            _Root.RLink = searchElement;
                            return _Root.RLink;
                        }
                    }
                    if (_Root == searchElement)
                    {
                        return _Root;
                    }
                }
                if (tempElement != null)
                {

                    if (tempElement > searchElement)
                    {
                        if (tempElement.LLink != null)
                        {
                            tempElement = tempElement.LLink;
                            continue;
                        }
                        if (tempElement.LLink == null)
                        {
                            tempElement.LLink = searchElement;
                            return tempElement.LLink;
                        }
                    }
                    if (tempElement < searchElement)
                    {
                        if (tempElement.RLink != null)
                        {
                            tempElement = tempElement.RLink;
                            continue;
                        }
                        if (tempElement.RLink == null)
                        {
                            tempElement.RLink = searchElement;
                            return tempElement.RLink;
                        }
                    }
                    if (tempElement == searchElement)
                    {
                        return tempElement;
                    }
                }
            }
        }

        public bool Delete(MainType Key)
        {
            Element tempElement = _Root;
            Element keyElement = new Element(Key);

            if(_Root == keyElement)
            {
                Insert(_Root.LLink, _Root.RLink);
                _Root = _Root.RLink;
                return true;
            }

            while (true)
            {
                if (keyElement > tempElement)
                {
                    if (tempElement.RLink == null)
                    {
                        return false;
                    }
                    if (tempElement.RLink != null)
                    {
                        if(tempElement.RLink == keyElement) // 12
                        {
                            if(tempElement.RLink.RLink != null)
                            {
                                if(tempElement.RLink.LLink != null)
                                {
                                    Insert(tempElement.RLink.LLink, tempElement.RLink.RLink);
                                    tempElement.RLink = tempElement.RLink.RLink;
                                    return true;
                                }
                                if(tempElement.RLink.LLink == null)
                                {
                                    tempElement.RLink = tempElement.RLink.RLink;
                                    return true;
                                }
                            }
                            if (tempElement.RLink.RLink == null)
                            {
                                if(tempElement.RLink.LLink != null)
                                {
                                    tempElement.RLink = tempElement.RLink.LLink;
                                }
                                else
                                {
                                    tempElement.RLink = null;
                                }
                                return true;
                            }
                        }
                        else
                        {
                            tempElement = tempElement.RLink;
                            continue;
                        }                       
                    }
                }
                if (keyElement < tempElement)
                {
                    if (tempElement.LLink == null)
                    {
                        return false;
                    }
                    if (tempElement.LLink != null)
                    {
                        if (tempElement.LLink == keyElement) // 12
                        {
                            if (tempElement.LLink.RLink != null)
                            {
                                if (tempElement.LLink.LLink != null)
                                {
                                    Insert(tempElement.LLink.LLink, tempElement.LLink.RLink);
                                    tempElement.LLink = tempElement.LLink.RLink;
                                    return true;
                                }
                                if (tempElement.LLink.LLink == null)
                                {
                                    tempElement.LLink = tempElement.LLink.RLink;
                                    return true;
                                }
                            }
                            if (tempElement.LLink.RLink == null)
                            {
                                if (tempElement.LLink.LLink != null)
                                {
                                    tempElement.LLink = tempElement.LLink.LLink;
                                }
                                else
                                {
                                    tempElement.LLink = null;
                                }
                                return true;
                            }
                        }
                        else
                        {
                            tempElement = tempElement.LLink;
                            continue;
                        }
                    }
                }
                if (keyElement == tempElement)
                {
                    return false;
                }
            }

        }

        public bool Insert(Element whatInsert, Element whereInsert)
        {
            if(whereInsert == null)
            {
                whereInsert = _Root;
            }
            Element tempElement = whereInsert;
            while (true)
            {
                if(whatInsert > tempElement)
                {
                    if(tempElement.RLink == null)
                    {
                        tempElement.RLink = whatInsert;
                        return true;
                    }
                    if(tempElement.RLink != null)
                    {
                        tempElement = tempElement.RLink;
                        continue;
                    }
                }
                if (whatInsert < tempElement)
                {
                    if (tempElement.LLink == null)
                    {
                        tempElement.LLink = whatInsert;
                        return true;
                    }
                    if (tempElement.LLink != null)
                    {
                        tempElement = tempElement.LLink;
                        continue;
                    }
                }
                if(whatInsert == tempElement)
                {
                    return false;
                }
            }
        }

        #region AVL Tree      

        public Tuple<Element,MainType> BalancedSearchWithInsertRecursion(Element element, Element keyElement)
        {
            if (element == null)
            {
                return new Tuple<Element, MainType>((Element)keyElement.Clone(), keyElement);
            }
            if (keyElement < element)
            {
                element.LLink = BalancedSearchWithInsertRecursion(element.LLink, keyElement).Item1;
            }
            if(keyElement > element)
            {
                element.RLink = BalancedSearchWithInsertRecursion(element.RLink, keyElement).Item1;
            }
            if(keyElement == element)
            {
                return new Tuple<Element, MainType>(null,element);
            }
            return new Tuple<Element, MainType>(BalanceElement(element),keyElement);
        }

        #region Rotations
        public Element RightRotation(Element ElementToRotate)
        {
            Element leftLink = ElementToRotate.LLink;
            ElementToRotate.LLink = leftLink.RLink;
            leftLink.RLink = ElementToRotate;
            Element tempElement = ElementToRotate;
            ElementToRotate = leftLink;
            ElementToRotate.RLink = tempElement;
            return ElementToRotate;
        }

        public Element LeftRotation(Element ElementToRotate)
        {
            Element rightLink = ElementToRotate.RLink;
            ElementToRotate.RLink = rightLink.LLink;
            rightLink.LLink = ElementToRotate;
            Element tempElement = ElementToRotate;
            ElementToRotate = rightLink;
            ElementToRotate.LLink = tempElement;
            return ElementToRotate;
        }
        #endregion

        #region Balancing
        public Element BalanceElement(Element ElementToBalance)
        {
            int TempBalanceFactor = BalanceFactor(ElementToBalance);
            if (TempBalanceFactor < -1)
            {
                if (BalanceFactor(ElementToBalance.RLink) > 0)
                {
                    ElementToBalance.RLink = RightRotation(ElementToBalance.RLink);
                }
                return LeftRotation(ElementToBalance);
            }
            if(TempBalanceFactor > 1)
            {
                if (BalanceFactor(ElementToBalance.LLink) < 0)
                {
                    ElementToBalance.LLink = LeftRotation(ElementToBalance.LLink);
                }
                return RightRotation(ElementToBalance);
            }
            return ElementToBalance;
        }

        private int GetElementHeight(Element element)
        {
            int Height = 0;
            if (element != null)
            {
                int leftHight = GetElementHeight(element.LLink);
                int righHeight = GetElementHeight(element.RLink);
                Height = (leftHight > righHeight ? leftHight : righHeight) + 1;
            }
            return Height;
        }

        private int BalanceFactor(Element element)
        {
            if(element != null)
            {
                int leftHeight = GetElementHeight(element.LLink ?? null);
                int rightheight = GetElementHeight(element.RLink ?? null);
                int TempBalanceFactor = leftHeight - rightheight;
                return TempBalanceFactor;
            }
            return 0;
        }
        #endregion

        #endregion

        #endregion

        #region Base element
        public class Element:ICloneable
        {
            #region Fields
            private MainType Key;

            public Element RLink { get; set; }

            public Element LLink { get; set; }

            #endregion

            #region Constructors

            public Element() { }

            public Element(MainType Key)
            {
                this.Key = Key;
                RLink = null;
                LLink = null;
            }
            #endregion

            #region Functions
            public object Clone()
            {
                return new Element() { Key = this.Key, RLink = this.RLink, LLink = this.LLink};
            }   
            #endregion

            #region Compare Operators
            public static bool operator >(Element element1,Element element2)
            {
                return element1.Key.CompareTo(element2.Key) > 0;
            }

            public static bool operator <(Element element1, Element element2)
            {
                
                return element1.Key.CompareTo(element2.Key) < 0;
            }

            public static bool operator ==(Element element1, Element element2)
            {
                if(Object.ReferenceEquals(element1,null) && Object.ReferenceEquals(element2,null))
                {
                    return true;
                }
                if(!Object.ReferenceEquals(element1, null) && Object.ReferenceEquals(element2, null))
                {
                    return false;
                }
                return element1.Key.Equals(element2.Key);
            }

            public static bool operator !=(Element element1, Element element2)
            {
                if (!(element1 == element2))
                {
                    return true;
                }
                if(!Object.ReferenceEquals(element1, null) && Object.ReferenceEquals(element2, null))
                {
                    return false;
                }
                if (Object.ReferenceEquals(element1, null) && Object.ReferenceEquals(element2, null))
                {
                    return false;
                }
                return element1.Key.Equals(element2.Key);
            }
            #endregion

            #region Converters
            public static implicit operator MainType(Element element)
            {
                return element.Key;
            }

            public override string ToString()
            {
                return Key.ToString();
            }
            #endregion
        }
        #endregion

        #region Converters
        public override string ToString()
        {
            string result = "";

            void printTree(Element element)
            {
                if (element == null) 
                {
                    return;
                }

                printTree(element.LLink);
                result += element.ToString() + ",";
                printTree(element.RLink); 
            }

            printTree(_Root);
            return result;
        }

        public string ToString(string parameters)
        {
            string result = "";

            void printTree(Element element)
            {
                if (element == null)
                {
                    return;
                }
                if(parameters == "desc")
                {
                    printTree(element.LLink);
                    result += element.ToString() + ",";
                    printTree(element.RLink);
                }
                if(parameters == "!desc")
                {
                    result += element.ToString() + "|";
                    printTree(element.LLink);
                    printTree(element.RLink);
                }
                if (parameters == "")
                {
                    printTree(element.LLink);
                    printTree(element.RLink);
                    result += element.ToString() + ";";
                }
            }

            printTree(_Root);
            return result;
        }
        #endregion
    }
}
