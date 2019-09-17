using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Logic
{
    public class CardapioLogic
    {
        public async Task<string> GetCardapio (string sdsFiltro)
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_cardapio/" + sdsFiltro)).Content.ReadAsStringAsync();

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
