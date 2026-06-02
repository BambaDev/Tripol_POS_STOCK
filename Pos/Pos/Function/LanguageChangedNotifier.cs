using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class LanguageChangedNotifier
    {
        public static event Action LanguageChanged;

        public static void OnLanguageChanged()
        {
            LanguageChanged?.Invoke();
        }
    }
}
