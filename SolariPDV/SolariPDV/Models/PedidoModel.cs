using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace SolariPDV.Models
{
    public class PedidoModel : ObservableCollection<ItemPedidoModel>
    {
        public long ID_PEDIDO { get; set; }
        public string DS_CLIENTE { get; set; }
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public string DS_TELEFONE { get; set; }
        public DateTime DT_PEDIDO { get; set; }
        public Double? VL_PEDIDO
        {
            get
            {
                Double? total = 0;
                foreach (var it in this)
                {
                    total = total + it.VL_TOTAL;
                }
                return total;
            }
        }
        public string DS_VLPEDIDO
        {
            get
            {
                return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_PEDIDO);
            }
        }
    }

    public class ItemPedidoModel
    {
        public long ID_ITEM { get; set; }
        public long ID_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public long? ID_TAMANHO { get; set; }
        public string DS_TAMANHO { get; set; }
        public double? VL_UNITARIO { get; set; }
        public double QT_PEDIDO { get; set; }
        public double? VL_TOTAL { get { return VL_UNITARIO * QT_PEDIDO; }  }
        public string DS_VLTOTAL
        {
            get
            {
                if (VL_TOTAL == null)
                    return "R$ Váriavel";
                else
                    return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_TOTAL);
            }
        }
    }
}
