using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Models.MCD
{
    public class ReceiveViewModel
    {
        public rpc_tran_mcd_receive_DTO MasterData { get; set; }
        public List<rpc_tran_mcd_receive_detail_DTO> DetailData { get; set; }
    }
}
