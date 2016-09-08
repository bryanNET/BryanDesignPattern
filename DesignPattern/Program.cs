using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using I = DesignPattern.Iterator;

namespace DesignPattern
{
    /*
    参考1 http://www.cnblogs.com/cotton/p/4362782.html
    参考2 http://www.cnblogs.com/zhili/category/496417.html
         */
    class Program
    {
      
        /*
         * 创建型模式：简单工厂、工厂方法模式、抽象工厂模式、建造者模式、原型模式、单例模式
         * 结构型模式：适配器模式、桥接模式、装饰模式、组合模式、外观模式、享元模式、代理模式。
         * 行为型模式：模板（模版）方法模式、命令模式、迭代器模式、观察者模式、中介者模式、备忘录模式、解释器模式、状态模式、策略模式、责任/职责链模式、访问者模式。
         */
        static void Main(string[] args)
        {
            #region 创建型模式
             
            //简单工厂
            //简单工厂模式又称之为静态工厂方法，属于创建型模式。在简单工厂模式中，可以根据传递的参数不同，返回不同类的实例。简单工厂模式定义了一个类，这个类专门用于创建其他类的实例，这些被创建的类都有一个共同的父类。
            Console.WriteLine("简单工厂");
            Console.WriteLine(SimpleFactory.GetOperation(op.add, 1.1, 2.2).GetResult());

            //工厂方法
            //工厂方法模式定义了一个创建对象的接口，但由子类决定要实例化的类是哪一个。工厂方法模式让实例化推迟到子类。和简单工厂区别在于，每个工厂只管生产自己对应的产品，而简单工厂是一个工厂生产各种产品。
            Console.WriteLine("\n工厂方法");
            ILoggerFactory factorymethod = new EventLoggerFactory();
            ILogger iLogger = factorymethod.CreateLogger();
            iLogger.write("123");
            factorymethod = new FileLoggerFactory();
            iLogger = factorymethod.CreateLogger();
            iLogger.write("321");
            Console.ReadLine();


            //抽象工厂
            //抽象工厂模式提供一个接口，用于创建相关或者依赖对象的家族，而不需要明确指定具体类。抽象工厂允许客户端使用抽象的接口来创建一组相关的产品，而不需要关系实际产出的具体产品是什么。这样一来，客户就可以从具体的产品中被解耦。和工厂方法主要区别于，抽象工厂内要像像定义中说的一样，‘创建一组相关的产品’。
            //简单工厂是一个工厂生产多个产品；工厂方法是拆分成子工厂，分别生产各自产品；抽象工厂整合工厂方法和简单工厂，随着子工厂规模变大，也可以生产多个类似产品。
            Console.WriteLine("\n抽象工厂");
            AbstractFactory absFactory = new ChineseFactory();
            absSalary chSalary = absFactory.CreateSalary(10000, 8000, 0.12);
            absSocialSecurity chScSc = absFactory.CreateSocialSecurity(1200);
            Console.WriteLine(chSalary.CalculateTax());
            Console.WriteLine(chScSc.GetSocialSecurity());
            absFactory = new ForeignerFactory();
            chSalary = absFactory.CreateSalary(10000, 8000, 0.12);
            chScSc = absFactory.CreateSocialSecurity(1200);
            Console.WriteLine(chSalary.CalculateTax());
            Console.WriteLine(chScSc.GetSocialSecurity());
            Console.ReadLine();



            //创造者模式
            //建造者模式（Builder Pattern）:将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示。
            //建造者模式构建复杂对象就像造汽车一样，是一个一个组件一个一个步骤创建出来的，它允许用户通过制定的对象类型和内容来创建他们，但是用户并不需要知道这个复杂对象是如何构建的，它只需要明白通过这样做我可以得到一个完整的复杂对象实例。
            //和工厂方法很像，创造者是一个builder内每个方法分别创建产品零部件，而工厂方法是每个factory生产一个产品。如果把builder的零部件当做一个完整产品呢？是不是就像 builder又再一次封装了factory~ 

            //建造者模式使得建造代码与表示代码的分离，可以使客户端不必知道产品内部组成的细节，从而降低了客户端与具体产品之间的耦合度
            Console.WriteLine("\n创造者模式");
            Waitor waiter = new Waitor();
            Builder b1 = new MealABuilder();
            Builder b2 = new MealBBuilder();

            waiter.PrepareMeal(b1);
            Meal ma = b1.GetMeal();
            Console.WriteLine(ma.getFood() + "\t" + ma.getDrink());

            waiter.PrepareMeal(b2);
            Meal mb = b2.GetMeal();
            Console.WriteLine(mb.getFood() + "\t" + mb.getDrink());
            Console.ReadLine();



            //原型模式（Prototype Pattern）
            //原型模式就是用原型实例指定创建对象的种类，并且通过复制这些原型创建新的对象。说到复制，就会有深/浅两种复制，这是面向对象的值类型和引用类型的差异，
            //当创建一个类的实例的过程很昂贵或很复杂，并且我们需要创建多个这样类的实例时，如果我们用new操作符去创建这样的类实例，这未免会增加创建类的复杂度和耗费更多的内存空间，因为这样在内存中分配了多个一样的类实例对象，然后如果采用工厂模式来创建这样的系统的话，随着产品类的不断增加，导致子类的数量不断增多，反而增加了系统复杂程度，所以在这里使用工厂模式来封装类创建过程并不合适，然而原型模式可以很好地解决这个问题，因为每个类实例都是相同的，当我们需要多个相同的类实例时，没必要每次都使用new运算符去创建相同的类实例对象，此时我们一般思路就是想——只创建一个类实例对象，如果后面需要更多这样的实例，可以通过对原来对象拷贝一份来完成创建，这样在内存中不需要创建多个相同的类实例，从而减少内存的消耗和达到类实例的复用。 然而这个思路正是原型模式的实现方式。下面就具体介绍下设计模式中的原型设计模式。
            Console.WriteLine("\n原型模式");
            Red r = new Red();
            r.o.value = 20;//改变引用值
            ColorPrototype RCopy = r.Clone();
            RCopy.o.value = 30;
            Console.WriteLine(r.o.value);//30 浅拷贝，指向同一个应用对象，一处改变，都改变

            Green g = new Green();
            g.o.value = 20;
            ColorPrototype GCopy = g.Clone();
            GCopy.o.value = 30;
            Console.WriteLine(g.o.value);//20 深拷贝，引用对象独立
            Console.ReadLine();


            //单例模式
            //保证一个类仅有一个实例，并提供一个访问它的全局访问点。
            Console.WriteLine("\n单例模式");
            Task[] tArr = new Task[]{
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count()),
            Task.Run(() => Singleton.GetInstance().count())
            };
            Singleton.GetInstance().count();
            Task.WaitAll(tArr);
            Console.WriteLine("danger:" + Singleton.GetInstance().getCnt());

