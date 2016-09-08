using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
及两个角色：

抽象处理者角色（Handler）：定义出一个处理请求的接口。这个接口通常由接口或抽象类来实现。
具体处理者角色（ConcreteHandler）：具体处理者接受到请求后，可以选择将该请求处理掉，或者将请求传给下一个处理者。因此，每个具体处理者需要保存下一个处理者的引用，以便把请求传递下去。

责任链模式的适用场景 
　　在以下场景中可以考虑使用责任链模式：
一个系统的审批需要多个对象才能完成处理的情况下，例如请假系统等。
代码中存在多个if-else语句的情况下，此时可以考虑使用责任链模式来对代码进行重构。

四、责任链模式的优缺点
　　责任链模式的优点不言而喻，主要有以下点：
降低了请求的发送者和接收者之间的耦合。
把多个条件判定分散到各个处理类中，使得代码更加清晰，责任更加明确。
　　责任链模式也具有一定的缺点，如：
在找到正确的处理对象之前，所有的条件判定都要执行一遍，当责任链过长时，可能会引起性能的问题
可能导致某个请求不被处理。

*/
    #region 1
    public class Request
    {
        int days;
        string name;
        public Request(int days, string name)
        {
            this.days = days;
            this.name = name;
        }
        public int GetDays()
        {
            return days;
        }
        public string GetName()
        {
            return name;
        }

    }
    public abstract class Responsibility
    {
        protected Responsibility responsibility;
        public Responsibility(Responsibility responsibility)
        {
            this.responsibility = responsibility;
        }
        public abstract void HandleRequest(Request request);
    }
    /// <summary>
    /// 领导
    /// </summary>
    public class Leader : Responsibility
    {
        public Leader(Responsibility responsibility)
            : base(responsibility)
        { }
        public override void HandleRequest(Request request)
        {
            if (request.GetDays() < 3)
            {
                Console.WriteLine("Leader passed {0}'s {1} days request", request.GetName(), request.GetDays());
            }
            else
            {
                this.responsibility.HandleRequest(request);
            }
        }
    }
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : Responsibility
    {
        public Department(Responsibility responsibility)
            : base(responsibility)
        { }
        public override void HandleRequest(Request request)
        {
            if (request.GetDays() < 8)
            {
                Console.WriteLine("Department passed {0}'s {1} days request", request.GetName(), request.GetDays());
            }
            else
            {
                this.responsibility.HandleRequest(request);
            }
        }
    }
    //责任链终端必须处理
    /// <summary>
    /// 老板
    /// </summary>
    public class Boss : Responsibility
    {
        public Boss() : base(null) { }
        public override void HandleRequest(Request request)
        {
            if (request.GetDays() < 15)
            {
                Console.WriteLine("老板同意了Boss passed {0}'s {1} 天的请求days request", request.GetName(), request.GetDays());
            }
            else
            {
                Console.WriteLine("老板拒绝了 Boss refused {0}'s 的 {1} 天的请求days request", request.GetName(), request.GetDays());
            }
        }
    }
    #endregion

    #region 2
    //下面以公司采购东西为例子来实现责任链模式。公司规定，采购架构总价在1万之内，经理级别的人批准即可，总价大于1万小于2万5的则还需要副总进行批准，总价大于2万5小于10万的需要还需要总经理批准，而大于总价大于10万的则需要组织一个会议进行讨论。对于这样一个需求，最直观的方法就是设计一个方法，参数是采购的总价，然后在这个方法内对价格进行调整判断，然后针对不同的条件交给不同级别的人去处理，这样确实可以解决问题，但这样一来，我们就需要多重if-else语句来进行判断，但当加入一个新的条件范围时，我们又不得不去修改原来设计的方法来再添加一个条件判断，这样的设计显然违背了“开-闭”原则。这时候，可以采用责任链模式来解决这样的问题。


    // 采购请求
    public class PurchaseRequest
    {
        // 金额
        public double Amount { get; set; }
        // 产品名字
        public string ProductName { get; set; }
        public PurchaseRequest(double amount, string productName)
        {
            Amount = amount;
            ProductName = productName;
        }
    }

    // 审批人,Handler
    public abstract class Approver
    {
        public Approver NextApprover { get; set; }
        public string Name { get; set; }
        public Approver(string name)
        {
            this.Name = name;
        }
        public abstract void ProcessRequest(PurchaseRequest request);
    }

    // ConcreteHandler
    public class Manager : Approver
    {
        public Manager(string name)
            : base(name)
        { }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 10000.0)
            {
                Console.WriteLine("设计模式{0}-{1} 经理学习努力批准请求approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    // ConcreteHandler,副总
    public class VicePresident : Approver
    {
        public VicePresident(string name)
            : base(name)
        {
        }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 25000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    // ConcreteHandler，总经理
    public class President : Approver
    {
        public President(string name)
            : base(name)
        { }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 100000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                Console.WriteLine("Request需要组织一个会议讨论");
            }
        }
    }
    #endregion

    #region 3
    //管理者
    abstract class Manager2
    {
        protected string name;
        //管理者的上级
        protected Manager2 superior;

        public Manager2(string name)
        {
            this.name = name;
        }

        //设置管理者的上级
        public void SetSuperior(Manager2 superior)
        {
            this.superior = superior;
        }

        //申请请求
        abstract public void RequestApplications(RequestGZ request);
    }

    //经理
    class CommonManager : Manager2
    {
        public CommonManager(string name)
            : base(name)
        { }
        public override void RequestApplications(RequestGZ request)
        {

            if (request.RequestType == "请假" && request.Number <= 2)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else
            {
                if (superior != null)
                    superior.RequestApplications(request);
            }

        }
    }

    //总监
    class Majordomo : Manager2
    {
        public Majordomo(string name)
            : base(name)
        { }
        public override void RequestApplications(RequestGZ request)
        {

            if (request.RequestType == "请假" && request.Number <= 5)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else
            {
                if (superior != null)
                    superior.RequestApplications(request);
            }
        }
    }

    //总经理
    class GeneralManager : Manager2
    {
        public GeneralManager(string name)
            : base(name)
        { }
        public override void RequestApplications(RequestGZ request)
        {

            if (request.RequestType == "请假")
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else if (request.RequestType == "加薪" && request.Number <= 500)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else if (request.RequestType == "加薪" && request.Number > 500)
            {
                Console.WriteLine("{0}:{1} 数量{2} 再说吧", name, request.RequestContent, request.Number);
            }
        }
    }

    //申请
    class RequestGZ
    {
        //申请类别
        private string requestType;
        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        //申请内容
        private string requestContent;
        public string RequestContent
        {
            get { return requestContent; }
            set { requestContent = value; }
        }

        //数量
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }

    #endregion
}
