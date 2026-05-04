using System;
using System.Collections.Generic;


public abstract class Person
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public int NoTelepon { get; set; }

    public Person(string Nama, string Alamat, int NoTelepon)
    {
        this.Nama = Nama;
        this.Alamat = Alamat;
        this.NoTelepon = NoTelepon;
    }

    public abstract string GetInfo();
}

public abstract class Produk
{
    public string kode { get; set; }
    public string NamaProduk { get; set; }
    public double Harga { get; set; } // fix: string → double
    public int Stok { get; set; }

    public Produk(string kode, string NamaProduk, double Harga, int Stok)
    {
        this.kode = kode;
        this.NamaProduk = NamaProduk;
        this.Harga = Harga;
        this.Stok = Stok;
    }

    public abstract double HitungHarga();
}



class Pelanggan : Person
{
    protected decimal _total;

    public Pelanggan(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon)
    {
        _total = total;
    }

    public virtual decimal HitungDiskon()
    {
        if (_total > 100000)
            return _total * 0.1m; // diskon 10% jika total > 100.000
        return 0;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}";
    }
}

class PelangganReguler : Pelanggan
{
    public PelangganReguler(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon, total) { }

    public override decimal HitungDiskon()
    {
        return base.HitungDiskon(); // ikut aturan induk: 10% jika > 100.000
    }

    public override string GetInfo()
    {
        return base.GetInfo() + ", Tipe: Reguler";
    }
}

class PelangganVIP : Pelanggan
{
    public PelangganVIP(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon, total) { }

    public override decimal HitungDiskon()
    {
        return _total * 0.20m; // fix: VIP selalu dapat diskon 20%
    }

    public override string GetInfo()
    {
        return base.GetInfo() + ", Tipe: VIP";
    }
}


// ================================================================
// TEKNISI — Pewarisan dari Person
// ================================================================
class Teknisi : Person
{
    public string Spesialisasi { get; set; }
    public string JamKerja { get; set; }

    public Teknisi(string Nama, string Alamat, int NoTelepon, string Spesialisasi, string JamKerja)
        : base(Nama, Alamat, NoTelepon)
    {
        this.Spesialisasi = Spesialisasi;
        this.JamKerja = JamKerja;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Spesialisasi: {Spesialisasi}";
    }

    public virtual decimal HitungKomisi()
    {
        if (Spesialisasi == "Elektronik") return 50000;
        if (Spesialisasi == "Komputer") return 75000;
        return 0;
    }
}


// ================================================================
// KARYAWAN ADMIN — Pewarisan dari Person
// ================================================================
class KaryawanAdmin : Person
{
    public string Divisi { get; set; }
    public string LevelAkses { get; set; }

    public KaryawanAdmin(string Nama, string Alamat, int NoTelepon, string Divisi, string LevelAkses)
        : base(Nama, Alamat, NoTelepon)
    {
        this.Divisi = Divisi;
        this.LevelAkses = LevelAkses;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Divisi: {Divisi}, Level Akses: {LevelAkses}";
    }
}


// ================================================================
// PRODUK — Pewarisan dari Produk
// ================================================================
class Sparepart : Produk
{
    public string Merk { get; set; }
    public string Kategori { get; set; }

    public Sparepart(string kode, string NamaProduk, double Harga, int Stok, string Kategori, string Merk)
        : base(kode, NamaProduk, Harga, Stok)
    {
        this.Kategori = Kategori;
        this.Merk = Merk;
    }

    public override double HitungHarga()
    {
        return Harga; // harga normal
    }
}

// Polimorfisme — ProdukBekas hitung harga berbeda
class ProdukBekas : Produk
{
    public string Kondisi { get; set; } // "Baik", "Cukup", "Rusak Ringan"
    public double DiskonPersen { get; set; }

    public ProdukBekas(string kode, string NamaProduk, double Harga, int Stok, string Kondisi, double DiskonPersen)
        : base(kode, NamaProduk, Harga, Stok)
    {
        this.Kondisi = Kondisi;
        this.DiskonPersen = DiskonPersen;
    }

    public override double HitungHarga()
    {
        return Harga - (Harga * DiskonPersen / 100); // harga dikurangi diskon kondisi
    }
}


// ================================================================
// PENJUALAN — Enkapsulasi (field private, akses via method)
// ================================================================
class Penjualan
{
    private int _idPelanggan;
    private int _idKaryawan;
    private double _total;
    private double _diskon;
    private string _status;
    private DateTime _tanggal;

    // Detail item yang dibeli
    private List<DetailPenjualan> _details = new List<DetailPenjualan>();