            Task[] tArrSafe = new Task[]{
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count()),
            Task.Run(() => Singleton.GetSafeInstance().count())
            };
            Singleton.GetSafeInstance().count();
            Task.WaitAll(tArrSafe);
            Console.WriteLine("safe:" + Singleton.GetSafeInstance().getCnt());

            //2  http://www.cnblogs.com/webabcd/archive/2007/02/10/647140.html
            // 使用单例模式，保证一个类仅有一个实例
            Console.WriteLine(Singleton<Test>.Instance.Time);
            Console.WriteLine("\t");
            Console.WriteLine(Singleton<Test>.Instance.Time);
            Console.WriteLine("\t");
            // 不用单例模式
            Test t = new Test();
            Console.WriteLine(t.Time);
            Console.WriteLine("\t");
            Test t2 = new Test();
            Console.WriteLine(t2.Time);
            Console.WriteLine("\t");
            Console.ReadLine();


            #endregion

            #region 结构型模式


            //适配器模式（Adapter Pattern）
            //适配器模式就是将一个类的接口，转换成客户期望的另一个接口。适配器让原本接口不兼容的类可以合作无间。在适配器模式中，我们可以定义一个包装类，包装不兼容接口的对象，这个包装类就是适配器，它所包装的对象就是适配者。
            //适配器提供给客户需要的接口，适配器的实现就是将客户的请求转换成对适配者的相应的接口的引用。也就是说，当客户调用适配器的方法时，适配器方法内部将调用 适配者的方法，客户并不是直接访问适配者的，而是通过调用适配器方法访问适配者。因为适配器可以使互不兼容的类能够“合作愉快”。 http://www.cnblogs.com/webabcd/archive/2007/04/08/704916.html
            Console.WriteLine("\n适配器模式");
            EventLogger eLog = new EventLogger();
            FileLogger fLog = new FileLogger();
            LogAdaptor adapter = new LogAdaptor(eLog);
            adapter.writelog("123123");
            adapter = new LogAdaptor(fLog);
            adapter.writelog("123123");
            //买的电器插头是2个孔的，但是我们买的插座只有三个孔的，此时我们就希望电器的插头可以转换为三个孔的就好，这样我们就可以直接把它插在插座上，此时三个孔插头就是客户端期待的另一种接口，自然两个孔的插头就是现有的接口，适配器模式就是用来完成这种转换的
            // 现在客户端可以通过电适配要使用2个孔的插头了
            IThreeHole threehole = new PowerAdapter();
            threehole.Request();
            Console.ReadLine();


