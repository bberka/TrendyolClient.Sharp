using System.Collections.Generic;

public readonly struct TrendyolShipmentProvider
{
    public int Id { get; }
    public string Code { get; }
    public string Name { get; }
    public string TaxNumber { get; }

    private TrendyolShipmentProvider(
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

    public static readonly TrendyolShipmentProvider KolayGelsin = new(38, "SENDEOMP", "Kolay Gelsin Marketplace", "2910804196");

    public static readonly TrendyolShipmentProvider Borusan = new(30, "BORMP", "Borusan Lojistik Marketplace", "1800038254");

    public static readonly TrendyolShipmentProvider DHL = new(10, "DHLECOMMP", "DHL eCommerce Marketplace", "6080712084");

    public static readonly TrendyolShipmentProvider PTT = new(19, "PTTMP", "PTT Kargo Marketplace", "7320068060");

    public static readonly TrendyolShipmentProvider Surat = new(9, "SURATMP", "Sürat Kargo Marketplace", "7870233582");

    public static readonly TrendyolShipmentProvider TrendyolExpress = new(17, "TEXMP", "Trendyol Express Marketplace", "8590921777");

    public static readonly TrendyolShipmentProvider Horoz = new(6, "HOROZMP", "Horoz Kargo Marketplace", "4630097122");

    public static readonly TrendyolShipmentProvider Ceva = new(20, "CEVAMP", "CEVA Marketplace", "8450298557");

    public static readonly TrendyolShipmentProvider Yurtici = new(4, "YKMP", "Yurtiçi Kargo Marketplace", "3130557669");

    public static readonly TrendyolShipmentProvider Aras = new(7, "ARASMP", "Aras Kargo Marketplace", "720039666");

    public static IEnumerable<TrendyolShipmentProvider> GetAll()
    {
        yield return KolayGelsin;
        yield return Borusan;
        yield return DHL;
        yield return PTT;
        yield return Surat;
        yield return TrendyolExpress;
        yield return Horoz;
        yield return Ceva;
        yield return Yurtici;
        yield return Aras;
    }
}