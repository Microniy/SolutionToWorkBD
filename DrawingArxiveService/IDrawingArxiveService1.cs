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
        bool SetRepeatArxivation(DrawingItem item);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class DrawingItem
    {
        private readonly int _id;
        private readonly string _path;

        [DataMember]
        public int ID => _id;

        [DataMember]
        public string Path => _path;
        
        public DrawingItem(int id, string path)
        {
            _id = id;
            _path = path;
        }
        public DrawingItem() : this(-1, string.Empty)
        {

        }
    }
}
