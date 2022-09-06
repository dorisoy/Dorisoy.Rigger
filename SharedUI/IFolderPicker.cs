using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models
{
    public interface IFolderPicker
    {
        Task<string> PickFolder();
    }
}
