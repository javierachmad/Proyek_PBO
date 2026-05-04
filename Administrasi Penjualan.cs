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

