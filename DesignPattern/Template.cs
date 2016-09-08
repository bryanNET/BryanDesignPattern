using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
     模板方法模式中涉及了两个角色：

抽象模板角色（Vegetable扮演这个角色）：定义了一个或多个抽象操作，以便让子类实现，这些抽象操作称为基本操作。
具体模板角色（ChineseCabbage和Spinach扮演这个角色）：实现父类所定义的一个或多个抽象方法。

        模板方法模式的优缺点
下面让我们继续分析下模板方法的优缺点。

优点：

实现了代码复用
能够灵活应对子步骤的变化，符合开放-封闭原则
缺点：因为引入了一个抽象类，如果具体实现过多的话，需要用户或开发人员需要花更多的时间去理清类之间的关系。

 附：在.NET中模板方法的应用也很多，例如我们在开发自定义的Web控件或WinForm控件时，我们只需要重写某个控件的部分方法。

四、总结
到这里，模板方法的介绍就结束了，模板方法模式在抽象类中定义了算法的实现步骤，将这些步骤的实现延迟到具体子类中去实现，从而使所有子类复用了父类的代码，所以模板方法模式是基于继承的一种实现代码复用的技术。
*/
    public abstract class Template
    {/// <summary>
     /// 煮水
        /// </summary>
        protected void boilWater()
        {
            Console.WriteLine("煮水boil water");
        }
        /// <summary>
        /// 酿造
        /// </summary>
        protected virtual void brew()
        {
            Console.WriteLine("酿造brew");
        }
        /// <summary>
        /// 倒入杯子
        /// </summary>
        protected void pourInCup()
        {
            Console.WriteLine("倒入杯子pour into cup");
        }
        /// <summary>
        /// 添加其他
        /// </summary>
        protected virtual void addOther()
        {
            Console.WriteLine("添加add other");
        }

        /// <summary>
        ///  // 模板方法，不要把模版方法定义为Virtual或abstract方法，避免被子类重写，防止更改流程的执行顺序
        /// </summary>
        public void makeBeverage()
        {
            boilWater();
            brew();
            pourInCup();
            addOther();
        }
    }
    public class Tea : Template
    {
        /// <summary>
        /// 茶
        /// </summary>
        protected override void brew()
        {
            Console.WriteLine("茶 tea");
        }
        /// <summary>
        /// 柠檬
        /// </summary>
        protected override void addOther()
        {
            Console.WriteLine("柠檬 add lemon");
        }
    }
    public class Coffee : Template
    {
        /// <summary>
        /// 咖啡
        /// </summary>
        protected override void brew()
        {
            Console.WriteLine("咖啡 coffee");
        }
        /// <summary>
        /// 糖
        /// </summary>
        protected override void addOther()
        {
            Console.WriteLine("糖 add sugar");
        }
    }
}
