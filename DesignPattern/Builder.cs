using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{

    /*建造模式的实现要点：

在建造者模式中，指挥者是直接与客户端打交道的，指挥者将客户端创建产品的请求划分为对各个部件的建造请求，再将这些请求委派到具体建造者角色，具体建造者角色是完成具体产品的构建工作的，却不为客户所知道。
建造者模式主要用于“分步骤来构建一个复杂的对象”，其中“分步骤”是一个固定的组合过程，而复杂对象的各个部分是经常变化的（也就是说电脑的内部组件是经常变化的，这里指的的变化如硬盘的大小变了，CPU由单核变双核等）。
产品不需要抽象类，由于建造模式的创建出来的最终产品可能差异很大，所以不大可能提炼出一个抽象产品类。
在前面文章中介绍的抽象工厂模式解决了“系列产品”的需求变化，而建造者模式解决的是 “产品部分” 的需要变化。
由于建造者隐藏了具体产品的组装过程，所以要改变一个产品的内部表示，只需要再实现一个具体的建造者就可以了，从而能很好地应对产品组成组件的需求变化。
*/
    /// <summary>
    /// 产品类
    /// </summary>
    public class Meal
    {
        private string food;
        private string drink;
        public Meal() { }
        public void setFood(string food)
        {
            this.food = food;
        }
        public void setDrink(string drink)
        {
            this.drink = drink;
        }
        public string getFood()
        {
            return this.food;
        }
        public string getDrink()
        {
            return this.drink;
        }
    }

    /// <summary>
    ///  抽象建造者，这个场景下为 "组装人" ，这里也可以定义为接口
    /// 建造者，分别建造不同部件，然后返回整体
    /// </summary>
    public abstract class Builder
    {
        protected Meal meal = new Meal();
        public abstract void buildFood();
        public abstract void buildDrink();

        // 获得组装好
        public Meal GetMeal()
        {
            return meal;
        }
    }
    /// <summary>
    ///  具体创建者，具体的某个人为具体创建者，例如：装机A
    /// </summary>
    public class MealABuilder : Builder
    {
        public override void buildFood()
        {
            meal.setFood("A food");
        }
        public override void buildDrink()
        {
            meal.setDrink("A drink");
        }
    }
    /// <summary>
    ///  具体创建者，具体的某个人为具体创建者，例如：装机B
    /// </summary>
    public class MealBBuilder : Builder
    {
        public override void buildFood()
        {
            meal.setFood("B food");
        }
        public override void buildDrink()
        {
            meal.setDrink("B drink");
        }
    }
    /// <summary>
    ///建造者模式中的指挥者
    ///指挥创建过程类
    /// </summary>
    public class Waitor
    { 
        // 组装
        public void PrepareMeal(Builder builder)
        {
            builder.buildDrink();
            builder.buildFood();
        }
    }
}
