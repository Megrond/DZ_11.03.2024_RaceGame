/* Игра «Автомобильные гонки»
 Разработать игру "Автомобильные гонки" с использованием делегатов.
	  1. В игре использовать несколько типов автомобилей: спортивные, легковые, грузовые и автобусы.
	  2. Реализовать игру «Гонки». Принцип игры: Автомобили двигаются от старта к финишу со скоростями, которые
	     изменяются в установленных пределах случайным образом. Победителем считается автомобиль, пришедший к финишу первым.
	Рекомендации по выполнению работы
	  1. Разработать абстрактный класс «автомобиль» (класс Car). Собрать в нем все общие поля,
	     свойства (например, скорость) методы (например, ехать).
	  2. Разработать классы автомобилей с конкретной реализацией конструкторов и методов, свойств.
	     В классы автомобилей добавить необходимые события (например, финиш).
	  3. Класс игры должен производить запуск соревнований автомобилей, выводить сообщения о текущем положении
	     автомобилей, выводить сообщение об автомобиле, победившем в гонках. Создать делегаты, обеспечивающие
	     вызов методов из классов автомобилей (например, выйти на старт, начать гонку).
	  4. Игра заканчивается, когда какой-то из автомобилей проехал определенное расстояние
	     (старт в положении 0, финиш в положении 100). Уведомление об окончании гонки
	     (прибытии какого-либо автомобиля на финиш) реализовать с помощью событий.*/

using static System.Console;

delegate void MoveDelegate();

class Car
{
    public string name;
    public int speed;

    public Car(string name)
    {
        this.name = name;
        this.speed = 0;
    }

    public virtual void Drive()
    {
        WriteLine($"{name} движется со скоростью {speed} км/ч");
    }

    public virtual void Accelerate(int minSpeed, int maxSpeed)
    {
        Random random = new Random();
        this.speed = random.Next(minSpeed, maxSpeed);
    }
}

class SportsCar : Car
{
    public SportsCar(string name) : base(name) { }

    public override void Drive()
    {
        WriteLine($"Спортивный автомобиль {name} движется со скоростью {speed} км/ч");
    }
}

class PassengerCar : Car
{
    public PassengerCar(string name) : base(name) { }

    public override void Drive()
    {
        WriteLine($"Легковой автомобиль {name} движется со скоростью {speed} км/ч");
    }
}

class Truck : Car
{
    public Truck(string name) : base(name) { }

    public override void Drive()
    {
        WriteLine($"Грузовик {name} движется со скоростью {speed} км/ч");
    }
}

class Bus : Car
{
    public Bus(string name) : base(name) { }

    public override void Drive()
    {
        WriteLine($"Автобус {name} движется со скоростью {speed} км/ч");
    }
}

class RaceGame
{
    public event EventHandler<string> RaceFinished;

    public List<Car> cars;

    public RaceGame()
    {
        cars = new List<Car>();
    }

    public void AddCar(Car car)
    {
        cars.Add(car);
    }

    public void StartRace(int minSpeed, int maxSpeed)
    {
        foreach (Car car in cars)
        {
            car.Accelerate(minSpeed, maxSpeed);
        }

        Car winner = cars.OrderBy(car => car.speed).Last();
        RaceFinished?.Invoke(this, $"Победил: {winner.name} со скоростью {winner.speed} км/ч");
    }
}

class Program
{
    static void Main(string[] args)
    {
        RaceGame raceGame = new RaceGame();

        SportsCar sportsCar = new SportsCar("Спортивный автомобиль");
        PassengerCar passengerCar = new PassengerCar("Легковой автомобиль");
        Truck truck = new Truck("Грузовик");
        Bus bus = new Bus("Автобус");

        raceGame.AddCar(sportsCar);
        raceGame.AddCar(passengerCar);
        raceGame.AddCar(truck);
        raceGame.AddCar(bus);

        raceGame.RaceFinished += (sender, result) =>
        {
            WriteLine(result);

            foreach (Car car in raceGame.cars)
            {
                WriteLine($"{car.name} проехал {car.speed} км");
            }
        };

        const int minSpeed = 20;
        const int maxSpeed = 120;

        raceGame.StartRace(minSpeed, maxSpeed);
    }
}
