using System;
using System.Collections.Generic;

// Temel Çalışan sınıfı
class Calisan
{
    public string Isim { get; set; }
    public string Soyisim { get; set; }
    public decimal Maas { get; set; }  // Brüt maaş
    public int MesaiSaati { get; set; }  // Mesai saati
    public decimal MesaiUcreti { get; set; } // Mesai ücreti
    public decimal VergiOrani { get; set; }  // Vergi oranı
    public int YillikIzinHakki { get; set; }  // Yıllık izin hakkı
    public int KullanilanIzinGunu { get; set; }  // Kullanılan izin günleri

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

    public virtual decimal MaasHesapla()
    {
        decimal mesaiUcretiToplam = MesaiSaati * MesaiUcreti;
        decimal toplamMaas = Maas + mesaiUcretiToplam;

        decimal izinKesintisi = KullanilanIzinGunu * (Maas / YillikIzinHakki);
        toplamMaas -= izinKesintisi;

        decimal vergi = toplamMaas * (VergiOrani / 100);
        toplamMaas -= vergi;

        return toplamMaas;
    }
}

// Tam Zamanlı Çalışan sınıfı
class TamZamanliCalisan : Calisan
{
    public TamZamanliCalisan(string isim, string soyisim, decimal temelMaas, int mesaiSaati, decimal mesaiUcreti, decimal vergiOrani, int yillikIzinHakki, int kullanilanIzinGunu)
        : base(isim, soyisim, temelMaas, mesaiSaati, mesaiUcreti, vergiOrani, yillikIzinHakki, kullanilanIzinGunu)
    {
    }

    public override decimal MaasHesapla()
    {
        decimal toplamMaas = base.MaasHesapla();
        toplamMaas += 500; // Tam zamanlı çalışanlara ek 500 TL bonus
        return toplamMaas;
    }
}

// Stajyer sınıfı
class Stajyer : Calisan
{
    public Stajyer(string isim, string soyisim, decimal temelMaas, int mesaiSaati, decimal mesaiUcreti, decimal vergiOrani, int yillikIzinHakki, int kullanilanIzinGunu)
        : base(isim, soyisim, temelMaas, mesaiSaati, mesaiUcreti, vergiOrani, yillikIzinHakki, kullanilanIzinGunu)
    {
    }

    public override decimal MaasHesapla()
    {
        decimal toplamMaas = base.MaasHesapla();
        toplamMaas -= toplamMaas * 0.1m; // %10 stajyer kesintisi
        return toplamMaas;
    }
}

// Program sınıfı
class Program
{
    static void Main(string[] args)
    {
        List<Calisan> calisanlar = new List<Calisan>();

        while (true)
        {
            Console.WriteLine("\nÇalışan Ekleme:");
            Console.WriteLine("1. Tam Zamanlı Çalışan");
            Console.WriteLine("2. Stajyer");
            Console.WriteLine("3. Çıkış ve Maaşları Göster");
            Console.Write("Seçiminiz: ");
            int secim = Convert.ToInt32(Console.ReadLine());

            if (secim == 3)
                break;

            Console.Write("İsim: ");
            string isim = Console.ReadLine();
            Console.Write("Soyisim: ");
            string soyisim = Console.ReadLine();
            Console.Write("Brüt Maaş: ");
            decimal temelMaas = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Ek Mesai Saati: ");
            int mesaiSaati = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ek Mesai Ücreti: ");
            decimal mesaiUcreti = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Vergi Oranı (%): ");
            decimal vergiOrani = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Yıllık İzin Hakkı (gün): ");
            int yillikIzinHakki = Convert.ToInt32(Console.ReadLine());
            Console.Write("Kullanılan İzin Günleri: ");
            int kullanilanIzinGunu = Convert.ToInt32(Console.ReadLine());

            if (secim == 1)
            {
                calisanlar.Add(new TamZamanliCalisan(isim, soyisim, temelMaas, mesaiSaati, mesaiUcreti, vergiOrani, yillikIzinHakki, kullanilanIzinGunu));
            }
            else if (secim == 2)
            {
                calisanlar.Add(new Stajyer(isim, soyisim, temelMaas, mesaiSaati, mesaiUcreti, vergiOrani, yillikIzinHakki, kullanilanIzinGunu));
            }
            else
            {
                Console.WriteLine("Hatalı seçim, lütfen tekrar deneyin.");
            }
        }

        Console.WriteLine("\nÇalışanların Maaşları:");
        foreach (var calisan in calisanlar)
        {
            Console.WriteLine($"{calisan.Isim} {calisan.Soyisim} - Net Maaş: {calisan.MaasHesapla():F2} TL");
        }
    }
}
