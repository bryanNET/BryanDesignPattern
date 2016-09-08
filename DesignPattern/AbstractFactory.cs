using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    //抽象实体
    //简单工厂是一个工厂生产多个产品；工厂方法是拆分成子工厂，分别生产各自产品；抽象工厂整合工厂方法和简单工厂，随着子工厂规模变大，也可以生产多个类似产品。
    public abstract class absSalary
    {
        /// <summary>
        /// 工资
        /// </summary>
        protected double salary;
        /// <summary>
        /// 奖金
        /// </summary>
        protected double bonus;
        /// <summary>
        /// 税
        /// </summary>
        protected double tax;
        public absSalary(double sal, double bns, double t)
        {
            this.salary = sal;
            this.bonus = bns;
            this.tax = t;
        }
        public abstract double CalculateTax();
    }
    public class ChineseSalary : absSalary
    {
        public ChineseSalary(double sal, double bns, double t)
            : base(sal, bns, t)
        {
        }
        public override double CalculateTax()
        {
            return (base.salary + base.bonus - 3500) * base.tax;
        }
    }
    public class ForeignerSalary : absSalary
    {
        public ForeignerSalary(double sal, double bonus, double tax)
            : base(sal, bonus, tax)
        {
        }
        /// <summary>
        /// 计算纳税
        /// </summary>
        /// <returns></returns>
        public override double CalculateTax()
        {
            return (base.salary + base.bonus - 4000) * base.tax;
        }
    }

    public abstract class absSocialSecurity
    {
        /// <summary>
        /// 社会保障
        /// </summary>
        protected double SocialSecurity;

        public absSocialSecurity()
        {
            this.SocialSecurity = 0;
        }
        /// <summary>
        /// 得到 社会保障
        /// </summary>
        /// <returns></returns>
        public virtual double GetSocialSecurity()
        {
            return this.SocialSecurity;
        }
    }
    public class ChineseSocialSecurity : absSocialSecurity
    {
        public ChineseSocialSecurity(double socialsecurity)
            : base()
        {
            base.SocialSecurity = socialsecurity < 1000 ? 1000 : socialsecurity;
        }
    }
    //外国人社会保险
    public class ForeignerSocialSecurity : absSocialSecurity
    {
        public ForeignerSocialSecurity(double socialsecurity)
            : base()
        {
            base.SocialSecurity = socialsecurity < 1500 ? 1500 : socialsecurity;
        }
    }

    //抽象工厂      ，生产一系列产品（多个Create方法，分别对应不同产品）
    public interface AbstractFactory
    {
        /// <summary>
        /// abs薪水
        /// bonus 奖金  tax 税
        /// </summary>       
        absSalary CreateSalary(double sal, double bonus, double tax);
        //bs的社会保障
        absSocialSecurity CreateSocialSecurity(double socialsecurity);
    }
    //中国工厂
    public class ChineseFactory : AbstractFactory
    {
        public absSalary CreateSalary(double sal, double bonus, double tax)
        {
            return new ChineseSalary(sal, bonus, tax);
        }
        public absSocialSecurity CreateSocialSecurity(double socialsecurity)
        {
            return new ChineseSocialSecurity(socialsecurity);
        }
    }

    //外国人的工厂
    public class ForeignerFactory : AbstractFactory
    {
        public absSalary CreateSalary(double sal, double bonus, double tax)
        {
            return new ForeignerSalary(sal, bonus, tax);
        }
        public absSocialSecurity CreateSocialSecurity(double socialsecurity)
        {
            return new ForeignerSocialSecurity(socialsecurity);
        }
    }
}