            //桥接模式（Bridge Pattern）
            //桥接模式即将抽象部分与它的实现部分分离开来，使他们都可以独立变化。
            //桥接模式将继承关系转化成关联关系，它降低了类与类之间的耦合度，减少了系统中类的数量，也减少了代码量。
            //个人感觉，代理模式、适配器模式和桥接模式相类似，代理模式是一个代理对外表示一个特定的类，适配器模式相当于一个适配器代理多个类，而桥接模式则更加适用于多个对多个的时候
            Console.WriteLine("\n桥接模式");
            Color blue = new Blue();
            Color white = new White();
            Shape squre = new Squre();
            Shape circle = new Circle();
            squre.SetColor(blue);
            squre.Draw();
            circle.SetColor(white);
            circle.Draw();
            //三层架构中就应用了桥接模式，三层架构中的业务逻辑层BLL中通过桥接模式与数据操作层解耦（DAL），其实现方式就是在BLL层中引用了DAL层中一个引用。这样数据操作的实现可以在不改变客户端代码的情况下动态进行更换
            BusinessObject customers = new CustomersBusinessObject("ShangHai");
            customers.Dataacces = new CustomersDataAccess();
            customers.Add("小六");
            Console.WriteLine("增加了一位成员的结果：");
            customers.ShowAll();
            customers.Delete("王五");
            Console.WriteLine("删除了一位成员的结果：");
            customers.ShowAll();
            Console.WriteLine("更新了一位成员的结果：");
            customers.Update("Learning_Hard");
            customers.ShowAll();
            Console.ReadLine();


            //装饰者模式（Decorator Pattern）
            //装饰者模式以对客户透明的方式动态地给一个对象附加上更多的责任，装饰者模式相比生成子类可以更灵活地增加功能。          
            Console.WriteLine("\n装饰者模式");
            Car car = new Benz();
            car = new ColorDecorator(car).decorate("red");
            car.run();
            car = new CompartmentDecorator(car).decorate(3);
            car.run();
            //给手机添加贴膜，手机挂件，手机外壳等，如果此时利用继承来实现的话，就需要定义无数的类，如StickerPhone（贴膜是手机类）、AccessoriesPhone（挂件手机类）等，这样就会导致 ”子类爆炸“问题，为了解决这个问题，我们可以使用装饰者模式来动态地给一个对象添加额外的职责
            // 我买了个苹果手机
            Phone phone = new ApplePhone();
            // 现在想贴膜了
            Decorator2 applePhoneWithSticker = new Sticker(phone);
            // 扩展贴膜行为
            applePhoneWithSticker.Print();
            Console.WriteLine("----------------------\n");
            // 现在我想有挂件了
            Decorator2 applePhoneWithAccessories = new Accessories(phone);
            // 扩展手机挂件行为
            applePhoneWithAccessories.Print();
            Console.WriteLine("----------------------\n");
            // 现在我同时有贴膜和手机挂件了
            Sticker sticker = new Sticker(phone);
            Accessories applePhoneWithAccessoriesAndSticker = new Accessories(sticker);
            applePhoneWithAccessoriesAndSticker.Print();
            Console.ReadLine();


            //组合模式（Composite Pattern）
            //组合模式允许你将对象组合成树形结构来表现”部分-整体“的层次结构，使得客户以一致的方式处理单个对象以及对象的组合
            Console.WriteLine("\n组合模式");
            Folder folder1 = new Folder("study");
            File img = new ImageFile("img");
            folder1.AddFile(img);
            Folder folder2 = new Folder("c#");
            folder2.AddFile(img);
            folder1.AddFile(folder2);
            folder1.Display();
            //组合模式解耦了客户程序与复杂元素内部结构，从而使客户程序可以向处理简单元素一样来处理复杂元素。
            //在以下情况下应该考虑使用组合模式：       
            //需要表示一个对象整体或部分的层次结构。
            //希望用户忽略组合对象与单个对象的不同，用户将统一地使用组合结构中的所有对象。
            ConcreteCompany root = new ConcreteCompany("北京总公司");
            root.Add(new HRDepartment("总公司人力资源部"));
            root.Add(new FinanceDepartment("总公司财务部"));
            ConcreteCompany comp = new ConcreteCompany("上海华东分公司");
            comp.Add(new HRDepartment("华东分公司人力资源部"));
            comp.Add(new FinanceDepartment("华东分公司财务部"));
            root.Add(comp);
            ConcreteCompany comp1 = new ConcreteCompany("南京办事处");
            comp1.Add(new HRDepartment("南京办事处人力资源部"));
            comp1.Add(new FinanceDepartment("南京办事处财务部"));
            comp.Add(comp1);
            ConcreteCompany comp2 = new ConcreteCompany("杭州办事处");
            comp2.Add(new HRDepartment("杭州办事处人力资源部"));
            comp2.Add(new FinanceDepartment("杭州办事处财务部"));
            comp.Add(comp2);
            Console.WriteLine("\n结构图：");
            root.Display(1);
            Console.WriteLine("\n职责：");
            root.LineOfDuty();

