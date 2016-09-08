using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*组合模式中涉及到三个角色：

抽象构件（Component）角色：这是一个抽象角色，上面实现中Graphics充当这个角色，它给参加组合的对象定义出了公共的接口及默认行为，可以用来管理所有的子对象（在透明式的组合模式是这样的）。在安全式的组合模式里，构件角色并不定义出管理子对象的方法，这一定义由树枝结构对象给出。
树叶构件（Leaf）角色：树叶对象时没有下级子对象的对象，上面实现中Line和Circle充当这个角色，定义出参加组合的原始对象的行为
树枝构件（Composite）角色：代表参加组合的有下级子对象的对象，上面实现中ComplexGraphics充当这个角色，树枝对象给出所有管理子对象的方法实现，如Add、Remove等。

        优点：

组合模式使得客户端代码可以一致地处理对象和对象容器，无需关系处理的单个对象，还是组合的对象容器。
将”客户代码与复杂的对象容器结构“解耦。
可以更容易地往组合对象中加入新的构件。
缺点：使得设计更加复杂。客户端需要花更多时间理清类之间的层次关系。（这个是几乎所有设计模式所面临的问题）。

注意的问题：

有时候系统需要遍历一个树枝结构的子构件很多次，这时候可以考虑把遍历子构件的结构存储在父构件里面作为缓存。
客户端尽量不要直接调用树叶类中的方法（在我上面实现就是这样的，创建的是一个树枝的具体对象，应该使用Graphics complexGraphics = new ComplexGraphics("一个复杂图形和两条线段组成的复杂图形");），而是借用其父类（Graphics）的多态性完成调用，这样可以增加代码的复用性。
*/
    #region 组合模式1

    /// <summary>
    /// 文件   
    /// 抽象类 抽象构件（Component）角色
    /// </summary>
    public abstract class File
    {
        protected string name;
        public File(string name)
        {
            this.name = name;
        }
        public abstract void Display();
    }

    /// <summary>
    /// 文件夹
    /// 树枝构件（Composite）角色
    /// </summary>
    public class Folder : File
    {
        IList<File> list;


        public Folder(string name)
            : base(name)
        {
            list = new List<File>();
        }
        public void AddFile(File file)
        {
            list.Add(file);
        }
        public void RemoveFile(File file)
        {
            list.Remove(file);
        }
        public override void Display()
        {
            Console.WriteLine("folder:" + this.name);
            foreach (File f in list)
            {
                f.Display();
            }
        }
    }
    /// <summary>
    /// Image文件
    /// 树叶构件（Leaf）角色
    /// </summary>
    public class ImageFile : File
    {
        public ImageFile(string name)
            : base(name)
        {
        }
        public override void Display()
        {
            Console.WriteLine("ImageFile:" + this.name);
        }
    }
    #endregion

    #region 组合模式2 结构图

    /*结构图：
-北京总公司
---总公司人力资源部
---总公司财务部
---上海华东分公司
-----华东分公司人力资源部
-----华东分公司财务部
-----南京办事处
-------南京办事处人力资源部
-------南京办事处财务部
-----杭州办事处
-------杭州办事处人力资源部
-------杭州办事处财务部

职责：
总公司人力资源部 员工招聘培训管理
总公司财务部 公司财务收支管理
华东分公司人力资源部 员工招聘培训管理
华东分公司财务部 公司财务收支管理
南京办事处人力资源部 员工招聘培训管理
南京办事处财务部 公司财务收支管理
杭州办事处人力资源部 员工招聘培训管理
杭州办事处财务部 公司财务收支管理
*/
    abstract class Company
    {
        protected string name;

        public Company(string name)
        {
            this.name = name;
        }

        public abstract void Add(Company c);//增加
        public abstract void Remove(Company c);//移除
        public abstract void Display(int depth);//显示
        public abstract void LineOfDuty();//履行职责

    }

    class ConcreteCompany : Company
    {
        private List<Company> children = new List<Company>();

        public ConcreteCompany(string name)
            : base(name)
        { }

        public override void Add(Company c)
        {
            children.Add(c);
        }

        public override void Remove(Company c)
        {
            children.Remove(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            foreach (Company component in children)
            {
                component.Display(depth + 2);
            }
        }

        //履行职责
        public override void LineOfDuty()
        {
            foreach (Company component in children)
            {
                component.LineOfDuty();
            }
        }

    }

    //人力资源部
    class HRDepartment : Company
    {
        public HRDepartment(string name)
            : base(name)
        { }

        public override void Add(Company c)
        {
        }

        public override void Remove(Company c)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }


        public override void LineOfDuty()
        {
            Console.WriteLine("{0} 员工招聘培训管理", name);
        }
    }

    //财务部
    class FinanceDepartment : Company
    {
        public FinanceDepartment(string name)
            : base(name)
        { }

        public override void Add(Company c)
        {
        }

        public override void Remove(Company c)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }

        public override void LineOfDuty()
        {
            Console.WriteLine("{0} 公司财务收支管理", name);
        }

    }
    #endregion
}
