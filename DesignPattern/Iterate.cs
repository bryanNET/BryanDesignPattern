using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    /*
迭代器模式由以下角色组成：

迭代器角色（Iterator）：迭代器角色负责定义访问和遍历元素的接口
具体迭代器角色（Concrete Iteraror）：具体迭代器角色实现了迭代器接口，并需要记录遍历中的当前位置。
聚合角色（Aggregate）：聚合角色负责定义获得迭代器角色的接口
具体聚合角色（Concrete Aggregate）：具体聚合角色实现聚合角色接口。     
  
迭代器模式的适用场景
　　在下面的情况下可以考虑使用迭代器模式：

系统需要访问一个聚合对象的内容而无需暴露它的内部表示。
系统需要支持对聚合对象的多种遍历。
系统需要为不同的聚合结构提供一个统一的接口。
五、迭代器模式的优缺点
　　由于迭代器承担了遍历集合的职责，从而有以下的优点：

迭代器模式使得访问一个聚合对象的内容而无需暴露它的内部表示，即迭代抽象。
迭代器模式为遍历不同的集合结构提供了一个统一的接口，从而支持同样的算法在不同的集合结构上进行操作
　　迭代器模式存在的缺陷：

迭代器模式在遍历的同时更改迭代器所在的集合结构会导致出现异常。所以使用foreach语句只能在对集合进行遍历，不能在遍历的同时更改集合中的元素。       */
    #region 1
    public class Persons : IEnumerable
    {
        string[] m_Names;

        public Persons(params string[] Names)
        {
            m_Names = new string[Names.Length];

            Names.CopyTo(m_Names, 0);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (string s in m_Names)
            {
                yield return s;
            }
        }

        public int Length { get { return m_Names.Length; } }

        public string this[int i]
        {
            get
            {
                return m_Names[i];
            }
            set
            {
                m_Names[i] = value;
            }
        }
    }

    #endregion
  
    #region 2 //http://www.cnblogs.com/webabcd/archive/2007/05/17/750715.html

    /// <summary>
    /// Message实体类
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">Message内容</param>
        /// <param name="pt">Message发布时间</param>
        public MessageModel(string msg, DateTime pt)
        {
            this._message = msg;
            this._publishTime = pt;
        }

        private string _message;
        /// <summary>
        /// Message内容
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private DateTime _publishTime;
        /// <summary>
        /// Message发布时间
        /// </summary>
        public DateTime PublishTime
        {
            get { return _publishTime; }
            set { _publishTime = value; }
        }
    }


    /// <summary>
    /// 集合接口（Aggregate）
    /// </summary>
    public interface ICollection
    {
        /// <summary>
        /// 创建迭代器对象
        /// </summary>
        /// <returns></returns>
        IIterator CreateIterator();
    }
    /// <summary>
    /// 集合（ConcreteAggregate）
    /// </summary>
    public class Collection : ICollection
    {
        private List<MessageModel> list = new List<MessageModel>();

        /// <summary>
        /// 创建迭代器对象
        /// </summary>
        /// <returns></returns>
        public IIterator CreateIterator()
        {
            return new Iterator(this);
        }

        /// <summary>
        /// 集合内的对象总数
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">index</param>
        /// <returns></returns>
        public MessageModel this[int index]
        {
            get { return list[index]; }
            set { list.Add(value); }
        }

    }
  
    /// <summary>
    /// 迭代器接口（IIterator）
    /// </summary>
    public interface IIterator
    {
        /// <summary>
        /// 第一个对象
        /// </summary>
        /// <returns></returns>
        MessageModel First();

        /// <summary>
        /// 下一个对象
        /// </summary>
        /// <returns></returns>
        MessageModel Next();

        /// <summary>
        /// 当前对象
        /// </summary>
        MessageModel CurrentMessageModel { get; }

        /// <summary>
        /// 是否迭代完毕
        /// </summary>
        bool IsDone { get; }
    }

    /// <summary>
    /// 迭代器（Iterator）
    /// </summary>
    public class Iterator : IIterator
    {
        private Collection _collection;
        private int _current = 0;
        private int _step = 1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection"></param>
        public Iterator(Collection collection)
        {
            this._collection = collection;
        }

        /// <summary>
        /// 第一个对象
        /// </summary>
        /// <returns></returns>
        public MessageModel First()
        {
            _current = 0;
            return _collection[_current];
        }

        /// <summary>
        /// 下一个对象
        /// </summary>
        /// <returns></returns>
        public MessageModel Next()
        {
            _current += _step;

            if (!IsDone)
            {
                return _collection[_current];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 当前对象
        /// </summary>
        public MessageModel CurrentMessageModel
        {
            get { return _collection[_current]; }
        }

        /// <summary>
        /// 是否迭代完毕
        /// </summary>
        public bool IsDone
        {
            get { return _current >= _collection.Count ? true : false; }
        }

        /// <summary>
        /// 步长
        /// </summary>
        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
    }


    #endregion

    #region 3
    abstract class Aggregate
    {
        public abstract Iterator2 CreateIterator();
    }

    class ConcreteAggregate : Aggregate
    {
        private IList<object> items = new List<object>();
        public override Iterator2 CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public object this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }

    abstract class Iterator2
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    class ConcreteIterator : Iterator2
    {
        private ConcreteAggregate aggregate;
        private int current = 0;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public override object First()
        {
            return aggregate[0];
        }

        public override object Next()
        {
            object ret = null;
            current++;

            if (current < aggregate.Count)
            {
                ret = aggregate[current];
            }

            return ret;
        }

        public override object CurrentItem()
        {
            return aggregate[current];
        }

        public override bool IsDone()
        {
            return current >= aggregate.Count ? true : false;
        }

    }

    class ConcreteIteratorDesc : Iterator2
    {
        private ConcreteAggregate aggregate;
        private int current = 0;

        public ConcreteIteratorDesc(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
            current = aggregate.Count - 1;
        }

        public override object First()
        {
            return aggregate[aggregate.Count - 1];
        }

        public override object Next()
        {
            object ret = null;
            current--;
            if (current >= 0)
            {
                ret = aggregate[current];
            }

            return ret;
        }

        public override object CurrentItem()
        {
            return aggregate[current];
        }

        public override bool IsDone()
        {
            return current < 0 ? true : false;
        }

    }
    #endregion
}