            Console.ReadLine();


            //外观模式（Facade Pattern）
            //外观模式提供了一个统一的接口，用来访问子系统中的一群接口。外观定义了一个高层接口，让子系统更容易使用。使用外观模式时，我们创建了一个统一的类，用来包装子系统中一个或多个复杂的类，客户端可以直接通过外观类来调用内部子系统中方法，从而外观模式让客户和子系统之间避免了紧耦合。
            Console.WriteLine("\n外观模式");
            Facade facade = new Facade();
            Console.WriteLine("电视打开，电灯关闭！");
            facade.on();
            Console.WriteLine("3秒后电灯打开，电视关闭");
            Thread.Sleep(3000);
            facade.off();
            //  private static
            RegistrationFacade facade2 = new RegistrationFacade();
            if (facade2.RegisterCourse("设计模式", "Learning Hard"))
            {
                Console.WriteLine("选课成功");
            }
            else
            {
                Console.WriteLine("选课失败");
            }
            Console.ReadLine();


            //享元模式（Flyweight Pattern）
            //享元模式——运用共享技术有效地支持大量细粒度的对象。享元模式可以避免大量相似类的开销，在软件开发中如果需要生成大量细粒度的类实例来表示数据，如果这些实例除了几个参数外基本上都是相同的，这时候就可以使用享元模式来大幅度减少需要实例化类的数量。如果能把这些参数（指的这些类实例不同的参数）移动类实例外面，在方法调用时将他们传递进来，这样就可以通过共享大幅度地减少单个实例的数目。（这个也是享元模式的实现要领）,然而我们把类实例外面的参数称为享元对象的外部状态，把在享元对象内部定义称为内部状态。
            //具体享元对象的内部状态与外部状态的定义为：
            //内部状态：在享元对象的内部并且不会随着环境的改变而改变的共享部分
            //外部状态：随环境改变而改变的，不可以共享的状态。
            Console.WriteLine("\n享元模式");
            FlyweightFactory flyfactory = new FlyweightFactory();
            IFlyweight flyweidht = flyfactory.getPen("red");
            Console.WriteLine(flyweidht.GetColor());
            flyweidht = flyfactory.getPen("blue");
            Console.WriteLine(flyweidht.GetColor());
            flyweidht = flyfactory.getPen("red");
            Console.WriteLine(flyweidht.GetColor());
            flyfactory.Display();

            // 定义外部状态，例如字母的位置等信息
            int externalstate = 10;
            // 初始化享元工厂
            FlyweightFactory2 factory = new FlyweightFactory2();
            // 判断是否已经创建了字母A，如果已经创建就直接使用创建的对象A
            Flyweight fa = factory.GetFlyweight("A");
            if (fa != null)
            {
                // 把外部状态作为享元对象的方法调用参数
                fa.Operation(--externalstate);
            }
            // 判断是否已经创建了字母B
            Flyweight fb = factory.GetFlyweight("B");
            if (fb != null)
            {
                fb.Operation(--externalstate);
            }
            // 判断是否已经创建了字母C
            Flyweight fc = factory.GetFlyweight("C");
            if (fc != null)
            {
                fc.Operation(--externalstate);
            }
            // 判断是否已经创建了字母D
            Flyweight fd = factory.GetFlyweight("D");
            if (fd != null)
            {
                fd.Operation(--externalstate);
            }
            else
            {
                Console.WriteLine("驻留池中不存在字符串D");
                // 这时候就需要创建一个对象并放入驻留池中
                ConcreteFlyweight d = new ConcreteFlyweight("D");
                factory.flyweights.Add("D", d);
            }
            Console.Read();
            WebSiteFactory f = new WebSiteFactory();
            WebSite fx = f.GetWebSiteCategory("产品展示");
            fx.Use(new User("小菜"));
            WebSite fy = f.GetWebSiteCategory("产品展示");
            fy.Use(new User("大鸟"));
            WebSite fz = f.GetWebSiteCategory("产品展示");
            fz.Use(new User("娇娇"));
            WebSite fl = f.GetWebSiteCategory("博客");
            fl.Use(new User("老顽童"));
            WebSite fm = f.GetWebSiteCategory("博客");
            fm.Use(new User("桃谷六仙"));
            Console.WriteLine("得到网站分类总数为 {0}", f.GetWebSiteCount());


