﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Logic
{
    class CaracLogic
    {
        public async Task<string> GetCarac(long nidMater)
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_carac/" + nidMater)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return retorno;
            }
            catch
            {
                return null;
            }
        }
    }
}
