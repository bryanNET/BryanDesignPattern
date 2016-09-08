using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /// <summary>
    /// 单例模式：确保一个类只有一个实例,并提供一个访问它的全局访问点
    /// </summary>
    public class Singleton
    {
        private int cnt = 0;
        // 定义一个静态变量来保存类的实例
        private static Singleton instance = null;

        // 定义一个静态变量来保存类的实例
        //volatile多用于多线程的环境，当一个变量定义为volatile时，读取这个变量的值时候每次都是从momery里面读取而不是从cache读。这样做是为了保证读取该变量的信息都是最新的，而无论其他线程如何更新这个变量。
        private volatile static Singleton safeInstance = null;

        // 定义一个标识确保线程同步
        private static readonly object lockedobj = new object();
      
        // 定义私有构造函数，使外界不能创建该类实例
        private Singleton()
        {
        }
        
        /// <summary>
          /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
          /// </summary>
          /// <returns></returns>
        public static Singleton GetInstance()
        {   // 如果类的实例不存在则创建，否则直接返回
            if (instance == null) instance = new Singleton();
            return instance;
        }
        /// <summary>
        /// 多线程安全 单例
        /// </summary>
        /// <returns></returns>
        public static Singleton GetSafeInstance()
        {
            //可以解决多线程的问题,但是上面代码对于每个线程都会对线程辅助对象locker加锁之后再判断实例是否存在，对于这个操作完全没有必要的，因为当第一个线程创建了该类的实例之后，后面的线程此时只需要直接判断（uniqueInstance==null）为假，此时完全没必要对线程辅助对象加锁之后再去判断，所以上面的实现方式增加了额外的开销，损失了性能，为了改进上面实现方式的缺陷，我们只需要在lock语句前面加一句（uniqueInstance==null）的判断就可以避免锁所增加的额外开销，这种实现方式我们就叫它 “双重锁定”
            // 双重锁定只需要一句判断就可以了
            if (safeInstance == null)
            { 
                // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
                // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                lock (lockedobj)
                {
                    if (safeInstance == null)
                    {
                        safeInstance = new Singleton();
                    }
                }
            }
            return safeInstance;
        }
        public void count()
        {
            cnt += 1;
        }
        public int getCnt()
        {
            return cnt;
        }
    }

    /// <summary>
    /// 泛型实现单例模式
    /// http://www.cnblogs.com/webabcd/archive/2007/02/10/647140.html
    /// </summary>
    /// <typeparam name="T">需要实现单例的类</typeparam>
    public class Singleton<T> where T : new()
    {
        /// <summary>
        /// 返回类的实例
        /// </summary>
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }

        class SingletonCreator
        {
            //internal:内部的 类只能在当前项目中访问
            internal static readonly T instance = new T();
        }
    }
    public class Test
    {
        private DateTime _time;

        public Test()
        {
            System.Threading.Thread.Sleep(3000);
            _time = DateTime.Now;
        }

        public string Time
        {
            get { return _time.ToString(); }
        }
    }
}
