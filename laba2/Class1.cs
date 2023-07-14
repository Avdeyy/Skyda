using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    internal class ProtectionSystem
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int PastDays { get; set; }
        public int PLN { get; set; }
        public int FPLN { get; set; }
        int CFPLN { get; set; }
        public virtual bool ProtectionCheck()
        {
            Date = Date.AddDays(1);
            PastDays++;
            if (FPLN == CFPLN)
            {
                return true;
            }
            else
            {
                CFPLN++;
                return false;
            };
        }

        public virtual void GetAttack()
        {
            if (FPLN == PLN)
            {
                Console.WriteLine("КАК ЭТО ПРОИЗОШЛО???...");
                return;
            };

            Random random = new Random();
            int x = random.Next(0, 100);

            if (x >= 50)
            {
                FPLN++;
                Console.WriteLine("Ему повезло! Будьте готовы к следующим атакам!");
            }
            else Console.WriteLine("Мы непобедимы!");
        }
    }

    internal class Skyda
    {
        public int FPLNS { get; set; }
        public int KFPLN { get; set; }
        public ProtectionSystem Protectionsystem { get; set; }
        public event ProtectionFallHandler ProtectionFall;
        public virtual void Attack()
        {
            Protectionsystem.GetAttack();
            FPLNS = Protectionsystem.FPLN;
            NotifyProtectionFall();
        }
        public virtual void NotifyProtectionFall()
        {
            bool a = Protectionsystem.ProtectionCheck();
            if (a == false)
            {
                ProtectionFall?.Invoke(this, new ProtectionFallEventArgs(FPLNS, Protectionsystem));
            }
        }
    }

    internal interface IReactProtectionFall
    {
        public int LRN { get; set; }
        public string Message { get; set; }
        public void Subscribe(Skyda skyda)
        {
            skyda.ProtectionFall += OnProtectionFall;
        }
        public void OnProtectionFall(object x, ProtectionFallEventArgs y)
        {

        }
    }

    internal class BasicLayerNotifier : IReactProtectionFall
    {
        public string Message { get; set; }
        public int LRN { get; set; }
        public void OnProtectionFall(object sender, ProtectionFallEventArgs args)
        {
            Skyda skyda = sender as Skyda;
            if (skyda != null)
            {
                Message = $"Система: {skyda.Protectionsystem.Title}, Дата: {skyda.Protectionsystem.Date}, Номер прорванного слоя: {skyda.FPLNS}";
                Console.WriteLine(Message);
            }
            else
            {
                Console.WriteLine("Преобразование типов не удалось((");
            }
        }
        public void Subscribe(Skyda skyda)
        {
            skyda.ProtectionFall += OnProtectionFall;
        }
        public void Unsubscribe(Skyda skyda)
        {
            skyda.ProtectionFall -= OnProtectionFall;
        }
    }

    internal class EndLayerNotifier : IReactProtectionFall
    {
        public string Message { get; set; }
        public int LRN { get; set; }
        public void OnProtectionFall(object sender, ProtectionFallEventArgs args)
        {
            Skyda skyda = sender as Skyda;
            if (skyda != null)
            {
                Message = $"Система:{skyda.Protectionsystem.Title}, Дата:{skyda.Protectionsystem.Date}, Номер прорванного слоя:{skyda.FPLNS}, Кол-во прошедших дней:{skyda.Protectionsystem.PastDays}";
                Console.WriteLine(Message);
            }
            else
            {
                Console.WriteLine("Преобразование типов не удалось((");
            }
        }
        public void Subscribe(Skyda skyda)
        {
            skyda.ProtectionFall += OnProtectionFall;
        }
        public void Unsubscribe(Skyda skyda)
        {
            skyda.ProtectionFall -= OnProtectionFall;
        }
    }

    delegate void ProtectionFallHandler(object sender, ProtectionFallEventArgs args);

    internal class ProtectionFallEventArgs : EventArgs
    {
        public ProtectionFallEventArgs(int falledProtectionLayersNumber, ProtectionSystem system)
        {
            FPLSN = falledProtectionLayersNumber;
            System = system;
        }
        public int FPLSN { get; set; }
        public ProtectionSystem System { get; set; }
    }
}
