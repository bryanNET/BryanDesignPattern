using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{

    /*原型模式的优点有：

原型模式向客户隐藏了创建新实例的复杂性
原型模式允许动态增加或较少产品类。
原型模式简化了实例的创建结构，工厂方法模式需要有一个与产品类等级结构相同的等级结构，而原型模式不需要这样。
产品类不需要事先确定产品的等级结构，因为原型模式适用于任何的等级结构
原型模式的缺点有：

每个类必须配备一个克隆方法
 配备克隆方法需要对类的功能进行通盘考虑，这对于全新的类不是很难，但对于已有的类不一定很容易，特别当一个类引用不支持串行化的间接对象，或者引用含有循环结构的时候。
 */
    /// <summary>
    /// 原型
    /// </summary>
    [Serializable]
    public class other
    {
        public int value { get; set; }
        public other()
        {
            value = 10;
        }
    }
    /// <summary>
    /// 原型
    /// </summary>
    [Serializable]
    public abstract class ColorPrototype
    {
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }

        public other o = new other();

        //浅拷贝
        public virtual ColorPrototype Clone()
        {
            return (ColorPrototype)this.MemberwiseClone();
        }
    }
    /// <summary>
    /// 创建具体原型
    /// </summary>
    public class Red : ColorPrototype
    {
        /// <summary>
        ///  浅拷贝
        /// </summary>
        /// <returns></returns>
        public override ColorPrototype Clone()
        {
            // // 调用Clone方法实现的是浅拷贝，另外还有深拷贝
            return base.Clone();
        }
    }

    [Serializable]
    public class Green : ColorPrototype
    {/// <summary>
     /// 深拷贝，引用对象独立
     /// </summary>
     /// <returns></returns>
        public override ColorPrototype Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            ColorPrototype obj = (ColorPrototype)formatter.Deserialize(stream);
            return obj;
        }
    }
}
