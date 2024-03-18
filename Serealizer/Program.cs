// See https://aka.ms/new-console-template for more information
using Serealizer.Test;


WatchTest.WatchAction mySerAction = () => Serealizer.Classes.Serealizer.Serealize(TestClass.Get());
WatchTest.WatchAction NewtonSerAction = () => Newtonsoft.Json.JsonConvert.SerializeObject(TestClass.Get());

string myJsonStr = (string)mySerAction();
string NewtonJsonStr = (string)NewtonSerAction();

WatchTest.WatchAction MyDeserAction = () => Serealizer.Classes.Serealizer.Deserealise<TestClass>(myJsonStr);
WatchTest.WatchAction NewtonDeserAction = () => Newtonsoft.Json.JsonConvert.DeserializeObject<TestClass>(NewtonJsonStr);


TestClass myDeserObj = (TestClass)MyDeserAction();
TestClass NewtonDeserObj = (TestClass)NewtonDeserAction();

Console.WriteLine($"Сереализовано: {myJsonStr}");
Console.WriteLine($"Сереализовано by Newton: {myJsonStr}");
Console.WriteLine($"Десереализация одинакова: {myDeserObj.Equals(NewtonDeserObj)}");

Console.WriteLine("\n");

//Func

WatchTest watch = new WatchTest(10000);


for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Тест #{i + 1}");
    Console.WriteLine($"Моя сериализация: {watch.Watch(mySerAction)}");
    Console.WriteLine($"Моя десериализация: {watch.Watch(NewtonSerAction)}");
    Console.WriteLine($"Newton сериализация: {watch.Watch(MyDeserAction)}");
    Console.WriteLine($"Newton десериализация: {watch.Watch(NewtonDeserAction)}");
    Console.WriteLine($"---------");

}


