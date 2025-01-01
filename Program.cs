using System;

class Calisan
{
    // Çalışanın bilgilerini tutacak özellikler
    public string Isim { get; set; }
    public string Soyisim { get; set; }
    public decimal Maas { get; set; }  // Maaş
    public int MesaiSaati { get; set; }    // Mesai saati
    public decimal MesaiUcreti { get; set; } // Mesai ücreti
    public decimal VergiOrani { get; set; }  // Vergi oranı
    public int YillikIzinHakki { get; set; }  // Yıllık izin hakkı
    public int KullanilanIzinGunu { get; set; }  // Kullanılan izin günleri

    // Constructor (Yapıcı metod), çalışan bilgilerini başlatmak için kullanılır
    public Calisan(string isim, string soyisim, decimal temelMaas, int mesaiSaati, decimal mesaiUcreti, decimal vergiOrani, int yillikIzinHakki, int kullanilanIzinGunu)
    {
        Isim = isim;
        Soyisim = soyisim;
        Maas = temelMaas;
        MesaiSaati = mesaiSaati;
        MesaiUcreti = mesaiUcreti;
        VergiOrani = vergiOrani;
        YillikIzinHakki = yillikIzinHakki;
        KullanilanIzinGunu = kullanilanIzinGunu;
    }

    // Maaş hesaplama metodunu oluşturuyoruz
    public decimal MaasHesapla()
    {
        decimal mesaiUcretiToplam = MesaiSaati * MesaiUcreti;  // Mesai ücretini hesapla
        decimal toplamMaas = Maas + mesaiUcretiToplam;     // Toplam maaşı hesapla

        // İzin durumu: Kullanılan izin günleri maaştan düşülecek
        decimal izinKesintisi = KullanilanIzinGunu * (Maas / YillikIzinHakki);  // Kullanılan izin günlerinin maaşa etkisi
        toplamMaas -= izinKesintisi;

        // Vergi hesaplama
        decimal vergi = toplamMaas * (VergiOrani / 100);  // Maaş üzerinden vergi hesapla
        toplamMaas -= vergi;  // Vergiyi maaştan düş

        return toplamMaas;
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Kullanıcıdan çalışan bilgilerini alıyoruz
        Console.Write("Çalışanın ismini girin: ");
        string isim = Console.ReadLine();

        Console.Write("Çalışanın soyismini girin: ");
        string soyisim = Console.ReadLine();

        Console.Write("Brüt Maaşını girin: ");
        decimal temelMaas = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Mesai saati girin: ");
        int mesaiSaati = Convert.ToInt32(Console.ReadLine());

        Console.Write("Mesai ücretini girin: ");
        decimal mesaiUcreti = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Vergi oranını girin (yüzde olarak): ");
        decimal vergiOrani = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Çalışanın yıllık izin hakkını girin (gün olarak): ");
        int yillikIzinHakki = Convert.ToInt32(Console.ReadLine());

        Console.Write("Çalışanın kullandığı izin günlerini girin: ");
        int kullanilanIzinGunu = Convert.ToInt32(Console.ReadLine());

        // Çalışan nesnesi oluşturuluyor
        Calisan calisan = new Calisan(isim, soyisim, temelMaas, mesaiSaati, mesaiUcreti, vergiOrani, yillikIzinHakki, kullanilanIzinGunu);

        // Çalışanın maaşını hesapla
        decimal toplamMaas = calisan.MaasHesapla();

        // Maaş bileşenlerini yazdırıyoruz
        decimal mesaiMaaşı = mesaiSaati * mesaiUcreti;
        decimal vergi = temelMaas * (vergiOrani / 100);
        decimal netMaas = temelMaas + mesaiMaaşı - vergi;

        // Sonuçları yazdırıyoruz
        Console.WriteLine($"Brüt Maaş: {temelMaas:F2} TL");
        Console.WriteLine($"Mesai Ücreti: {mesaiMaaşı:F2} TL");
        Console.WriteLine($"Vergi (yüzde {vergiOrani}%): {vergi:F2} TL");
        Console.WriteLine($"Net Maaş (vergi ve mesai sonrası): {netMaas:F2} TL");
    }
}
