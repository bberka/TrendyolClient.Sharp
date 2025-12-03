namespace TrendyolClient.Sharp.Common;

/// <summary>
///   https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kargo-sirketleri-listesi
/// </summary>
/// <param name="Id"></param>
/// <param name="Code"></param>
/// <param name="Name"></param>
/// <param name="TaxNumber"></param>
public readonly struct TrendyolShippingCompanies
{
    public int Id { get; }
    public string Code { get; }
    public string Name { get; }
    public string TaxNumber { get; }

    /// <summary>
    ///   The cargo company information to be sent in the requests to the createProduct V2 service and the ID values of this
    ///   information are on the table below.
    ///   Using these values, you need to integrate the necessary information into your systems.
    ///   <br></br>
    ///   <br></br>
    ///   The shipping companies you send when you send the product should not be different from the shipping company you have
    ///   approved in your Trendyol agreement.
    ///   This will prevent your products from being published.
    /// </summary>
    public static readonly TrendyolShippingCompanies[] List =
    {
        new(42, "DHLMP", "DHL Marketplace", "951-241-77-13"),
        new(38, "SENDEOMP", "Sendeo Marketplace", "2910804196"),
        new(35, "OCTOMP", "Octovan Lojistik Marketplace", "6330506845"),
        new(30, "BORMP", "Borusan Lojistik Marketplace", "1800038254"),
        new(14, "CAIMP", "Cainiao Marketplace", "0"),
        new(10, "MNGMP", "MNG Kargo Marketplace", "6080712084"),
        new(19, "PTTMP", "PTT Kargo Marketplace", "7320068060"),
        new(9, "SURATMP", "Sürat Kargo Marketplace", "7870233582"),
        new(17, "TEXMP", "Trendyol Express Marketplace", "8590921777"),
        new(6, "HOROZMP", "Horoz Kargo Marketplace", "4630097122"),
        new(20, "CEVAMP", "CEVA Marketplace", "8450298557"),
        new(4, "YKMP", "Yurtiçi Kargo Marketplace", "3130557669"),
        new(7, "ARASMP", "Aras Kargo Marketplace", "720039666")
    };

    private TrendyolShippingCompanies(
        int id,
        string code,
        string name,
        string taxNumber
    )
    {
        Id = id;
        Code = code;
        Name = name;
        TaxNumber = taxNumber;
    }
}