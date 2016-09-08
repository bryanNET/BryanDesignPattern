using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
三个角色：
环境角色（Context）：持有一个Strategy类的引用
抽象策略角色（Strategy）：这是一个抽象角色，通常由一个接口或抽象类来实现。此角色给出所有具体策略类所需实现的接口。
具体策略角色（ConcreteStrategy）：包装了相关算法或行为。     

策略者模式的适用场景
 　　在下面的情况下可以考虑使用策略模式：

一个系统需要动态地在几种算法中选择一种的情况下。那么这些算法可以包装到一个个具体的算法类里面，并为这些具体的算法类提供一个统一的接口。
如果一个对象有很多的行为，如果不使用合适的模式，这些行为就只好使用多重的if-else语句来实现，此时，可以使用策略模式，把这些行为转移到相应的具体策略类里面，就可以避免使用难以维护的多重条件选择语句，并体现面向对象涉及的概念。
五、策略者模式的优缺点
 　　策略模式的主要优点有：

策略类之间可以自由切换。由于策略类都实现同一个接口，所以使它们之间可以自由切换。
易于扩展。增加一个新的策略只需要添加一个具体的策略类即可，基本不需要改变原有的代码。
避免使用多重条件选择语句，充分体现面向对象设计思想。
　　策略模式的主要缺点有：

客户端必须知道所有的策略类，并自行决定使用哪一个策略类。这点可以考虑使用IOC容器和依赖注入的方式来解决，关于IOC容器和依赖注入（Dependency Inject）的文章可以参考：IoC 容器和Dependency Injection 模式。
策略模式会造成很多的策略类。         */

    #region 策略者模式1
    /// <summary>
    /// 订单策略
    /// </summary>
    public abstract class OrderStrategy
    {
        public List<int> orderList;
        public abstract void Order();
        public void display()
        {
            foreach (int i in orderList)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine();
        }
    }
    /// <summary>
    /// 冒泡策略
    /// </summary>
    public class BubbleStrategy : OrderStrategy
    {
        public override void Order()
        {
            for (int i = 0; i < orderList.Count; i++)
            {
                for (int j = i + 1; j < orderList.Count; j++)
                {
                    if (orderList[i] < orderList[j])//冒泡降序 小的冒上去
                    {
                        int temp = orderList[i];
                        orderList[i] = orderList[j];
                        orderList[j] = temp;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 选择策略
    /// </summary>
    public class SelectionStrategy : OrderStrategy
    {
        public override void Order()
        {
            for (int i = 0; i < orderList.Count; i++)
            {
                int smallvalue = orderList[i];
                int smallposition = i;
                for (int j = i + 1; j < orderList.Count; j++)
                {
                    if (orderList[j] < smallvalue)
                    {
                        smallposition = j;
                        smallvalue = orderList[j];
                    }
                }
                //将最小值放到当前要排序的位置
                orderList[smallposition] = orderList[i];
                orderList[i] = smallvalue;
            }
        }
    }
    /// <summary>
    /// 插入策略
    /// </summary>
    public class InsertionStrategy : OrderStrategy
    {
        public override void Order()
        {
            for (int i = 1; i < orderList.Count; i++)
            {
                int temp = orderList[i];//当前要插入的值，相当于位置I是个空白位置，供对比进行后移
                int j = i;
                //j之前的序列已经排序好，选一个位置把当前值插入

                while (j > 0)
                {
                    //i从1开始，是因为这里j要比较前一个值
                    if (temp < orderList[j - 1]) //插入过程中，每次比较的值大于当前值则向后移动
                    {
                        orderList[j] = orderList[j - 1];
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }
                //找到位置（break）或者循环正常结束（说明当前值最小）则赋值。
                orderList[j] = temp;
            }
        }
    }
    /// <summary>
    /// 策略管理器
    /// </summary>
    public class StrategyManager
    {
        OrderStrategy strategy;
        public void SetStrategy(OrderStrategy strategy)
        {
            this.strategy = strategy;
        }
        public void Sort(List<int> list)
        {
            this.strategy.orderList = list;
            this.strategy.Order();
            this.strategy.display();
        }
    }
    #endregion

    #region 策略2
    // 所得税计算策略
    public interface ITaxStragety
    {
        double CalculateTax(double income);
    }

    // 个人所得税
    public class PersonalTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return income * 0.12;
        }
    }

    // 企业所得税
    public class EnterpriseTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return (income - 3500) > 0 ? (income - 3500) * 0.045 : 0.0;
        }
    } 

    public class InterestOperation
    {
        private ITaxStragety m_strategy;
        public InterestOperation(ITaxStragety strategy)
        {
            this.m_strategy = strategy;
        }
        public double GetTax(double income)
        {
            return m_strategy.CalculateTax(income);
        }
    }


    #endregion
}
