using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DrawingArxiveService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDrawingArxiveService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDrawingArxiveService1
    {

        [OperationContract]
        IList<DrawingItem> GetWrongDrawingList();

        [OperationContract]
        void SetRepeatArxivation(int IdDoc);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class DrawingItem
    {
        
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }


}
}
