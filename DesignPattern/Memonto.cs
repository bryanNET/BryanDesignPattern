using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    //在不破坏封装性的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态。这样就可以将该对象恢复到原先保存的状态
    /*
  
备忘录模式中主要有三类角色：
发起人角色：记录当前时刻的内部状态，负责创建和恢复备忘录数据。
备忘录角色：负责存储发起人对象的内部状态，在进行恢复时提供给发起人需要的状态。
管理者角色：负责保存备忘录对象。     
 
        备忘录模式的适用场景
 　　在以下情况下可以考虑使用备忘录模式：

如果系统需要提供回滚操作时，使用备忘录模式非常合适。例如文本编辑器的Ctrl+Z撤销操作的实现，数据库中事务操作。
四、备忘录模式的优缺点
 　　备忘录模式具有以下优点：

如果某个操作错误地破坏了数据的完整性，此时可以使用备忘录模式将数据恢复成原来正确的数据。
备份的状态数据保存在发起人角色之外，这样发起人就不需要对各个备份的状态进行管理。而是由备忘录角色进行管理，而备忘录角色又是由管理者角色管理，符合单一职责原则。
　　当然，备忘录模式也存在一定的缺点：

在实际的系统中，可能需要维护多个备份，需要额外的资源，这样对资源的消耗比较严重。      */
    #region 1
    /// <summary>
    /// 备忘录：负责存储发起人对象的内部状态，在需要的时候提供发起人需要的内部状态
    /// </summary>
    public class Memonto
    {
        /// <summary>
        /// 血
        /// </summary>
        public int blood { get; set; }
        /// <summary>
        /// 魔法
        /// </summary>
        public int magic { get; set; }
    }

    /// <summary>
    /// 管理角色：对备忘录进行管理，保存和提供备忘录。
    /// </summary>
    public class Caretaker
    {
        private Memonto memonto;
        public void SetMemonto(Memonto memonto)
        {
            this.memonto = memonto;
        }
        public Memonto getMemonto()
        {
            return this.memonto;
        }
    }
    /// <summary>
    /// 发起人：记录当前时刻的内部状态，负责定义哪些属于备份范围的状态，负责创建和恢复备忘录数据。
    /// </summary>
    public class Original
    {
        public int blood { get; set; }
        public int magic { get; set; }
        public Memonto SaveMemonto()
        {
            return new Memonto() { blood = this.blood, magic = this.magic };
        }
        public void RestoreMemonto(Memonto memonto)
        {
            this.blood = memonto.blood;
            this.magic = memonto.magic;
        }
        public void display()
        {
            Console.WriteLine("blood:" + this.blood + "\t magic:" + this.magic);
        }
    }
    #endregion


    #region 2
    //想备份多个还原点怎么办呢？即恢复到3个人后，又想恢复到前面2个人的状态，这时候可能你会想，这样没必要啊，到时候在删除不就好了。但是如果在实际应用中，可能我们发了很多时间去创建通讯录中只有2个联系人的状态，恢复到3个人的状态后，发现这个状态时错误的，还是原来2个人的状态是正确的，难道我们又去花之前的那么多时间去重复操作吗？这显然不合理，如果就思考，能不能保存多个还原点呢？保存多个还原点其实很简单，只需要保存多个备忘录对象就可以了

    // 联系人
    public class ContactPerson
    {
        public string Name { get; set; }
        public string MobileNum { get; set; }
    }

    // 发起人
    public class MobileOwner
    {
        public List<ContactPerson> ContactPersons { get; set; }
        public MobileOwner(List<ContactPerson> persons)
        {
            ContactPersons = persons;
        }

        // 创建备忘录，将当期要保存的联系人列表导入到备忘录中 
        public ContactMemento CreateMemento()
        {
            // 这里也应该传递深拷贝，new List方式传递的是浅拷贝，
            // 因为ContactPerson类中都是string类型,所以这里new list方式对ContactPerson对象执行了深拷贝
            // 如果ContactPerson包括非string的引用类型就会有问题，所以这里也应该用序列化传递深拷贝
            return new ContactMemento(new List<ContactPerson>(this.ContactPersons));
        }

        // 将备忘录中的数据备份导入到联系人列表中
        public void RestoreMemento(ContactMemento memento)
        {
            if (memento != null)
            {
                // 下面这种方式是错误的，因为这样传递的是引用，
                // 则删除一次可以恢复，但恢复之后再删除的话就恢复不了.
                // 所以应该传递contactPersonBack的深拷贝，深拷贝可以使用序列化来完成
                this.ContactPersons = memento.ContactPersonBack;
            }
        }
        public void Show()
        {
            Console.WriteLine("联系人列表中有{0}个人，他们是:", ContactPersons.Count);
            foreach (ContactPerson p in ContactPersons)
            {
                Console.WriteLine("姓名: {0} 号码为: {1}", p.Name, p.MobileNum);
            }
        }
    }

    // 备忘录
    public class ContactMemento
    {
        public List<ContactPerson> ContactPersonBack { get; set; }
        public ContactMemento(List<ContactPerson> persons)
        {
            ContactPersonBack = persons;
        }
    }

    // 管理角色
    public class CaretakerMP
    {
        // 使用多个备忘录来存储多个备份点
        public Dictionary<string, ContactMemento> ContactMementoDic { get; set; }
        public CaretakerMP()
        {
            ContactMementoDic = new Dictionary<string, ContactMemento>();
        }
    }
    #endregion
}
