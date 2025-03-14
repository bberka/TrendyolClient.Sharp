﻿// using System.Threading.Tasks;
// using TrendyolClient.Sharp.Extensions;
// using TrendyolClient.Sharp.Models;
// using TrendyolClient.Sharp.Models.Finance.Response;
//
// namespace TrendyolClient.Sharp.Services.Finance
// {
//   public partial class TrendyolFinanceClient
//   {
//     #region CARGO DETAILS
//
//     /// <summary>
//     ///   https://developers.trendyol.com/docs/muhasebe-ve-finans-entegrasyonu/kargo-faturasi-entegrasyonu
//     ///   <br /><br />
//     ///   Cari Hesap Ekstresi Entegrasyonu üzerinden transactionType='DeductionInvoices' responsundan dönen data içerisinde ki
//     ///   alanlardan transactionType değeri "Kargo Faturası" yada "Kargo Fatura" olan kayıtların "Id" değeri
//     ///   "invoiceSerialNumber" değeridir.
//     /// </summary>
//     /// <param name="invoiceSerialNumber"></param>
//     /// <returns></returns>
//     public async Task<TrendyolApiResult<ResponseGetCargoInvoiceDetails>> GetCargoInvoiceDetails(string invoiceSerialNumber) {
//       var url = $"https://api.trendyol.com/integration/finance/che/sellers/{_supplierId}/cargo-invoice/{invoiceSerialNumber}/items";
//       var request = new TrendyolRequest(HttpClient, url);
//       var result = await request.SendGetRequestAsync();
//       var data = result.Content.JsonToObject<ResponseGetCargoInvoiceDetails>();
//       return result.WithData(data);
//     }
//
//     #endregion
//   }
// }

