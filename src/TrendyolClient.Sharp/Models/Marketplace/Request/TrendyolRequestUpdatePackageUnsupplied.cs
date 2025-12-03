using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestUpdatePackageUnsupplied
{
  public List<PackageStatusLine> Lines { get; set; }

  /// <summary>
  ///   500	Stok tükendi	Ürünün stoğu tükenmesi ve gönderimin gecikmesi gibi sebeplerle tedarik edilememesi durumunda
  ///   seçilmelidir.
  ///   <br /><br />
  ///   501	Kusurlu/Defolu/Bozuk Ürün	Ürün kusurlu/defolu/bozuk olduğu için gönderilememesi durumunda seçilmelidir.
  ///   <br /><br />
  ///   502	Hatalı Fiyat	Yanlış fiyat beslenmesi durumunda seçilmelidir.
  ///   <br /><br />
  ///   504	Entegrasyon Hatası	Entegrasyon firmasından kaynaklı olarak hatalı fiyat ya da stok aktarımında yaşanan sorunlarda
  ///   seçilmelidir.
  ///   <br /><br />
  ///   505	Toplu Alım	Üründe yapılan indirim sonrası tek bir üründen ve aynı müşteri tarafından toplu olarak satın alınması
  ///   durumunda seçilmelidir.
  ///   <br /><br />
  ///   506	Mücbir Sebep	Doğal afet, hastalık, cenaze vb. durumlarda seçilmelidir
  /// </summary>
  public long ReasonId { get; set; }
}