            //代理模式（Proxy Pattern）
            //代理模式就是给一个对象提供一个代理，并由代理对象控制对原对象的引用。在代理模式中，“第三者”代理主要是起到一个中介的作用，它连接客户端和目标对象。 
            Console.WriteLine("\n代理模式");
            Girl girl = new Girl("Han MeiMei");
            Boy boy = new Boy("Li Lei", girl);
            Proxy proxy = new Proxy(boy);
            proxy.GiveFlower();
            // 创建一个代理对象并发出请求
            PersonProxy proxyp = new Friend();
            proxyp.BuyProduct();
            Console.ReadLine();


            #endregion

            #region 行为型模式
            //模板方法模式（Template Method）
            //模板方法模式——在一个抽象类中定义一个操作中的算法骨架（对应于生活中的大家下载的模板），而将一些步骤延迟到子类中去实现（对应于我们根据自己的情况向模板填充内容）。模板方法使得子类可以不改变一个算法的结构前提下，重新定义算法的某些特定步骤，模板方法模式把不变行为搬到超类中，从而去除了子类中的重复代码。
            Console.WriteLine("\n模板模式");
            Template tea = new Tea();
            tea.makeBeverage();
            Template coffee = new Coffee();
            coffee.makeBeverage();
            Console.ReadLine();



            //命令模式（Command Pattern）
            //命令模式属于对象的行为型模式。命令模式是把一个操作或者行为抽象为一个对象中，通过对命令的抽象化来使得发出命令的责任和执行命令的责任分隔开。命令模式的实现可以提供命令的撤销和恢复功能。
            Console.WriteLine("\n命令模式");
            CDMachine cd = new CDMachine();
            TurnoffCommand off = new TurnoffCommand();
            TurnonCommand on = new TurnonCommand();
            Controller ctrl = new Controller(on, off);
            //遥控器发送命令到cd机
            ctrl.turnOn(cd);
            ctrl.turnOff(cd);
            // 院领导 // 初始化Receiver、Invoke和Command
            Receiver r1 = new Receiver();
            Command2 c = new ConcreteCommand(r1);
            Invoke i = new Invoke(c);
            // 院领导发出命令
            i.ExecuteCommand();
            Console.ReadLine();


            //迭代器模式（Iterator Pattern）
            //迭代器模式提供了一种方法顺序访问一个聚合对象（理解为集合对象）中各个元素，而又无需暴露该对象的内部表示，这样既可以做到不暴露集合的内部结构，又可让外部代码透明地访问集合内部的数据。
            Console.WriteLine("\n迭代器");
            Persons ps = new Persons(new string[] { "1", "2", "3" });
            foreach (string name in ps)
            {
                Console.WriteLine(name);
            }
            for (var ii = 0; ii < ps.Length; ii++)
            {
                Console.WriteLine(ps[ii]);
            }
            Console.ReadLine();

            //2
            //using  I = Iterator;
            Collection collection = new Collection();
            collection[0] = new MessageModel("第1条信息", DateTime.Now);
            collection[1] = new MessageModel("第2条信息", DateTime.Now);
            collection[2] = new MessageModel("第3条信息", DateTime.Now);
            collection[3] = new MessageModel("第4条信息", DateTime.Now);
            Iterator iterator = new Iterator(collection);
            iterator.Step = 2;
            for (MessageModel mm = iterator.First(); !iterator.IsDone; mm = iterator.Next())
            {
                Console.WriteLine(mm.Message);
                Console.WriteLine("<br />");
            }
            IList<string> a = new List<string>();
            a.Add("大鸟");
            a.Add("小菜");
            a.Add("行李");
            a.Add("老外");
            a.Add("公交内部员工");
            a.Add("小偷");
            foreach (string itemi in a)
            {
                Console.WriteLine("{0} 请买车票!", itemi);
            }
            IEnumerator<string> e = a.GetEnumerator();
            while (e.MoveNext())
            {
                Console.WriteLine("{0} 请买车票!", e.Current);
            }
            ConcreteAggregate a3 = new ConcreteAggregate();
            a3[0] = "大鸟";
            a3[1] = "小菜";
            a3[2] = "行李";
            a3[3] = "老外";
            a3[4] = "公交内部员工";
            a3[5] = "小偷";
            Iterator2 i3 = new ConcreteIterator(a3);
            //Iterator i = new ConcreteIteratorDesc(a);
            object item = i3.First();
            while (!i3.IsDone())
            {
                Console.WriteLine("{0} 请买车票!", i3.CurrentItem());
                i3.Next();
            }
            Console.ReadLine();