    public Penjualan(int idPelanggan, int idKaryawan)
    {
        _idPelanggan = idPelanggan;
        _idKaryawan = idKaryawan;
        _status = "lunas";
        _tanggal = DateTime.Now;
    }

    public void TambahDetail(DetailPenjualan detail)
    {
        _details.Add(detail);
        _total += detail.HitungSubtotal();
    }

    public void SetDiskon(double diskon) => _diskon = diskon;
    public double GetTotal() => _total;
    public double GetTotalBayar() => _total - _diskon;
    public string GetStatus() => _status;

    public void CetakNota()
    {
        Console.WriteLine("========== NOTA PENJUALAN ==========");
        Console.WriteLine($"Tanggal  : {_tanggal}");
        Console.WriteLine($"Status   : {_status}");
        Console.WriteLine("------------------------------------");
        foreach (var d in _details)
            Console.WriteLine($"  {d.NamaProduk} x{d.Jumlah} = Rp {d.HitungSubtotal():N0}");
        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Total    : Rp {_total:N0}");
        Console.WriteLine($"Diskon   : Rp {_diskon:N0}");
        Console.WriteLine($"Dibayar  : Rp {GetTotalBayar():N0}");
        Console.WriteLine("====================================");
    }
}


// ================================================================
// DETAIL PENJUALAN — item per baris di nota
// ================================================================
class DetailPenjualan
{
    public string NamaProduk { get; set; }
    public int Jumlah { get; set; }
    public double HargaSatuan { get; set; }

    public DetailPenjualan(string NamaProduk, int Jumlah, double HargaSatuan)
    {
        this.NamaProduk = NamaProduk;
        this.Jumlah = Jumlah;
        this.HargaSatuan = HargaSatuan;
    }

    public double HitungSubtotal()
    {
        return Jumlah * HargaSatuan;
    }
}


// ================================================================
// TIKET SERVIS — Enkapsulasi
// ================================================================
class TiketServis
{
    private int _idPelanggan;
    private Teknisi _teknisi;      // asosiasi: punya Teknisi, bukan turunan
    private string _keluhan;
    private string _diagnosa;
    private string _status;
    private double _biayaJasa;
    private DateTime _tglMasuk;

    private List<DetailServis> _details = new List<DetailServis>();

    public TiketServis(int idPelanggan, Teknisi teknisi, string keluhan)
    {
        _idPelanggan = idPelanggan;
        _teknisi = teknisi;
        _keluhan = keluhan;
        _status = "antrian";
        _tglMasuk = DateTime.Now;
    }

    public void SetDiagnosa(string diagnosa) => _diagnosa = diagnosa;
    public void SetBiayaJasa(double biaya) => _biayaJasa = biaya;
    public string GetStatus() => _status;

    public void UpdateStatus(string statusBaru)
    {
        _status = statusBaru;
        Console.WriteLine($"Status tiket diupdate: {_status}");
    }

    public void TambahDetailServis(DetailServis detail)
    {
        _details.Add(detail);
    }

    public double HitungTotalBiaya()
    {
        double totalSparepart = 0;
        foreach (var d in _details)
            totalSparepart += d.HitungSubtotal();
        return _biayaJasa + totalSparepart;
    }

    public void CetakTiket()
    {
        Console.WriteLine("========== TIKET SERVIS ==========");
        Console.WriteLine($"Tgl Masuk : {_tglMasuk}");
        Console.WriteLine($"Teknisi   : {_teknisi.Nama} ({_teknisi.Spesialisasi})");
        Console.WriteLine($"Keluhan   : {_keluhan}");
        Console.WriteLine($"Diagnosa  : {_diagnosa}");
        Console.WriteLine($"Status    : {_status}");
        Console.WriteLine("----------------------------------");
        foreach (var d in _details)
            Console.WriteLine($"  {d.NamaProduk} x{d.Jumlah} = Rp {d.HitungSubtotal():N0}");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"Biaya Jasa    : Rp {_biayaJasa:N0}");
        Console.WriteLine($"Total Biaya   : Rp {HitungTotalBiaya():N0}");
        Console.WriteLine("==================================");
    }
}


// ================================================================
// DETAIL SERVIS — sparepart yang dipakai saat servis
// ================================================================
class DetailServis
{
    public string NamaProduk { get; set; }
    public int Jumlah { get; set; }
    public double HargaPakai { get; set; }

    public DetailServis(string NamaProduk, int Jumlah, double HargaPakai)
    {
        this.NamaProduk = NamaProduk;
        this.Jumlah = Jumlah;
        this.HargaPakai = HargaPakai;
    }

    public double HitungSubtotal()
    {
        return Jumlah * HargaPakai;
    }
}

