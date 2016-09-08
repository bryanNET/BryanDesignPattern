using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
访问者模式中具体访问者的数目和具体节点的数目没有任何关系。从访问者的结构图可以看出，访问者模式涉及以下几类角色。
抽象访问者角色（Vistor）:声明一个活多个访问操作，使得所有具体访问者必须实现的接口。
具体访问者角色（ConcreteVistor）：实现抽象访问者角色中所有声明的接口。
抽象节点角色（Element）：声明一个接受操作，接受一个访问者对象作为参数。
具体节点角色（ConcreteElement）：实现抽象元素所规定的接受操作。
结构对象角色（ObjectStructure）：节点的容器，可以包含多个不同类或接口的容器。

访问者模式的应用场景
 　　每个设计模式都有其应当使用的情况，那让我们看看访问者模式具体应用场景。如果遇到以下场景，此时我们可以考虑使用访问者模式。
如果系统有比较稳定的数据结构，而又有易于变化的算法时，此时可以考虑使用访问者模式。因为访问者模式使得算法操作的添加比较容易。
如果一组类中，存在着相似的操作，为了避免出现大量重复的代码，可以考虑把重复的操作封装到访问者中。（当然也可以考虑使用抽象类了）
如果一个对象存在着一些与本身对象不相干，或关系比较弱的操作时，为了避免操作污染这个对象，则可以考虑把这些操作封装到访问者对象中。

四、访问者模式的优缺点 
 　　访问者模式具有以下优点：
访问者模式使得添加新的操作变得容易。如果一些操作依赖于一个复杂的结构对象的话，那么一般而言，添加新的操作会变得很复杂。而使用访问者模式，增加新的操作就意味着添加一个新的访问者类。因此，使得添加新的操作变得容易。
访问者模式使得有关的行为操作集中到一个访问者对象中，而不是分散到一个个的元素类中。这点类似与"中介者模式"。
访问者模式可以访问属于不同的等级结构的成员对象，而迭代只能访问属于同一个等级结构的成员对象。
　　访问者模式也有如下的缺点：

增加新的元素类变得困难。每增加一个新的元素意味着要在抽象访问者角色中增加一个新的抽象操作，并在每一个具体访问者类中添加相应的具体操作。
 */
    #region 1
    public interface Element
    {
        void accept(Visitor visitor);
    }
    public class ConcreteElementA : Element
    {
        string name;
        public void SetName(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return this.name;
        }
        public void accept(Visitor visitor)
        {
            visitor.visitElementA(this);
        }
    }
    public class ConcreteElementB : Element
    {
        int ID;
        public void SetID(int id)
        {
            this.ID = id;
        }
        public int GetID()
        {
            return this.ID;
        }
        public void accept(Visitor visitor)
        {
            visitor.visitElementB(this);
        }
    }

    public interface Visitor
    {
        void visitElementA(ConcreteElementA ea);
        void visitElementB(ConcreteElementB eb);
    }
    public class ConcreteVisitorA : Visitor
    {
        public void visitElementA(ConcreteElementA ea)
        {
            Console.WriteLine("ConcreteVisitorA visit ElemantA:" + ea.GetName());
        }
        public void visitElementB(ConcreteElementB eb)
        {
            Console.WriteLine("ConcreteVisitorA visit ElemantB:" + eb.GetID());
        }
    }
    public class ConcreteVisitorB : Visitor
    {
        public void visitElementA(ConcreteElementA ea)
        {
            Console.WriteLine("ConcreteVisitorB visit ElemantA:" + ea.GetName());
        }
        public void visitElementB(ConcreteElementB eb)
        {
            Console.WriteLine("ConcreteVisitorB visit ElemantB:" + eb.GetID());
        }
    }

    public class objectStructure
    {
        List<Element> elementlist = new List<Element>();
        public void Attach(Element e)
        {
            elementlist.Add(e);
        }
        public void Dettach(Element e)
        {
            elementlist.Remove(e);
        }
        public void Accept(Visitor visit)
        {
            foreach (Element e in elementlist)
            {
                e.accept(visit);
            }
        }
    }
    #endregion

    #region 2

    // 抽象元素角色
    public abstract class Elementvp
    {
        public abstract void Accept(IVistor vistor);
        public abstract void Print();
    }

    // 具体元素A
    public class ElementA : Elementvp
    {
        public override void Accept(IVistor vistor)
        {
            // 调用访问者visit方法
            vistor.Visit(this);
        }
        public override void Print()
        {
            Console.WriteLine("我是元素A");
        }
    }

    // 具体元素B
    public class ElementB : Elementvp
    {
        public override void Accept(IVistor vistor)
        {
            vistor.Visit(this);
        }
        public override void Print()
        {
            Console.WriteLine("我是元素B");
        }
    }

    // 抽象访问者
    public interface IVistor
    {
        void Visit(ElementA a);
        void Visit(ElementB b);
    }

    // 具体访问者
    public class ConcreteVistor : IVistor
    {
        // visit方法而是再去调用元素的Accept方法
        public void Visit(ElementA a)
        {
            a.Print();
        }
        public void Visit(ElementB b)
        {
            b.Print();
        }
    }

    // 对象结构
    public class ObjectStructure
    {
        private ArrayList elements = new ArrayList();

        public ArrayList Elements
        {
            get { return elements; }
        }

        public ObjectStructure()
        {
            Random ran = new Random();
            for (int i = 0; i < 6; i++)
            {
                int ranNum = ran.Next(10);
                if (ranNum > 5)
                {
                    elements.Add(new ElementA());
                }
                else
                {
                    elements.Add(new ElementB());
                }
            }
        }
    }
    #endregion
}
