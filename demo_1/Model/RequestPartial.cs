using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_1.Model
{
    public partial class Request
    {
        db_demo_2024Context dbContext = new db_demo_2024Context();
        public string ClientFIO { get { return $"{ClientSurname} {ClientName} {ClientPatronymic}"; } }
        public string RequestStatus 
        { get 
            {
                if (dbContext.RequestStatuses
                                .Include(x => x.Status)
                                .Where(x => x.RequestId == RequestId)
                                .OrderByDescending(x => x.ChangeDate).ToList().Count == 0)
                {
                    return "Статус не найден";
                }
                return dbContext.RequestStatuses
                                .Include(x => x.Status)
                                .Where(x => x.RequestId == RequestId)
                                .OrderByDescending(x => x.ChangeDate).ToList()[0].Status.StatusName;
            } 
        }
    }
}
