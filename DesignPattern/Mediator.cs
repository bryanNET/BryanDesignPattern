using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
     中介者模式的适用场景
 　　一般在以下情况下可以考虑使用中介者模式：

一组定义良好的对象，现在要进行复杂的相互通信。
想通过一个中间类来封装多个类中的行为，而又不想生成太多的子类。
四、中介者模式的优缺点
　　中介者模式具有以下几点优点：

简化了对象之间的关系，将系统的各个对象之间的相互关系进行封装，将各个同事类解耦，使得系统变为松耦合。
提供系统的灵活性，使得各个同事对象独立而易于复用。
　　然而，中介者模式也存在对应的缺点：

中介者模式中，中介者角色承担了较多的责任，所以一旦这个中介者对象出现了问题，整个系统将会受到重大的影响。例如，QQ游戏中计算欢乐豆的程序出错了，这样会造成重大的影响。
新增加一个同事类时，不得不去修改抽象中介者类和具体中介者类，此时可以使用观察者模式和状态模式来解决这个问题。
         */
    #region 1
    public abstract class Person
    {
        public string name;
        public Mediator mediator;
        public Person(string name, Mediator mediator)
        {
            this.name = name;
            this.mediator = mediator;
        }
        public void Contact(string msg)
        {
            //参数 this 代表 消息来自我
            this.mediator.SendMsg(msg, this);
        }

        internal void GetMsg(string msg)
        {
            Console.WriteLine(this.name + " 收到消息：" + msg);
        }
    }
    public class HouseOwner : Person
    {
        public HouseOwner(string name, Mediator mediator) : base(name, mediator) { }
    }
    public class Tenant : Person
    {
        public Tenant(string name, Mediator mediator) : base(name, mediator) { }
    }

    public interface Mediator
    {
        void SendMsg(string msg, Person p);
    }
    public class ConcreteMediator : Mediator
    {
        HouseOwner houseOwner;
        Tenant tenant;
        public ConcreteMediator()
        {
        }
        public void SetHouseOwner(HouseOwner houseOwner)
        {
            this.houseOwner = houseOwner;
        }
        public void SetTenant(Tenant tenant)
        {
            this.tenant = tenant;
        }
        public void SendMsg(string msg, Person p)
        {
            if (p.GetType() == houseOwner.GetType())
            {
                tenant.GetMsg(msg);
            }
            else
            {
                houseOwner.GetMsg(msg);
            }
        }
    }
    #endregion

    #region 抽象牌友类2
    //以现实生活中打牌的例子来实现下中介者模式。在现实生活中，两个人打牌，如果某个人赢了都会影响到对方状态的改变。
    //如果其中牌友A发生变化时，此时就会影响到牌友B的状态，如果涉及的对象变多的话，这时候某一个牌友的变化将会影响到其他所有相关联的牌友状态。例如牌友A算错了钱，这时候牌友A和牌友B的钱数都不正确了，如果是多个人打牌的话，影响的对象就会更多。这时候就会思考——能不能把算钱的任务交给程序或者算数好的人去计算呢，这时候就有了我们QQ游戏中的欢乐斗地主等牌类游戏了。所以上面的设计，我们还是有进一步完善的方案的，即加入一个中介者对象来协调各个对象之间的关联，这也就是中介者模式的应用了
  
        // 抽象牌友类
    public abstract class AbstractCardPartner
    {
        public int MoneyCount { get; set; }

        public AbstractCardPartner()
        {
            MoneyCount = 0;
        }

        public abstract void ChangeCount(int Count, AbstractMediator mediator);
    }

    // 牌友A类
    public class ParterA : AbstractCardPartner
    {
        // 依赖与抽象中介者对象
        public override void ChangeCount(int Count, AbstractMediator mediator)
        {
            mediator.AWin(Count);
        }
    }

    // 牌友B类
    public class ParterB : AbstractCardPartner
    {
        // 依赖与抽象中介者对象
        public override void ChangeCount(int Count, AbstractMediator mediator)
        {
            mediator.BWin(Count);
        }
    }

    // 抽象中介者类
    public abstract class AbstractMediator
    {
        protected AbstractCardPartner A;
        protected AbstractCardPartner B;
        public AbstractMediator(AbstractCardPartner a, AbstractCardPartner b)
        {
            A = a;
            B = b;
        }

        public abstract void AWin(int count);
        public abstract void BWin(int count);
    }

    // 具体中介者类
    public class MediatorPater : AbstractMediator
    {
        public MediatorPater(AbstractCardPartner a, AbstractCardPartner b)
            : base(a, b)
        {
        }

        public override void AWin(int count)
        {
            A.MoneyCount += count;
            B.MoneyCount -= count;
        }

        public override void BWin(int count)
        {
            B.MoneyCount += count;
            A.MoneyCount -= count;
        }
    }

    #endregion
}
