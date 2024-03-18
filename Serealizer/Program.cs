// See https://aka.ms/new-console-template for more information
using Serealizer.Test;


WatchTest.WatchAction MySerAction = () => Serealizer.Classes.Serealizer.Serealize(TestClass.Get());
WatchTest.WatchAction NewtonSerAction = () => Newtonsoft.Json.JsonConvert.SerializeObject(TestClass.Get());

string MyJsonStr = (string)MySerAction();
string NewtonJsonStr = (string)NewtonSerAction();

WatchTest.WatchAction MyDeserAction = () => Serealizer.Classes.Serealizer.Deserealise<TestClass>(MyJsonStr);
WatchTest.WatchAction NewtonDeserAction = () => Newtonsoft.Json.JsonConvert.DeserializeObject<TestClass>(NewtonJsonStr);


TestClass MyDeserObj = (TestClass)MyDeserAction();
TestClass NewtonDeserObj = (TestClass)NewtonDeserAction();

Console.WriteLine($"Сереализовано: {MyJsonStr}");
Console.WriteLine($"Сереализовано by Newton: {MyJsonStr}");
Console.WriteLine($"Десереализация одинакова: {MyDeserObj.Equals(NewtonDeserObj)}");

Console.WriteLine("\n");

//Func

WatchTest watch = new WatchTest(10000);


for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Тест #{i + 1}");
    Console.WriteLine($"Моя сериализация: {watch.Watch(MySerAction)}");
    Console.WriteLine($"Моя десериализация: {watch.Watch(MyDeserAction)}");
    Console.WriteLine($"Newton сериализация: {watch.Watch(NewtonSerAction)}");
    Console.WriteLine($"Newton десериализация: {watch.Watch(NewtonDeserAction)}");
    Console.WriteLine($"---------");

}


