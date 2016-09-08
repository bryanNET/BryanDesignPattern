using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    //享元模式（Flyweight Pattern）

    /*
抽象享元角色（Flyweight）:此角色是所有的具体享元类的基类，为这些类规定出需要实现的公共接口。那些需要外部状态的操作可以通过调用方法以参数形式传入。
具体享元角色（ConcreteFlyweight）：实现抽象享元角色所规定的接口。如果有内部状态的话，可以在类内部定义。
享元工厂角色（FlyweightFactory）：本角色复杂创建和管理享元角色。本角色必须保证享元对象可以被系统适当地共享，当一个客户端对象调用一个享元对象的时候，享元工厂角色检查系统中是否已经有一个符合要求的享元对象，如果已经存在，享元工厂角色就提供已存在的享元对象，如果系统中没有一个符合的享元对象的话，享元工厂角色就应当创建一个合适的享元对象。
客户端角色（Client）：本角色需要存储所有享元对象的外部状态
注：上面的实现只是单纯的享元模式，同时还有复合的享元模式，由于复合享元模式较复杂，这里就不给出实现了。


享元模式的优缺点
分析完享元模式的实现之后，让我们继续分析下享元模式的优缺点：

优点：
降低了系统中对象的数量，从而降低了系统中细粒度对象给内存带来的压力。
缺点：
为了使对象可以共享，需要将一些状态外部化，这使得程序的逻辑更复杂，使系统复杂化。
享元模式将享元对象的状态外部化，而读取外部状态使得运行时间稍微变长。

四、使用场景
在下面所有条件都满足时，可以考虑使用享元模式：

一个系统中有大量的对象；
这些对象耗费大量的内存；
这些对象中的状态大部分都可以被外部化
这些对象可以按照内部状态分成很多的组，当把外部对象从对象中剔除时，每一个组都可以仅用一个对象代替
软件系统不依赖这些对象的身份，
满足上面的条件的系统可以使用享元模式。但是使用享元模式需要额外维护一个记录子系统已有的所有享元的表，而这也需要耗费资源，所以，应当在有足够多的享元实例可共享时才值得使用享元模式。
*/
    #region  享元模式1
    /// <summary>
    /// 享元工厂角色
    /// </summary>
    public class FlyweightFactory
    {
        static Dictionary<string, IFlyweight> pendic = new Dictionary<string, IFlyweight>();
        public IFlyweight getPen(string color)
        {
            if (pendic.ContainsKey(color))
            {
                return pendic[color];
            }
            else
            {
                IFlyweight pen = new ConcreteFlyweight(color);
                pendic.Add(color, pen);
                return pen;
            }
        }
        public void Display()
        {
            foreach (KeyValuePair<string, IFlyweight> pair in pendic)
            {
                Console.WriteLine(pair.Value.GetType().FullName + ":" + pair.Key);
            }
        }
    }

    public interface IFlyweight
    {
        string GetColor();
    };
    public class ConcreteFlyweight : IFlyweight
    {
        string color;
        public ConcreteFlyweight(string color)
        {
            this.color = color;
        }
        public string GetColor()
        {
            return this.color;
        }
    }
    #endregion


    #region 享元模式2
    /// <summary>
    /// 享元工厂，负责创建和管理享元对象
    /// </summary>
    public class FlyweightFactory2
    {
        // 最好使用泛型Dictionary<string,Flyweighy>
       // public Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();
        public Hashtable flyweights = new Hashtable();

        public FlyweightFactory2()
        {
            flyweights.Add("A", new ConcreteFlyweight2("A"));
            flyweights.Add("B", new ConcreteFlyweight2("B"));
            flyweights.Add("C", new ConcreteFlyweight2("C"));
        }

        public Flyweight GetFlyweight(string key)
        {
            // 更好的实现如下
            //Flyweight flyweight = flyweights[key] as Flyweight;
            //if (flyweight == null)
            //{
            // Console.WriteLine("驻留池中不存在字符串" + key);
            // flyweight = new ConcreteFlyweight(key);
            //}
            //return flyweight;

            return flyweights[key] as Flyweight;
        }
    }

    /// <summary>
    ///  抽象享元类，提供具体享元类具有的方法
    /// </summary>
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicstate);
    }

    // 具体的享元对象，这样我们不把每个字母设计成一个单独的类了，而是作为把共享的字母作为享元对象的内部状态
    public class ConcreteFlyweight2 : Flyweight
    {
        // 内部状态
        private string intrinsicstate;

        // 构造函数
        public ConcreteFlyweight2(string innerState)
        {
            this.intrinsicstate = innerState;
        }

        /// <summary>
        /// 享元类的实例方法
        /// </summary>
        /// <param name="extrinsicstate">外部状态</param>
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("具体实现类: intrinsicstate内在状态 {0}, extrinsicstate外在状态{1}", intrinsicstate, extrinsicstate);
        }
    }
    #endregion

    #region  
    /*网站分类：产品展示 用户：小菜
网站分类：产品展示 用户：大鸟
网站分类：产品展示 用户：娇娇
网站分类：博客 用户：老顽童
网站分类：博客 用户：桃谷六仙
网站分类：博客 用户：南海鳄神
得到网站分类总数为 2
*/
    //用户
    public class User
    {
        private string name;

        public User(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }


    //网站工厂
    class WebSiteFactory
    {
        private Hashtable flyweights = new Hashtable();

        //获得网站分类
        public WebSite GetWebSiteCategory(string key)
        {
            if (!flyweights.ContainsKey(key))
                flyweights.Add(key, new ConcreteWebSite(key));
            return ((WebSite)flyweights[key]);
        }

        //获得网站分类总数
        public int GetWebSiteCount()
        {
            return flyweights.Count;
        }
    }

    //网站
    abstract class WebSite
    {
        public abstract void Use(User user);
    }

    //具体的网站
    class ConcreteWebSite : WebSite
    {
        private string name = "";
        public ConcreteWebSite(string name)
        {
            this.name = name;
        }

        public override void Use(User user)
        {
            Console.WriteLine("网站分类：" + name + " 用户：" + user.Name);
        }
    }
    #endregion
}
