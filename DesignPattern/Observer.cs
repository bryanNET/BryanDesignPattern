using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*观察者模式的结构图有以下角色：

抽象主题角色（Subject）：抽象主题把所有观察者对象的引用保存在一个列表中，并提供增加和删除观察者对象的操作，抽象主题角色又叫做抽象被观察者角色，一般由抽象类或接口实现。
抽象观察者角色（Observer）：为所有具体观察者定义一个接口，在得到主题通知时更新自己，一般由抽象类或接口实现。
具体主题角色（ConcreteSubject）：实现抽象主题接口，具体主题角色又叫做具体被观察者角色。
具体观察者角色（ConcreteObserver）：实现抽象观察者角色所要求的接口，以便使自身状态与主题的状态相协调。

观察者模式的适用场景
 　　在下面的情况下可以考虑使用观察者模式：

当一个抽象模型有两个方面，其中一个方面依赖于另一个方面，将这两者封装在独立的对象中以使它们可以各自独立地改变和复用的情况下。从方面的这个词中可以想到，观察者模式肯定在AOP（面向方面编程）中有所体现，更多内容参考：Observern Pattern in AOP.
当对一个对象的改变需要同时改变其他对象，而又不知道具体有多少对象有待改变的情况下。
当一个对象必须通知其他对象，而又不能假定其他对象是谁的情况下。
五、观察者模式的优缺点
　　观察者模式有以下几个优点：

观察者模式实现了表示层和数据逻辑层的分离，并定义了稳定的更新消息传递机制，并抽象了更新接口，使得可以有各种各样不同的表示层，即观察者。
观察者模式在被观察者和观察者之间建立了一个抽象的耦合，被观察者并不知道任何一个具体的观察者，只是保存着抽象观察者的列表，每个具体观察者都符合一个抽象观察者的接口。
观察者模式支持广播通信。被观察者会向所有的注册过的观察者发出通知。
　　观察者也存在以下一些缺点：

如果一个被观察者有很多直接和间接的观察者时，将所有的观察者都通知到会花费很多时间。
虽然观察者模式可以随时使观察者知道所观察的对象发送了变化，但是观察者模式没有相应的机制使观察者知道所观察的对象是怎样发生变化的。
如果在被观察者之间有循环依赖的话，被观察者会触发它们之间进行循环调用，导致系统崩溃，在使用观察者模式应特别注意这点。
 */
    #region 1
    public interface Observer
    {
        void Update(Subject subject);
    }
    public abstract class Subject
    {
        List<Observer> obsList = new List<Observer>();
        public void AddObserver(Observer observer)
        {
            obsList.Add(observer);
        }
        public void RemoveObserver(Observer observer)
        {
            obsList.Remove(observer);
        }
        public void notity()
        {
            foreach (Observer o in obsList)
            {
                o.Update(this);
            }
        }
        private string _state;
        public void SetState(string state)
        {
            this._state = state;
        }
        public string GetState()
        {
            return this._state;
        }
    }
    public class ConcreteSubject : Subject
    {
    }
    public class ConcreteObserver1 : Observer
    {
        public void Update(Subject subject)
        {
            Console.WriteLine("混凝土Ob服务器1得到通知 ConcreteObserver1 get notice:" + subject.GetState());
        }
    }
    public class ConcreteObserver2 : Observer
    {
        public void Update(Subject subject)
        {
            Console.WriteLine("混凝土Ob服务器2得到通知 ConcreteObserver2 get notice:" + subject.GetState());
        }
    } 
    #endregion

    #region 2
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    //事件委托的方式
    public delegate void updateDelegate(Subject subject);

    public class EventSubjet : Subject
    {
        public event updateDelegate UpdateHandler;
        public void EventNotify()
        {
            OnUpdate();
        }
        private void OnUpdate()
        {
            if (UpdateHandler != null)
            {
                UpdateHandler(this);
            }
        }
    }
    #endregion

    #region 3
    //使用事件和委托实现的观察者模式中，减少了订阅者接口类的定义，此时，.NET中的委托正式充到订阅者接口类的角色。使用委托和事件，确实简化了观察者模式的实现，减少了一个IObserver接口的定义


    // 委托充当订阅者接口类
    public delegate void NotifyEventHandler(object sender);

    // 抽象订阅号类
    public class TenXun
    {
        public NotifyEventHandler NotifyEvent;

        public string Symbol { get; set; }
        public string Info { get; set; }
        public TenXun(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        #region 新增对订阅号列表的维护操作
        public void AddObserver(NotifyEventHandler ob)
        {
            NotifyEvent += ob;
        }
        public void RemoveObserver(NotifyEventHandler ob)
        {
            NotifyEvent -= ob;
        }

        #endregion

        public void Update()
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this);
            }
        }
    }

    // 具体订阅号类
    public class TenXunGame : TenXun
    {
        public TenXunGame(string symbol, string info)
            : base(symbol, info)
        {
        }
    }

    // 具体订阅者类
    public class Subscriber
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrint(Object obj)
        {
            TenXun tenxun = obj as TenXun;

            if (tenxun != null)
            {
                Console.WriteLine("通知 Notified {0} of {1}'s" + " 信息是Info is: {2}", Name, tenxun.Symbol, tenxun.Info);
            }
        }
    }
    #endregion
}
