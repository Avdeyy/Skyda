// See https://aka.ms/new-console-template for more information

using laba2;

ProtectionSystem protectionSystem = new ProtectionSystem()
{
    Title = "SkyConqueror",
    Date = new DateTime(2023, 3, 23),
    PastDays = 0,
    PLN = 5,
    FPLN = 0,
};

Skyda skyda = new Skyda()
{
    KFPLN = 0,
    Protectionsystem = protectionSystem,
};

BasicLayerNotifier basicLayerNotifier = new BasicLayerNotifier();
EndLayerNotifier endLayerNotifier = new EndLayerNotifier();

basicLayerNotifier.Subscribe(skyda);
while (skyda.FPLNS != protectionSystem.PLN)
{
    if (skyda.FPLNS == protectionSystem.PLN - 1)
    {
        basicLayerNotifier.Unsubscribe(skyda);
        endLayerNotifier.Subscribe(skyda);
        skyda.Attack();
        endLayerNotifier.Unsubscribe(skyda);
        Console.ReadKey();
        continue;

    }
    skyda.Attack();
    Console.ReadKey();
}