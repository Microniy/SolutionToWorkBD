using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DrawingArxiveService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DrawingArxiveService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы DrawingArxiveService.svc или DrawingArxiveService.svc.cs в обозревателе решений и начните отладку.
    public class DrawingArxiveService : IDrawingArxiveService1
    {
        protected System.Data.DataTable dataTable; 
        public IList<DrawingItem> GetWrongDrawingList()
        {
            dataTable = RepositoryServer.LocalDb.TableWrongDrawings;
            List<DrawingItem> ExitList = new List<DrawingItem>();
            foreach(System.Data.DataRow dataRow in dataTable.Rows)
            {
                DrawingItem drawingItem = new DrawingItem { ID = Convert.ToInt32(dataRow[0]), Name = dataRow[1].ToString() };
                ExitList.Add(drawingItem);
            }
            return ExitList;
        }

        public void SetRepeatArxivation(int IdDoc)
        {
            RepositoryServer.LocalDb.UpdateDrawingAsync(IdDoc);
        }
    }
}
