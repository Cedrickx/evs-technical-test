using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public class TVAService : ITVAService
    {
        /// <summary>
        /// Vérifie si un numéro de TVA est valide
        /// </summary>
        /// <param name="tvaNumber">Numéro à vérifier</param>
        /// <returns>True si valide, false sinon</returns>
        public bool IsValid(string tvaNumber)
        {
            if (String.IsNullOrEmpty(tvaNumber))
                return false;

            if (!tvaNumber.All(char.IsDigit))
                return false;

            if (tvaNumber.Length != 15)
                return false;

            // Check with external API call
            // ...

            return true;
        }
    }
}
