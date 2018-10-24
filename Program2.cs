using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    /**//// <summary>
    ///     在Observer Pattern(观察者模式)中,此类作为所有Subject(目标)的抽象基类
    /// 所有要充当Subject的类(在此事例中为"猫")都继承于此类.
    ///     我们说此类作为模型,用于规划目标(即发布方)所产生的事件,及提供触发
    /// 事件的方法.
    ///     此抽象类无抽象方法,主要是为了不能实例化该类对象,确保模式完整性.
    ///     具体实施:
    ///     1.声明委托
    ///     2.声明委托类型事件
    ///     3.提供触发事件的方法
    /// </summary>
    public abstract class ModelBase
    {
        public ModelBase()
        {
        }
        /**//// <summary>
       /// 声明一个委托,用于代理一系列"无返回"及"不带参"的自定义方法
        /// </summary>
        public delegate void SubEventHandler(); 
        /**//// <summary>
        /// 声明一个绑定于上行所定义的委托的事件
        /// </summary>
        public event SubEventHandler SubEvent;
 
        /**//// <summary>
        /// 封装了触发事件的方法
        /// 主要为了规范化及安全性,除观察者基类外,其派生类不直接触发委托事件
        /// </summary>
        protected void Notify()
        {
            //提高执行效率及安全性
            if(this.SubEvent!=null)
                this.SubEvent();
                
        }
    }
    /**//// <summary>
    ///     在Observer Pattern(观察者模式)中,此类作为所有Observer(观察者)的抽象基类
    /// 所有要充当观察者的类(在此事例中为"老鼠"和"人")都继承于此类.
    ///     我们说此类作为观察者基类,用于规划所有观察者(即订阅方)订阅行为.
    ///     在此事例中,规划了针对目标基类(ModelBase)中声明的"无参无返回"委托的一个
    /// 方法(Response),并于构造该观察者时将其注册于具体目标(参数传递)的委托事件中.
    ///     具体实施过程:
    ///     1.指定观察者所观察的对象(即发布方).(通过构造器传递)
    ///     2.规划观察者自身需要作出响应方法列表
    ///     3.注册需要委托执行的方法.(通过构造器实现)
    /// </summary>
    public abstract class Observer
    {
        /**//// <summary>
        /// 构造时通过传入模型对象,把观察者与模型关联,并完成订阅.
        /// 在此确定需要观察的模型对象.
        /// </summary>
        /// <param name="childModel">需要观察的对象</param>
        public Observer(ModelBase childModel)
        {
            //订阅
            //把观察者行为(这里是Response)注册于委托事件
            childModel.SubEvent+=new ModelBase.SubEventHandler(Response);
        }
 
        /**//// <summary>
        /// 规划了观察者的一种行为(方法),所有派生于该观察者基类的具体观察者都
        /// 通过覆盖该方法来实现作出响应的行为.
        /// </summary>
        public abstract void Response();
    }
    /**//// <summary>
    /// 定义了另一个观察者基类.该观察者类型拥有两个响应行为.
    /// 并在构造时将响应行为注册于委托事件.
    /// (具体描述请参照另一观察者基类Observer)
    /// </summary>
    public abstract class Observer2
    {
        /**//// <summary>
        /// 构造时通过传入模型对象,把观察者与模型关联,并完成订阅.
        /// 在此确定需要观察的模型对象.
        /// </summary>
        /// <param name="childModel">需要观察的对象</param>
        public Observer2(ModelBase childModel)
        {
            //订阅
            //把观察者行为(这里是Response和Response2)注册于委托事件
            childModel.SubEvent+=new ModelBase.SubEventHandler(Response);
            childModel.SubEvent+=new ModelBase.SubEventHandler(Response2);
            
        }
        /**//// <summary>
        /// 规划了观察者的二种行为(方法),所有派生于该观察者基类的具体观察者都
        /// 通过覆盖该方法来实现作出响应的行为.
        /// </summary>
        public abstract void Response();
        public abstract void Response2();
    }
    /**//// <summary>
    ///     此类为观察者模式中的具体目标(即具体发布方),其继承于模型.
    /// 其中包含(调用)了在模型中被封装好的触发委托事件的方法.
    /// </summary>
    public class Cat : ModelBase
    {
        public Cat()
        {
        }
        /**//// <summary>
        /// 定义了猫的一种行为----大叫
        /// </summary>
        public void Cry()
        {
            System.Console.WriteLine("Cat Cry..");
            //调用了触发委托事件的方法.
            //通知委托开始执行观察者已订阅的方法.
            this.Notify();  
         }
    }
    /**//// <summary>
    ///     此类为观察者模式中的具体观察者(即具体发布方),其继承于观察者基类.
    /// 其中覆盖了观察者基类规划好的方法,实现了响应的具体行为.
    /// </summary>
    public class Mouse : Observer
    {
        /**//// <summary>
        /// 观察者可以拥有自己的成员(字段或者方法).
        /// 在此事例中增加了"老鼠的名字"
        /// </summary>
        private string name;
        /**//// <summary>
        ///     构造时确定观察者所需要观察的对象(具体目标),并传递给观察者基类构造器,
        /// 实现响应行为(方法)的订阅.另外,为观察者实例初始化成员.
        /// </summary>
        /// <param name="name">老鼠的名字</param>
        /// <param name="childModel">
        ///     需要观察的对象(发布方).
        ///     此处用模型基类来传递,是为了兼容所有派生于此模型的观察者,从而提高扩展性.
        /// </param>
        public Mouse(string name, ModelBase childModel) : base(childModel)
        {
            //初始化字段(老鼠的名字)
            this.name=name;         
        }
        /**//// <summary>
        /// 覆盖了该类观察者需要作出的具体响应行为.
        /// 此行为已在观察者基类中注册于委托事件,由委托事件调度执行,不需要直接调用.
        /// </summary>
        public override void Response()
        {
            //具体响应内容
            System.Console.WriteLine(this.name+"开始逃跑");
        }

    }
    /**//// <summary>
    ///     此类为观察者模式中的具体观察者(即具体发布方),其继承于观察者基类.
    /// 其中覆盖了观察者基类规划好的方法,实现了响应的具体行为.
    /// </summary>
    public class Master : Observer
    {
        /**//// <summary>
        ///     构造时确定观察者所需要观察的对象(具体目标),并传递给观察者基类构造器,
        /// 实现响应行为(方法)的订阅.
        /// </summary>
        public Master(ModelBase childModel) : base(childModel)
        {
       }
 
        /**//// <summary>
        /// 覆盖了该类观察者需要作出的具体响应行为.
        /// 此行为已在观察者基类中注册于委托事件,由委托事件调度执行,不需要直接调用.
        /// </summary>
        public override void Response()
        {
            System.Console.WriteLine("主人醒来");
        }
    }
    /**//// <summary>
    ///     此类为观察者模式中的具体观察者(即具体发布方),其继承了订阅了2个响应行为的
    /// 观察者基类.
    ///     其中覆盖了观察者基类规划好的二个方法,实现了响应的具体行为.
    /// </summary>
    public class Master2 : Observer2
    {
        /**//// <summary>
        ///     构造时确定观察者所需要观察的对象(具体目标),并传递给观察者基类构造器,
        /// 实现响应行为(方法)的订阅.
        /// </summary>
        public Master2(ModelBase childBase) : base(childBase)
        {
        }
 
        /**//// <summary>
        /// 覆盖了该类观察者需要作出的具体响应行为.
        /// 此行为已在观察者基类中注册于委托事件,由委托事件调度执行,不需要直接调用.
        /// </summary>
        public override void Response()
        {
            Console.WriteLine("baby醒来。。。。");
 
        }
        /**//// <summary>
        /// 覆盖了该类观察者需要作出的另一个响应行为.
        /// </summary>
        public override void Response2()
        {
            Console.WriteLine("开始哭闹。。。。。");
        }
    }



    




class Program
    {
        static void Main(string[] args)
        {

            //声明并实例化一个目标(即发布方)对象----猫
            Cat myCat = new Cat();
            //声明并实例化一个Mouse类型的观察者对象--名叫mouse1的老鼠.并把那只猫作为它所要观察的对象.
            Mouse myMouse1 = new Mouse("mouse1", myCat);
            //类似地生成另一只名叫mouse2的老鼠(观察者),把同一只猫作为它的观察的对象.
            Mouse myMouse2 = new Mouse("mouse2", myCat);
            //声明并实例化一个Master类型的观察者--主人,并同时把那只猫也作为他的观察对象.
            Master myMaster = new Master(myCat);
            //声明并实例化一个Master2类型的观察者--宝宝,同时把那只猫也
            Master2 myLittleMaster = new Master2(myCat);

            //猫大叫,并触发了委托事件,从而开始按顺序调用观察者已订阅的方法.
            myCat.Cry();

            Console.Read();
        }
    }
}
