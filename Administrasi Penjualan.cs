using System;
using System.Collections.Generic;
using System.Text;

public abstract class Person
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public int NoTelepon { get; set; }

    public Person (string Nama, string Alamat, int NoTelepon)
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
    public string namaproduk { get; set; }
    public string harga { get; set; }
    public int stok { get; set; }

    public Produk(string kode, string namaproduk, string harga, int stok)
    {
        this.kode = kode;
        this.namaproduk = namaproduk;
        this.harga = harga;
        this.stok = stok;
    }

    public abstract double HitungHarga();
}

class Pelanggan : Person
{
    private decimal _total;
    private int _stok;
    private int _harga;

    public Pelanggan(string Nama, string Alamat, int NoTelepon, decimal total, int stok, int harga) : 
        base(Nama, Alamat, NoTelepon)
    {
        _total = total;
        _stok = stok;
        _harga = harga;
    }

    public virtual decimal HitungDiskon()
    {
        if (_total > 100000)
        {
            return _total * 0.1m; // Diskon 10% untuk total di atas 100.000
        }
        return 0;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Total: {_total}, Stok: {_stok}, Harga: {_harga}";
    }
}

class PelangganReguler : Pelanggan
{
    public PelangganReguler(string Nama, string Alamat, int NoTelepon, decimal total, int stok, int harga) : 
        base(Nama, Alamat, NoTelepon, total, stok, harga)
    {
    }

    public override decimal HitungDiskon() {
        return base.HitungDiskon(); // Gunakan perhitungan diskon dari kelas dasar
 }

class PelangganVIP : Pelanggan
{
    public PelangganVIP(string Nama, string Alamat, int NoTelepon, decimal total, int stok, int harga) : 
            base(Nama, Alamat, NoTelepon, total, stok, harga)
    {
    }

    public override decimal HitungDiskon() {
            return 0;
}

class Teknisi : Person
{
    public string Spesialisasi { get; set; }
    public string JamKerja { get; set; }

    public Teknisi(string Nama, string Alamat, int NoTelepon, string Spesialisasi, string JamKerja) : 
        base(Nama, Alamat, NoTelepon)
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
        if (Spesialisasi == "Elektronik")
        {
            return 50000; // Komisi untuk teknisi elektronik
        }
        else if (Spesialisasi == "Komputer")
        {
            return 75000; // Komisi untuk teknisi komputer
        }
        return 0;
    }
}

class KaryawanAdmin : Person
{
    public string Divisi { get; set; }
    public string LevelAkses { get; set; }

    public KaryawanAdmin(string Nama, string Alamat, int NoTelepon, string Divisi, string LevelAkses) : 
                base(Nama, Alamat, NoTelepon)
    {
        this.Divisi = Divisi;
        this.LevelAkses = LevelAkses;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Divisi: {Divisi}, Level Akses: {LevelAkses}";
    }
}

class sparepart : Produk
{
    public string Merk { get; set; }

    public string Kategori { get; set; }

    public sparepart(string kode, string namaproduk, string harga, int stok, string Kategori, string Merk) : 
        base(kode, namaproduk, harga, stok)
    {
        this.Kategori = Kategori;
        this.Merk = Merk;
    }
    public override double HitungHarga()
    {
        return double.Parse(harga); // Mengembalikan harga sebagai double
    }
}