            //观察者模式（Observer Pattern）
            //　观察者模式定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象，这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己的行为。
            Console.WriteLine("\n观察者模式");
            Subject subject = new ConcreteSubject();
            subject.SetState("开始 start");
            Observer o1 = new ConcreteObserver1();
            Observer o2 = new ConcreteObserver2();
            subject.AddObserver(o1);
            subject.AddObserver(o2);
            subject.notity();
            subject.SetState("改变 change");
            subject.notity();
            //2 Subject eventSubject = new EventSubjet();
            EventSubjet eventSubject = new EventSubjet();
            eventSubject.UpdateHandler += o1.Update;
            eventSubject.UpdateHandler += o2.Update;
            eventSubject.SetState("事件 event");
            eventSubject.EventNotify();
            //3
            TenXun tenXun = new TenXunGame("TenXun Game游戏", "新游戏发布Have a new game published ....");
            Subscriber lh = new Subscriber("Learning Hard");
            Subscriber tom = new Subscriber("Tom");
            // 添加订阅者
            tenXun.AddObserver(new NotifyEventHandler(lh.ReceiveAndPrint));
            tenXun.AddObserver(new NotifyEventHandler(tom.ReceiveAndPrint));
            tenXun.Update();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("移除Tom订阅者");
            tenXun.RemoveObserver(new NotifyEventHandler(tom.ReceiveAndPrint));
            tenXun.Update();
            Console.ReadLine();


