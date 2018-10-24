using System;
using System.Collections;
namespace CSharp_Observe
{
    class Program
    {
        // Fields
        private ArrayList observers = new ArrayList();   //在program类中定义一个动态私有数组 取名observers
        // Methods
        public void Attach(Observer observer)
        {
            observers.Add(observer);                //在该类中定义添加功能  给私有成员observes添加内容
        }
        public void Detach(Observer observer)
        {
            observers.Remove(observer);         //在该类中定义移除功能  给私有成员observes移除内容
        }
        public void Notify()
        {
            foreach (Observer o in observers)      //遍历该数组  每次都执行更新
                o.Update();
        }
    }
    class ConcreteSubject : Program    // 继承了Program类
    {
        // Fields
        private string subjectState;
        // Properties
        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }
    }
    // "Observer"
    abstract class Observer
    {
        // Methods
        abstract public void Update();          //定义抽象方法 观察者
    }
    // "ConcreteObserver"
    class ConcreteObserver : Observer
    {
        // Fields
        private string name;
        private string observerState;
        private ConcreteSubject subject;
        // Constructors
        public ConcreteObserver(ConcreteSubject subject,
          string name)
        {
            this.subject = subject;
            this.name = name;
        }
        // Methods
        override public void Update()
        {
            observerState = subject.SubjectState;
            Console.WriteLine("Observer {0}'s new state is {1}",
              name, observerState);
        }
        // Properties
        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
    public class Client
    {
        public static void Main(string[] args)
        {
            // 先声明出基础的数据  以及初始化操作
            ConcreteSubject s = new ConcreteSubject();
            s.Attach(new ConcreteObserver(s, "1"));
            s.Attach(new ConcreteObserver(s, "2"));
            s.Attach(new ConcreteObserver(s, "3"));
            // 改变s   并且通知观察者  也就是调用函数  Notify
            s.SubjectState = "ABC";
            s.Notify();
            Console.ReadKey();
        }
    }
}