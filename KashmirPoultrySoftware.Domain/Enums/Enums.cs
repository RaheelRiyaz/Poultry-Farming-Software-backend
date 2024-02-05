using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Domain.Enums
{
    public enum HatchStatus : byte 
    {
        Released = 1,
        InProcess = 2
    }



    public enum PaymentStatus : byte
    {
        Paid = 1,
        Pending = 2
    }


    public enum PaymentMethod : byte
    {
        Cheque = 1,
        Cash = 2,
        NetBanking = 3
    }
}