            //中介者模式（Mediator Pattern）
            //　中介者模式，定义了一个中介对象来封装一系列对象之间的交互关系。中介者使各个对象之间不需要显式地相互引用，从而使耦合性降低，而且可以独立地改变它们之间的交互行为。
            Console.WriteLine("\n中介者模式");
            //中介单独存在
            //Mediator mediator = new ConcreteMediator();
            ConcreteMediator mediator = new ConcreteMediator();
            //房主和租客寻找中介
            HouseOwner houseOwner = new HouseOwner("houseowner", mediator);
            Tenant tenant = new Tenant("tenant", mediator);
            //中介给他们搭建链接
            mediator.SetHouseOwner(houseOwner);
            mediator.SetTenant(tenant);
            houseOwner.Contact("出租房");
            tenant.Contact("租房");
            //牌友A和牌友B都依赖于抽象的中介者类，这样如果其中某个牌友类变化只会影响到，只会影响到该变化牌友类本身和中介者类
            AbstractCardPartner A = new ParterA();
            AbstractCardPartner B = new ParterB();
            // 初始钱
            A.MoneyCount = 20;
            B.MoneyCount = 20;
            AbstractMediator mp1 = new MediatorPater(A, B);
            // A赢了
            A.ChangeCount(5, mp1);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount);// 应该是25
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount); // 应该是15
            // B 赢了
            B.ChangeCount(10, mp1);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount);// 应该是15
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount); // 应该是25
            Console.ReadLine();


            //备忘录模式（Memento Pattern）
            /*备忘录模式的优点有：
当发起人角色中的状态改变时，有可能这是个错误的改变，我们使用备忘录模式就可以把这个错误的改变还原。
备份的状态是保存在发起人角色之外的，这样，发起人角色就不需要对各个备份的状态进行管理。
备忘录模式的缺点：
在实际应用中，备忘录模式都是多状态和多备份的，发起人角色的状态需要存储到备忘录对象中，对资源的消耗是比较严重的。
如果有需要提供回滚操作的需求，使用备忘录模式非常适合，比如jdbc的事务操作，文本编辑器的Ctrl+Z恢复等。*/
            Console.WriteLine("\n备忘录模式");
            Caretaker caretaker = new Caretaker();
            Original original = new Original();
            original.blood = 100;
            original.magic = 100;
            original.display();
            caretaker.SetMemonto(original.SaveMemonto());
            original.blood = 50;
            original.magic = 50;
            original.display();
            original.RestoreMemonto(caretaker.getMemonto());
            original.display();
            //保存了多个状态，客户端可以选择恢复的状态点
            List<ContactPerson> persons = new List<ContactPerson>()
            {
                new ContactPerson() { Name= "Learning Hard", MobileNum = "123445"},
                new ContactPerson() { Name = "Tony", MobileNum = "234565"},
                new ContactPerson() { Name = "Jock", MobileNum = "231455"}
            };
            MobileOwner mobileOwner = new MobileOwner(persons);
            mobileOwner.Show();
            // 创建备忘录并保存备忘录对象
            CaretakerMP caretakermp = new CaretakerMP();
            caretakermp.ContactMementoDic.Add(DateTime.Now.ToString(), mobileOwner.CreateMemento());
            // 更改发起人联系人列表
            Console.WriteLine("----移除最后一个联系人--------");
            mobileOwner.ContactPersons.RemoveAt(2);
            mobileOwner.Show();
            // 创建第二个备份
            Thread.Sleep(1000);
            caretakermp.ContactMementoDic.Add(DateTime.Now.ToString(), mobileOwner.CreateMemento());
            // 恢复到原始状态
            Console.WriteLine("-------恢复联系人列表,请从以下列表选择恢复的日期------");
            var keyCollection = caretakermp.ContactMementoDic.Keys;
            foreach (string k in keyCollection)
            {
                Console.WriteLine("Key = {0}", k);
            }
            Console.WriteLine("录入 exit 跳出！");
            while (true)
            {
                Console.Write("请输入数字,按窗口的关闭键退出:");

                int index = -1;
                try
                {
                    index = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("输入的格式错误");
                    continue;
                }
                ContactMemento contactMentor = null;
                if (index < keyCollection.Count && caretakermp.ContactMementoDic.TryGetValue(keyCollection.ElementAt(index), out contactMentor))
                {
                    mobileOwner.RestoreMemento(contactMentor);
                    mobileOwner.Show();
                }
                else
                {
                    Console.WriteLine("输入的索引大于集合长度！");
                }
                if (Console.ReadLine() == "exit")
                    break;
            }
            Console.ReadLine();



            //解释器模式
            Console.WriteLine("\n解释器模式");
            Context context = new Context("ABcdeFG");
            UpperInterpreter ui = new UpperInterpreter();
            string inprResut = ui.Interprete(context);
            Console.WriteLine("up:" + inprResut);
            LowerInterpreter li = new LowerInterpreter();
            inprResut = li.Interprete(context);
            Console.WriteLine("low:" + inprResut);
            Console.ReadLine();


            //状态者模式（State Pattern）
            //　状态模式——允许一个对象在其内部状态改变时自动改变其行为，对象看起来就像是改变了它的类。
            Console.WriteLine("\n状态模式");
            Programmer programmer = new Programmer();
            Console.WriteLine(DateTime.Now + "程序员正在做什么呢？");
            programmer.Doing(DateTime.Now);
            Console.WriteLine(DateTime.Now.AddHours(-10) + "程序员正在做什么呢？");
            programmer.Doing(DateTime.Now.AddHours(-10));
            // 开一个新的账户
            Account account = new Account("Learning Hard");
            // 进行交易
            // 存钱
            account.Deposit(1000.0);
            account.Deposit(200.0);
            account.Deposit(600.0);
            // 付利息
            account.PayInterest();
            // 取钱
            account.Withdraw(2000.00);
            account.Withdraw(500.00);
            // 等待用户输入
            Console.ReadKey();
            Console.ReadLine();



            //策略者模式（Stragety Pattern）
            //策略模式是针对一组算法，将每个算法封装到具有公共接口的独立的类中，从而使它们可以相互替换。策略模式使得算法可以在不影响到客户端的情况下发生变化。
            Console.WriteLine("\n策略模式");
            BubbleStrategy bubble = new BubbleStrategy();
            SelectionStrategy selection = new SelectionStrategy();
            InsertionStrategy insertion = new InsertionStrategy();
            var list = new List<int>() { 3, 1, 6, 2, 5 };
            StrategyManager smanager = new StrategyManager();
            smanager.SetStrategy(bubble);
            smanager.Sort(list);
            smanager.SetStrategy(selection);
            smanager.Sort(list);
            smanager.SetStrategy(insertion);
            smanager.Sort(list);
            // 个人所得税方式
            InterestOperation operation = new InterestOperation(new PersonalTaxStrategy());
            Console.WriteLine("个人支付的税为：{0}", operation.GetTax(5000.00));
            // 企业所得税
            operation = new InterestOperation(new EnterpriseTaxStrategy());
            Console.WriteLine("企业支付的税为：{0}", operation.GetTax(50000.00));
            Console.ReadLine();



            //责任链模式
            //　责任链模式指的是——某个请求需要多个对象进行处理，从而避免请求的发送者和接收之间的耦合关系。将这些对象连成一条链子，并沿着这条链子传递该请求，直到有对象处理它为止。
            Console.WriteLine("\n责任链模式");
            Request request = new Request(20, "wang");
            Boss boss = new Boss();
            Department department = new Department(boss);
            Leader leader = new Leader(department);
            leader.HandleRequest(request);
            //2 公司采购东西
            PurchaseRequest requestTelphone = new PurchaseRequest(4000.0, "Telphone");
            PurchaseRequest requestSoftware = new PurchaseRequest(10000.0, "Visual Studio");
            PurchaseRequest requestComputers = new PurchaseRequest(40000.0, "Computers");
            Approver manager = new Manager("LearningHard");
            Approver Vp = new VicePresident("Tony");
            Approver Pre = new President("BossTom");
            // 设置责任链
            manager.NextApprover = Vp;
            Vp.NextApprover = Pre;
            // 处理请求
            manager.ProcessRequest(requestTelphone);
            manager.ProcessRequest(requestSoftware);
            manager.ProcessRequest(requestComputers);
            //3 请假加薪
            CommonManager jinli = new CommonManager("金利");
            Majordomo zongjian = new Majordomo("宗剑");
            GeneralManager zhongjingli = new GeneralManager("钟精励");
            jinli.SetSuperior(zongjian);
            zongjian.SetSuperior(zhongjingli);
            RequestGZ requestqj = new RequestGZ();
            requestqj.RequestType = "请假";
            requestqj.RequestContent = "小菜请假";
            requestqj.Number = 1;
            jinli.RequestApplications(requestqj);
            RequestGZ requestqj2 = new RequestGZ();
            requestqj2.RequestType = "请假";
            requestqj2.RequestContent = "小菜请假";
            requestqj2.Number = 4;
            jinli.RequestApplications(requestqj2);
            RequestGZ requestjx3 = new RequestGZ();
            requestjx3.RequestType = "加薪";
            requestjx3.RequestContent = "小菜请求加薪";
            requestjx3.Number = 500;
            jinli.RequestApplications(requestjx3);
            RequestGZ requestjx4 = new RequestGZ();
            requestjx4.RequestType = "加薪";
            requestjx4.RequestContent = "小菜请求加薪";
            requestjx4.Number = 1000;
            jinli.RequestApplications(requestjx4);
            Console.ReadLine();





            //访问者模式（Vistor Pattern）
            //访问者模式是封装一些施加于某种数据结构之上的操作。一旦这些操作需要修改的话，接受这个操作的数据结构则可以保存不变。访问者模式适用于数据结构相对稳定的系统， 它把数据结构和作用于数据结构之上的操作之间的耦合度降低，使得操作集合可以相对自由地改变。
            //数据结构的每一个节点都可以接受一个访问者的调用，此节点向访问者对象传入节点对象，而访问者对象则反过来执行节点对象的操作。这样的过程叫做“双重分派”。节点调用访问者，将它自己传入，访问者则将某算法针对此节点执行。
            Console.WriteLine("\n访问者模式");
            ConcreteElementA elementA = new ConcreteElementA();
            elementA.SetName("ea");
            ConcreteElementB elementB = new ConcreteElementB();
            elementB.SetID(2);
            objectStructure structure = new objectStructure();
            structure.Attach(elementA);
            structure.Attach(elementB);
            Visitor visitorA = new ConcreteVisitorA();
            Visitor visitorB = new ConcreteVisitorB();
            structure.Accept(visitorA);
            structure.Accept(visitorB);
            //添加了某个字段，这样就不得不去修改元素类了。此时，我们可以使用访问者模式来解决这个问题，即把作用于具体元素的操作由访问者对象来调用
            ObjectStructure objectStructure = new ObjectStructure();
            foreach (Elementvp ev in objectStructure.Elements)
            {
                // 每个元素接受访问者访问
                ev.Accept(new ConcreteVistor());
            }
            Console.WriteLine("\n---------完-------------");
            Console.ReadLine();


            #endregion


            Console.ReadKey();
        }
    }
}
