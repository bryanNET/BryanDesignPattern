using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
     * 例如，开学了，院领导说计算机学院要进行军训，计算机学院的学生要跑1000米，院领导的话也就相当于一个命令，他不可能直接传达给到学生，他必须让教官来发出命令，并监督学生执行该命令。在这个场景中，发出命令的责任是属于学院领导，院领导充当与命令发出者的角色，执行命令的责任是属于学生，学生充当于命令接收者的角色，而教官就充当于命令的发出者或命令请求者的角色，然而命令模式的精髓就在于把每个命令抽象为对象。
     * 
     从命令模式的结构图可以看出，它涉及到五个角色，它们分别是：
客户角色：发出一个具体的命令并确定其接受者。
命令角色：声明了一个给所有具体命令类实现的抽象接口
具体命令角色：定义了一个接受者和行为的弱耦合，负责调用接受者的相应方法。
请求者角色：负责调用命令对象执行命令。
接受者角色：负责具体行为的执行。


        命令模式的适用场景
 　　在下面的情况下可以考虑使用命令模式：

系统需要支持命令的撤销（undo）。命令对象可以把状态存储起来，等到客户端需要撤销命令所产生的效果时，可以调用undo方法吧命令所产生的效果撤销掉。命令对象还可以提供redo方法，以供客户端在需要时，再重新实现命令效果。
系统需要在不同的时间指定请求、将请求排队。一个命令对象和原先的请求发出者可以有不同的生命周期。意思为：原来请求的发出者可能已经不存在了，而命令对象本身可能仍是活动的。这时命令的接受者可以在本地，也可以在网络的另一个地址。命令对象可以串行地传送到接受者上去。
如果一个系统要将系统中所有的数据消息更新到日志里，以便在系统崩溃时，可以根据日志里读回所有数据的更新命令，重新调用方法来一条一条地执行这些命令，从而恢复系统在崩溃前所做的数据更新。
系统需要使用命令模式作为“CallBack(回调)”在面向对象系统中的替代。Callback即是先将一个方法注册上，然后再以后调用该方法。
五、命令模式的优缺点
 　　命令模式使得命令发出的一个和接收的一方实现低耦合，从而有以下的优点：

命令模式使得新的命令很容易被加入到系统里。
可以设计一个命令队列来实现对请求的Undo和Redo操作。
可以较容易地将命令写入日志。
可以把命令对象聚合在一起，合成为合成命令。合成命令式合成模式的应用。
　　命令模式的缺点：

使用命令模式可能会导致系统有过多的具体命令类。这会使得命令模式在这样的系统里变得不实际。

*/
    #region 命令模式1
    //接受命令的对象
    public class CDMachine
    {
        public void on()
        {
            Console.WriteLine("CD机打开  CD Machine turns on!");
        }
        public void off()
        {
            Console.WriteLine("CD机关闭  CD Machine turns off!");
        }
    }
    //定义命令
    public abstract class Command
    {
        public abstract void Execute(CDMachine cdMachine);
    }
    public class TurnonCommand : Command
    {
        public override void Execute(CDMachine cdMachine)
        {
            cdMachine.on();
        }
    }
    public class TurnoffCommand : Command
    {
        public override void Execute(CDMachine cdMachine)
        {
            cdMachine.off();
        }
    }
    //发送命令的对象
    public class Controller
    {
        //遥控的功能 --- 可发送的命令
        private TurnonCommand turnonCommand;
        private TurnoffCommand turnoffCommand;
        public Controller(TurnonCommand turnonCommand, TurnoffCommand turnoffCommand)
        {
            this.turnonCommand = turnonCommand;
            this.turnoffCommand = turnoffCommand;
        }

        public void turnOn(CDMachine cdMachine)
        {
            this.turnonCommand.Execute(cdMachine);
        }
        public void turnOff(CDMachine cdMachine)
        {
            this.turnoffCommand.Execute(cdMachine);
        }
    }
    #endregion


    #region 命令模式2
    // 教官，负责调用命令对象执行请求
    public class Invoke
    {
        public Command2 _command;

        public Invoke(Command2 command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Action();
        }
    }

    // 命令抽象类
    public abstract class Command2
    {
        // 命令应该知道接收者是谁，所以有Receiver这个成员变量
        protected Receiver _receiver;

        public Command2(Receiver receiver)
        {
            this._receiver = receiver;
        }

        // 命令执行方法
        public abstract void Action();
    }

    // 
    public class ConcreteCommand : Command2
    {
        public ConcreteCommand(Receiver receiver)
            : base(receiver)
        {
        }

        public override void Action()
        {
            // 调用接收的方法，因为执行命令的是学生
            _receiver.Run1000Meters();
        }
    }

    // 命令接收者——学生
    public class Receiver
    {
        public void Run1000Meters()
        {
            Console.WriteLine("跑1000米");
        }
    }
    #endregion
}
