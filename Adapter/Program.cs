//Є в нас така система: нові розетки, пристрої нового формату, що можно юзати напряму,
//пристрої старого формату, що можна підключити через один перехідник, та пристрої китайського формату,
//для яких потрібно у перший перехідник втикати ще один (для якогось китайського телефона) 
//Задача: описати таку систему.
using System;
namespace AdapterExample
{
    // Система яку будемо адаптовувати (розетки старого формату)
    class OldElectricitySystem
    {
        public string MatchOldSocket()
        {
            return "Щось втикнули у стару розетку";
        }
    }
    // Інтерфейс нової системи (тобто нові розеточки)
    interface INewElectricitySystem
    {
        string MatchNewSocket();
    }
    
    // Ну і власне пристрої нової системи (чи то розетка чи якийсь пристрій)
    class NewElectricitySystem : INewElectricitySystem
    {
        public string MatchNewSocket()
        {
            return "Щось втикнули у нову розетку";
        }
    }

    // Адаптер назовні виглядає як нові євророзетки, шляхом наслідування прийнятного у 
    // системі інтерфейсу, хоча на виході стоїть старий формат.
    //Тобто у нього ми втикаємо пристрої нового формату, а його втикаємо у старі розеточки
    class FirstAdapter : INewElectricitySystem
    {
        // Але всередині він старий
        private readonly OldElectricitySystem _adaptee;
        public FirstAdapter(OldElectricitySystem adaptee)
        {
            _adaptee = adaptee;
        }

        // А тут відбувається вся магія: наш адаптер «перекладає»
        // функціональність із нового стандарту на старий
        public string MatchNewSocket() // отримуємо вілку нового формату, а повертаємо старого
        {
            Console.WriteLine("Перший адаптер отримав вiлку нового формату");
            return _adaptee.MatchOldSocket();
        }
    }

    // інтерфейс китайської системи (описує пристрої китайського формату)
    interface IChineesElectricitySystem
    {
        string MatchChineesSocket();
    }
    // Самі пристрої китайського формату (чи то китайська розетка, чи то пристрій)
    class ChineesElectricitySystem : IChineesElectricitySystem
    {
        public string MatchChineesSocket()
        {
            return "chinees interface";
        }
    }
    // Адаптер назовні виглядає як розетки китайського формату, шляхом наслідування прийнятного у 
    // системі китайського інтерфейсу, хоча на виході стоїть новий (євро) формат.
    // Тобто у нього ми втикаємо пристрої китайського формату, а його втикаємо у нові розеточки
    class SecondAdapter : IChineesElectricitySystem
    {
        // Але всередині він старий
        private readonly INewElectricitySystem _adaptee;
        public SecondAdapter(INewElectricitySystem adaptee)
        {
            _adaptee = adaptee;
        }

        // А тут відбувається вся магія: наш адаптер «перекладає»
        // функціональність із китайського стандарту на новий
        public string MatchChineesSocket()// адаптер отримуємо вілку китайського формату, а повертаємо нового
        {
            Console.WriteLine("Другий адаптер отримав вiлку китайського формату");
            return _adaptee.MatchNewSocket();
        }
    }

    class  ElectricityConsumer // пристрої різних формату
    {
        // Зарядний пристрій , який розуміє тільки нову систему (тобто він заряджається від нових розеточек)
        public static void ChargeNotebook(INewElectricitySystem electricitySystem)
        {
            Console.WriteLine(electricitySystem.MatchNewSocket());
        }
        // Зарядний пристрій, який розуміє тільки китайську систему (тобто його ми повінні втикнути у китайську розеточку)
        public static void ChargeChineesNotebook(IChineesElectricitySystem electricitySystem)
        {
            Console.WriteLine(electricitySystem.MatchChineesSocket());
        }
    }

    public class AdapterDemo
    {
        static void Main()
        {
            // 1) Ми можемо користуватися новою системою без проблем
            var newElectricitySystem = new NewElectricitySystem();
            ElectricityConsumer.ChargeNotebook(newElectricitySystem); // втикаємо пристрій нового формату у нову розетку
            Console.WriteLine("-------");
            // 2) Ми повинні адаптуватися до старої системи, використовуючи адаптер
            var oldElectricitySystem = new OldElectricitySystem();
            INewElectricitySystem firstAdapter = new FirstAdapter(oldElectricitySystem); // показуємо адаптеру в яку саме стару розетку його будемо втикати
            ElectricityConsumer.ChargeNotebook(firstAdapter);     // втикаємо ноут у адаптер, а адаптер у розетку
            Console.WriteLine("-------");
            // 3) Перевіряємо підключення китайського пристрою то мережі
            var chineesElectricitySystem = new ChineesElectricitySystem();
            var secondAdapter = new SecondAdapter(firstAdapter);  // показуємо адаптеру куди його будемо втикати (у перший адаптер)
            firstAdapter = new FirstAdapter(oldElectricitySystem);// показуємо першому адаптеру куди будемо його втикати (у стару розетку)
            ElectricityConsumer.ChargeChineesNotebook(secondAdapter); // втикаємо китайський пристрій у вторий адаптер, вторий адаптер у перший, а перший у розетку
            Console.WriteLine("-------");
            // 4) Також можемо другий адаптер пітключити у розетку нового формату
            secondAdapter = new SecondAdapter(newElectricitySystem);  // показуємо адаптеру куди його будемо втикати (у перший адаптер)
            ElectricityConsumer.ChargeChineesNotebook(secondAdapter); // втикаємо китайський пристрій у вторий адаптер, вторий адаптер у перший, а перший у розетку

            Console.ReadKey();
        }
    }
}
