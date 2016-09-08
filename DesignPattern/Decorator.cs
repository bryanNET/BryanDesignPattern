using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    //装饰者模式采用对象组合而非继承的方式实现了再运行时动态地扩展对象功能的能力，而且可以根据需要扩展多个功能，避免了单独使用继承带来的 ”灵活性差“和”多子类衍生问题“。同时它很好地符合面向对象设计原则中 ”优先使用对象组合而非继承“和”开放-封闭“原则。

    /// <summary>
    /// 即装饰者模式中的抽象组件类
    /// </summary>
    public abstract class Car
    {
        public string color { get; set; }
        public int compartment { get; set; }
        public void run()
        {
            Console.WriteLine(color + " " + compartment + " compartment " + this.GetType().Name + "  is running!");
        }
    }

    /// <summary>
    /// 即装饰着模式中的具体组件类
    /// </summary>
    public class Benz : Car
    {
        public Benz()
        {
            base.color = "black";
            base.compartment = 1;
        }
    }
    public class QQ : Car
    {
        public QQ()
        {
            base.color = "black";
            base.compartment = 1;
        }
    }

    /// <summary>
    /// 装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Car
    /// </summary>
    public abstract class Decorator : Car
    {
        public Car car;
        public Decorator(Car car)
        {
            this.car = car;
        }
    }
    /// <summary>
    /// 即具体装饰者
    /// </summary>
    public class ColorDecorator : Decorator
    {
        //一般在构造函数内完成属性的修改（装饰），这里单独加了一个decorate方法
        public ColorDecorator(Car car) : base(car)
        {
        }
        public Car decorate(string color)
        {
            base.car.color = color;
            return base.car;
        }
    }
    /// <summary>
    ///Compartment室    即具体装饰者
    /// </summary>
    public class CompartmentDecorator : Decorator
    {
        public CompartmentDecorator(Car car)
            : base(car)
        {
        }
        public Car decorate(int compartment)
        {
            base.car.compartment = compartment;
            return base.car;
        }
    }
    #region 以手机和手机配件的例子来演示装饰者模式的实现
    /*
     * 装饰者模式中各个角色有：
抽象构件（Phone）角色：给出一个抽象接口，以规范准备接受附加责任的对象。
具体构件（AppPhone）角色：定义一个将要接收附加责任的类。
装饰（Dicorator）角色：持有一个构件（Component）对象的实例，并定义一个与抽象构件接口一致的接口。
具体装饰（Sticker和Accessories）角色：负责给构件对象 ”贴上“附加的责任。*/

    /// <summary>
    /// 手机抽象类，即装饰者模式中的抽象组件类
    /// </summary>
    public abstract class Phone
    {
        public abstract void Print();
    }

    /// <summary>
    /// 苹果手机，即装饰着模式中的具体组件类
    /// </summary>
    public class ApplePhone : Phone
    {
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("开始执行具体的对象——苹果手机");
        }
    }

    /// <summary>
    /// 装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Photo
    /// </summary>
    public abstract class Decorator2 : Phone
    {
        private Phone phone;

        public Decorator2(Phone p)
        {
            this.phone = p;
        }

        public override void Print()
        {
            if (phone != null)
            {
                phone.Print();
            }
        }
    }

    /// <summary>
    /// 贴膜，即具体装饰者
    /// </summary>
    public class Sticker : Decorator2
    {
        public Sticker(Phone p)
            : base(p)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddSticker();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddSticker()
        {
            Console.WriteLine("现在苹果手机有贴膜了");
        }
    }

    /// <summary>
    /// 手机挂件
    /// </summary>
    public class Accessories : Decorator2
    {
        public Accessories(Phone p)
            : base(p)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddAccessories();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddAccessories()
        {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }
    }
    #endregion
}
