using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    //外观模式（Facade Pattern）
    //门面（Facade）角色：客户端调用这个角色的方法。该角色知道相关的一个或多个子系统的功能和责任，该角色会将从客户端发来的请求委派带相应的子系统中去。
    //子系统（subsystem）角色：可以同时包含一个或多个子系统。每个子系统都不是一个单独的类，而是一个类的集合。每个子系统都可以被客户端直接调用或被门面角色调用。对于子系统而言，门面仅仅是另外一个客户端，子系统并不知道门面的存在。
    /*三、外观的优缺点
优点：

外观模式对客户屏蔽了子系统组件，从而简化了接口，减少了客户处理的对象数目并使子系统的使用更加简单。
外观模式实现了子系统与客户之间的松耦合关系，而子系统内部的功能组件是紧耦合的。松耦合使得子系统的组件变化不会影响到它的客户。
缺点：

如果增加新的子系统可能需要修改外观类或客户端的源代码，这样就违背了”开——闭原则“（不过这点也是不可避免）。
四、使用场景
 在以下情况下可以考虑使用外观模式：

外一个复杂的子系统提供一个简单的接口
提供子系统的独立性
在层次化结构中，可以使用外观模式定义系统中每一层的入口。其中三层架构就是这样的一个例子。
*/
    #region 外观模式1
    /// <summary>
    /// 门面（Facade）角色
    /// </summary>
    public class Facade
    {
        Light _light = new Light();
        TV _tv = new TV();
        public void off()
        {
            _light.on();
            _tv.off();
        }
        public void on()
        {
            _tv.on();
            _light.off();
        }
    }

    /// <summary>
    /// 子系统（subsystem）角色：
    /// </summary>
    class Light
    {
        public void on()
        {
            Console.WriteLine("light on!");
        }
        public void off()
        {
            Console.WriteLine("light off!");
        }
    }
    class TV
    {
        public void on()
        {
            Console.WriteLine("tv on!");
        }
        public void off()
        {
            Console.WriteLine("tv off!");
        }
    }

    #endregion


    #region 外观模式2
    // 外观类
    public class RegistrationFacade
    {
        //验证课程
        private RegisterCourse registerCourse;
        //发生通知
        private NotifyStudent notifyStu;
        public RegistrationFacade()
        {
            registerCourse = new RegisterCourse();
            notifyStu = new NotifyStudent();
        }

        public bool RegisterCourse(string courseName, string studentName)
        {
            if (!registerCourse.CheckAvailable(courseName))
            {
                return false;
            }

            return notifyStu.Notify(studentName);
        }
    }

    #region 子系统
    // 相当于子系统A
    public class RegisterCourse
    {
        public bool CheckAvailable(string courseName)
        {
            Console.WriteLine("正在验证课程 {0}是否人数已满", courseName);
            return true;
        }
    }

    // 相当于子系统B
    public class NotifyStudent
    {
        public bool Notify(string studentName)
        {
            Console.WriteLine("正在向{0}发生通知", studentName);
            return true;
        }
    }
    #endregion
    #endregion
}
